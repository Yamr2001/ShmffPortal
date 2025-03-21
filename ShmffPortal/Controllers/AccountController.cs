using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShmffPortal.Models;
using ShmffPortal.Helpers;
using System.Web.Security;
using System.Security.Principal;
using ShmffPortal.BLL;
using System.IO;
using ShmffPortal.Filters;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ShmffPortal.DAL;
using System.Data;

namespace ShmffPortal.Controllers
{
    [Authorize]
    // [LoggerActionFilter]
    public class AccountController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private NewPortalDBEntities entityDb;
        DataAccessCls dataAccessCls;
        private USERSBll userBll = new USERSBll();

        public NewPortalDBEntities entity
        {
            get
            {
                if (entityDb == null)
                {
                    entityDb = new NewPortalDBEntities();
                }
                return entityDb;
            }
        }

        public DataAccessCls dataAccess
        {
            get
            {
                if (dataAccessCls == null)
                {
                    dataAccessCls = new DataAccessCls();
                }
                return dataAccessCls;
            }
        }
        //private CognosEntities Cdb = new CognosEntities();
        private static int saltLengthLimit = 32;

        public AccountController()
        {
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]

        public ActionResult Login(string returnUrl)
        {
            // DateTime checkdate = Convert.ToDateTime("15/12/2019");
            //   if (checkdate < DateTime.Now) { }
            FormsAuthentication.SignOut();
            // Second we clear the principal to ensure the user does not retain any authentication
            //required NameSpace: using System.Security.Principal;
            //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();

            ViewBag.ReturnUrl = returnUrl;
            if (Session["UserID"] != null && Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[ValidateGoogleCaptcha]
        [LoggerActionFilter]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]

        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            //  if (!ReCaptchaPassed(model.GoogleCaptchToken))
            ////if (!response.Success)
            //{
            //    TempData["Error"] = "حدث  خطأ,برجاء التجربة مرة اخري";
            //    return View();
            //}


            if (userBll.checkAllCanreg() == "false")
            {
                string UserName = User.Identity.Name;
                bool canregOrLog = userBll.CanRegOrLoging(model.SSN);
                if (!canregOrLog)
                {
                    TempData["error"] = "لا يمكن التسجيل /تسجيل الدخول في المرحلة الحاليه";
                    return RedirectToAction("error", "home");
                }
            }

            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[saltLengthLimit];

            var CheckSSN = entity.RegisterSSNs.Where(c => c.SSN == model.SSN).FirstOrDefault();
            if (CheckSSN != null)
            {
                OldHASHValue = CheckSSN.Password;
                SALT = CheckSSN.SALT;

                bool isLogin =  Helper.CompareHashValue(CheckSSN.Password, model.password, CheckSSN.SALT);
                if (isLogin)
                {
                    //Login Success
                    //For Set Authentication in Cookie (Remeber ME Option)
                    SignInRemember(CheckSSN.SSN);
                    //Set A Unique ID in session
                    String TOKEN = Helper.GenerateToken();
                    Session["UserID"] = CheckSSN.ID;
                    Session["Token"] = TOKEN;
                    Session["SSN"] = Helper.Encode(CheckSSN.SSN);
                    Session["Name"] = CheckSSN.FullName;
                    Session["lastlogin"] = CheckSSN.LastLogin;
                    Session["sessId"] = Session.SessionID;
                    CheckSSN.Token = TOKEN;
                    CheckSSN.LastLogin = DateTime.Now;
                    try
                    {
                        entity.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }

                    // Logging(CheckSSN.SSN, DateTime.Now);
                    if (CheckSSN.Verified == false)
                    {
                        return RedirectToAction("VerifyCode", "Account");
                    }
                    ViewBag.AcceptedLogin = true;
                    // If we got this far, something failed, redisplay form
                    if (Session["Login"] != null)
                    {
                        if (Session["Login"].ToString() == "0")
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Login Fail
                    TempData["Error"] = "كلمة المرور خطأ";

                }
            }
            else
            {
                TempData["Error"] = "برجاء التسجيل اولا";
            }
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[ValidateGoogleCaptcha]
        //[LoggerActionFilter]
        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    //CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
        //    //  if (!ReCaptchaPassed(model.GoogleCaptchToken))
        //    ////if (!response.Success)
        //    //{
        //    //    TempData["Error"] = "حدث  خطأ,برجاء التجربة مرة اخري";
        //    //    return View();
        //    //}
        //    //dataAccessCls = new DataAccessCls();
        //    string OldHASHValue = string.Empty;
        //    byte[] SALT = new byte[saltLengthLimit];

        //    var checkedSSNdt = dataAccess.ExcuteQuery("select * from RegisterSSN where ssn='" + model.SSN + "'");


        //    // var CheckSSN = entity.RegisterSSNs.Where(c => c.SSN == model.SSN).FirstOrDefault();
        //    //if (CheckSSN != null && checkedSSN.Rows.Count > 0)

        //       // var CheckSSN = entity.RegisterSSNs.Where(c => c.SSN == model.SSN).FirstOrDefault();
        //    if (  checkedSSNdt.Rows.Count>0)
        //    {
        //        DataRow row = checkedSSNdt.Rows[0];

        //        OldHASHValue = row["Password"].ToString() ;
        //        SALT = (byte[])row["SALT"];

        //        bool isLogin = Helper.CompareHashValue(row["Password"].ToString(), model.password, SALT);
        //        if (isLogin)
        //        {
        //            //Login Success
        //            //For Set Authentication in Cookie (Remeber ME Option)
        //            SignInRemember(row["SSN"].ToString());
        //            //Set A Unique ID in session
        //            String TOKEN = Helper.GenerateToken();
        //            Session["UserID"] = row["ID"].ToString();
        //            Session["Token"] = TOKEN;
        //            Session["SSN"] = Helper.Encode(row["SSN"].ToString());
        //            Session["Name"] = row["FullName"].ToString();
        //            Session["lastlogin"] = row["LastLogin"].ToString();
        //            Session["sessId"] = Session.SessionID;
        //            //CheckSSN.Token = TOKEN;
        //            //CheckSSN.LastLogin = DateTime.Now;
        //            dataAccess.ExcuteQuery("update RegisterSSN set Token='"+TOKEN+ "',LastLogin=getdate() where ssn='" + model.SSN + "'");

        //            //try
        //            //{
        //            //    entity.SaveChanges();
        //            //}
        //            //catch (Exception ex)
        //            //{

        //            //}

        //            // Logging(CheckSSN.SSN, DateTime.Now);
        //            if ((bool)row["Verified"] == false)
        //            {
        //                return RedirectToAction("VerifyCode", "Account", model.SSN);
        //            }
        //            ViewBag.AcceptedLogin = true;
        //            // If we got this far, something failed, redisplay form
        //            if (Session["Login"] != null)
        //            {
        //                if (Session["Login"].ToString() == "0")
        //                {
        //                    return RedirectToAction("Applications", "Reservation");
        //                }

        //            }
        //            return RedirectToAction("Applications", "Reservation");
        //        }
        //        else
        //        {
        //            //Login Fail
        //            TempData["Error"] = "كلمة المرور خطأ";

        //        }
        //    }
        //    else
        //    {
        //        TempData["Error"] = "برجاء التسجيل اولا";
        //    }
        //    return View();
        //}

        //public static CaptchaResponse ValidateCaptcha(string response)
        //{
        //    string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
        //    var client = new WebClient();
        //    var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
        //    return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        //}

        public static bool ReCaptchaPassed(string gRecaptchaResponse)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
            using (var httpClient = new HttpClient())
            {
                var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={SiteSettings.GoogleRecaptchaSecretKey}&response={gRecaptchaResponse}").Result;
                if (res.StatusCode != HttpStatusCode.OK)
                    return false;
                string JSONres = res.Content.ReadAsStringAsync().Result;
                dynamic JSONdata = JObject.Parse(JSONres);
                if (JSONdata.success != "true")
                    return false;
                return true;
            }
        }




        //public static void Logging(string ssn, DateTime? _dateTime)
        //{
        //    try
        //    {
        //        string strPath = @"D:\inetpub\wwwroot\CServices\Portal\APP_Data\UploadedFiles\Log.txt";
        //        if (!System.IO.File.Exists(strPath))
        //        {
        //            System.IO.File.Create(strPath).Dispose();
        //        }
        //        using (StreamWriter sw = System.IO.File.AppendText(strPath))
        //        {
        //            sw.WriteLine(_dateTime);
        //            sw.WriteLine(ssn);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // var newTicket = new FormsAuthenticationTicket(
            //1,
            //"aa",
            //DateTime.Now,
            //DateTime.Now.AddDays(1),
            //isPersistent,
            ////JsonConvert.SerializeObject(userName ?? new LoggedInUser()));
            //JsonConvert.SerializeObject(userName));
            // cookie.Value = FormsAuthentication.Encrypt(newTicket);
            //FormsAuthentication.SetAuthCookie(userName, isPersistent, FormsAuthentication.FormsCookiePath + "; SameSite=Lax");
            var session = Session;
            //Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent, FormsAuthentication.FormsCookiePath + "; SameSite=Lax");
            //FormsAuthentication.SetAuthCookie(username, false, FormsAuthentication.FormsCookiePath + "; SameSite=Lax");


            //HttpCookie httpCookie = Request.Cookies[cookie];//get the decrypted value of cookie which is forms authentication ticket
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);
        }

        [LoggerActionFilter]
        public ActionResult VerifyCode()
        {
            if (Session["UserID"] == null && !Request.IsAuthenticated)
            {
                TempData["Error"] = "برجاء تسجيل الدخول اولا";
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (userBll.checkAllCanreg() == "false")
                {
                    string UserName = User.Identity.Name;
                    bool canregOrLog = userBll.CanRegOrLoging(UserName);
                    if (!canregOrLog)
                    {
                        TempData["error"] = "لا يمكن التسجيل /تسجيل الدخول في المرحلة الحاليه";
                        return RedirectToAction("error", "home");
                    }
                }
                
                int userid = Convert.ToInt32(Session["UserID"].ToString());
                string token = Session["Token"].ToString();
                var customer = entity.RegisterSSNs.Where(c => c.ID == userid && c.Token == token).FirstOrDefault();

                if (customer.Verified == true)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            //if (Session["UserID"] != null && Request.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult VerifyCode(VerifyViewModel model)
        {
            if (Session["UserID"] == null && !Request.IsAuthenticated)
            {
                TempData["Error"] = "برجاء تسجيل الدخول اولا";
                return RedirectToAction("Login", "Account");
            }
            int userid = Convert.ToInt32(Session["UserID"].ToString());
            string token = Session["Token"].ToString();
            var customer = entity.RegisterSSNs.Where(c => c.ID == userid && c.Token == token).FirstOrDefault();

            if (customer.Verified == true)
            {
                return RedirectToAction("Index", "Home");
            }
            if (customer != null)
            {
                if (customer.VerificationCode == model.Verify_Code)
                {
                    customer.Verified = true;
                    customer.VerificationCode = null;
                    TempData["Success"] = "تم تفعيل الحساب بنجاح";
                    entity.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "الرقم الذي قمت بادخالة غير صحيح";
                }
            }
            else
            {
                TempData["Error"] = "برجاء تسجيل الدخول مرة اخري ببيانات بطريقة صحيحة";
            }
            return View();
        }

        public ActionResult changePhone()
        {
            try
            {
                if (Session["UserID"] == null && !Request.IsAuthenticated)
                {
                    TempData["Error"] = "برجاء تسجيل الدخول اولا";
                    return RedirectToAction("Login", "Account");
                }
                int userid = Convert.ToInt32(Session["UserID"].ToString());
                string token = Session["Token"].ToString();

                var registerssn = entity.RegisterSSNs.Where(a => a.ID == userid).FirstOrDefault();
                if (registerssn.Verified == true)
                {
                    return RedirectToAction("index", "home");
                }
                return View(registerssn);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public ActionResult ChangePhone(RegisterSSN reg)
        {
            if (Session["UserID"] == null && !Request.IsAuthenticated)
            {
                TempData["Error"] = "برجاء تسجيل الدخول اولا";
                return RedirectToAction("Login", "Account");
            }
            int userid = Convert.ToInt32(Session["UserID"].ToString());
            string token = Session["Token"].ToString();
            if (reg.Mobile.Length > 11)
            {
                TempData["Error"] = "برجاء ادخال رقم محمول صحيح";
                return View(reg);
            }
            var registerssn = entity.RegisterSSNs.Where(a => a.ID == userid).FirstOrDefault();
            registerssn.Mobile = reg.Mobile;
            registerssn.VerifyCount += 1;
            entity.SaveChanges();
            SMSHelper SMS = new SMSHelper();
            //string VerificationCode = r.Next(1000, 9999).ToString();
            SMS.sendMessage("عزيزي العميل رمز التأكيد لحسابكم هو " + registerssn.VerificationCode, reg.Mobile);
            return RedirectToAction("VerifyCode", "Account");

            return View(reg);
        }

        [HttpPost]
        public ActionResult Resend()
        {
            int userid = Convert.ToInt32(Session["UserID"].ToString());
            string token = Session["Token"].ToString();
            Random r = new Random();
            var result = entity.RegisterSSNs.Where(x => x.ID == userid).FirstOrDefault();
            result.VerifyCount += 1;
            entity.SaveChanges();
            SMSHelper SMS = new SMSHelper();
            //string VerificationCode = r.Next(1000, 9999).ToString();
            SMS.sendMessage("عزيزي العميل رمز التأكيد لحسابكم هو " + result.VerificationCode, result.Mobile);
            return Json(true);
        }
        [AllowAnonymous]
        [LoggerActionFilter]
        public ActionResult Register()
        {
 
            if (Session["UserID"] != null && Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[ValidateGoogleCaptcha]
        [LoggerActionFilter]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                Random r = new Random();
                string OldHASHValue = string.Empty;
                byte[] SALT = new byte[saltLengthLimit];
                if (ModelState.IsValid)
                {
                    if (userBll.checkAllCanreg() == "false")
                    {
                        string UserName = User.Identity.Name;
                        bool canregOrLog = userBll.CanRegOrLoging(model.SSN);
                        if (!canregOrLog)
                        {
                            TempData["error"] = "لا يمكن التسجيل /تسجيل الدخول في المرحلة الحاليه";
                            return RedirectToAction("error", "home");
                        }
                    }

                    var result = entity.RegisterSSNs.Where(x => x.SSN == model.SSN).FirstOrDefault();
                    if (result != null)
                    {
                        TempData["Error"] = "الرقم القومي مسجل من قبل";
                    }
                    else
                    {
                        CitizenData citizenData = CitizenDataExtractor.GetCitizenData(model.SSN);
                        string bdate = citizenData.MonthOfBirth + "/" + citizenData.DayOfBirth + "/" + citizenData.YearOfBirth;

                        //int years = (DateTime.Now - Convert.ToDateTime(bdate)).Yea;
                        TimeSpan TS = DateTime.Now - new DateTime(citizenData.YearOfBirth, citizenData.MonthOfBirth, citizenData.DayOfBirth);
                        double Years = TS.TotalDays / 365.25;
                        if (Years >= 21)
                        {
                            SMSHelper SMS = new SMSHelper();
                            string VerificationCode = r.Next(1000, 9999).ToString();
                            SALT = Helper.Get_SALT(6);
                            RegisterSSN registermodel = new RegisterSSN();
                            string TOKEN = Helper.GenerateToken();
                            registermodel.SSN = model.SSN;
                            registermodel.FullName = model.FullName;
                            registermodel.Mobile = model.Phone.ToString();
                            registermodel.Password = Helper.Get_HASH_SHA512(model.password, SALT);
                            registermodel.SALT = SALT;
                            registermodel.RegisterDate = DateTime.Now;
                            registermodel.Token = TOKEN;
                            registermodel.VerificationCode = VerificationCode;
                            registermodel.Verified = false;
                            int gand = Convert.ToInt32(model.SSN[12].ToString());
                            int gender = 2;

                            if (gand % 2 != 0)
                            {
                                gender = 1;
                            }

                            registermodel.Gander = gender;
                            registermodel.VerifyCount = 1;
                            entity.RegisterSSNs.Add(registermodel);
                            entity.SaveChanges();
                            SMS.sendMessage("عزيزي العميل رمز التأكيد لحسابكم هو " + VerificationCode, model.Phone);
                            TempData["Success"] = "تم التسجيل بنجاح برجاء ادخال رمز التأكيد المرسل علي الموبايل ";
                            SignInRemember(registermodel.SSN);
                            //Set A Unique ID in session
                            var CheckSSN = entity.RegisterSSNs.Where(c => c.SSN == registermodel.SSN).FirstOrDefault();
                            Session["UserID"] = CheckSSN.ID;
                            Session["Token"] = TOKEN;
                            Session["SSN"] = Helper.Encode(CheckSSN.SSN);
                            Session["Name"] = CheckSSN.FullName;
                            Session["lastlogin"] = DateTime.Now;
                            CheckSSN.Token = TOKEN;
                            CheckSSN.LastLogin = DateTime.Now;
                            entity.SaveChanges();
                            return RedirectToAction("VerifyCode", "Account");
                        }
                        else
                        {
                            TempData["Error"] = "السن اقل من 21 عام";
                        }
                        //  return RedirectToAction("Login", "Account");
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Error"] = " حدث خطأ برجاء التجرية مره اخري";// + ex.Message;
                return RedirectToAction("Register");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[AllowAnonymous]
        //public ActionResult Sendmsg(string ssn)
        //{
        //    Random r = new Random();
        //    var row = entity.RegisterSSNs.Where(a => a.SSN == ssn).FirstOrDefault();
        //    row.VerificationCode = r.Next(1000, 9999).ToString();
        //    entity.SaveChanges();

        //    if (row != null)
        //    {
        //        SMSHelper SMS = new SMSHelper();
        //        SMS.sendMessage("عزيزي العميل رمز التأكيد لحسابكم هو " + row.VerificationCode, row.Mobile);
        //    }
        //    return View();
        //}

        [AllowAnonymous]
       // [LoggerActionFilter]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [LoggerActionFilter]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CheckSSN = entity.RegisterSSNs.Where(c => c.SSN == model.SSN).FirstOrDefault();
                if (CheckSSN != null)
                {
                    //if (CheckSSN.Verified == false)
                    //{
                    //    ViewBag.SSNError = "الحساب الخاص بك غير مفعل برجاء تسجيل الدخول وتفعيل الحساب اولا";
                    //}
                    //else 
                    if (model.Phone != CheckSSN.Mobile)
                    {

                        ViewBag.SSNError = "الموبايل غير مطابق للموبايل الذي تم التسجيل به";
                    }
                    //else if (((TimeSpan)(DateTime.Now - (CheckSSN.LastPasswordRestore??DateTime.Now))).Hours<6 )
                    //else if (CheckSSN.LastPasswordRestore != null)
                    //{
                    //    //if (((TimeSpan)(DateTime.Now - CheckSSN.LastPasswordRestore)).Hours < 6)
                    //    //{
                    //    //    ViewBag.SSNError = "برجاء تجربة استرجاع كلمة المرور بعد 6 ساعات مرة اخري";
                    //    //}
                    //    //else
                    //    //{
                    //        //change password and send it by SMS
                    //        //CheckSSN.
                    //        SMSHelper SMS = new SMSHelper();
                    //        Random rnd = new Random();
                    //        string Password = rnd.Next(10000000, 99999999).ToString();
                    //        byte[] SALT = new byte[saltLengthLimit];
                    //        SALT = Helper.Get_SALT(6);
                    //        CheckSSN.Password = Helper.Get_HASH_SHA512(Password, SALT);
                    //        CheckSSN.SALT = SALT;
                    //        CheckSSN.LastPasswordRestore = DateTime.Now;
                    //        CheckSSN.PasswordRestoreNumber = (CheckSSN.PasswordRestoreNumber ?? 0) + 1;
                    //        entity.SaveChanges();
                    //        SMS.sendMessage("كلمة المرور الجديدة على مركز خدمة المواطنين , هي " + Password, CheckSSN.Mobile);
                    //        TempData["Success"] = "تم ارسال كلمة المرور الجديدة للموبايل المسجل لدينا";
                    //        return RedirectToAction("login", "Account");
                    //   // }
                    //}
                    else
                    {
                        //change password and send it by SMS
                        //CheckSSN.
                        SMSHelper SMS = new SMSHelper();
                        Random rnd = new Random();
                        string Password = rnd.Next(100000, 999999).ToString();
                        byte[] SALT = new byte[saltLengthLimit];
                        SALT = Helper.Get_SALT(6);
                        CheckSSN.Password = Helper.Get_HASH_SHA512(Password, SALT);
                        CheckSSN.SALT = SALT;
                        CheckSSN.LastPasswordRestore = DateTime.Now;
                        CheckSSN.PasswordRestoreNumber = (CheckSSN.PasswordRestoreNumber ?? 0) + 1;
                        entity.SaveChanges();
                        SMS.sendMessage("كلمة المرور الجديدة على البوابة الالكترونية , هي " + Password, CheckSSN.Mobile);
                        TempData["Success"] = "تم ارسال كلمة المرور الجديدة للموبايل المسجل لدينا";
                        return RedirectToAction("login", "Account");
                    }
                }
                else
                {
                    ViewBag.SSNError = "الرقم القومي غير مسجل برجاء التسجيل";
                }
            }
            else
            {
                ViewBag.SSNError = "برجاء ادخال البيانات بشكل صحيح";
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //return RedirectToAction("Index", "Home");
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                return RedirectToAction("Login", "Account");
            }
            catch
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            entity.Dispose();
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
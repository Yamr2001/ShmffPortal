using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ShmffPortal.BLL;
using ShmffPortal.Filters;
using ShmffPortal.Helpers;
using ShmffPortal.Infrastuture;
using ShmffPortal.Models;
using ShmffPortal.UnitOfWorkF;

namespace ShmffPortal.Controllers
{

    [CheckAuthorization]
    public class ReservationController : Controller
    {
        private NewPortalDBEntities entityDb;

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

        private UnitOfWork uow = new UnitOfWork();

        USERSBll userbll = new USERSBll();
        // GET: Reservation
        public ActionResult Index(long? advid)
        {
            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }
            ViewBag.ID = advid;
            //var advs = uow.NEWADVERTISMENTSRepository.FindBy(a => a.ID == advid && a.ISCLOSED != true).FirstOrDefault();
            //if (advs != null)
            //{


            //    if (!userbll.IsBewteenTwoDates(DateTime.Now, advs.REGISTRATIONSTARTDATE ?? DateTime.Now, advs.REGISTRATIONSENDDATE ?? DateTime.Now))
            //    {
            //        return RedirectToAction("Index", "Home");

            //    }


            //    //if (DateTime.Now > advs.REGISTRATIONSENDDATE)
            //    //{
            //    //    return RedirectToAction("Index", "Home");
            //    //}
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            string SSN = Helper.Decode(Session["SSN"].ToString());

            //var ssnCanReserv = entity.EXCEPTION_SSN_RESERV.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (ssnCanReserv != null)
            //{
            //    if (ssnCanReserv.ADVID != advs.PORTALADVID)
            //    {
            //        ViewBag.cantRegHere = 1;
            //    }
            //}
            //else
            //{
            //    ViewBag.cantRegHere = 1;
            //}

            //try
            //{
            //    var Result = CallWebService(SSN);
            //    if (Result.StatusCode == 1)
            //    {
            //        userbll.saveFiledRegistration(SSN, "", 0, 1, 0, Result.StatusCode, Result.SbkResult.FirstOrDefault());
            //    }
            //}
            //catch
            //{
            //}
            // return View();
            //}

            var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();
            if (_Subsidy != null)
            {
                return RedirectToAction("Applications", "Reservation");
            }
            else
            {
                #region Validation for check if has unit middle or low income
                //check low income
                //var CheckHasUnit = userbll.checkCustomerHasUnits(SSN);
                //var paidcheck = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).FirstOrDefault();
                //if (paidcheck != null)
                //{
                //as if old ALLOCATION_DEALLOCATION==0
                //var hasException = userbll.exceptionSSnHasApp(SSN);
                //if (!hasException)
                //{
                //    if (CheckHasUnit != null)
                //    {
                //        if (CheckHasUnit.ALLOCATION_DEALLOCATION == 1)
                //        {
                //            ViewBag.HasUnitBefore = 1;
                //            ViewBag.CheckHasUnit = CheckHasUnit;
                //            userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");
                //        }
                //    }
                //    else
                //    {
                //        bool exceptionssn = userbll.exceptionSSn(SSN);
                //        if (exceptionssn == false)
                //        {
                //            var Dimsub = entity.V_Dim_subsidy.Where(a => a.PRIMARY_INVESTOR_SSN == SSN || a.SPOUSE_SSN == SSN).FirstOrDefault();
                //            if (Dimsub != null)
                //            {
                //                var transactiontransformation = entity.V_Transaction_Transformation.Where(a => a.IS_CANCELLED == 0 && (a.SSN == SSN || a.SSN == Dimsub.SPOUSE_SSN || a.SSN == Dimsub.PRIMARY_INVESTOR_SSN)).FirstOrDefault();
                //                if (transactiontransformation != null)
                //                {
                //                    //var withdraw = entity.v_Egypt_Post_Withdrawl.Any(a => a.CLIENT_SSN == SSN);
                //                    //if (!withdraw)
                //                    //{
                //                    //    ViewBag.NoCancelled = 1;
                //                    //    ViewBag.Dimsub = Dimsub;
                //                    //    userbll.saveFiledRegistration(SSN, "", 0, 0, 1, 0, "");
                //                    //}
                //                }
                //            }
                //        }
                //    }

                //    var DimsubSpouse = entity.subsidy_request.Where(a => a.SPOUSE_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();
                //    if (DimsubSpouse != null)
                //    {
                //        ViewBag.HasAppBefore = 1;
                //    }

                //    //check middel income
                //    var middlecheck = entity.CheckMiddelIncomes.Where(a => (a.SPOUSE_SSN == SSN || a.PRIMARY_INVESTOR_SSN == SSN) && a.STEPNAME != "Rejection List" && a.WithdrawlSSN == null).FirstOrDefault();
                //    if (middlecheck != null)
                //    {
                //        ViewBag.HasUnitMiddelBefore = 1;
                //        ViewBag.CheckHasMiddleUnit = middlecheck;
                //        userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");
                //    }

                //    var middleExistSystemLoan = entity.CheckMiddelIncomes.Where(a => (a.SPOUSE_SSN == SSN || a.PRIMARY_INVESTOR_SSN == SSN) && a.STEPNAME == "Rejection List" && a.WithdrawlSSN == null).FirstOrDefault();
                //    if (middleExistSystemLoan != null)
                //    {
                //        ViewBag.HasUnitMiddelBefore = 1;
                //        ViewBag.CheckHasMiddleUnit = middleExistSystemLoan;
                //        userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");
                //    }
                //}
                #endregion
                //}
                //else
                //{
                //    ViewBag.Nopaid = 0;
                //}
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TermsCondition model)
        {
            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }

            ViewBag.ID = model.ADVID;
            if (model.accepted == true)
            {
                Session["Accepted"] = true;
                Session["ApplicationAccepted"] = model.ADVID;
                return Redirect("~/Reservation/Create?advid=" + model.ADVID);
            }
            return View();

        }
        [LoggerActionFilter]
        public ActionResult Applications()
        {
            string SSN = Helper.Decode(Session["SSN"].ToString());
            var Model = entity.Allsubsidy_requests.Where(a => a.PRIMARY_INVESTOR_SSN == SSN).ToList();

            //#region CheckPaid and withdrowal
            //var middlecheck = entity.CheckMiddelIncomes.Where(a => a.WithdrawlSSN == SSN && a.ADVERTISEMENT_CODE == 5000021).FirstOrDefault();
            //List<HDB_Payment_Confirmation> lstHDB_Payment_Confirmation = new List<HDB_Payment_Confirmation>();
            //if (middlecheck == null)
            //{
            //    lstHDB_Payment_Confirmation = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            //}
            //else
            //{
            //    lstHDB_Payment_Confirmation = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            //}

            //if (lstHDB_Payment_Confirmation.Count() > 0)
            //{
            //    ViewBag.paid = 1;
            //    ViewBag.Payment = lstHDB_Payment_Confirmation;
            //}

            ////var activeRowSubsidy = Model.Where(a => a.IS_CANCELED != 1).FirstOrDefault();
            ////if (activeRowSubsidy != null)
            ////{
            ////    var lstHDB_Payment_Confirmation = await entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToListAsync();
            ////    if (lstHDB_Payment_Confirmation.Count > 0)
            ////    {
            ////        ViewBag.Payment = lstHDB_Payment_Confirmation;
            ////        ViewBag.paid = 1;
            ////    }
            ////}

            //#endregion

            var settings = entity.SETTINGS.FirstOrDefault();
            if (settings != null)
            {
                ViewBag.settings = settings;
            }
            DateTime NowToday = DateTime.Now;
            //reserve before all
            var ssnCanReserv = entity.EXCEPTION_SSN_RESERV.Where(a => a.SSN == SSN).FirstOrDefault();
            ViewBag.ssn = SSN;
            if (ssnCanReserv != null)
            {
                ViewBag.canres = 1;
                ViewBag.reservStartDate = ssnCanReserv.RESERVATION_START_DATE;
                ViewBag.reservEndDate = ssnCanReserv.RESERVATION_END_DATE;
            }

            var ssncan = entity.ReservSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
            ViewBag.ssn = SSN;
            if (ssncan != null)
            {
                ViewBag.canres = 1;
                ViewBag.ssncanreservtype = ssncan.Type;
                ViewBag.StartDatetime = ssncan.STARTDATE;
                ViewBag.ENDDATEtime = ssncan.ENDDATE;
            }
            if (ssncan != null && NowToday > ssncan.STARTDATE && NowToday < ssncan.ENDDATE)
            {
                ViewBag.reservunit = 1;
                //if (ssncan==1)
                //{
                //    ViewBag.Url = "~/";
                //}
            }
            //var Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (Sbk != null)
            //{
            //    if (Sbk.STATUSCODE == 1)
            //    {
            //        ViewBag.SBK = 1;
            //    }
            //}
            //var row = Model.Where(a => a.IS_CANCELED != 1).FirstOrDefault();
            //if (row != null)
            //{
            //    if (row.SPOUSE_SSN != null && row.SPOUSE_SSN != "")
            //    {
            //        Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN || a.SPOUSE_SSN == row.SPOUSE_SSN).FirstOrDefault();
            //        if (Sbk != null)
            //        {
            //            if (Sbk.STATUSCODE == 1)
            //            {
            //                ViewBag.SBK = 1;
            //            }
            //        }
            //    }
            //}
            return View(Model);
        }

        [LoggerActionFilter]
        public ActionResult Create(long? advid)
        {

            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }
            string SSN = Helper.Decode(Session["SSN"].ToString());
            var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();
            var dictOffsetAccountType = new Dictionary<string, string> {
            { "0", "لا" }, { "1", "نعم" }
            };
            ListsExtender Liste = new ListsExtender();
            var classofWork = entity.v_CLASSIFICATION_OF_WORK;
            var acadimicDegree = entity.v_ACADEMIC_DEGREE;
            var governrate = entity.v_GOVERNRATE;
            var residenceType = entity.v_RESIDENCE_TYPE;
            if (_Subsidy == null)
            {
                if (Session["Accepted"] == null || Session["ApplicationAccepted"].ToString() != advid.ToString())
                {
                    return RedirectToAction("Index", "Home");
                }
                long Userid = Convert.ToInt64(Session["UserID"].ToString());
                var user = entity.RegisterSSNs.Where(a => a.ID == Userid).FirstOrDefault();
                ViewBag.User = user;
                ViewBag.PrimaryInvestorSSN = user.SSN;

                ViewBag.PrimaryInvestorName = user.FullName;
                ViewBag.PrimaryPhone = user.Mobile;
                ViewBag.Gander = user.Gander;
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == advid && a.ISCLOSED != true).FirstOrDefault();

                CitizenData citizenData = CitizenDataExtractor.GetCitizenData(user.SSN);
                ViewBag.BDate = citizenData.DayOfBirth + "/" + citizenData.MonthOfBirth + "/" + citizenData.YearOfBirth;
                ViewBag.CityID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == Myadv.PORTALADVID).Select(a => new { a.CITYID, a.CITYNAME }).Distinct(), "CITYID", "CITYNAME");
                ViewBag.TamwelTypeId = new SelectList(entity.TamwelTypes, "ID", "Name_Ar");
                ViewBag.MARITAL_STATUS_ID = new SelectList(entity.v_Martial_Status, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID = new SelectList(classofWork, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(classofWork, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA = new SelectList(classofWork, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID = new SelectList(acadimicDegree, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_SECEXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR");
                ViewBag.SPOUSE_EMPLOYER_GOVERNORATE = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID = new SelectList(classofWork, "ID", "NAME_AR");
                ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(classofWork, "ID", "NAME_AR");
                ViewBag.SPOUSE_ACADEMIC_DEGREE_ID = new SelectList(acadimicDegree, "ID", "NAME_AR");
                ViewBag.SPOUSE_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR");
                ViewBag.CURRENT_RESIDENCE_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.CURRENT_RESIDENCE_RESIDENCE_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR");
                ViewBag.RESIDENTIAL_EXTRA_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR");
                ViewBag.RESIDENTIAL_SECEXTRA_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR");
                ViewBag.SPOUSE_EGYPTIAN = Liste.GetCountries(dictOffsetAccountType);
                ViewBag.SPOUSE_IS_WORKING = Liste.GetCountries(dictOffsetAccountType);
                ViewBag.IS_JOINT_OWNERSHIP = Liste.GetCountries(dictOffsetAccountType);
                //ViewBag.ADVID = new SelectList(entity.v_ADVERTISMENT, "ID", "NAME_AR");
                ViewBag.ADVFLAG = advid;
                ViewBag.ID = 0;
                //ViewBag.PROJECTID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == advid), "ID", "NAME_AR");
                var Advs = entity.NEWADVERTISMENTS.Where(a => a.ID == advid && a.ISCLOSED != true).FirstOrDefault();
                ViewBag.Advs = Advs;

                var hasException = userbll.exceptionSSnHasApp(SSN);
                if (!hasException)
                {
                    var Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN).FirstOrDefault();
                    if (Sbk != null)
                    {
                        if (Sbk.STATUSCODE == 1)
                        {
                            ViewBag.SBK = 1;
                        }
                    }
                }
            }
            else
            {
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
                ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
                ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
                ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
                ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);
                ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
                ViewBag.CityID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == Myadv.PORTALADVID).Select(a => new { a.CITYID, a.CITYNAME }).Distinct(), "CITYID", "CITYNAME", _Subsidy.CityID);
                ViewBag.TamwelTypeId = new SelectList(entity.TamwelTypes, "ID", "Name_Ar", _Subsidy.TamwelTypeId);
                ViewBag.MARITAL_STATUS_ID = new SelectList(entity.v_Martial_Status, "ID", "NAME_AR", _Subsidy.MARITAL_STATUS_ID);
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID = new SelectList(classofWork, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID);
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(classofWork, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA);
                ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA = new SelectList(classofWork, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA);
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE);
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID = new SelectList(acadimicDegree, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID);
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_EXTRA);
                ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_SECEXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_SECEXTRA);
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA);
                ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA);
                ViewBag.SPOUSE_EMPLOYER_GOVERNORATE = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE);
                ViewBag.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA);
                ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID = new SelectList(classofWork, "ID", "NAME_AR", _Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID);
                ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(classofWork, "ID", "NAME_AR", _Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA);
                ViewBag.SPOUSE_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(acadimicDegree, "ID", "NAME_AR", _Subsidy.SPOUSE_ACADEMIC_DEGREE_ID_EXTRA);
                ViewBag.SPOUSE_ACADEMIC_DEGREE_ID = new SelectList(acadimicDegree, "ID", "NAME_AR", _Subsidy.SPOUSE_ACADEMIC_DEGREE_ID);
                ViewBag.CURRENT_RESIDENCE_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.CURRENT_RESIDENCE_GOVERNORATE_ID);
                ViewBag.CURRENT_RESIDENCE_RESIDENCE_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR", _Subsidy.CURRENT_RESIDENCE_RESIDENCE_TYPE_ID);
                ViewBag.RESIDENTIAL_EXTRA_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR", _Subsidy.RESIDENTIAL_EXTRA_TYPE_ID);
                ViewBag.RESIDENTIAL_SECEXTRA_TYPE_ID = new SelectList(residenceType, "ID", "NAME_AR", _Subsidy.RESIDENTIAL_SECEXTRA_TYPE_ID);
                ViewBag.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID);
                ViewBag.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID = new SelectList(governrate, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID);
                ViewBag.SPOUSE_EGYPTIAN = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.SPOUSE_EGYPTIAN.ToString());
                ViewBag.SPOUSE_IS_WORKING = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.SPOUSE_IS_WORKING.ToString());
                ViewBag.IS_JOINT_OWNERSHIP = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.IS_JOINT_OWNERSHIP.ToString());
                ViewBag.ADVFLAG = _Subsidy.ADVFLAG;
                ViewBag.ID = _Subsidy.ID;
                //ViewBag.PROJECTID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == _Subsidy.ADVFLAG), "ID", "NAME_AR", _Subsidy.PROJECTID);
                ViewBag.Advs = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();

                // ViewBag.secextraAddress = _Subsidy.secextraAddress;

                if (_Subsidy.PRIMARY_INVESTOR_EXTRA_RESIDENTIAL_STREET != null && _Subsidy.PRIMARY_INVESTOR_EXTRA_RESIDENTIAL_BUILDING_NUMBER != null && _Subsidy.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID != null && _Subsidy.RESIDENTIAL_EXTRA_TYPE_ID != null)
                {
                    ViewBag.extraAddress = 1;
                }
                if (_Subsidy.PRIMARY_INVESTOR_SECEXTRA_RESIDENTIAL_STREET != null && _Subsidy.PRIMARY_INVESTOR_SECEXTRA_RESIDENTIAL_BUILDING_NUMBER != null && _Subsidy.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID != null && _Subsidy.RESIDENTIAL_SECEXTRA_TYPE_ID != null)
                {
                    ViewBag.secextraAddress = 1;
                }
                if (_Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_NAME_EXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_ADDRESS_EXTRA != null)
                {
                    ViewBag.Primaryextraincome = 1;
                }
                if (_Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_NAME_SECEXTRA != null && _Subsidy.PRIMARY_INVESTOR_EMPLOYER_ADDRESS_SECEXTRA != null)
                {
                    ViewBag.Primarysecextraincome = 1;
                }
                if (_Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA != null && _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA != null && _Subsidy.SPOUSE_EMPLOYER_NAME_EXTRA != null)
                {
                    ViewBag.Spouseextraincome = 1;
                }
            }

            if (_Subsidy != null)
            {
                var hasException = userbll.exceptionSSnHasApp(SSN);
                if (!hasException)
                {
                    var Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN || a.SPOUSE_SSN == _Subsidy.SPOUSE_SSN).FirstOrDefault();
                    if (Sbk != null)
                    {
                        if (Sbk.STATUSCODE == 1)
                        {
                            ViewBag.SBK = 1;
                        }
                    }
                }
                if (_Subsidy.IS_CANCELED == 1)
                {
                    return RedirectToAction("Applications");
                }
                if (_Subsidy.PICTURE_SSN == 1)
                {
                    _Subsidy.PICTURE_SSNB = true;
                }
                if (_Subsidy.PICTURE_INCOME_PROOF == 1)
                {
                    _Subsidy.PICTURE_INCOME_PROOFB = true;
                }
                if (_Subsidy.PICTURE_CURRENT_RESIDENCE_PROOF == 1)
                {
                    _Subsidy.PICTURE_CURRENT_RESIDENCE_PROOFB = true;
                }
                if (_Subsidy.APPLICATION_CHECKBOX == 1)
                {
                    _Subsidy.APPLICATION_CHECKBOXB = true;
                }

                if (_Subsidy.ACKNOWLEDGMENT_CHECKBOX == 1)
                {
                    _Subsidy.ACKNOWLEDGMENT_CHECKBOXB = true;
                }

                if (_Subsidy.PICTURE_SPOUSE_SSN == 1)
                {
                    _Subsidy.PICTURE_SPOUSE_SSNB = true;
                }

                if (_Subsidy.PICTURE_MARRIAGE_DIVISION_PROOF == 1)
                {
                    _Subsidy.PICTURE_MARRIAGE_DIVISION_PROOFB = true;
                }

                if (_Subsidy.PICTURE_HUSBAND_NOT_WORKING == 1)
                {
                    _Subsidy.PICTURE_HUSBAND_NOT_WORKINGB = true;
                }

                if (_Subsidy.PARENT_PROOF_CHECKBOX == 1)
                {
                    _Subsidy.PARENT_PROOF_CHECKBOXB = true;
                }


                if (_Subsidy.DIVORCED_PROOF_CHECKBOX == 1)
                {
                    _Subsidy.DIVORCED_PROOF_CHECKBOXB = true;
                }

                if (_Subsidy.MARRIAGE_DEATH_PROOF_CHECKBOX == 1)
                {
                    _Subsidy.MARRIAGE_DEATH_PROOF_CHECKBOXB = true;
                }

                if (_Subsidy.WIDOWED_PROOF_CHECKBOX == 1)
                {
                    _Subsidy.WIDOWED_PROOF_CHECKBOXB = true;
                }

                if (_Subsidy.PICTURE_SPOUSE_INCOME_PROOF == 1)
                {
                    _Subsidy.PICTURE_SPOUSE_INCOME_PROOFB = true;
                }

                if (_Subsidy.FAMILYREGISTRATION_CHECKBOX == 1)
                {
                    _Subsidy.FAMILYREGISTRATION_CHECKBOXB = true;
                }

                Requested_Unit request_Unit = entity.Requested_Unit.Where(ru => ru.SUBSIDY_REQUEST_ID == _Subsidy.ID).FirstOrDefault();

                if (request_Unit != null)
                {
                    _Subsidy.Adv_Id = request_Unit.ADVERTISMENT_ID.Value;
                    _Subsidy.Project_Id = request_Unit.PROJECT_ID.Value;
                    _Subsidy.CityID = request_Unit.CITY_ID.Value;
                    _Subsidy.Gov_Id = request_Unit.GOVERNORATE_ID.Value;
                }

                return View(_Subsidy);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerActionFilter]
        public ActionResult Create(subsidy_request _Subsidy, long? id, long? advid, HttpPostedFileBase ATTACHMENT_NAME)
        {
            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }
            //if (id == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            string SSN = Helper.Decode(Session["SSN"].ToString());

            long Userid = Convert.ToInt64(Session["UserID"].ToString());
            var user = entity.RegisterSSNs.Where(a => a.ID == Userid).FirstOrDefault();
            ViewBag.User = user;
            CitizenData citizenData = CitizenDataExtractor.GetCitizenData(user.SSN);
            string bdate = citizenData.DayOfBirth + "/" + citizenData.MonthOfBirth + "/" + citizenData.YearOfBirth;
            ViewBag.BDate = bdate;

            var dictOffsetAccountType = new Dictionary<string, string> {
            { "0", "لا" }, { "1", "نعم" }
            };
            ListsExtender Liste = new ListsExtender();

            //ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
            //ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
            ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
            ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);

            var adv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
            ViewBag.Advs = adv;
            ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
            ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
            // ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
            ViewBag.CityID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == adv.PORTALADVID).Select(a => new { a.CITYID, a.CITYNAME }).Distinct(), "CITYID", "CITYNAME");

            ViewBag.TamwelTypeId = new SelectList(entity.TamwelTypes, "ID", "Name_ar", _Subsidy.TamwelTypeId);
            ViewBag.MARITAL_STATUS_ID = new SelectList(entity.v_Martial_Status, "ID", "NAME_AR", _Subsidy.MARITAL_STATUS_ID);
            ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID = new SelectList(entity.v_CLASSIFICATION_OF_WORK, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID);
            ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(entity.v_CLASSIFICATION_OF_WORK, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA);
            ViewBag.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA = new SelectList(entity.v_CLASSIFICATION_OF_WORK, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_SECEXTRA);
            ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE);
            ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA);
            ViewBag.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_SECEXTRA);
            ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID = new SelectList(entity.v_ACADEMIC_DEGREE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID);
            ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(entity.v_ACADEMIC_DEGREE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_EXTRA);
            ViewBag.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_SECEXTRA = new SelectList(entity.v_ACADEMIC_DEGREE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID_SECEXTRA);
            ViewBag.SPOUSE_EMPLOYER_GOVERNORATE = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE);
            ViewBag.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA);
            ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA = new SelectList(entity.v_CLASSIFICATION_OF_WORK, "ID", "NAME_AR", _Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA);
            ViewBag.SPOUSE_CLASSIFICATION_OF_WORK_ID = new SelectList(entity.v_CLASSIFICATION_OF_WORK, "ID", "NAME_AR", _Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID);
            ViewBag.SPOUSE_ACADEMIC_DEGREE_ID = new SelectList(entity.v_ACADEMIC_DEGREE, "ID", "NAME_AR", _Subsidy.SPOUSE_ACADEMIC_DEGREE_ID);
            ViewBag.CURRENT_RESIDENCE_GOVERNORATE_ID = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.CURRENT_RESIDENCE_GOVERNORATE_ID);
            ViewBag.CURRENT_RESIDENCE_RESIDENCE_TYPE_ID = new SelectList(entity.v_RESIDENCE_TYPE, "ID", "NAME_AR", _Subsidy.CURRENT_RESIDENCE_RESIDENCE_TYPE_ID);
            ViewBag.ADVFLAG = _Subsidy.ADVFLAG;
            ViewBag.ADVID = _Subsidy.ADVFLAG;
            //ViewBag.PROJECTID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == _Subsidy.ADVFLAG), "ID", "NAME_AR", _Subsidy.PROJECTID);
            ViewBag.RESIDENTIAL_EXTRA_TYPE_ID = new SelectList(entity.v_RESIDENCE_TYPE, "ID", "NAME_AR", _Subsidy.RESIDENTIAL_EXTRA_TYPE_ID);
            ViewBag.RESIDENTIAL_SECEXTRA_TYPE_ID = new SelectList(entity.v_RESIDENCE_TYPE, "ID", "NAME_AR", _Subsidy.RESIDENTIAL_SECEXTRA_TYPE_ID);
            ViewBag.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_EXTRA_GOVERNORATE_ID);
            ViewBag.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID = new SelectList(entity.v_GOVERNRATE, "ID", "NAME_AR", _Subsidy.PRIMARY_INVESTOR_SECEXTRA_GOVERNORATE_ID);
            ViewBag.SPOUSE_ACADEMIC_DEGREE_ID_EXTRA = new SelectList(entity.v_ACADEMIC_DEGREE, "ID", "NAME_AR", _Subsidy.SPOUSE_ACADEMIC_DEGREE_ID_EXTRA);
            ViewBag.SPOUSE_EGYPTIAN = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.SPOUSE_EGYPTIAN.ToString());
            ViewBag.SPOUSE_IS_WORKING = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.SPOUSE_IS_WORKING.ToString());
            ViewBag.IS_JOINT_OWNERSHIP = Liste.GetCountriesEdit(dictOffsetAccountType, _Subsidy.IS_JOINT_OWNERSHIP.ToString());
            _Subsidy.PRIMARY_INVESTOR_TOTAL_MONTHLY_INCOME = _Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME;
            _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE = citizenData.MonthOfBirth + "/" + citizenData.DayOfBirth + "/" + citizenData.YearOfBirth;// citizenData.MonthOfBirth
            ViewBag.ID = _Subsidy.ID;

            ViewBag.secextraAddress = _Subsidy.secextraAddress;
            ViewBag.extraAddress = _Subsidy.extraAddress;
            ViewBag.Primaryextraincome = _Subsidy.Primaryextraincome;
            ViewBag.Primarysecextraincome = _Subsidy.Primarysecextraincome;
            ViewBag.Spouseextraincome = _Subsidy.Spouseextraincome;
            //var Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (Sbk != null)
            //{
            //    if (Sbk.STATUSCODE == 1)
            //    {
            //        ViewBag.SBK = 1;
            //    }
            //}
            //if (_Subsidy.SPOUSE_SSN != "" && _Subsidy.SPOUSE_SSN != null)
            //{
            //    Sbk = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN || a.SPOUSE_SSN == _Subsidy.SPOUSE_SSN).FirstOrDefault();
            //    if (Sbk != null)
            //    {
            //        if (Sbk.STATUSCODE == 1)
            //        {
            //            ViewBag.SBK = 1;
            //        }
            //    }
            //}
            //   _Subsidy.ID = 0;
            if (ModelState.IsValid)
            {


                #region check mony income 
                double total = 0;
                if (_Subsidy.MARITAL_STATUS_ID == 0 || _Subsidy.MARITAL_STATUS_ID == 2 || _Subsidy.MARITAL_STATUS_ID == 4)
                {
                    total = (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA ?? 0) + (_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME ?? 0) + (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_SECEXTRA ?? 0);
                    if (total > 40000)
                    {
                        TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                        return View(_Subsidy);
                    }
                }
                else
                {
                    total = (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA ?? 0) + (_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME ?? 0) + (_Subsidy.SPOUSE_MONTHLY_INCOME ?? 0) + (double)(_Subsidy.SPOUSE_MONTHLY_INCOME_EXTRA ?? 0) + (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_SECEXTRA ?? 0);
                    if (total > 50000)
                    {
                        TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                        return View(_Subsidy);
                    }
                }

                //if (_Subsidy.MARITAL_STATUS_ID == 0 || _Subsidy.MARITAL_STATUS_ID == 2 || _Subsidy.MARITAL_STATUS_ID == 4)
                //{


                //    total = (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA ?? 0) + (_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME ?? 0) + (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_SECEXTRA ?? 0);

                //    if (ViewBag.Advs.PORTALADVID == 5000151)
                //    {
                //        if (total > 10000)
                //        {
                //            TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                //            return View(_Subsidy);
                //        }
                //    }
                //    else
                //    {
                //        if (total > 40000)
                //        {
                //            TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                //            return View(_Subsidy);
                //        }
                //    }
                //}
                //else
                //{
                //    total = (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA ?? 0) + (_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME ?? 0) + (_Subsidy.SPOUSE_MONTHLY_INCOME ?? 0) + (double)(_Subsidy.SPOUSE_MONTHLY_INCOME_EXTRA ?? 0) + (double)(_Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_SECEXTRA ?? 0);
                //    if (ViewBag.Advs.PORTALADVID == 5000151)
                //    {
                //        if (total > 14000)
                //        {
                //            TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                //            return View(_Subsidy);
                //        }
                //    }
                //    else
                //    {
                //        if (total > 50000)
                //        {
                //            TempData["Error"] = "لا يمكن تقديم الطلب لتجاوز الدخل";
                //            return View(_Subsidy);
                //        }
                //    }
                //}



                #endregion

                if (_Subsidy.MARITAL_STATUS_ID == 1 || _Subsidy.MARITAL_STATUS_ID == 6)
                {
                    if (_Subsidy.SPOUSE_CELL_NUMBER != "" && _Subsidy.SPOUSE_CELL_NUMBER != null)
                    {
                        string phoneprovider = _Subsidy.SPOUSE_CELL_NUMBER.Substring(0, 3);
                        if (_Subsidy.SPOUSE_CELL_NUMBER.Length < 11)
                        {

                            TempData["Error"] = "رقم الهاتف المحمول الشخصي للزوج/الزوجة غير صحيح  ";
                            return View(_Subsidy);
                        }
                        if (phoneprovider != "011" && phoneprovider != "010" && phoneprovider != "012" && phoneprovider != "015")
                        {
                            TempData["Error"] = "رقم الهاتف المحمول الشخصي للزوج/الزوجة غير صحيح  ";
                            return View(_Subsidy);
                        }

                    }
                    if (_Subsidy.SPOUSE_EGYPTIAN == null)
                    {
                        TempData["Error"] = "برجاء تحديد هل الزوج/الزوجة مصرية";
                        return View(_Subsidy);
                    }
                    if (_Subsidy.SPOUSE_EGYPTIAN == 1)
                    {
                        int gand = Convert.ToInt32(_Subsidy.SPOUSE_SSN[12].ToString());
                        int gender = 2;
                        if (gand % 2 != 0)
                        {
                            gender = 1;
                        }

                        if (gender == user.Gander)
                        {
                            TempData["Error"] = "الرقم القومي للزوج/الزوجة غير متطابق مع بيانات صاحب الطلب";
                            return View(_Subsidy);
                        }
                    }

                    if (_Subsidy.SPOUSE_IS_WORKING == null)
                    {
                        TempData["Error"] = "برجاء تحديد هل الزوج/الزوجة تعمل";
                        return View(_Subsidy);
                    }
                    if (_Subsidy.IS_JOINT_OWNERSHIP == null)
                    {
                        TempData["Error"] = "برجاء تحديد هل الزوج/الزوجة شريك في تملك الوحدة";
                        return View(_Subsidy);
                    }
                    if (_Subsidy.SPOUSE_BIRTH_DATE == null || _Subsidy.SPOUSE_BIRTH_DATE == "")
                    {
                        TempData["Error"] = "الرقم القومي للزوج/الزوجة غير متطابق مع بيانات صاحب الطلب";
                        return View(_Subsidy);
                    }
                    if (_Subsidy.SPOUSE_SSN == "" || _Subsidy.SPOUSE_SSN == null || _Subsidy.SPOUSE_FULL_NAME == "" || _Subsidy.SPOUSE_FULL_NAME == null || _Subsidy.SPOUSE_CELL_NUMBER == "" || _Subsidy.SPOUSE_CELL_NUMBER == null)
                    {
                        TempData["Error"] = "برجاء ادخال بيانات الزوجة/الزوج";// "بيانات الزوج/الزوجة غير مكتملة";
                        return View(_Subsidy);
                    }
                    else
                    {
                        //try
                        //{
                        //    var Result = CallWebService(_Subsidy.SPOUSE_SSN);
                        //    if (Result.StatusCode == 1)
                        //    {
                        //        userbll.saveFiledRegistration(SSN, _Subsidy.SPOUSE_SSN, 1, 1, 0, Result.StatusCode, Result.SbkResult.FirstOrDefault());
                        //    }
                        //}
                        //catch
                        //{
                        //}
                        // return View(_Subsidy);

                        var SubsidySpousebefore = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == _Subsidy.SPOUSE_SSN && a.PRIMARY_INVESTOR_SSN != _Subsidy.PRIMARY_INVESTOR_SSN).FirstOrDefault();
                        if (SubsidySpousebefore != null)
                        {
                            TempData["Error"] = "يوجد طلب اخر باسم الشريك";
                            return View(_Subsidy);
                        }
                        var hasException = userbll.exceptionSSnHasApp(SSN);
                        if (!hasException)
                        {
                            #region before has unit formiddle
                            var CheckHasUnit = userbll.checkCustomerHasUnits(_Subsidy.SPOUSE_SSN);
                            if (CheckHasUnit != null)
                            {
                                if (CheckHasUnit.ALLOCATION_DEALLOCATION == 1)
                                {
                                    userbll.saveFiledRegistration(SSN, _Subsidy.SPOUSE_SSN, 1, 1, 0, 0, "");
                                    TempData["Error"] = "يوجد تخصيص سابق للزوجة اعلان" + CheckHasUnit.Adv_Name + " المشروع " + CheckHasUnit.Project_Name + " الوحده    وفي حالة الرغبة في الاستمرار في تسجيل طلبكم وقد قمتم بالفعل بسحب مقدم جدية الحجز بالاعلان السابق/ أو لم تقوموا باسترداد مقدم جدية الحجز بالإعلان السابق، يرجى التواصل مع مركز الاتصالات على رقم (090071117 من أى خط أرضى -1188 /5777/5999 من أى رقم موبايل من الأحد إلى الخميس من الساعة التاسعة صباحاً حتى السادسة مساءاً:";
                                    // + "<br />"
                                    // + "• الاسم"
                                    // + "<br />"
                                    // + "• الموبايل"
                                    // + "<br />"
                                    // + "• الرقم القومي"
                                    // + "<br />"
                                    // + "• في حالة سحب مقدم جدية الحجز يتم إرفاق إفادة باسترداد مبلغ مقدم جدية الحجز وعدم وجود سبق الاستفادة"
                                    // + "<br />"
                                    //+ "• في حالة عدم سحب مقدم جدية الحجز يتم الإفادة بالرغبة فى الاستمرار بالتقديم حاليا دون استرداد مقدم جدية الحجز لحين تأكيد حجز الوحدة السكنية";
                                    return View(_Subsidy);
                                }
                                else
                                {
                                    bool exceptionssn = userbll.exceptionSSn(SSN);
                                    if (exceptionssn == false)
                                    {
                                        var Dimsub = entity.V_Dim_subsidy.Where(a => a.SPOUSE_SSN == _Subsidy.SPOUSE_SSN).FirstOrDefault();
                                        if (Dimsub != null)
                                        {
                                            // var transactiontransformation = entity.V_Transaction_Transformation.Any(a => a.IS_CANCELLED == 0&&a.SSN== _Subsidy.SPOUSE_SSN);
                                            // var transactiontransformation = entity.V_Transaction_Transformation.Where(a => a.IS_CANCELLED == 0 && (a.SSN == SSN || a.SSN == Dimsub.SPOUSE_SSN || a.SSN == Dimsub.PRIMARY_INVESTOR_SSN)).FirstOrDefault();

                                            // if (transactiontransformation != null)
                                            // {
                                            //     userbll.saveFiledRegistration(SSN, _Subsidy.SPOUSE_SSN, 1, 0, 1, 0, "");
                                            //     TempData["Error"] = "يوجد تقديم للزوجة في الاعلان " + Dimsub.Adv_Name + " المشروع " + Dimsub.Gov_Name + "   وفي حالة الرغبة في الاستمرار في تسجيل طلبكم وقد قمتم بالفعل بسحب مقدم جدية الحجز بالاعلان السابق/ أو لم تقوموا باسترداد مقدم جدية الحجز بالإعلان السابق، يرجى التواصل مع مركز الاتصالات على رقم (090071117 من أى خط أرضى -1188 /5777/5999 من أى رقم موبايل من الأحد إلى الخميس من الساعة التاسعة صباحاً حتى السادسة مساءاً، أو إرسال رسالة عبر البريد الإلكتروني  middleincome.shmff@gmail.com على أن تحتوي على البيانات التالية:"
                                            // + "<br />"
                                            // + "• الاسم"
                                            // + "<br />"
                                            // + "• الموبايل"
                                            // + "<br />"
                                            // + "• الرقم القومي"
                                            // + "<br />"
                                            // + "• في حالة سحب مقدم جدية الحجز يتم إرفاق إفادة باسترداد مبلغ مقدم جدية الحجز وعدم وجود سبق الاستفادة"
                                            // + "<br />"
                                            //+ "• في حالة عدم سحب مقدم جدية الحجز يتم الإفادة بالرغبة فى الاستمرار بالتقديم حاليا دون استرداد مقدم جدية الحجز لحين تأكيد حجز الوحدة السكنية";
                                            //     return View(_Subsidy);
                                            // }
                                        }
                                    }
                                }
                            }

                            var middlecheck = entity.CheckMiddelIncomes.Where(a => (a.SPOUSE_SSN == _Subsidy.SPOUSE_SSN || a.PRIMARY_INVESTOR_SSN == _Subsidy.SPOUSE_SSN) && a.STEPNAME != "Rejection List" && a.WithdrawlSSN == null).FirstOrDefault();
                            if (middlecheck != null)
                            {
                                //ViewBag.HasUnitMiddelBefore = 1;
                                //ViewBag.CheckHasUnit = middlecheck;
                                userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");
                                string message = "    یوجد لدیكم " + middlecheck.PRIMARY_INVESTOR_FULL_NAME + " تقدیم سابق في (" +
                                                middlecheck.ADV_NAME + " " + middlecheck.NAME_AR + ")،                                             وفي حالة الرغبة في الاستمرار في تسجيل طلبكم وقد قمتم بالفعل بسحب مقدم جدية الحجز بالاعلان السابق/ أو لم تقوموا باسترداد مقدم جدية الحجز بالإعلان السابق، يرجى التواصل مع مركز الاتصالات على رقم (090071117 من أى خط أرضى -1188 /5777/5999 من أى رقم موبايل من الأحد إلى الخميس من الساعة التاسعة صباحاً حتى السادسة مساءاً:";
                                // + "<br />"
                                // + "• الاسم"
                                // + "<br />"
                                // + "• الموبايل"
                                // + "<br />"
                                // + "• الرقم القومي"
                                // + "<br />"
                                // + "• في حالة سحب مقدم جدية الحجز يتم إرفاق إفادة باسترداد مبلغ مقدم جدية الحجز وعدم وجود سبق الاستفادة"
                                // + "<br />"
                                //+ "• في حالة عدم سحب مقدم جدية الحجز يتم الإفادة بالرغبة فى الاستمرار بالتقديم حاليا دون استرداد مقدم جدية الحجز لحين تأكيد حجز الوحدة السكنية";
                                TempData["Error"] = message;// "یوجد لدیكم " + _Subsidy.SPOUSE_FULL_NAME + " طلب سابق (" + middlecheck.ADV_NAME + " )وفي حالة الرغبة في الاستمرار في الحجز یرجي إرسال ما یفید سحب مقدم جدیة الحجز بالاعلان السابق على البرید الإلكتروني com.gmail @shmff.middleincome أو التواصل مع مركز الاتصالات(090071117 من أى خط أرضى -1188 / 5999 / 5777 من أى رقم موبایل من الأحد إلى الخمیس من الساعة التاسعة صباحاً حتى السادسة مساءاً على أن یتم ذلك خلال مرحلة التسجیل على البوابة الإلكترونیة"; //"يوجد تخصيص سابق للزوجة اعلان" + CheckHasUnit.Adv_Name + " المشروع " + CheckHasUnit.Project_Name + " الوحده ";
                                return View(_Subsidy);



                            }

                            var middleExistSystemLoan = entity.CheckMiddelIncomes.Where(a => (a.SPOUSE_SSN == _Subsidy.SPOUSE_SSN || a.PRIMARY_INVESTOR_SSN == _Subsidy.SPOUSE_SSN) && a.STEPNAME == "Rejection List" && a.WithdrawlSSN == null).FirstOrDefault();
                            if (middleExistSystemLoan != null)
                            {

                                string message = "يوجد لديك تقديم في اعلان سابق  " + middleExistSystemLoan.ADV_NAME + "   وفي حالة الرغبة في الاستمرار في تسجيل طلبكم وقد قمتم بالفعل بسحب مقدم جدية الحجز بالاعلان السابق/ أو لم تقوموا باسترداد مقدم جدية الحجز بالإعلان السابق، يرجى التواصل مع مركز الاتصالات على رقم (090071117 من أى خط أرضى -1188 /5777/5999 من أى رقم موبايل من الأحد إلى الخميس من الساعة التاسعة صباحاً حتى السادسة مساءاً:";
                                // + "<br />"
                                // + "• الاسم"
                                // + "<br />"
                                // + "• الموبايل"
                                // + "<br />"
                                // + "• الرقم القومي"
                                // + "<br />"
                                // + "• في حالة سحب مقدم جدية الحجز يتم إرفاق إفادة باسترداد مبلغ مقدم جدية الحجز وعدم وجود سبق الاستفادة"
                                // + "<br />"
                                //+ "• في حالة عدم سحب مقدم جدية الحجز يتم الإفادة بالرغبة فى الاستمرار بالتقديم حاليا دون استرداد مقدم جدية الحجز لحين تأكيد حجز الوحدة السكنية";

                                ViewBag.HasUnitMiddelBefore = 1;
                                //ViewBag.CheckHasUnit = middlecheck;
                                userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");
                                TempData["Error"] = message;// "یوجد لدیكم " + _Subsidy.SPOUSE_FULL_NAME + " تقديم  سابق  الإعلان الأول – التعاون مع ھیئة المجتمعات (" + middlecheck.ADV_NAME + ") ، فى حالة وجود أى استفسار برجاء إرسال رسالة إلى البرید الإلكتروني com.gmail @shmff.middleincome أو التواصل مع مركز الإتصالات (090071117 من أى خط أرضى -1188 / 5999 / 5777 من أى رقم موبایل من الأحد إلى الخمیس من الساعة التاسعة صباحاً حتى السادسة مساءاً على أن یتم ذلك خلال مرحلة التسجیل على البوابة الإلكترونیة";
                                                            //TempData["Error"] = "         " + CheckHasUnit.Adv_Name + " المشروع " + CheckHasUnit.Project_Name + " الوحده ";
                                return View(_Subsidy);
                            }
                            #endregion


                        }

                    }
                    if (_Subsidy.Primaryextraincome == 1)
                    {
                        if (_Subsidy.PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID_EXTRA == null || _Subsidy.PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE_EXTRA == null || _Subsidy.PRIMARY_INVESTOR_EMPLOYER_NAME_EXTRA == null || _Subsidy.PRIMARY_INVESTOR_EMPLOYER_NAME_EXTRA == ""
                            || _Subsidy.PRIMARY_INVESTOR_EMPLOYER_ADDRESS_EXTRA == null || _Subsidy.PRIMARY_INVESTOR_EMPLOYER_ADDRESS_EXTRA == "" || _Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA == 0 || _Subsidy.PRIMARY_INVESTOR_MONTHLY_INCOME_EXTRA == null
                            )
                        {
                            TempData["Error"] = "بيانات مصادر الدخل الاخري غير مكتملة";
                            return View(_Subsidy);
                        }
                        if (_Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA == 3)
                        {
                            if (_Subsidy.SPOUSE_OTHER_WORK_CLASSIFICATIONS_EXTRA == "" || _Subsidy.SPOUSE_OTHER_WORK_CLASSIFICATIONS_EXTRA == null)
                            {
                                TempData["Error"] = "بيانات وصف العمل غير مكتملة";
                                return View(_Subsidy);
                            }
                        }
                    }


                    if (_Subsidy.SPOUSE_IS_WORKING == 1)
                    {
                        if (_Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID == null || _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE == null || _Subsidy.SPOUSE_EMPLOYER_NAME == "" || _Subsidy.SPOUSE_EMPLOYER_NAME == null || _Subsidy.SPOUSE_EMPLOYER_ADDRESS == "" || _Subsidy.SPOUSE_EMPLOYER_ADDRESS == null
                            || _Subsidy.SPOUSE_ACADEMIC_DEGREE_ID == null || _Subsidy.SPOUSE_MONTHLY_INCOME == 0 || _Subsidy.SPOUSE_MONTHLY_INCOME == null
                            )
                        {
                            TempData["Error"] = "بيانات مصادر دخل الزوج/الزوجة غير مكتملة";
                            return View(_Subsidy);
                        }

                        if (_Subsidy.Spouseextraincome == 1)
                        {
                            if (_Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA == null || _Subsidy.SPOUSE_EMPLOYER_GOVERNORATE_EXTRA == null || _Subsidy.SPOUSE_EMPLOYER_NAME_EXTRA == null || _Subsidy.SPOUSE_EMPLOYER_NAME_EXTRA == ""
                                || _Subsidy.SPOUSE_EMPLOYER_ADDRESS_EXTRA == null || _Subsidy.SPOUSE_EMPLOYER_ADDRESS_EXTRA == "" || _Subsidy.SPOUSE_MONTHLY_INCOME_EXTRA == 0 || _Subsidy.SPOUSE_MONTHLY_INCOME_EXTRA == null
                                )
                            {
                                TempData["Error"] = "بيانات مصادر دخل الزوج/الزوجة الاخري غير مكتملة";
                                return View(_Subsidy);
                            }
                            if (_Subsidy.SPOUSE_CLASSIFICATION_OF_WORK_ID_EXTRA == 3)
                            {
                                if (_Subsidy.SPOUSE_OTHER_WORK_CLASSIFICATIONS_EXTRA == "" || _Subsidy.SPOUSE_OTHER_WORK_CLASSIFICATIONS_EXTRA == null)
                                {
                                    TempData["Error"] = "بيانات وصف العمل  للزوجة غير مكتملة";
                                    return View(_Subsidy);
                                }
                            }
                        }
                    }
                }

                if (_Subsidy.MARITAL_STATUS_ID == 5 || _Subsidy.MARITAL_STATUS_ID == 6 || _Subsidy.MARITAL_STATUS_ID == 3)
                {
                    if (_Subsidy.NUMBER_OF_FAMILY_MEMBERS == null)
                    {
                        TempData["Error"] = "برجاء ادخال عدد الابناء";// "عدد الابناء غير مكتمل";
                        return View(_Subsidy);
                    }
                }
                //if (_Subsidy.PICTURE_SSN == 0 || _Subsidy.PICTURE_INCOME_PROOF == 0 || _Subsidy.PICTURE_CURRENT_RESIDENCE_PROOF == 0 || _Subsidy.APPLICATION_CHECKBOX == 0 || _Subsidy.ACKNOWLEDGMENT_CHECKBOX == 0)
                //{
                //    TempData["Error"] = "برجاء عمل تحديد علي المرفقات المطللوبة";// "عدد الابناء غير مكتمل";
                //    return View(_Subsidy);
                //}

                using (var context = new NewPortalDBEntities())
                {
                    //context.Database.Log = Console.Write;

                    //using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                    try
                    {
                        if (_Subsidy.ID != 0)
                        {
                            if (ATTACHMENT_NAME != null)
                            {
                                if (ATTACHMENT_NAME.ContentLength > 0)
                                {
                                    if (ATTACHMENT_NAME.ContentLength <= 2 * 1024 * 1024)
                                    {
                                        if (Path.GetExtension(ATTACHMENT_NAME.FileName).ToLower() == ".pdf")
                                        {
                                            string filename = Path.GetFileName(ATTACHMENT_NAME.FileName);
                                            var formattedFileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(filename));
                                            string folderpath = Path.Combine(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN), formattedFileName);
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/")))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/"));
                                            }
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN)))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN));
                                            }
                                            if (!Directory.Exists(folderpath))
                                            {
                                                ATTACHMENT_NAME.SaveAs(folderpath);
                                                _Subsidy.ATTACHMENT_NAME = folderpath;
                                            }
                                        }
                                        else
                                        {
                                            TempData["Error"] = " صيغة الملف ليست صحيحة(.pdf)";
                                            return View(_Subsidy);
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "حجم الملف اكبر من الحجم المطلوب (2MB)";
                                        return View(_Subsidy);
                                    }
                                }
                            }
                            else
                            {
                                // _Subsidy.ATTACHMENT_NAME=
                                //  TempData["Error"] = " برجاء إرفاق المستندات المطلوبة";
                                //  return View(_Subsidy);
                            }
                            var subsidyrow = entity.subsidy_request.Where(a => a.ID == _Subsidy.ID).FirstOrDefault();
                            _Subsidy.UPDATE_DATE = DateTime.Now;
                            _Subsidy.CREATION_DATE = subsidyrow.CREATION_DATE;
                            // entity.Entry(_Subsidy).State = EntityState.Modified;
                            // entity.subsidy_request.AddOrUpdate(_Subsidy);
                            if (_Subsidy.ATTACHMENT_NAME == null)
                            {
                                _Subsidy.ATTACHMENT_NAME = subsidyrow.ATTACHMENT_NAME;
                            }
                            context.subsidy_request.Attach(_Subsidy);


                            var CheckExits = checkAppexist(SSN, _Subsidy.ID);
                            if (CheckExits == true)
                            {
                                TempData["Error"] = "يوجد لديك طلب اخر بالفعل";// "عدد الابناء غير مكتمل";
                                return View(_Subsidy);
                            }

                            Requested_Unit request_Unit = context.Requested_Unit.Where(ru => ru.SUBSIDY_REQUEST_ID == _Subsidy.ID).FirstOrDefault();
                            if (request_Unit == null)
                            {
                                request_Unit = new Requested_Unit()
                                {
                                    SUBSIDY_REQUEST_ID = _Subsidy.ID,
                                    ADVERTISMENT_ID = _Subsidy.Adv_Id,
                                    PROJECT_ID = _Subsidy.Project_Id,
                                    CITY_ID = _Subsidy.CityID,
                                    GOVERNORATE_ID = _Subsidy.Gov_Id
                                };

                                context.Requested_Unit.Add(request_Unit);
                            }
                            else
                            {
                                request_Unit.ADVERTISMENT_ID = _Subsidy.Adv_Id;
                                request_Unit.PROJECT_ID = _Subsidy.Project_Id;
                                request_Unit.CITY_ID = _Subsidy.CityID;
                                request_Unit.GOVERNORATE_ID = _Subsidy.Gov_Id;
                            }

                            context.Entry(_Subsidy).State = EntityState.Modified;
                            int i = context.SaveChanges();
                            //var tempunit = context.TempUnitReserverfs.Where(a => a.UNITCODE == _Subsidy.UNITCODE && a.RequestedUnitID == _Subsidy.RequestedUnitID).FirstOrDefault();
                            //if (tempunit == null)
                            //{
                            //    TempData["Error"] = "يرجي التأكد من الوحدة المختارة ";
                            //    return View(_Subsidy);
                            //}
                            //tempunit.SUbsidrRequestID = _Subsidy.ID;
                            //tempunit.IstempOrReserved = 2;
                            // int x = context.SaveChanges();
                            // transaction.Commit();


                            return RedirectToAction("applications");
                        }
                        else
                        {
                            if (ATTACHMENT_NAME != null)
                            {
                                if (ATTACHMENT_NAME.ContentLength > 0)
                                {
                                    if (ATTACHMENT_NAME.ContentLength <= 2 * 1024 * 1024)
                                    {
                                        if (Path.GetExtension(ATTACHMENT_NAME.FileName).ToLower() == ".pdf")
                                        {
                                            string filename = Path.GetFileName(ATTACHMENT_NAME.FileName);
                                            var formattedFileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(filename));
                                            string folderpath = Path.Combine(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN), formattedFileName);
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/")))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/"));
                                            }
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN)))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN));
                                            }
                                            if (!Directory.Exists(folderpath))
                                            {
                                                ATTACHMENT_NAME.SaveAs(folderpath);
                                                _Subsidy.ATTACHMENT_NAME = folderpath;
                                            }
                                        }
                                        else
                                        {
                                            TempData["Error"] = " صيغة الملف ليست صحيحة(.pdf)";
                                            return View(_Subsidy);

                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "حجم الملف اكبر من الحجم المطلوب (2MB)";
                                        return View(_Subsidy);
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = " برجاء إرفاق المستندات المطلوبة";
                                return View(_Subsidy);
                            }

                            _Subsidy.CREATION_DATE = DateTime.Now;
                            //var tempunit = context.TempUnitReserverfs.Where(a => a.UNITCODE == _Subsidy.UNITCODE && a.RequestedUnitID == _Subsidy.RequestedUnitID).FirstOrDefault();
                            //if (tempunit == null)
                            //{
                            //    TempData["Error"] = "يرجي التأكد من الوحدة المختارة ";
                            //    return View(_Subsidy);
                            //}
                            var CheckExits = checkAppexist(SSN);
                            if (CheckExits == true)
                            {
                                TempData["Error"] = "يوجد لديك طلب اخر بالفعل";// "عدد الابناء غير مكتمل";
                                return View(_Subsidy);
                            }

                            context.subsidy_request.Add(_Subsidy);
                            int i = context.SaveChanges();

                            Requested_Unit request_Unit = context.Requested_Unit.Where(ru => ru.SUBSIDY_REQUEST_ID == _Subsidy.ID).FirstOrDefault();

                            if (request_Unit == null)
                            {
                                request_Unit = new Requested_Unit()
                                {
                                    SUBSIDY_REQUEST_ID = _Subsidy.ID,
                                    ADVERTISMENT_ID = _Subsidy.Adv_Id,
                                    PROJECT_ID = _Subsidy.Project_Id,
                                    CITY_ID = _Subsidy.CityID,
                                    GOVERNORATE_ID = _Subsidy.Gov_Id
                                };

                                context.Requested_Unit.Add(request_Unit);
                            }
                            else
                            {
                                request_Unit.ADVERTISMENT_ID = _Subsidy.Adv_Id;
                                request_Unit.ADVERTISMENT_ID = _Subsidy.Adv_Id;
                                request_Unit.PROJECT_ID = _Subsidy.Project_Id;
                                request_Unit.CITY_ID = _Subsidy.CityID;
                                request_Unit.GOVERNORATE_ID = _Subsidy.Gov_Id;
                            }

                            context.SaveChanges();
                            //tempunit.SUbsidrRequestID = _Subsidy.ID;
                            //tempunit.IstempOrReserved = 2;
                            //context.SaveChanges();
                            //transaction.Commit();

                            TempData["Success"] = "تم إرسال طلبكم بنجاح ";

                            return RedirectToAction("applications");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions logExceptions = new LogExceptions();
                        logExceptions.LogError(ex);
                        TempData["Error"] = "حدث خطأ في ادخال بعض البيانات برجاء التجربة مره اخري";
                    }
                    //}
                }

            }
            else
            {
                TempData["Error"] = "يرجي إدخال كافة البيانات المطلوبة بشكل صحيح";
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return View(_Subsidy);
        }

        public ActionResult Cancel(int? id)
        {
            // return RedirectToAction("index");
            string SSN = Helper.Decode(Session["SSN"].ToString());
            //return RedirectToAction("Applications", "Reservation");
            var applicationrequest = entity.subsidy_request.Where(a => a.ID == id && a.PRIMARY_INVESTOR_SSN == SSN).FirstOrDefault();
            var ClientAdv = entity.NEWADVERTISMENTS.Where(a => a.ID == applicationrequest.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();

            if (ClientAdv != null)
            {
                if (DateTime.Now > ClientAdv.REGISTRATIONSTARTDATE)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (applicationrequest.IS_CANCELED == 1)
            {
                TempData["Error"] = "تم الغاء طلبك من قبل";
            }
            else if (applicationrequest != null)
            {
                TempUnitReserverf tm = new TempUnitReserverf();
                tm.UNITCODE = applicationrequest.UNITCODE;
                tm.SSN = SSN;
                tm.CancelationReservedDate = DateTime.Now;
                tm.IS_CANCELED = 1;
                tm.IstempOrReserved = 1;
                entity.TempUnitReserverfs.Add(tm);
                entity.SaveChanges();

                //UBSIDY_REQUESTED_UNIT su = new UBSIDY_REQUESTED_UNIT();
                //su.UNITCODE
                var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.UNITCODE == applicationrequest.UNITCODE && a.SSN == SSN).FirstOrDefault();
                if (su != null)
                {
                    entity.UBSIDY_REQUESTED_UNIT.Remove(su);
                }
                applicationrequest.IS_CANCELED = 1;
                applicationrequest.CANCELATIONDATE = DateTime.Now;
                // applicationrequest.UNITCODE = null;
                //var tempunit = entity.TempUnitReserverfs.Where(a => a.UNITCODE == applicationrequest.UNITCODE && a.RequestedUnitID == applicationrequest.RequestedUnitID).FirstOrDefault();
                //if (tempunit != null)
                //{
                //    tempunit.IS_CANCELED = 1;
                //}
                entity.SaveChanges();
                TempData["Error"] = "تم الغاء طلبك";
            }
            else
            {
                TempData["Error"] = "برجاء التأكد من الاجراء المراد تنفيذه";
            }


            return RedirectToAction("Applications");
        }

        private ActionResult getAttachment(string fileName)
        {
            try
            {
                string pathf = Server.MapPath("~/App_Data/UploadedFiles/Reserv/");
                string SSN = Helper.Decode(Session["SSN"].ToString());
                pathf = pathf + "\\" + SSN + "\\";
                string filepath = Path.Combine(pathf, fileName);

                //    string fileName = Path.GetFileName(filepath);
                return File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                //var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //var fsResult = new FileStreamResult(fileStream, "application/pdf");
                //return fsResult;
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult error()
        {
            return View();
        }

        public JsonResult GetPRoJECTS(int id)
        {
            string SSN = Helper.Decode(Session["SSN"].ToString());
            //int cityid = 0;
            //var ssncan = entity.ReservSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (ssncan != null)
            //{
            //    cityid = ssncan.CITYID ?? 0;
            //}
            //10
            var lst = entity.v_PROJECTS.Where(s => s.ADVID == id).ToList().Distinct();
            return Json(new SelectList(lst, "ID", "CITYNAME"));
        }

        public JsonResult GetUnits(int id, int ADVID)
        {
            //       return Json(true);

            //check typeofunit ith type of Client
            string SSN = Helper.Decode(Session["SSN"].ToString());
            List<v_Unconditional_Unit> lst = new List<v_Unconditional_Unit>();
            lst = entity.v_Unconditional_Unit.Where(s => s.PROJECT_ID == id && s.ADVERTISEMENT_ID == ADVID && s.subsidy_requestID == null && s.IS_CONTRACTED == 1).OrderBy(a => a.UNIT_CODE).ToList();

            //var ssncan = entity.ReservSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (ssncan.Type != 3)
            //{
            //    lst = entity.v_Unconditional_Unit.Where(s => s.PROJECT_ID == id && s.ADVERTISEMENT_ID == ADVID && s.subsidy_requestID == null && s.IS_CONTRACTED == ssncan.Type).ToList();
            //}
            //else
            //{
            //    if (ssncan.CITYID == 10)
            //    {
            //        lst = entity.v_Unconditional_Unit.Where(s => s.PROJECT_ID == id && s.ADVERTISEMENT_ID == ADVID && s.subsidy_requestID == null && (s.IS_CONTRACTED == 2 || s.IS_CONTRACTED == 3)).ToList();
            //    }
            //    else
            //    {
            //        lst = entity.v_Unconditional_Unit.Where(s => s.PROJECT_ID == id && s.ADVERTISEMENT_ID == ADVID && s.subsidy_requestID == null && s.IS_CONTRACTED == 3).ToList();
            //    }
            //}

            return Json(new SelectList(lst, "ID", "UNIT_CODE"));
            //  return Json(new SelectList(lst, "ID", "UNIT_CODE"));
        }

        public ActionResult ReservUnit(int? id)
        {

            //return RedirectToAction("Applications");
            ReuestedUnitVM mode = new ReuestedUnitVM();
            ViewBag.ID = id;
            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }
            string SSN = Helper.Decode(Session["SSN"].ToString());


            #region check downpayment for adv

            //var middlecheck = entity.CheckMiddelIncomes.Where(a => a.WithdrawlSSN == SSN && a.ADVERTISEMENT_CODE == 5000021).FirstOrDefault();
            //var checkpaid = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN&&a.ADVERTISEMENT_CODE==5000023).FirstOrDefault();
            List<HDB_Payment_Confirmation> checkpaid = new List<HDB_Payment_Confirmation>();
            //if (middlecheck == null)
            //{
            //    checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            //}
            //else
            //{
            //    checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            //}

            //if (checkpaid.Count() == 0)
            //{
            //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
            //    return RedirectToAction("error", "Reservation");
            //}
            #endregion

            var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1 && a.ID == id).FirstOrDefault();

            if (_Subsidy == null)
            {
                return RedirectToAction("Applications", "Reservation");
            }

            //if (DateTime.Now < new DateTime(2020, 09, 28))
            //{
            //}
            //else
            //{
            //    ViewBag.ssn = SSN;
            //    var ssncan = entity.ReservSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
            //    if (ssncan == null)
            //    {
            //        return RedirectToAction("Applications", "Reservation");
            //    }
            //    else
            //    {
            //        if (DateTime.Now > ssncan.STARTDATE && DateTime.Now < ssncan.ENDDATE)
            //        {
            //            ViewBag.reservunit = 1;
            //        }
            //        else
            //        {
            //            return RedirectToAction("Applications", "Reservation");
            //        }
            //    }
            //}
            var hasException = userbll.exceptionSSnHasApp(SSN);
            if (!hasException)
            {
                #region check before has units

                var previousget = entity.SSNPreviousTakens.Where(a => a.SSN == SSN || a.SSN == _Subsidy.SPOUSE_SSN).FirstOrDefault();
                if (previousget != null)
                {
                    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً  لسبق الاستفادة  : " + previousget.Message;// "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";

                    return RedirectToAction("error", "Reservation");
                }
            }
            #endregion

            DateTime NowToday = DateTime.Now;
            //reserve before all
            var ssnCanReserv = entity.EXCEPTION_SSN_RESERV.Where(a => a.SSN == SSN).FirstOrDefault();
            ViewBag.ssn = SSN;
            if (ssnCanReserv != null)
            {
                if (ssnCanReserv.RESERVATION_START_DATE <= DateTime.Now && ssnCanReserv.RESERVATION_END_DATE >= DateTime.Now)
                {
                    ViewBag.canres = 1;
                    ViewBag.reservStartDate = ssnCanReserv.RESERVATION_START_DATE;
                    ViewBag.reservEndDate = ssnCanReserv.RESERVATION_START_DATE;
                }
                else
                {
                    TempData["Error"] = "لم يتم بدء الحجز";
                    return RedirectToAction("applications", "Reservation");
                }
            }
            else
            {
                var settings = entity.SETTINGS.FirstOrDefault();
                if (settings != null)
                {
                    if (settings.OPENRESERV == false)
                    {
                        TempData["Error"] = "لم يتم بدء الحجز";
                        return RedirectToAction("applications", "Reservation");
                    }
                }
            }


            #region check money for adv

            var ClientAdv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
            if (ClientAdv != null)
            {
                //if (DateTime.Now < ClientAdv.RESERVATIONSTARTDATE)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }

            //if (checkpaid.Sum(a => a.DOWNPAYMENT_AMOUNT_COLLECTED_MFF) < (double)ClientAdv.DOWNPAYMENT_AMOUNT)
            //{
            //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
            //    return View(_Subsidy);
            //}

            #endregion

            ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
            ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
            ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
            ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);
            ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
            if (_Subsidy.ADVFLAG != null)
            {
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
                ViewBag.MyMainADv = Myadv.PORTALADVID;
                ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR", Myadv.PORTALADVID);
            }
            else
            {
                ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR");
            }

            //ViewBag.PROJECTID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == _Subsidy.ADVID), "ID", "NAME_AR");

            var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();

            if (su != null)
            {
                _Subsidy.UNITCODE = su.UNITCODE;
                ViewBag.Isreserved = 1;
                ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
            }

            return View(_Subsidy);
        }


        public ActionResult Download(int? id)
        {
            try
            {
                string FilePath = "";
                if (id == null)
                {
                    return RedirectToAction("index");
                }
                string SSN = Helper.Decode(Session["SSN"].ToString());

                var Adv = entity.subsidy_request.Where(a => a.ID == id && a.IS_CANCELED != 1 && a.PRIMARY_INVESTOR_SSN == SSN).FirstOrDefault();
                if (Adv != null)
                {
                    string fileName = Adv.ATTACHMENT_NAME;
                    FilePath = fileName;// Server.MapPath("~/App_Data/" + fileName);
                    return File(FilePath, System.Net.Mime.MediaTypeNames.Application.Octet, "attachment.pdf");
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerActionFilter]
        public ActionResult ReservUnit(int? UnitCode, long? ProjectID, long? AdvID, int? id, HttpPostedFileBase OTHER_ATTACHMENT_NAME)
        {
            try
            {
                ViewBag.ID = id;
                string SSN = Helper.Decode(Session["SSN"].ToString());
                var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1 && a.ID == id).FirstOrDefault();
                ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
                ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
                ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
                ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);
                ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();


                if (_Subsidy.ADVFLAG != null)
                {
                    ViewBag.MyMainADv = Myadv.PORTALADVID;
                    ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR", Myadv.PORTALADVID);
                }
                else
                {
                    ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR");
                }


                if (UnitCode == null || ProjectID == null || id == null)
                {
                    TempData["Error"] = "يرجي ادخال كافة البيانات المطلوبة";
                    return View();
                }

                //  ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS, "PORTALADVID", "ADVNAME_AR", AdvID);
                var _Unconditional_Unit = entity.v_Unconditional_Unit.Where(a => a.ID == UnitCode).FirstOrDefault();
                if (_Unconditional_Unit != null)
                {
                    // var servedBefore = entity.subsidy_request.Where(a => a.UNITCODE == UnitCode).FirstOrDefault();
                    if (_Unconditional_Unit.PRIMARY_INVESTOR_SSN == SSN)
                    {
                        //check if has unit before
                        TempData["Success"] = "الوحدة تم حجزها من قبلك";
                        var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();
                        if (su != null)
                        {
                            _Subsidy.UNITCODE = su.UNITCODE;
                            ViewBag.Isreserved = 1;
                            ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
                        }
                        return View(_Subsidy);
                    }
                    else if (_Unconditional_Unit.PRIMARY_INVESTOR_SSN != null)
                    {
                        TempData["Error"] = "الوحدة تم حجزها من قبل عميل اخر";
                        return View();
                        // return Json(new { status = 0, errormesssage = "الوحدة محجوزة  ", data = new { } });
                    }
                    else
                    {

                        #region check money first which paid
                        ////check money first which paid
                        var Advs = entity.NEWADVERTISMENTS.Where(a => a.PORTALADVID == _Unconditional_Unit.ADVERTISEMENT_ID && a.ISCLOSED != true).FirstOrDefault();
                        // var ClientAmountrow = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN).FirstOrDefault();
                        var middlecheck = entity.CheckMiddelIncomes.Where(a => a.WithdrawlSSN == SSN && a.ADVERTISEMENT_CODE == 5000021).FirstOrDefault();
                        //var checkpaid = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN&&a.ADVERTISEMENT_CODE==5000023).FirstOrDefault();
                        List<HDB_Payment_Confirmation> checkpaid = new List<HDB_Payment_Confirmation>();
                        if (middlecheck == null)
                        {
                            checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
                        }
                        else
                        {
                            checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
                        }

                        if (checkpaid.Count() == 0)
                        {
                            TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
                            return RedirectToAction("error", "Reservation");
                        }
                        #endregion

                        //if (ClientAmountrow == null)
                        //{
                        //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
                        //    return View();
                        //    //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز لاستكمال الإجراءات", data = new { } });
                        //}

                        //if (ClientAmountrow.TotalDOwnPAyment < (double)Advs.DOWNPAYMENT_AMOUNT)
                        //{
                        //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
                        //    return View();
                        //    //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز كاملة لاستكمال الإجراءات", data = new { } });
                        //}

                        #region check money paid before

                        var ClientNewAdv = entity.NEWADVERTISMENTS.Where(a => a.PORTALADVID == AdvID && a.ISCLOSED != true).FirstOrDefault();
                        if (checkpaid.Sum(a => a.DOWNPAYMENT_AMOUNT_COLLECTED_MFF) < (double)ClientNewAdv.DOWNPAYMENT_AMOUNT)
                        {
                            TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
                            return View(_Subsidy);
                            //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز كاملة لاستكمال الإجراءات", data = new { } });
                        }

                        #endregion
                        //var lst = entity.v_PROJECTS.Where(s => s.ADVERTISEMENT_ID == id).ToList().Distinct();
                        //var _Unconditional_Unit = entity.v_Unconditional_Unit.Where(a => a.UNIT_CODE == UnitCode ).FirstOrDefault();
                        //if (_Unconditional_Unit != null)
                        //{
                        // var servedBefore = entity.CheckReservedForsameUser(SSN, _Unconditional_Unit.ID);
                        //if (AdvID != Myadv.PORTALADVID)
                        //{
                        //    if (OTHER_ATTACHMENT_NAME != null)
                        //    {
                        //        if (OTHER_ATTACHMENT_NAME.ContentLength > 0)
                        //        {
                        //            if (OTHER_ATTACHMENT_NAME.ContentLength <= 2 * 1024 * 1024)
                        //            {
                        //                if (Path.GetExtension(OTHER_ATTACHMENT_NAME.FileName) == ".pdf")
                        //                {
                        //                    string filename = Path.GetFileName(OTHER_ATTACHMENT_NAME.FileName);
                        //                    var formattedFileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(filename));
                        //                    string folderpath = Path.Combine(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN), formattedFileName);
                        //                    if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/")))
                        //                    {
                        //                        Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/"));
                        //                    }
                        //                    if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN)))
                        //                    {
                        //                        Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN));
                        //                    }
                        //                    if (!Directory.Exists(folderpath))
                        //                    {
                        //                        OTHER_ATTACHMENT_NAME.SaveAs(folderpath);
                        //                        _Subsidy.OTHER_ATTACHMENT_NAME = folderpath;
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    TempData["Error"] = " صيغة الملف ليست صحيحة(.pdf)";
                        //                    return View(_Subsidy);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                TempData["Error"] = "حجم الملف اكبر من الحجم المطلوب (2MB)";
                        //                return View(_Subsidy);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            TempData["Error"] = " الملف مطلوب";
                        //            return View(_Subsidy);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        TempData["Error"] = " الملف مطلوب";
                        //        return View(_Subsidy);
                        //    }
                        //}


                        #region check adv not same adv 
                        if (AdvID != Myadv.PORTALADVID)
                        {
                            if (OTHER_ATTACHMENT_NAME != null)
                            {
                                if (OTHER_ATTACHMENT_NAME.ContentLength > 0)
                                {
                                    if (OTHER_ATTACHMENT_NAME.ContentLength <= 2 * 1024 * 1024)
                                    {
                                        if (Path.GetExtension(OTHER_ATTACHMENT_NAME.FileName).ToLower() == ".pdf")
                                        {
                                            string filename = Path.GetFileName(OTHER_ATTACHMENT_NAME.FileName);
                                            var formattedFileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(filename));
                                            string folderpath = Path.Combine(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN), formattedFileName);
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/")))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/"));
                                            }
                                            if (!Directory.Exists(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN)))
                                            {
                                                Directory.CreateDirectory(Server.MapPath("~/APP_Data/UploadedFiles/Reserv/" + _Subsidy.PRIMARY_INVESTOR_SSN));
                                            }
                                            if (!Directory.Exists(folderpath))
                                            {
                                                OTHER_ATTACHMENT_NAME.SaveAs(folderpath);
                                                _Subsidy.OTHER_ATTACHMENT_NAME = folderpath;
                                            }
                                        }
                                        else
                                        {
                                            TempData["Error"] = " صيغة الملف ليست صحيحة(.pdf)";
                                            return View(_Subsidy);

                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "حجم الملف اكبر من الحجم المطلوب (2MB)";
                                        return View(_Subsidy);
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = " الملف مطلوب";
                                    return View(_Subsidy);
                                }
                            }
                            else
                            {
                                TempData["Error"] = " الملف مطلوب";
                                return View(_Subsidy);
                            }
                        }

                        #endregion

                        TempUnitReserverf tm = new TempUnitReserverf();
                        tm.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        tm.SSN = SSN;
                        tm.RequestedUnitID = _Unconditional_Unit.ID;
                        tm.PROJECTID = _Unconditional_Unit.PROJECT_ID;
                        tm.Reservedate = DateTime.Now;
                        tm.IstempOrReserved = 3;
                        entity.TempUnitReserverfs.Add(tm);
                        entity.SaveChanges();


                        var _subsidy_request = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.ID == id).FirstOrDefault();
                        //_subsidy_request.UNITCODE = UnitCode;
                        _subsidy_request.PROJECTID = _Unconditional_Unit.PROJECT_ID;
                        //_subsidy_request.ADVID = Advs.PORTALADVID;
                        //_subsidy_request.RequestedUnitID = _Unconditional_Unit.ID;
                        _subsidy_request.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        UBSIDY_REQUESTED_UNIT su = new UBSIDY_REQUESTED_UNIT();
                        su.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        su.UNIT_ID = _Unconditional_Unit.ID;
                        su.ADVERTISMENT_ID = Advs.PORTALADVID;
                        su.PROJECT_ID = _Unconditional_Unit.PROJECT_ID;
                        su.SSN = SSN;
                        su.DATE_ADDED = DateTime.Now;
                        su.SUBSIDY_REQUEST_ID = _subsidy_request.ID;
                        entity.UBSIDY_REQUESTED_UNIT.Add(su);

                        int result = entity.SaveChanges();
                        if (result >= 1)
                        {
                            TempData["Success"] = "تم الاختيار بنجاح";
                            su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();
                            if (su != null)
                            {
                                _Subsidy.UNITCODE = su.UNITCODE;
                                ViewBag.Isreserved = 1;
                                ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
                            }
                            SMSHelper sMS = new SMSHelper();
                            sMS.sendMessage("تم حجز الوحدة السكنية بكود رقم  " + _Unconditional_Unit.UNIT_CODE + " بنجاح", _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER);
                            return View(_Subsidy);
                        }
                        // return Json(new { status = 1, errormesssage = "تم الاختيار بنجاح", data = _Unconditional_Unit });
                        else
                        {
                            TempData["Error"] = "لم يتم الاختيار للوحدة يرجي التجربة مره اخري وادخال كافة البيانات المطلوبة بشكل صحيح";
                            return View();
                        }
                    }

                    //if (servedBefore > 0)
                    //{
                    //    return Json(new { status = 1, errormesssage = "تم ", data = _Unconditional_Unit });
                    //}
                    //try
                    //{
                    //    //long subsidyrequestid = userbll.GetSubsidyRequestID(SSN);
                    //    _Unconditional_Unit.RESERVED = 1;
                    //    TempUnitReserverf tempunit = new TempUnitReserverf();
                    //    tempunit.RequestedUnitID = _Unconditional_Unit.ID;
                    //    tempunit.IstempOrReserved = 1;
                    //    tempunit.SSN = Helper.Decode(Session["SSN"].ToString());
                    //    tempunit.SUbsidrRequestID = 0;
                    //    tempunit.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                    //    tempunit.ReservedDate = DateTime.Now;
                    //    tempunit.PROJECTID = _Unconditional_Unit.PROJECT_ID;

                    //    entity.TempUnitReserverfs.Add(tempunit);
                    //    entity.SaveChanges();
                    //    return Json(new { status = 1, errormesssage = "تم ", data = _Unconditional_Unit });
                    //}
                    //catch (Exception ex)
                    //{
                    //    return Json(new { status = 0, errormesssage = "الوحدة محجوزه من قبل عميل اخر ", data = new { } });
                    //}
                }
                else
                {
                    TempData["Error"] = "الوحدة غير موجوده او تخص مشروع اخر";
                    return View();
                    //return Json(new { status = 0, errormesssage = "الوحدة غير موجوده او تخص مشروع اخر", data = new { } });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "الوحدة تم حجزها من قبل عميل اخر";
                return View();
                // return Json(new { status = 0, errormesssage = "الوحدة محجوزة  ", data = new { } });
            }

            return View();
        }
        [LoggerActionFilter]
        public ActionResult UnReserveUnit(int id)
        {
            //  return RedirectToAction("Applications", "Reservation");
            if (OurConfiguration.UnreservUnit == "false")
            {
                return RedirectToAction("Applications", "Reservation");
            }
            string SSN = Helper.Decode(Session["SSN"].ToString());
            //if (DateTime.Now < new DateTime(2020, 09, 28))
            //{
            //}
            //else
            //{
            //    // return RedirectToAction("Applications");
            //}
            //  var unit = entity.TempUnitReserverfs.Where(a => a.SSN == SSN).FirstOrDefault();
            var _Unconditional_Unit = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SUBSIDY_REQUEST_ID == id && a.SSN == SSN).FirstOrDefault();
            //if (_Unconditional_Unit != null)
            //{
            //    _Unconditional_Unit.RESERVED = null;
            //}
            //var ssncan = entity.ReservSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
            //if (ssncan == null)
            //{
            //    return RedirectToAction("Applications", "Reservation");
            //}
            //else
            //{
            //if (DateTime.Now > ssncan.STARTDATE && DateTime.Now < ssncan.ENDDATE)
            //{
            //    ViewBag.reservunit = 1;
            //}
            //else
            //{
            //    return RedirectToAction("Applications", "Reservation");
            //}
            //}

            if (_Unconditional_Unit != null)
            {
                TempUnitReserverf tm = new TempUnitReserverf();
                tm.UNITCODE = _Unconditional_Unit.UNITCODE;
                tm.SSN = SSN;
                tm.CancelationReservedDate = DateTime.Now;
                tm.IS_CANCELED = 1;
                tm.IstempOrReserved = 2;

                entity.TempUnitReserverfs.Add(tm);

                var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.UNITCODE == _Unconditional_Unit.UNITCODE && a.SSN == SSN).FirstOrDefault();
                entity.UBSIDY_REQUESTED_UNIT.Remove(su);

                var unconditiona = entity.Unconditional_Unit.Where(a => a.UNIT_CODE == _Unconditional_Unit.UNITCODE).FirstOrDefault();
                if (unconditiona != null)
                {
                    unconditiona.IS_CONTRACTED = 0;
                }

                var _subsidy_request = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.ID == id).FirstOrDefault();

                _subsidy_request.OTHER_ATTACHMENT_NAME = null;
                _subsidy_request.PROJECTID = null;
                _subsidy_request.UNITCODE = null;

                entity.SaveChanges();
                // return Json(new { status = 1, errormesssage = "تم الالغاء" });
                TempData["Success"] = "تم الالغاء بنجاح";
                SMSHelper sMS = new SMSHelper();
                sMS.sendMessage("تم إلغاء حجز الوحدة  بكود رقم  " + _Unconditional_Unit.UNITCODE + " بنجاح", _subsidy_request.PRIMARY_INVESTOR_CELL_NUMBER);
            }
            else
            {
                TempData["Error"] = "الوحدة ملغاه من قبل او الطلب غير صحيح";
            }

            return RedirectToAction("ReservUnitNew", new { id = id });

        }

        //public InqSbkResponse CallWebService(String SSN)
        //{
        //    var _url = "http://172.17.7.47:8070/service.php";
        //    var _action = "http://xxxxxxxx/Service1.asmx?op=HelloWorld";

        //    XmlDocument soapEnvelopeXml = CreateSoapEnvelope(SSN);
        //    HttpWebRequest webRequest = CreateWebRequest(_url, _action);
        //    InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

        //    // begin async call to web request.
        //    //IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

        //    //// suspend this thread until call is complete. You might want to
        //    //// do something usefull here like update your UI.
        //    //asyncResult.AsyncWaitHandle.WaitOne();

        //    // get the response from the completed web request.
        //    string soapResult;
        //    using (WebResponse webResponse = webRequest.GetResponse())
        //    {
        //        using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
        //        {
        //            soapResult = rd.ReadToEnd();
        //        }

        //        var Value = XDocument.Parse(soapResult);

        //        XNamespace ns = @"http://schemas.xmlsoap.org/soap/envelope/";
        //        var unwrappedResponse = Value.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

        //        XmlSerializer oXmlSerializer = new XmlSerializer(typeof(InqSbkResponse));

        //        var responseObj = (InqSbkResponse)oXmlSerializer.Deserialize(unwrappedResponse.CreateReader());

        //        return responseObj;
        //        //                Console.Write(soapResult);
        //    }
        //}

        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope(string SSN)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
            @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:InqSbk>
         <tem:NationalId>" + SSN + "</tem:NationalId><tem:StageNumber>99</tem:StageNumber></tem:InqSbk></soapenv:Body></soapenv:Envelope>");
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }


        [LoggerActionFilter]
        public ActionResult ReservUnitNew()
        {
            ReuestedUnitVM mode = new ReuestedUnitVM();
            // return RedirectToAction("applications");
            if (userbll.ChecUServerifired() == false)
            {
                return RedirectToAction("VerifyCode", "Account");
            }
            string SSN = Helper.Decode(Session["SSN"].ToString());


            #region check downpayment for adv

            var middlecheck = entity.CheckMiddelIncomes.Where(a => a.WithdrawlSSN == SSN && a.ADVERTISEMENT_CODE == 5000021).FirstOrDefault();
            //var checkpaid = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN&&a.ADVERTISEMENT_CODE==5000023).FirstOrDefault();
            List<HDB_Payment_Confirmation> checkpaid = new List<HDB_Payment_Confirmation>();
            if (middlecheck == null)
            {
                checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            }
            else
            {
                checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
            }

            if (checkpaid.Count() == 0)
            {
                TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
                return RedirectToAction("error", "Reservation");
            }
            #endregion

            var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();

            if (_Subsidy == null)
            {
                return RedirectToAction("Applications", "Reservation");
            }


            if (_Subsidy.StatusId == 2)
            {
                TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم الانطباق";
                return RedirectToAction("error", "Reservation");
            }
            ViewBag.ID = _Subsidy.ID;

            var hasException = userbll.exceptionSSnHasApp(SSN);
            if (!hasException)
            {
                #region check before has units

                var previousget = entity.SSNPreviousTakens.Where(a => a.SSN == SSN || a.SSN == _Subsidy.SPOUSE_SSN).FirstOrDefault();
                if (previousget != null)
                {
                    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً  لسبق الاستفادة  : " + previousget.Message;// "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";

                    return RedirectToAction("error", "Reservation");
                }
            }
            #endregion

            DateTime NowToday = DateTime.Now;
            //reserve before all
            var ssnCanReserv = entity.EXCEPTION_SSN_RESERV.Where(a => a.SSN == SSN).FirstOrDefault();
            ViewBag.ssn = SSN;
            if (ssnCanReserv != null)
            {
                if (ssnCanReserv.RESERVATION_START_DATE <= DateTime.Now && ssnCanReserv.RESERVATION_END_DATE >= DateTime.Now)
                {
                    ViewBag.canres = 1;
                    ViewBag.reservStartDate = ssnCanReserv.RESERVATION_START_DATE;
                    ViewBag.reservEndDate = ssnCanReserv.RESERVATION_START_DATE;
                }
                else
                {
                    TempData["Error"] = "لم يتم بدء الحجز";
                    return RedirectToAction("applications", "Reservation");
                }
            }
            else
            {
                var settings = entity.SETTINGS.FirstOrDefault();
                if (settings != null)
                {
                    if (settings.OPENRESERV == false)
                    {
                        TempData["Error"] = "لم يتم بدء الحجز";
                        return RedirectToAction("applications", "Reservation");
                    }
                }
            }


            #region check money for adv
            var ClientAdv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
            if (ClientAdv != null)
            {
                //if (DateTime.Now < ClientAdv.RESERVATIONSTARTDATE)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }

            //if (checkpaid.Sum(a => a.DOWNPAYMENT_AMOUNT_COLLECTED_MFF) < (double)ClientAdv.DOWNPAYMENT_AMOUNT)
            //{
            //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
            //    return View(_Subsidy);
            //}
            #endregion
            ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
            ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
            ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
            ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);
            ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
            if (_Subsidy.ADVFLAG != null)
            {
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();
                ViewBag.MyMainADv = Myadv.PORTALADVID;
                ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR", Myadv.PORTALADVID);
            }
            else
            {
                ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR");
            }

            //ViewBag.PROJECTID = new SelectList(entity.v_PROJECTS.Where(a => a.ADVERTISEMENT_ID == _Subsidy.ADVID), "ID", "NAME_AR");

            var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();

            if (su != null)
            {
                _Subsidy.UNITCODE = su.UNITCODE;
                ViewBag.Isreserved = 1;
                ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
            }

            return View(_Subsidy);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerActionFilter]
        public ActionResult ReservUnitNew(string UnitCode)
        {
            try
            {
                string SSN = Helper.Decode(Session["SSN"].ToString());
                var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();
                ViewBag.ID = _Subsidy.ID;
                ViewBag.PrimaryInvestorName = _Subsidy.PRIMARY_INVESTOR_FULL_NAME;
                ViewBag.PrimaryPhone = _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER;
                ViewBag.PrimaryInvestorSSN = _Subsidy.PRIMARY_INVESTOR_SSN;
                ViewBag.Gander = Convert.ToInt32(_Subsidy.PRIMARY_INVESTOR_GENDER);
                ViewBag.BDate = _Subsidy.PRIMARY_INVESTOR_BIRTH_DATE;
                var Myadv = entity.NEWADVERTISMENTS.Where(a => a.ID == _Subsidy.ADVFLAG && a.ISCLOSED != true).FirstOrDefault();


                if (_Subsidy.ADVFLAG != null)
                {
                    ViewBag.MyMainADv = Myadv.PORTALADVID;
                    ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR", Myadv.PORTALADVID);
                }
                else
                {
                    ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true), "PORTALADVID", "ADVNAME_AR");
                }

                if (string.IsNullOrWhiteSpace(UnitCode))
                {
                    TempData["Error"] = "يرجي ادخال كافة البيانات المطلوبة";
                    return View(_Subsidy);
                }

                //  ViewBag.ADVID = new SelectList(entity.NEWADVERTISMENTS, "PORTALADVID", "ADVNAME_AR", AdvID);
                var _Unconditional_Unit = entity.v_Unconditional_Unit.Where(a => a.UNIT_CODE == UnitCode && a.IS_CONTRACTED == 1).FirstOrDefault();
                if (_Unconditional_Unit != null)
                {
                    // var servedBefore = entity.subsidy_request.Where(a => a.UNITCODE == UnitCode).FirstOrDefault();
                    if (_Unconditional_Unit.PRIMARY_INVESTOR_SSN == SSN)
                    {
                        //check if has unit before
                        TempData["Success"] = "الوحدة تم حجزها من قبلك";
                        var su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();
                        if (su != null)
                        {
                            _Subsidy.UNITCODE = su.UNITCODE;
                            ViewBag.Isreserved = 1;
                            ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
                        }
                        return View(_Subsidy);
                    }
                    else if (_Unconditional_Unit.PRIMARY_INVESTOR_SSN != null)
                    {
                        TempData["Error"] = "الوحدة تم حجزها من قبل عميل اخر";
                        return View(_Subsidy);
                        // return Json(new { status = 0, errormesssage = "الوحدة محجوزة  ", data = new { } });
                    }
                    else
                    {

                        #region check money first which paid
                        ////check money first which paid
                        var Advs = entity.NEWADVERTISMENTS.Where(a => a.PORTALADVID == _Unconditional_Unit.ADVERTISEMENT_ID && a.ISCLOSED != true).FirstOrDefault();
                        // var ClientAmountrow = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN).FirstOrDefault();
                        var middlecheck = entity.CheckMiddelIncomes.Where(a => a.WithdrawlSSN == SSN && a.ADVERTISEMENT_CODE == 5000021).FirstOrDefault();
                        //var checkpaid = entity.TotalPaymentForClients.Where(a => a.CLIENT_SSN == SSN&&a.ADVERTISEMENT_CODE==5000023).FirstOrDefault();
                        List<HDB_Payment_Confirmation> checkpaid = new List<HDB_Payment_Confirmation>();
                        if (middlecheck == null)
                        {
                            checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
                        }
                        else
                        {
                            checkpaid = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).ToList();
                        }

                        if (checkpaid.Count() == 0)
                        {
                            TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
                            return View(_Subsidy);
                        }
                        #endregion

                        //if (ClientAmountrow == null)
                        //{
                        //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم سداد مقدم جدية الحجز";
                        //    return View();
                        //    //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز لاستكمال الإجراءات", data = new { } });
                        //}

                        //if (ClientAmountrow.TotalDOwnPAyment < (double)Advs.DOWNPAYMENT_AMOUNT)
                        //{
                        //    TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
                        //    return View();
                        //    //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز كاملة لاستكمال الإجراءات", data = new { } });
                        //}

                        #region check money paid before

                        var ClientNewAdv = entity.NEWADVERTISMENTS.Where(a => a.PORTALADVID == _Unconditional_Unit.ADVERTISEMENT_ID && a.ISCLOSED != true).FirstOrDefault();
                        if (checkpaid.Sum(a => a.DOWNPAYMENT_AMOUNT_COLLECTED_MFF) < (double)ClientNewAdv.DOWNPAYMENT_AMOUNT)
                        {
                            TempData["Error"] = "عفواً لا يُمكنكم استكمال حجز الوحدة السكنية نظراً لعدم كفاية مقدم جدية الحجز";
                            return View(_Subsidy);
                            //return Json(new { status = 0, errormesssage = "يرجي سداد جدية الحجز كاملة لاستكمال الإجراءات", data = new { } });
                        }

                        #endregion


                        #region check adv not same adv 


                        #endregion

                        TempUnitReserverf tm = new TempUnitReserverf();
                        tm.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        tm.SSN = SSN;
                        tm.RequestedUnitID = _Unconditional_Unit.ID;
                        tm.PROJECTID = _Unconditional_Unit.PROJECT_ID;
                        tm.Reservedate = DateTime.Now;
                        tm.IstempOrReserved = 3;
                        entity.TempUnitReserverfs.Add(tm);
                        //  entity.SaveChanges();


                        var _subsidy_request = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.ID == _Subsidy.ID).FirstOrDefault();
                        //_subsidy_request.UNITCODE = UnitCode;
                        _subsidy_request.PROJECTID = _Unconditional_Unit.PROJECT_ID;
                        //_subsidy_request.ADVID = Advs.PORTALADVID;
                        //_subsidy_request.RequestedUnitID = _Unconditional_Unit.ID;
                        _subsidy_request.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        UBSIDY_REQUESTED_UNIT su = new UBSIDY_REQUESTED_UNIT();
                        su.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                        su.UNIT_ID = _Unconditional_Unit.ID;
                        su.ADVERTISMENT_ID = Advs.PORTALADVID;
                        su.PROJECT_ID = _Unconditional_Unit.PROJECT_ID;
                        su.SSN = SSN;
                        su.DATE_ADDED = DateTime.Now;
                        su.SUBSIDY_REQUEST_ID = _Subsidy.ID;
                        entity.UBSIDY_REQUESTED_UNIT.Add(su);

                        int result = entity.SaveChanges();
                        if (result >= 1)
                        {
                            TempData["Success"] = "تم الاختيار بنجاح";
                            su = entity.UBSIDY_REQUESTED_UNIT.Where(a => a.SSN == SSN).FirstOrDefault();
                            if (su != null)
                            {
                                _Subsidy.UNITCODE = su.UNITCODE;
                                ViewBag.Isreserved = 1;
                                ViewBag.UnitDetails = entity.v_Unconditional_Unit.Where(a => a.ID == su.UNIT_ID).FirstOrDefault();
                            }
                            SMSHelper sMS = new SMSHelper();
                            sMS.sendMessage("تم حجز الوحدة السكنية بكود رقم  " + _Unconditional_Unit.UNIT_CODE + " بنجاح", _Subsidy.PRIMARY_INVESTOR_CELL_NUMBER);
                            return View(_Subsidy);
                        }
                        // return Json(new { status = 1, errormesssage = "تم الاختيار بنجاح", data = _Unconditional_Unit });
                        else
                        {
                            TempData["Error"] = "لم يتم الاختيار للوحدة يرجي التجربة مره اخري وادخال كافة البيانات المطلوبة بشكل صحيح";
                            return View(_Subsidy);
                        }
                    }

                    //if (servedBefore > 0)
                    //{
                    //    return Json(new { status = 1, errormesssage = "تم ", data = _Unconditional_Unit });
                    //}
                    //try
                    //{
                    //    //long subsidyrequestid = userbll.GetSubsidyRequestID(SSN);
                    //    _Unconditional_Unit.RESERVED = 1;
                    //    TempUnitReserverf tempunit = new TempUnitReserverf();
                    //    tempunit.RequestedUnitID = _Unconditional_Unit.ID;
                    //    tempunit.IstempOrReserved = 1;
                    //    tempunit.SSN = Helper.Decode(Session["SSN"].ToString());
                    //    tempunit.SUbsidrRequestID = 0;
                    //    tempunit.UNITCODE = _Unconditional_Unit.UNIT_CODE;
                    //    tempunit.ReservedDate = DateTime.Now;
                    //    tempunit.PROJECTID = _Unconditional_Unit.PROJECT_ID;

                    //    entity.TempUnitReserverfs.Add(tempunit);
                    //    entity.SaveChanges();
                    //    return Json(new { status = 1, errormesssage = "تم ", data = _Unconditional_Unit });
                    //}
                    //catch (Exception ex)
                    //{
                    //    return Json(new { status = 0, errormesssage = "الوحدة محجوزه من قبل عميل اخر ", data = new { } });
                    //}
                }
                else
                {
                    TempData["Error"] = "الوحدة تم حجزها من قبل عميل اخر";
                    return View(_Subsidy);
                    //return Json(new { status = 0, errormesssage = "الوحدة غير موجوده او تخص مشروع اخر", data = new { } });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "الوحدة تم حجزها من قبل عميل اخر";
                return View();
                // return Json(new { status = 0, errormesssage = "الوحدة محجوزة  ", data = new { } });
            }

            return View();
        }

        public ActionResult Wishes()
        {
            return RedirectToAction("index");
            string SSN = Helper.Decode(Session["SSN"].ToString());

            ViewBag.unitWhishes = entity.SubsidyWhishes.Where(a => a.SSN == SSN).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wishes(string Unit_Code)
        {
            string SSN = Helper.Decode(Session["SSN"].ToString());

            var existCode = entity.Subsidy_Unit_Wish.Where(a => a.SSN == SSN && a.Unit_Code == Unit_Code).FirstOrDefault();

            if (existCode != null)
            {
                TempData["Error"] = "الوحدة مضافة من قبل";
            }
            else
            {
                var unitWish = entity.v_Unconditional_Unit.Where(a => a.UNIT_CODE == Unit_Code && a.IS_CONTRACTED == 1).FirstOrDefault();
                if (unitWish != null)
                {
                    Subsidy_Unit_Wish subsidy_Unit_Wish = new Subsidy_Unit_Wish();
                    subsidy_Unit_Wish.Unit_Code = Unit_Code;
                    subsidy_Unit_Wish.SSN = SSN;
                    subsidy_Unit_Wish.Unit_ID = unitWish.ID;
                    subsidy_Unit_Wish.Create_Date = DateTime.Now;
                    entity.Subsidy_Unit_Wish.Add(subsidy_Unit_Wish);
                    entity.SaveChanges();
                    TempData["Success"] = "تم اضافة الاختيار الي الرغبات بنجاح";
                }
                else
                {
                    TempData["Error"] = "الوحدة غير موجوده او غير متاحه حاليا";
                }
            }

            ViewBag.unitWhishes = entity.SubsidyWhishes.Where(a => a.SSN == SSN).ToList();
            return View();
        }


        private bool checkAppexist(string SSN, long id = 0)
        {
            if (id == 0)
            {
                return entity.subsidy_request.Any(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1);
            }
            else
            {
                return entity.subsidy_request.Any(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1 && a.ID != id);
            }
            //return entity.subsidy_request.Any(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1);
            //return entity.subsidy_request.Any(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1);
        }

        protected override void Dispose(bool disposing)
        {
            entity.Dispose();
            base.Dispose(disposing);
        }
    }
}
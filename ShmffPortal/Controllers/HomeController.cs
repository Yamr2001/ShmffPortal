using ShmffPortal.Filters;
using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;
using ShmffPortal.BLL;
using System.Data.SqlClient;
using System.Data;
using ShmffPortal.Helpers;

namespace ShmffPortal.Controllers
{
    //  [LoggerActionFilter]
    [RegFilter]
    public class HomeController : Controller
    {
        private NewPortalDBEntities entityDb;
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

         
        public ActionResult Index()
        {

            if (userBll.checkAllCanreg() == "false")
            {
                string UserName = User.Identity.Name;
                bool canregOrLog = userBll.CanRegOrLoging(UserName);
                if (!canregOrLog)
                {
                    return RedirectToAction("login", "account");
                }
            }

            ViewBag.Advs = entity.NEWADVERTISMENTS.Where(a => a.ISCLOSED != true).ToList();
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (userBll.checkAllCanreg() == "false")
            {
                string UserName = User.Identity.Name;
                bool canregOrLog = userBll.CanRegOrLoging(UserName);
                if (!canregOrLog)
                {
                    return RedirectToAction("login", "account");
                }
            }

            if (id == null)
            {
                return RedirectToAction("index");
            }

            var adv = entity.NEWADVERTISMENTS.Where(a => a.ID == id && a.ISCLOSED != true).FirstOrDefault();
            if (adv == null)
            {
                return RedirectToAction("index");
            }
            return View(adv);
        }
        public ActionResult CalculateLoan()
        {
            if (userBll.checkAllCanCalc() == "false")
            {
                string UserName = User.Identity.Name;
                bool canregOrLog = userBll.CanRegOrLoging(UserName);
                if (!canregOrLog)
                {
                    return RedirectToAction("index", "home");
                }
            }
            if (userBll.checkAllCanreg() == "false")
            {
                string UserName = User.Identity.Name;
                bool canregOrLog = userBll.CanRegOrLoging(UserName);
                if (!canregOrLog)
                {
                    return RedirectToAction("login", "account");
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult CalculateLoan(CalculateLoan calculateLoan)
        {
            try
            {

                double interestrate = calculateLoan.InterestRate / 100d;

                int NoOfMonth = (calculateLoan.Year * 12);
                double valLoan = calculateLoan.Loan * ((interestrate) * (365 / 12)) / NoOfMonth;

                //Application app = new Application();
                //WorksheetFunction functions = app.WorksheetFunction;
                //double valInterset = -functions.CumPrinc((interestrate * (365 / 12)) / NoOfMonth, NoOfMonth, calculateLoan.Loan, 1, 1, 0);
                //double Installment = valLoan + valInterset;

                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionSMS"].ToString()))
                using (SqlCommand cmd = new SqlCommand("p_check_loan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_1", interestrate);
                    cmd.Parameters.AddWithValue("@p_2", calculateLoan.InstallmentPrice);
                    cmd.Parameters.AddWithValue("@p_3", 0);
                    cmd.Parameters.AddWithValue("@p_4", 0);
                    cmd.Parameters.AddWithValue("@p_5", calculateLoan.Year * 12);
                    cmd.Parameters.AddWithValue("@gid", "1");
                    var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    var result = returnParameter.Value;
                    calculateLoan.Loan = Math.Round(Convert.ToDouble(result), 2);
                    calculateLoan.DownPayment = calculateLoan.UnitPrice - calculateLoan.Loan;//   Math.Round(Installment, 2);
                }
            }
            catch (Exception ex)
            {
                LogExceptions logEx = new LogExceptions();
                logEx.LogError(ex);
            }
            //double valInterset = -functions.CumPrinc(((interestrate) * (365 / 12)) / 360, 360, 630000, 1, 1, 0);
            return View(calculateLoan);
        }
        //public ActionResult Download(int? id)
        //{
        //    try
        //    {
        //         string FilePath = "";
        //        if (id == null)
        //        {
        //            return RedirectToAction("index");
        //        }
        //        var Adv = entity.NEWADVERTISMENTS.Where(a => a.ID == id&& a.ISCLOSED != true).FirstOrDefault();
        //        if (Adv != null)
        //        {
        //            string fileName = Adv.BOURCHOUR;
        //            FilePath = Server.MapPath("~/App_Data/" + fileName);
        //            return File(FilePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //        }
        //        else
        //        {
        //            return RedirectToAction("index");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("index");
        //    }
        //}

        public ActionResult error()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            entity.Dispose();
            base.Dispose(disposing);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
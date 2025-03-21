using ShmffPortal.BLL;
using ShmffPortal.Helpers;
using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ShmffPortal.Controllers
{
    public class AdminCheckController : Controller
    {
        //NewPortalDBEntities entity = new NewPortalDBEntities();
        //USERSBll userbll = new USERSBll();

        //// GET: AdminCheck
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult Check()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult check(string SSN)
        //{
        //    var _Subsidy = entity.subsidy_request.Where(a => a.PRIMARY_INVESTOR_SSN == SSN && a.IS_CANCELED != 1).FirstOrDefault();
        //    if (_Subsidy != null)
        //    {
        //        ViewBag.msg = "العميل لديه طلب بالفعل";
        //    }
        //    else
        //    {
        //        var CheckHasUnit = userbll.checkCustomerHasUnits(SSN);
        //        //var paidcheck = entity.HDB_Payment_Confirmation.Where(a => a.CLIENT_SSN == SSN).FirstOrDefault();
        //        //if (paidcheck != null)
        //        //{
        //        //as if old ALLOCATION_DEALLOCATION==0
        //        if (CheckHasUnit != null)
        //        {
        //            if (CheckHasUnit.ALLOCATION_DEALLOCATION == 1)
        //            {
        //                ViewBag.HasUnitBefore = 1;
        //                ViewBag.CheckHasUnit = CheckHasUnit;
        //                //  userbll.saveFiledRegistration(SSN, "", 0, 1, 0, 0, "");

        //            }
        //        }
        //        else
        //        {
        //            bool exceptionssn = userbll.exceptionSSn(SSN);
        //            if (exceptionssn == false)
        //            {
        //                var Dimsub = entity.V_Dim_subsidy.Where(a => a.PRIMARY_INVESTOR_SSN == SSN || a.SPOUSE_SSN == SSN).FirstOrDefault();
        //                if (Dimsub != null)
        //                {
        //                    var transactiontransformation = entity.V_Transaction_Transformation.Where(a => a.IS_CANCELLED == 0 && (a.SSN == SSN || a.SSN == Dimsub.SPOUSE_SSN || a.SSN == Dimsub.PRIMARY_INVESTOR_SSN)).FirstOrDefault();
        //                    if (transactiontransformation != null)
        //                    {
        //                        var withdraw = entity.v_Egypt_Post_Withdrawl.Any(a => a.CLIENT_SSN == SSN);
        //                        if (!withdraw)
        //                        {
        //                            ViewBag.NoCancelled = 1;
        //                            ViewBag.Dimsub = Dimsub;
        //                            // userbll.saveFiledRegistration(SSN, "", 0, 0, 1, 0, "");
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.Noprom = "لا توجد مشكله";
        //            }
        //        }
        //        // ViewBag.Noprom = "لا توجد مشكله";


        //        //}
        //        //else
        //        //{
        //        //    ViewBag.Nopaid = 0;
        //        //}
        //    }
        //    return View();
        //}


        //public ActionResult SendSMS()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult sendsms(string SSN)
        //{
        //    var regSSN = entity.RegisterSSNs.Where(a => a.SSN == SSN).FirstOrDefault();
        //    if (regSSN != null)
        //    {
        //        if (regSSN.VerificationCode == null)
        //        {
        //            ViewBag.msg = " تم تفعيل الحساب بالفعل";

        //        }
        //        else { 
        //        SMSHelper sms = new SMSHelper();
        //        sms.sendMessageWithDLR("عزيزي العميل رمز التأكيد لحسابكم هو " + regSSN.VerificationCode, regSSN.Mobile);
        //            ViewBag.msg = " تم ارسال الكود للعميل";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.msg = "العميل غير مسجل";
        //    }
        //    return View();
        //}

        ////    public ActionResult Openreserv()
        ////    {
        ////        var row = entity.SETTINGS.FirstOrDefault();
        ////        if (row != null)
        ////        {
        ////            ViewBag.settings = row;
        ////        }
        ////        return View();
        ////    }

        ////    [HttpPost]
        ////    public ActionResult Openreserv(int? id)
        ////    {

        ////        var row = entity.SETTINGS.FirstOrDefault();
        ////        if (row != null)
        ////        {
        ////            row.OPENRESERV = true;
        ////            row.STARTDATE = DateTime.Now;
        ////        }
        ////        else
        ////        {
        ////            SETTING seting = new SETTING();
        ////            seting.OPENRESERV = true;
        ////            seting.STARTDATE = DateTime.Now;
        ////            entity.SETTINGS.Add(seting);
        ////        }

        ////        entity.SaveChanges();
        ////        TempData["success"] = "تم فتح التسجيل";
        ////        var row2 = entity.SETTINGS.FirstOrDefault();
        ////        if (row2 != null)
        ////        {
        ////            ViewBag.settings = row2;
        ////        }

        ////        return View();
        ////    }



        ////    public ActionResult AllUnits()
        ////    {

        ////        return View(entity.v_Unconditional_Unit.ToList());
        ////    }
        ////}
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShmffPortal.Models;

namespace ShmffPortal.Controllers
{
    public class HDB_Payment_ConfirmationController : Controller
    {
        private NewPortalDBEntities db = new NewPortalDBEntities();

        // GET: HDB_Payment_Confirmation
        public ActionResult Index()
        {
            return View(db.HDB_Payment_Confirmation.ToList());
        }

        // GET: HDB_Payment_Confirmation/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HDB_Payment_Confirmation hDB_Payment_Confirmation = db.HDB_Payment_Confirmation.Find(id);
            if (hDB_Payment_Confirmation == null)
            {
                return HttpNotFound();
            }
            return View(hDB_Payment_Confirmation);
        }

        // GET: HDB_Payment_Confirmation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HDB_Payment_Confirmation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HDB_Payment_Confirmation hDB_Payment_Confirmation)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                hDB_Payment_Confirmation.PAYMENT_TYPE = 0;
                hDB_Payment_Confirmation.MOBILE_NUMBER = "01066566336";
                hDB_Payment_Confirmation.APPLICANT_SSN = hDB_Payment_Confirmation.CLIENT_SSN;
                hDB_Payment_Confirmation.BROCHURE_FEES_AMOUNT_COLLECTED_HDB = 0;
                hDB_Payment_Confirmation.ADMINISTRATIVE_EXPENSES_AMOUNT_COLLECTED_MFF = 0;
                hDB_Payment_Confirmation.DOWNPAYMENT_FEES_AMOUNT_COLLECTED_HDB = 0;
                hDB_Payment_Confirmation.CREATION_DATE = DateTime.Now;
                hDB_Payment_Confirmation.PAYMENT_DATE = DateTime.Now;
                hDB_Payment_Confirmation.PAYMENT_NUMBER = random.Next(11111, 999999).ToString();
                hDB_Payment_Confirmation.APPLICANT_NAME_AR = hDB_Payment_Confirmation.CLIENT_NAME_AR;
                db.HDB_Payment_Confirmation.Add(hDB_Payment_Confirmation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hDB_Payment_Confirmation);
        }

        // GET: HDB_Payment_Confirmation/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HDB_Payment_Confirmation hDB_Payment_Confirmation = db.HDB_Payment_Confirmation.Find(id);
            if (hDB_Payment_Confirmation == null)
            {
                return HttpNotFound();
            }
            return View(hDB_Payment_Confirmation);
        }

        // POST: HDB_Payment_Confirmation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CLIENT_SSN,CLIENT_NAME_AR,ADVERTISEMENT_CODE,ADVERTISEMENT_NAME,BROCHURE_TYPE,ADMINISTRATIVE_EXPENSES_AMOUNT_COLLECTED_MFF,DOWNPAYMENT_AMOUNT_COLLECTED_MFF,BROCHURE_FEES_AMOUNT_COLLECTED_HDB,DOWNPAYMENT_FEES_AMOUNT_COLLECTED_HDB,PAYMENT_NUMBER,PAYMENT_DATE,HDB_FINANCIAL_CODE,HDB_BRANCH_NAME,CREATION_DATE,APPLICANT_SSN,APPLICANT_NAME_AR,BROCHURE_FEES_AMOUNT_COLLECTED_MFF,IS_CANCELLED,MOBILE_NUMBER,PAYMENT_TYPE")] HDB_Payment_Confirmation hDB_Payment_Confirmation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hDB_Payment_Confirmation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hDB_Payment_Confirmation);
        }

        // GET: HDB_Payment_Confirmation/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HDB_Payment_Confirmation hDB_Payment_Confirmation = db.HDB_Payment_Confirmation.Find(id);
            if (hDB_Payment_Confirmation == null)
            {
                return HttpNotFound();
            }
            return View(hDB_Payment_Confirmation);
        }

        // POST: HDB_Payment_Confirmation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HDB_Payment_Confirmation hDB_Payment_Confirmation = db.HDB_Payment_Confirmation.Find(id);
            db.HDB_Payment_Confirmation.Remove(hDB_Payment_Confirmation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult TransferData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferData(string CLIENT_SSN,string UNIT_CODE)
        {
            using (var db = new NewPortalDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("EXEC TrnsferSSNS @SSN",
                           new SqlParameter("SSN", CLIENT_SSN));

                        // Execute the first stored procedure
                        db.Database.ExecuteSqlCommand("EXEC TrnsferPayments @SSN",
                            new SqlParameter("SSN", CLIENT_SSN) );

                        db.Database.ExecuteSqlCommand("EXEC TrnsferUnit @unitcode",
                            new SqlParameter("unitcode", UNIT_CODE));
                        // Execute the second stored procedure

                        db.Database.ExecuteSqlCommand("EXEC TrnsferUbsidyReserved @SSN",
                             new SqlParameter("SSN", CLIENT_SSN));

                        // Execute additional procedures as needed
                        db.Database.ExecuteSqlCommand("EXEC TrnsferSynced  @SSN",
                             new SqlParameter("SSN", CLIENT_SSN));

                        db.Database.ExecuteSqlCommand("EXEC TrnsferReservedUnit  @SSN",
                         new SqlParameter("SSN", CLIENT_SSN));
                        // Commit the transaction if all succeed
                        transaction.Commit();
                        ViewBag.success = "تم النقل";
                    }
                    catch (Exception ex)
                    {
                        // Rollback if any procedure fails
                        transaction.Rollback();
                        // Log or handle the error as needed
                        ViewBag.error = "لم يتم النقل يوجد بيانات خطأ "+ex.Message;
                    }
                }
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

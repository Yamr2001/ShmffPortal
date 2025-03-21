using ShmffPortal.Infrastuture;
using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShmffPortal.BLL
{
    public class USERSBll
    {
        private NewPortalDBEntities entityDb;


        public NewPortalDBEntities entity
        {
            get
            {
                if (entityDb==null)
                {
                    entityDb = new NewPortalDBEntities();
                }
                return entityDb;
            }
        }

        public bool ChecUServerifired()
        {
            bool result = false;
            int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            string token = HttpContext.Current.Session["Token"].ToString();
            var verifiedcheck = entity.RegisterSSNs.Where(c => c.ID == userid && c.Token == token).FirstOrDefault();

            if (verifiedcheck != null)
            {
                result = verifiedcheck.Verified ?? false;
            }
            return result;
        }

        public GETCustomersUnits_Result checkCustomerHasUnits(string SSN)
        {
            // bool result = false;
            //   string SSN = Helper.Decode(HttpContext.Current.Session["SSN"].ToString());
            //var result = Dbc.CustomersUnits.Where(a => a.PRIMARY_INVESTOR_SSN == SSN || a.SPOUSE_SSN == SSN).OrderByDescending(a => a.TRANSACTION_TIMESTAMP).FirstOrDefault();
            var result = entity.GETCustomersUnits(SSN).OrderByDescending(a => a.TRANSACTION_TIMESTAMP).FirstOrDefault();
            return result;
        }

        public bool IsBewteenTwoDates(DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }
        public void saveFiledRegistration(string SSN, string SpouseSSN, int SSNORSPOUSE, int HAVEBEFORE, int INADVONLY, int Status, string result)
        {

            var registerdbefore = entity.SSNTRIESNOACCEPTEs.Where(a => a.SSN == SSN || a.SPOUSE_SSN == SpouseSSN).FirstOrDefault();
            if (registerdbefore != null)
            {
                registerdbefore.SPOUSE_SSN = SpouseSSN;
                registerdbefore.SSN = SSN;
                registerdbefore.INADVONLY = INADVONLY;
                registerdbefore.HAVEBEFORE = HAVEBEFORE;
                registerdbefore.SSNORSPOUSE = SSNORSPOUSE;
                registerdbefore.STATUSCODE = Status;
                registerdbefore.RESULT = result;

                entity.SaveChanges();
            }
            else
            {
                SSNTRIESNOACCEPTE SSNbefore = new SSNTRIESNOACCEPTE();
                SSNbefore.SPOUSE_SSN = SpouseSSN;
                SSNbefore.SSN = SSN;
                SSNbefore.INADVONLY = INADVONLY;
                SSNbefore.HAVEBEFORE = HAVEBEFORE;
                SSNbefore.SSNORSPOUSE = SSNORSPOUSE;
                SSNbefore.STATUSCODE = Status;
                SSNbefore.RESULT = result;
                entity.SSNTRIESNOACCEPTEs.Add(SSNbefore);
                entity.SaveChanges();
            }
        }

        public bool exceptionSSn(string SSN)
        {
            var result = entity.EXCEPTIONSSNs.Any(a => a.SSN == SSN &&a.F_DELETED!=true);
            return result;
        }


        public bool exceptionSSnHasApp(string SSN)
        {
            var result = entity.EXCEPTION_HAVE_APP.Any(a => a.SSN == SSN && a.F_DELETED != true);
            return result;
        }
        

        public string checkAllCanreg()
        {
            return OurConfiguration.allReg;
        }
        public string checkAllCanCalc()
        {
            return OurConfiguration.CanCalc;
        }

        public bool CanRegOrLoging(string ssn)
        {
            return entity.SSN_CAN_REG.Any(a=>a.SSN==ssn&&a.F_Deleted!=true);
        }



        public long GetSubsidyRequestID(string ssn)
        {
            long SubsidyRequestID = 0;
            SubsidyRequestID = entity.Get_Subsidy_RequestID(ssn).FirstOrDefault() ?? 0;

            return SubsidyRequestID;
        }

    }
}
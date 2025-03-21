using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
namespace ShmffPortal.Infrastuture
{
    public class OurConfiguration
    {
        public static string allReg
        {
            get { 
                return WebConfigurationManager.AppSettings["allReg"];
            }
        }

        public static string CanCalc
        {
            get
            {
                return WebConfigurationManager.AppSettings["CalcLoan"];
            }
        }

        public static string UnreservUnit
        {
            get
            {
                return WebConfigurationManager.AppSettings["unreservunit"];
            }
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShmffPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HDB_Payment_Confirmation
    {
        public long ID { get; set; }
        public string CLIENT_SSN { get; set; }
        public string CLIENT_NAME_AR { get; set; }
        public Nullable<long> ADVERTISEMENT_CODE { get; set; }
        public string ADVERTISEMENT_NAME { get; set; }
        public string BROCHURE_TYPE { get; set; }
        public Nullable<double> ADMINISTRATIVE_EXPENSES_AMOUNT_COLLECTED_MFF { get; set; }
        public Nullable<double> DOWNPAYMENT_AMOUNT_COLLECTED_MFF { get; set; }
        public Nullable<double> BROCHURE_FEES_AMOUNT_COLLECTED_HDB { get; set; }
        public Nullable<double> DOWNPAYMENT_FEES_AMOUNT_COLLECTED_HDB { get; set; }
        public string PAYMENT_NUMBER { get; set; }
        public Nullable<System.DateTime> PAYMENT_DATE { get; set; }
        public string HDB_FINANCIAL_CODE { get; set; }
        public string HDB_BRANCH_NAME { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string APPLICANT_SSN { get; set; }
        public string APPLICANT_NAME_AR { get; set; }
        public Nullable<double> BROCHURE_FEES_AMOUNT_COLLECTED_MFF { get; set; }
        public Nullable<short> IS_CANCELLED { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public Nullable<int> PAYMENT_TYPE { get; set; }
    }
}

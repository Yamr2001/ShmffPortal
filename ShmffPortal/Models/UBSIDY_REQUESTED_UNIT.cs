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
    
    public partial class UBSIDY_REQUESTED_UNIT
    {
        public int ID { get; set; }
        public string UNITCODE { get; set; }
        public Nullable<long> SUBSIDY_REQUEST_ID { get; set; }
        public Nullable<long> PROJECT_ID { get; set; }
        public Nullable<long> ADVERTISMENT_ID { get; set; }
        public string SSN { get; set; }
        public Nullable<int> UNIT_ID { get; set; }
        public Nullable<System.DateTime> DATE_ADDED { get; set; }
    }
}

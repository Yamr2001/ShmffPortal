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
    
    public partial class CitiesLocation
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public string SiteLocaion { get; set; }
        public string MapUrl { get; set; }
        public Nullable<int> Adv_ID { get; set; }
    
        public virtual NEWADVERTISMENT NEWADVERTISMENT { get; set; }
    }
}

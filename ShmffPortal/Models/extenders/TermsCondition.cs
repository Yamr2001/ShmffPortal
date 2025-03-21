using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
    public class TermsCondition
    {
        public int ADVID { get; set; }
      //  public bool Verified { get; set; }
        [Display(Name = " لقد قرأت وقبلت الشروط والاحكام")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "يجب الموافقة علي الشروط والاحكام")]
        public bool accepted { get; set; }
    }
}
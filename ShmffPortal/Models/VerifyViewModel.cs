using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
    public class VerifyViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        public string Verify_Code { get; set; }
    }
}
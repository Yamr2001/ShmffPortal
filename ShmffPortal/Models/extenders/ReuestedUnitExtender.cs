using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
    public class ReuestedUnitVM
    {
        [Required(ErrorMessage = "مطلوب")]
        [Range(0, int.MaxValue)]
        public int ADVID { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        [Range(0, int.MaxValue)]
        public int PROJECTID { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public string UNITCODE { get; set; }

        public string OTHER_ATTACHMENT_NAME { get; set; }
    }
}
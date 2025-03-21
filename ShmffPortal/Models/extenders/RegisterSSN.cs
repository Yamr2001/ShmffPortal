using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
    [MetadataType(typeof(RegisterSSNMetadata))]

    public partial class RegisterSSN
    {

    }


    public class RegisterSSNMetadata
    {
        //[RegularExpression(@"^(\d{11})$", ErrorMessage = "ادخل رقم صحيح")]
        [Required(ErrorMessage = "مطلوب")]
        public string Mobile { get; set; }
    }

}
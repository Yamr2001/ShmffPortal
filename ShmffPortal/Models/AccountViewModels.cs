using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShmffPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        public string SSN { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public string password { get; set; }

        public string GoogleCaptchToken { get; set; }

    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669 ]+$", ErrorMessage = "يجب إدخال الاسم بالغة العربية فقط")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public string SSN { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "كلمة المرو يجب ان تكون 6 علي الاقل و 15 علي الأكثر")]
        public string password { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        [Compare("password", ErrorMessage = "كلمة المرور غير متطابقة مع تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "مطلوب")]
       // [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$",ErrorMessage ="ادخل رقم صحيح")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "ادخل رقم صحيح")]

        public string Phone { get; set; }
        //[Required(ErrorMessage = "مطلوب")]
        public int Gander { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public int status { get; set; }
       
        public byte[] SALT { get; set; }
        public string VerificationCode { get; set; }
        //public bool Verified { get; set; }
        //[Display(Name = " لقد قرأت وقبلت الشروط والاحكام")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "يجب الموافقة علي الشروط والاحكام")]
        //public bool TermsAndConditions { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        [Display(Name = "الرقم القومي لصاحب الطلب")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "برجاء ادخال 14 رقم الخاص بالرقم القومي")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "مطلوب")]
        [Display(Name = "الموبايل لصاحب الطلب")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "برجاءادخال رقم موبايل صحيح")]
        public string Phone { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{

    [MetadataType(typeof(CalculateLoanMetadata))]

    public partial class CalculateLoan
    {
        public double UnitPrice { get; set; }
        public double DownPayment { get; set; }
        public double Loan { get; set; }
        public int Year { get; set; }
        public int MaritalStatus { get; set; }
        public int InterestRate { get; set; }
        public double InstallmentPrice { get; set; }
        public int MaximumFinancingPeriod { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public decimal SpouseSalary { get; set; }
        public decimal SalaryCut { get; set; }

    }


    public class CalculateLoanMetadata
    {
        [Display(Name = "سعر الوحدة")]
        [Required(ErrorMessage = "مطلوب")]
        public double UnitPrice { get; set; }

        [Display(Name = "المقدم")]
        public double DownPayment { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        [Display(Name = "الحالةالاجتماعية")]
        public int MaritalStatus { get; set; }

        [Display(Name = "دخل الزوجة")]
        public decimal SpouseSalary { get; set; }

        [Display(Name = "الحد الأقصي لمدة التمويل")]
        public int MaximumFinancingPeriod { get; set; }
        [Display(Name = "قيمة التمويل")]
        public double Loan { get; set; }

        [Display(Name = "عدد سنوات التمويل")]
        [Required(ErrorMessage = "مطلوب")]
        [Range(0, 30, ErrorMessage = "يجب ادخال رقم بين 0 و 30")]
        public int Year { get; set; }

        [Display(Name = "نسبة الفائدة (تحدد وفقًا لدخل المتقدم وسعر الوحدة السكنية)")]
        [Required(ErrorMessage = "مطلوب")]

        public int InterestRate { get; set; }

        [Display(Name = "القسط الشهري")]
        public double InstallmentPrice { get; set; }
        [Display(Name = "سن المتقدم")]
        [Required(ErrorMessage = "مطلوب")]
        [Range(21, 60, ErrorMessage = "يجب ادخال سن بين 21 و 60")]
        public int Age { get; set; }

        [Display(Name = "دخل المتقدم")]
        [Required(ErrorMessage = "مطلوب")]
        [Range(1000, 50000, ErrorMessage = "يجب ادخال دخل بين 1000 و 50000")]
        public decimal Salary { get; set; }

        [Display(Name = "نسبة الخصم من الدخل %")]
        [Required(ErrorMessage = "مطلوب")]
        [Range(1, 40, ErrorMessage = "يجب ادخال نسبه بين 1 و 40")]
        public decimal SalaryCut { get; set; }




    }

}
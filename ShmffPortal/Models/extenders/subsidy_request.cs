using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
    [MetadataType(typeof(subsidy_requestMetadata))]
    public partial class subsidy_request
    {
        public bool IS_JOINT_OWNERSHIPB
        {
            get { return IS_JOINT_OWNERSHIP == 1; }
            set
            {
                if (value)
                    IS_JOINT_OWNERSHIP = 1;
                else
                    IS_JOINT_OWNERSHIP = 0;
            }
        }
        

        public long? RequestedUnitID { get; set; }
        public int? extraAddress { get; set; }
        public int? Primaryextraincome { get; set; }
        public int? Primarysecextraincome { get; set; }
        public int? secextraAddress { get; set; }

        public int? Spouseextraincome { get; set; }

        public int Adv_Id { get; set; }

        public long Project_Id { get; set; }

        public long Gov_Id { get; set; }
        //public bool SPOUSE_EGYPTIANB
        //{
        //    get { return SPOUSE_EGYPTIAN == 1; }
        //    set
        //    {
        //        if (value)
        //            SPOUSE_EGYPTIAN = 1;
        //        else
        //            SPOUSE_EGYPTIAN = 0;
        //    }
        //}

        public bool PICTURE_SSNB
        {
            get { return PICTURE_SSN == 1; }
            set
            {
                if (value)
                    PICTURE_SSN = 1;
                else
                    PICTURE_SSN = 0;
            }
        }
        //[Range(typeof(bool), "true", "true", ErrorMessage = "{0} مطلوب")]
        public bool PICTURE_INCOME_PROOFB
        {
            get { return PICTURE_INCOME_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_INCOME_PROOF = 1;
                else
                    PICTURE_INCOME_PROOF = 0;
            }
        }
        //[Range(typeof(bool), "true", "true", ErrorMessage = "{0} مطلوب")]
        public bool PICTURE_CURRENT_RESIDENCE_PROOFB
        {
            get { return PICTURE_CURRENT_RESIDENCE_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_CURRENT_RESIDENCE_PROOF = 1;
                else
                    PICTURE_CURRENT_RESIDENCE_PROOF = 0;
            }
        }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "{0} مطلوب")]
        public bool APPLICATION_CHECKBOXB
        {
            get { return APPLICATION_CHECKBOX == 1; }
            set
            {
                if (value)
                    APPLICATION_CHECKBOX = 1;
                else
                    APPLICATION_CHECKBOX = 0;
            }
        }
        //[Range(typeof(bool), "true", "true", ErrorMessage = "{0} مطلوب")]
        public bool ACKNOWLEDGMENT_CHECKBOXB
        {
            get { return ACKNOWLEDGMENT_CHECKBOX == 1; }
            set
            {
                if (value)
                    ACKNOWLEDGMENT_CHECKBOX = 1;
                else
                    ACKNOWLEDGMENT_CHECKBOX = 0;
            }
        }

        public bool PICTURE_SPOUSE_SSNB
        {
            get { return PICTURE_SPOUSE_SSN == 1; }
            set
            {
                if (value)
                    PICTURE_SPOUSE_SSN = 1;
                else
                    PICTURE_SPOUSE_SSN = 0;
            }
        }

        public bool PICTURE_MARRIAGE_DIVISION_PROOFB
        {
            get { return PICTURE_MARRIAGE_DIVISION_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_MARRIAGE_DIVISION_PROOF = 1;
                else
                    PICTURE_MARRIAGE_DIVISION_PROOF = 0;
            }
        }

        public bool PICTURE_HUSBAND_NOT_WORKINGB
        {
            get { return PICTURE_HUSBAND_NOT_WORKING == 1; }
            set
            {
                if (value)
                    PICTURE_HUSBAND_NOT_WORKING = 1;
                else
                    PICTURE_HUSBAND_NOT_WORKING = 0;
            }
        }


        public bool DOWNPAYMENT_CHECKBOXB
        {
            get { return DOWNPAYMENT_CHECKBOX == 1; }
            set
            {
                if (value)
                    DOWNPAYMENT_CHECKBOX = 1;
                else
                    DOWNPAYMENT_CHECKBOX = 0;
            }
        }

        public bool BROUCHOUR_CHECKBOXB
        {
            get { return BROUCHOUR_CHECKBOX == 1; }
            set
            {
                if (value)
                    BROUCHOUR_CHECKBOX = 1;
                else
                    BROUCHOUR_CHECKBOX = 0;
            }
        }
        public bool FAMILYREGISTRATION_CHECKBOXB
        {
            get { return FAMILYREGISTRATION_CHECKBOX == 1; }
            set
            {
                if (value)
                    FAMILYREGISTRATION_CHECKBOX = 1;
                else
                    FAMILYREGISTRATION_CHECKBOX = 0;
            }
        }


        public bool PARENT_PROOF_CHECKBOXB
        {
            get { return PARENT_PROOF_CHECKBOX == 1; }
            set
            {
                if (value)
                    PARENT_PROOF_CHECKBOX = 1;
                else
                    PARENT_PROOF_CHECKBOX = 0;
            }
        }
        public bool DIVORCED_CHECKBOXB
        {
            get { return DIVORCED_CHECKBOX == 1; }
            set
            {
                if (value)
                    DIVORCED_CHECKBOX = 1;
                else
                    DIVORCED_CHECKBOX = 0;
            }
        }

        public bool DIVORCED_PROOF_CHECKBOXB
        {
            get { return DIVORCED_PROOF_CHECKBOX == 1; }
            set
            {
                if (value)
                    DIVORCED_PROOF_CHECKBOX = 1;
                else
                    DIVORCED_PROOF_CHECKBOX = 0;
            }
        }

        public bool PICTURE_INHERTINCE_PROOFB
        {
            get { return PICTURE_INHERTINCE_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_INHERTINCE_PROOF = 1;
                else
                    PICTURE_INHERTINCE_PROOF = 0;
            }
        }

        public bool PICTURE_REALESTATE_INTREST_PROOFB
        {
            get { return PICTURE_REALESTATE_INTREST_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_REALESTATE_INTREST_PROOF = 1;
                else
                    PICTURE_REALESTATE_INTREST_PROOF = 0;
            }
        }

        public bool PICTURE_SPOUSE_INCOME_PROOFB
        {
            get { return PICTURE_SPOUSE_INCOME_PROOF == 1; }
            set
            {
                if (value)
                    PICTURE_SPOUSE_INCOME_PROOF = 1;
                else
                    PICTURE_SPOUSE_INCOME_PROOF = 0;
            }
        }

        public bool MARRIAGE_DEATH_PROOF_CHECKBOXB
        {
            get { return MARRIAGE_DEATH_PROOF_CHECKBOX == 1; }
            set
            {
                if (value)
                    MARRIAGE_DEATH_PROOF_CHECKBOX = 1;
                else
                    MARRIAGE_DEATH_PROOF_CHECKBOX = 0;
            }
        }
        //WIDOWED_PROOF_CHECKBOX
        public bool WIDOWED_PROOF_CHECKBOXB
        {
            get { return WIDOWED_PROOF_CHECKBOX == 1; }
            set
            {
                if (value)
                    WIDOWED_PROOF_CHECKBOX = 1;
                else
                    WIDOWED_PROOF_CHECKBOX = 0;
            }
        }


       // public string UNITCODE { get; set; }//   public Nullable<short> IS_JOINT_OWNERSHIP { get; set; }
    }

    public class subsidy_requestMetadata
    {
        [Required(ErrorMessage ="{0} مطلوب")]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669 ]+$", ErrorMessage = "يجب إدخال الاسم بالغة العربية فقط")]
        public string PRIMARY_INVESTOR_FULL_NAME { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string PRIMARY_INVESTOR_SSN { get; set; }
        public string PRIMARY_INVESTOR_TELEPHONE_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "ادخل رقم صحيح")]
        public string PRIMARY_INVESTOR_CELL_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string PRIMARY_INVESTOR_GENDER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(1, double.MaxValue,ErrorMessage = " صافي الدخل الشهري غير صحيح")]
        public Nullable<double> PRIMARY_INVESTOR_MONTHLY_INCOME { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        public Nullable<System.DateTime> PRIMARY_INVESTOR_BIRTH_DATE { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        public Nullable<double> PRIMARY_INVESTOR_TOTAL_MONTHLY_INCOME { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> MARITAL_STATUS_ID { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> PRIMARY_INVESTOR_CLASSIFICATION_OF_WORK_ID { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> PRIMARY_INVESTOR_EMPLOYER_GOVERNORATE { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string PRIMARY_INVESTOR_EMPLOYER_NAME { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string PRIMARY_INVESTOR_EMPLOYER_ADDRESS { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> PRIMARY_INVESTOR_ACADEMIC_DEGREE_ID { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "ادخل رقم صحيح")]
        public string INSURANCE_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_STREET_NAME { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_BUILDING_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_UNIT_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_UNIT_FLOOR { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_POSTAL_CODE { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> CURRENT_RESIDENCE_GOVERNORATE_ID { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_CITY { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue)]
        public Nullable<long> CURRENT_RESIDENCE_RESIDENCE_TYPE_ID { get; set; }
        public string CURRENT_RESIDENCE_OTHER_RESIDENCE_INFO { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public string CURRENT_RESIDENCE_COMMENT { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        //[Range(1, int.MaxValue)]
        public Nullable<long> PROJECTID { get; set; }
        //[Required(ErrorMessage = "{0} مطلوب")]
        public string UNITCODE { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public Nullable<long> CityID { get; set; }

        [Required(ErrorMessage = "{0} مطلوب")]
        public long Project_Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب")]
        public int Adv_Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب")]
        public long Gov_Id { get; set; }
    }
}
//-----------------------------------------------------------------------
// <copyright file="AppProjectDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectDetails
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "RequestNo is required.")]
        public string RequestNo { get; set; }

        [Required(ErrorMessage = "RequestLetterNo is required.")]
        public string RequestLetterNo { get; set; }

        [Required(ErrorMessage = "RequestLetterDate is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? RequestLetterDate { get; set; }

        public bool? IsWorkOrderRequired { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public int ProjectYear { get; set; }

        public int AgencyId { get; set; }

        public string AgencyName { get; set; }

        public string EfileNo { get; set; }

        public string PrizmId { get; set; }

        public string Status { get; set; }

        [Script]
        public string Remarks { get; set; }

        public string NicsiprojectCode { get; set; }

        public string Nicsipino { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Pidate { get; set; }

        public decimal? Piamount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SubmitTime { get; set; }

        public string Ipaddress { get; set; }

        public string SubmitBy { get; set; }

        public string ModifyBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifyOn { get; set; }

        public string IsActive { get; set; }

        public string? IsRequestAvailable { get; set; }
    }

    public class Script : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var remarks = (AppProjectDetails)validationContext.ObjectInstance;
            bool validator = false;
            if (remarks.Remarks != null)
            {
                if (remarks.Remarks.Contains("<script>") || remarks.Remarks.Contains("</script>") || remarks.Remarks.Contains("<style>") || remarks.Remarks.Contains("<style>"))
                {
                    validator = true;
                    return new ValidationResult("script not allowed.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

}

//-----------------------------------------------------------------------
// <copyright file="AppOnboardingRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppOnboardingRequest
    {
        [Required(ErrorMessage = "Agency is required.")]
        public int? AgencyTypeId { get; set; }

        public int MinistryId { get; set; }

        [AppOnboardingRequestValidation]
        public string MinistryOther { get; set; } = null;

        [Required(ErrorMessage = "OrganizationId is required.")]
        public int OrganizationId { get; set; }

        [AppOnboardingRequestValidation]
        public string OrganizationOther { get; set; } = null;

        [Required(ErrorMessage = "SessionYear is required.")]
        public int SessionYear { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [AppOnboardingRequestValidation]
        public string Address { get; set; }

        [Required(ErrorMessage = "PinCode is required.")]
        [MaxLength(6)]
        [MinLength(6)]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = "Invalid Pincode")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Contact person is required")]
        [RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid Designation")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [RegularExpression("^[a-zA-Z0-9_. -]*$", ErrorMessage = "Invalid Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Services is required")]
        public string Services { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,5}$", ErrorMessage = "Invalid Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        public string MobileNo { get; set; }

        public string IpAddress { get; set; }

        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "Format is required")]
        
        public string Format { get; set; }

        [Required(ErrorMessage = "FileExtension is required")]
        public string FileExtension { get; set; }

        [Required(ErrorMessage = "ModifiedDate is required")]
        [DataType(DataType.DateTime)]
        public string ModifiedDate { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }
        public string? RequestNo { get; set; }

        [Required(ErrorMessage = "CoodinatorName is required")]
        [RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid CoodinatorName")]
        public string CoodinatorName { get; set; }

        [Required(ErrorMessage = "CoodinatorDesignation is required")]
        [RegularExpression("^[a-zA-Z0-9_. -]*$", ErrorMessage = "Invalid CoodinatorDesignation")]
        public string CoodinatorDesignation { get; set; }

        [Required(ErrorMessage = "CoodinatorEmail is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,5}$", ErrorMessage = "Invalid CoodinatorEmail")]
        public string CoodinatorEmail { get; set; }

        [Required(ErrorMessage = "CoodinatorMobile is required")]
        public string CoodinatorMobile { get; set; }

        [Required(ErrorMessage = "StateID is required")]
        public string StateID { get; set; }

        [Required(ErrorMessage = "DistrictID is required")]
        public string DistrictID { get; set; }

        public int? AgencyStateId { get; set; }

        [Required(ErrorMessage = "hodEncryptedMail is required")]
        public string hodEncryptedMail { get; set; }

        [Required(ErrorMessage = "cordinatiorEncryptedMail is required")]
        public string cordinatiorEncryptedMail { get; set; }

        [Required(ErrorMessage = "CurrentStage is required")]
        public string CurrentStage { get; set; }

        [Required(ErrorMessage = "PdfContent is required")]
        public string PdfContent { get; set; }

        public string Remarks { get; set; }
        
    }

    public class AppOnboardingRequestValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var appOnboardingRequest = (AppOnboardingRequest)validationContext.ObjectInstance;
            if ((appOnboardingRequest.Address.Contains("<script>")) || 
                (appOnboardingRequest.Address.Contains("</script>")) || 
                (appOnboardingRequest.Address.Contains("<style>")) ||
                (appOnboardingRequest.Address.Contains("<style>")) ||
                (appOnboardingRequest.MinistryOther.Contains("<script>")) ||
                (appOnboardingRequest.MinistryOther.Contains("</script>")) ||
                (appOnboardingRequest.MinistryOther.Contains("<style>")) ||
                (appOnboardingRequest.MinistryOther.Contains("<style>")) ||
                (appOnboardingRequest.OrganizationOther.Contains("<script>")) ||
                (appOnboardingRequest.OrganizationOther.Contains("</script>")) ||
                (appOnboardingRequest.OrganizationOther.Contains("<style>")) ||
                (appOnboardingRequest.OrganizationOther.Contains("<style>"))
                )
            {
                
                return new ValidationResult("script not allowed."); ;
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="AppOnboardingResponse.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppOnboardingResponse
    {
        [Required(ErrorMessage = "RequestNo is required")]
        public string RequestNo { get; set; } = null!;

        //[Required(ErrorMessage = "Agency is required")]
        public string Agency { get; set; }

        [Required(ErrorMessage = "Services is required")]
        public string Services { get; set; }

        //[Required(ErrorMessage = "Ministry is required")]
        public string Ministry { get; set; }

        public string NameofOrganization { get; set; }

        public string CompleteMailingAddress { get; set; }

        [MaxLength(6)]
        [MinLength(6)]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = "Invalid Pincode")]
        public string PinCode { get; set; }

        public string HeadofOrganization { get; set; }

        [Required(ErrorMessage = "cordinatorName is required")]
        public string cordinatorName { get; set; }
        
        [Required(ErrorMessage = "cordMail is required")]
        public string cordMail { get; set; }

        public string Designation { get; set; }
 
        public string Email { get; set; }

        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Remarks is required")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "EncryptedRequestNumber is required")]
        public string EncryptedRequestNumber { get; set; }

        [Required(ErrorMessage = "MailingEmail is required")]
        public string MailingEmail { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set;}

        public string CordMobileNo { get; set; }

    }
}

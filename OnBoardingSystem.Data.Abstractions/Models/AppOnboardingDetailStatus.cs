//-----------------------------------------------------------------------
// <copyright file="AppOnboardingDetailStatus.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    using System.ComponentModel.DataAnnotations;
    public class AppOnboardingDetailStatus
    {
        [Required(ErrorMessage = "Encrypted RequestNumber is required.")]
        public string EncryptedRequestNumber { get; set; }

        [Required(ErrorMessage = "Encrypted RequestNumber is required.")]
        public string requestNo { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        public string Remarks { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "cordName is required.")]
        public string cordName { get; set; }

        public string cordNumber { get; set; }

        public string Activity { get; set; }

    }
}

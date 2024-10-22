//-----------------------------------------------------------------------
// <copyright file="OTPModal.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class OTPModal
    {
        [Required(ErrorMessage = "Otp is required.")]
        public string? Otp { get; set; }

        [Required(ErrorMessage = "OtpSms is required.")]
        public string? OtpSms { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "coodinatorName is required.")]
        public string? coodinatorName { get; set; }

    }
}

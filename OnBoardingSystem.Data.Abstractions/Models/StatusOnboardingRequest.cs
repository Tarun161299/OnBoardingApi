//-----------------------------------------------------------------------
// <copyright file="StatusOnboardingRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class StatusOnboardingRequest
    {
        public string RequestNo { get; set; } = null!;

        public string? Status {     get; set; } 

        public string? Remarks { get; set; }

        public string? Email { get; set; }

    }
}

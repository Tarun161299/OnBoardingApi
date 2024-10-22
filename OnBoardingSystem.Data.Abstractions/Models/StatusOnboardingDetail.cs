//-----------------------------------------------------------------------
// <copyright file="StatusOnboardingRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class StatusOnboardingDetail
    {
        public string? RequestNo { get; set; }

        public string? Status { get; set; } 

        public string? Remarks { get; set; }

        public string? CordEmail { get; set; }
        public string? CordName { get; set; }
    }
}

// <copyright file="MdMinistryRequestInfoList.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class Status
    {
        public List<StatusOnboardingRequest> StatusRequest { get; set; }
        public List<StatusOnboardingDetail> StatusDetail { get; set; }
    }
}

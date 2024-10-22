//-----------------------------------------------------------------------
// <copyright file="ZmstInstitute.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstInstitute
    {
        public string InstCd { get; set; }
        public string InstNm { get; set; }
        public string? AbbrNm { get; set; }
        public string InstTypeId { get; set; }
        public string? SeatType { get; set; }
        public string? AgencyId { get; set; }
        public string? InstAdd { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? Pincode { get; set; }
        public string? InstPhone { get; set; }
        public string? InstFax { get; set; }
        public string? InstWebSite { get; set; }
        public string? EmailId { get; set; }
        public string? AltEmailId { get; set; }
        public string? ContactPerson { get; set; }
        public string? Designation { get; set; }
        public string? MobileNo { get; set; }
        public string? AISHE { get; set; }
        public string? OldInstituteCode { get; set; }

    }
}
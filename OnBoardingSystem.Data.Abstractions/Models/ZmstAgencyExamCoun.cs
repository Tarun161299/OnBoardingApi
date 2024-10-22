//-----------------------------------------------------------------------
// <copyright file="ZmstServiceType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstAgencyExamCouns
    {
        public int AgencyId { get; set; }

        public string? ExamCounsId { get; set; }

        public string? Description { get; set; }

        public string? AgencyName { get; set; }
    }
}

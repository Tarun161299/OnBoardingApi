//-----------------------------------------------------------------------
// <copyright file="MdAgency.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdAgency
    {
        public int AgencyId { get; set; }

        public string AgencyName { get; set; } = null!;

        public string? Abbreviation { get; set; }

        public string? AgencyType { get; set; }

        public string? StateId { get; set; }

        public string? ServiceTypeId { get; set; }

        public string? Address { get; set; }

        public string? IsActive { get; set; }

        public int? Priority { get; set; }

        public int? AgencyTypeId { get; set; }

    }
}

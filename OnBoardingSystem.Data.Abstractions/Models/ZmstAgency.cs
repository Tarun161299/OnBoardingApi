//-----------------------------------------------------------------------
// <copyright file="ZmstAgency.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstAgency
    {
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string? AgencyAbbr { get; set; }
        public string? AgencyType { get; set; }
        public string? StateId { get; set; }

        public string? StateName { get; set; }
        public string? ServiceTypeId { get; set; }
        public string? Address { get; set; }
        public string? IsActive { get; set; }
        public int? Priority { get; set; }
        public string? BoardRequestLetter { get; set; }

        public string? ServiceType { get; set; }

        public string? Active { get; set; }
    }
		}
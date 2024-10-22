//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteAgency.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstInstituteAgency
    {
		public string  InstCd{ get; set; }
		public string  AgencyId{ get; set; }
        public string? InstName { get; set; }
        public string? AgencyName{ get; set; }

    }
}
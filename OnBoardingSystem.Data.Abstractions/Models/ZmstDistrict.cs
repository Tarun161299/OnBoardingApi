//-----------------------------------------------------------------------
// <copyright file="ZmstDistrict.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstDistrict
    {
		public string? StateId{ get; set; }
			public string? StateName{ get; set; }
			public string? DistrictId{ get; set; }
			public string? DistrictName{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
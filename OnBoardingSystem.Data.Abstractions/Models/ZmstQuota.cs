//-----------------------------------------------------------------------
// <copyright file="ZmstQuota.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstQuota
    {
		public string QuotaId{ get; set; }
			public string Name{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
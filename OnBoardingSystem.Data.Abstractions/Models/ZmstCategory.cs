//-----------------------------------------------------------------------
// <copyright file="ZmstCategory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstCategory
    {
		public string CategoryId{ get; set; }
			public string? CategoryName{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
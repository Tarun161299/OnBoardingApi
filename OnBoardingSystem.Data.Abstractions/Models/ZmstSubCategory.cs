//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstSubCategory
    {
		public string SubCategoryId{ get; set; }
			public string SubCategoryName{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
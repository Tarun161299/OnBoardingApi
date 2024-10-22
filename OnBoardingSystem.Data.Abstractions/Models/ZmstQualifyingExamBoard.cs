//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamBoard.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstQualifyingExamBoard
    {
		public string Id{ get; set; }
			public string? Description{ get; set; }
			public string QualificationId{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
//-----------------------------------------------------------------------
// <copyright file="MdActivityType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
	public class MdActivityType
	{
        public int ActivityId { get; set; }
		public string? Activity { get; set; }

		public string? ActivityGroup { get; set; }
        public bool? IsActive { get; set; }

	}
}

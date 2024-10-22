//-----------------------------------------------------------------------
// <copyright file="AppProjectActivityStatusTracking.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
	public class AppProjectActivityStatusTracking
	{
		public int Id { get; set; }

		public int ProjectActivityId { get; set; }

		public string Status { get; set; } = null!;

		public DateTime SubmitTime { get; set; }
	}
}

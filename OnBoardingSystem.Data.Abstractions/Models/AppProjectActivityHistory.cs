//-----------------------------------------------------------------------
// <copyright file="AppProjectActivityHistory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
	public class AppProjectActivityHistory
	{
		public int Id { get; set; }

		public string ActivityParentRefId { get; set; } = null!;

		public int ActivityId { get; set; }

		public string Status { get; set; } = null!;

		public DateTime SubmitTime { get; set; }

		public string IpAddress { get; set; } = null!;
	}
}

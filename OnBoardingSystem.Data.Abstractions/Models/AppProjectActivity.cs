//-----------------------------------------------------------------------
// <copyright file="AppProjectActivity.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectActivity
    {
		public int Id { get; set; }

		/// <summary>
		/// RequestNo or Project Code etc
		/// </summary>
		public string ActivityParentRefId { get; set; } = null!;

		public int ActivityId { get; set; }

		public string Status { get; set; } = null!;

		public DateTime SubmitTime { get; set; }

		public string? IpAddress { get; set; }
        public string ActivityName { get; set; } 

    }
}


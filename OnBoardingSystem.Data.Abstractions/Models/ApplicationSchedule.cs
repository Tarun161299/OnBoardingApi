//-----------------------------------------------------------------------
// <copyright file="ApplicationSchedule.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ApplicationSchedule
    {
        public int? Id { get; set; }

        public string AppId { get; set; } = null!;

        public string? Summary { get; set; }

        public List<AppScheduleData> JSummary { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }

        public string? IpAddress { get; set; }

        public int? Priority { get; set; }

        public string? Status { get; set; }

        public string? projectName { get; set; }

        //public List<AppScheduleData> appScheduleData { get; set; }
    }
}

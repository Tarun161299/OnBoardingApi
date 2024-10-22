//-----------------------------------------------------------------------
// <copyright file="AppScheduleData.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppScheduleData
    {
        public string? boardid { get; set; }

        public string roundno { get; set; } 

        public string? activityid { get; set; }

        public string? ActivityNm { get; set; }

        public string? description { get; set; }

        public string? sdate { get; set; }

        public string? cDate { get; set; }

        public string? ScheduleStatus { get; set; }

        //public string? projectName { get; set; }

    }
}

//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingCourse.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstQualifyingCourse
    {
        public string QualificationCourseId { get; set; }
        public string? QualificationCourseName { get; set; }
        public string QualificationId { get; set; }
        public string QualificationName { get; set; }

    }
}
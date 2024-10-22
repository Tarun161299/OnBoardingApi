//-----------------------------------------------------------------------
// <copyright file="qualificationDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class QualificationDetails
    {
        public int QualificationDetailsId { get; set; }
        public string? EmpCode { get; set; }
        public string? ExamPassed { get; set; }
        public string? BoardUniv { get; set; }
        public string? PassYear { get; set; }
        public string? Division { get; set; }

        public string? Documents { get; set; }
        public string? ExamName { get; set; }

        public string? EmpName { get; set; }
    }
}
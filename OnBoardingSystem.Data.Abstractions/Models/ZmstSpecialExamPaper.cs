//-----------------------------------------------------------------------
// <copyright file="ZmstSpecialExamPaper.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstSpecialExamPaper
    {
        public string Id { get; set; }
        public string? Description { get; set; }
        public string? AlternateNames { get; set; }
        public string SpecialExamId { get; set; }
        public string SpecialExamName { get; set; }

    }
}
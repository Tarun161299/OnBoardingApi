//-----------------------------------------------------------------------
// <copyright file="ZmstExperienceType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstExperienceType
    {
        public string Id { get; set; }
        public string? ExperienceType { get; set; }

    }
}
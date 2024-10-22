//-----------------------------------------------------------------------
// <copyright file="ZmstApplicantType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstApplicantType
    {
        public int ID { get; set; }
        public string? TypeName { get; set; }

    }
}
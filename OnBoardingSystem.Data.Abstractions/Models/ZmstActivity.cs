//-----------------------------------------------------------------------
// <copyright file="ZmstActivity.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstActivity
    {
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int? DisplayPriority { get; set; }

    }
}
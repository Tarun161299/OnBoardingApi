//-----------------------------------------------------------------------
// <copyright file="MdStatus.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdStatus
    {
        public string StatusId { get; set; }
        public string? Status { get; set; }
        public string? ActivityId { get; set; }
        public bool? IsActive { get; set; }

    }
}
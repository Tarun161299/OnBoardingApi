//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategoryPriority.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstSubCategoryPriority
    {
        public string SubCategoryPriorityId { get; set; }
        public string Description { get; set; }
        public string SubCategoryId { get; set; }
        public string? AlternateNames { get; set; }
        public string? SubCategoryName { get; set; }
    }
}
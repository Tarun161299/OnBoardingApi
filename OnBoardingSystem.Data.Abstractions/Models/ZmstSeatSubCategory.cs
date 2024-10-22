//-----------------------------------------------------------------------
// <copyright file="ZmstSeatSubCategory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstSeatSubCategory
    {
        public string SeatSubCategoryId { get; set; }
        public string Description { get; set; }
        public string? Alternatenames { get; set; }

    }
}
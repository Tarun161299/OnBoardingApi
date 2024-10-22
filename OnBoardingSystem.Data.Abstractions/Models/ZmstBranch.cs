//-----------------------------------------------------------------------
// <copyright file="ZmstBranch.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstBranch
    {
        public string BrCd { get; set; }
        public string BrNm { get; set; }
        public string? Stream { get; set; }

    }
}
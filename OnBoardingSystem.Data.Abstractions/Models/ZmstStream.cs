//-----------------------------------------------------------------------
// <copyright file="ZmstStream.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstStream
    {
        public string StreamId { get; set; }
        
        public string StreamName { get; set; }
        
        public string? AlternateNames { get; set; }

        public string? instCd { get; set; }

    }
}
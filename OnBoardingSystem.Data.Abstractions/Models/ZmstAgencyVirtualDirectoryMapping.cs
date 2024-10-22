//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyVirtualDirectoryMapping.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstAgencyVirtualDirectoryMapping
    {
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string? BaseDirectory { get; set; }
        public string VirtualDirectory { get; set; }
        public string VirtualDirectoryType { get; set; }
        public bool? IsActive { get; set; }

    }
}
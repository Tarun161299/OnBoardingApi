//-----------------------------------------------------------------------
// <copyright file="AppSetting.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppSetting
    {
        public string ReferenceKey { get; set; } = null!;

        public string Value { get; set; } = null!;

        public string Description { get; set; }

        public string Type { get; set; } = null!;
    }
}

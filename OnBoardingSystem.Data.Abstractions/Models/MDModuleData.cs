//-----------------------------------------------------------------------
// <copyright file="MDModule.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MDModuleData
    {
        public string ModuleId { get; set; } = null!;

        public string? Heading { get; set; }

        public string? SubHeading { get; set; }

        public string? Url { get; set; }
        public string? MainModule { get; set; }
        public string? Path { get; set; }

        public string? Parent { get; set; }

        public string? IsActive { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="MDModule.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MDModule
    {
        public List<MDModuleData> ParentModuleList { get; set; }
        public List<MDModuleData> SubModuleList { get; set; }
    }
}

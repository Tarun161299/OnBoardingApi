//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermission.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppRoleModulePermission
    {
        public string? RoleId { get; set; }

        public string? ModuleId { get; set; }

        public string? IsReadOnly { get; set; }

        public string? IsActive { get; set; }

        public string? ModuleName { get; set; }

        public string? Assign { get; set; }
    }
}

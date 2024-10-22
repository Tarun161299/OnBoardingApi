//-----------------------------------------------------------------------
// <copyright file="AppUserRoleMapping.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppUserRoleMapping:AppRoleModulePermission
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
        public string? IsReadOnly { get; set; }
        public string? IsActive { get; set; }
        public string? Assign { get; set; }
        public string? Role { get; set; }
    }
}

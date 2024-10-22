//-----------------------------------------------------------------------
// <copyright file="AppDoumentTypeRoleMapping.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppDocumentTypeRoleMapping
    {
        public string? DocumentTypeId { get; set; }

        public string? RoleId { get; set; }

        public bool? IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppDocumentTypeRoleMapping
{
    public string? DocumentTypeId { get; set; }

    public string? RoleId { get; set; }

    public bool? IsMaster { get; set; }

    public bool? IsVisible { get; set; }

    public bool? IsActive { get; set; }
}

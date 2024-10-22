using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppRoleModulePermission
{
    public string RoleId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public string? IsReadOnly { get; set; }

    public string? IsActive { get; set; }
}

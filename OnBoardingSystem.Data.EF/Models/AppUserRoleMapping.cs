using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppUserRoleMapping
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string? IsReadOnly { get; set; }

    public string? IsActive { get; set; }
}

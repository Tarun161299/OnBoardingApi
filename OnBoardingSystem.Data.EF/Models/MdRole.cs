using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdRole
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string Description { get; set; } = null!;
}

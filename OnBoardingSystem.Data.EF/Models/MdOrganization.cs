using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdOrganization
{
    public string OrganizationId { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? StateId { get; set; }
}

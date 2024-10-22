using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstResidentialEligibility
{
    public string ResidentialEligibilityId { get; set; } = null!;

    public string? ResidentialEligibilityName { get; set; }
}

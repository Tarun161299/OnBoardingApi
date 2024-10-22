using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQualification
{
    public string QualificationId { get; set; } = null!;

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

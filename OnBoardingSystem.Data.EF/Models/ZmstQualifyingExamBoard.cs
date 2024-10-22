using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQualifyingExamBoard
{
    public string Id { get; set; } = null!;

    public string? Description { get; set; }

    public string QualificationId { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

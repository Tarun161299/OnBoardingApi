using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSpecialExamPaper
{
    public string Id { get; set; } = null!;

    public string? Description { get; set; }

    public string? AlternateNames { get; set; }

    public string SpecialExamId { get; set; } = null!;
}

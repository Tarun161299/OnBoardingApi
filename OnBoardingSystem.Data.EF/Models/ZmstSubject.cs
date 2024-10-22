using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSubject
{
    public string QualificationId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

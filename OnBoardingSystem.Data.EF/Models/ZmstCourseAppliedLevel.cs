using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstCourseAppliedLevel
{
    public string CourseLevelId { get; set; } = null!;

    public string? CourseLevelName { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstCourseApplied
{
    public string CourseId { get; set; } = null!;

    public string? CourseName { get; set; }

    public string? AlternameNames { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQualifyingCourse
{
    public string QualificationCourseId { get; set; } = null!;

    public string? QualificationCourseName { get; set; }

    public string QualificationId { get; set; } = null!;
}

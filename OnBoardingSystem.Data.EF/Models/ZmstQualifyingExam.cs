using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQualifyingExam
{
    public string QualifyingExamId { get; set; } = null!;

    public string? QualifyingExamName { get; set; }
}

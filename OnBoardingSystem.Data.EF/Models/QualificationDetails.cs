using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class QualificationDetails
{
    public int QualificationDetailsId { get; set; }

    public string? EmpCode { get; set; }

    public string? ExamPassed { get; set; }

    public string? BoardUniv { get; set; }

    public string? PassYear { get; set; }

    public string? Division { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class TempDelQualificationDetails
{
    public string EmpCode { get; set; } = null!;

    public string? ExamPassed { get; set; }

    public string? BoardUniv { get; set; }

    public string? PassYear { get; set; }

    public string? Division { get; set; }

    public string? CertificateName { get; set; }

    public byte[]? Certificate { get; set; }
}

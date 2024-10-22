using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstAgencyExamCouns
{
    /// <summary>
    /// Required
    /// </summary>
    public int AgencyId { get; set; }

    /// <summary>
    /// Required
    /// </summary>
    public string ExamCounsId { get; set; } = null!;

    /// <summary>
    /// Required
    /// </summary>
    public string? Description { get; set; }
}

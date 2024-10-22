using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class WorkorderdetailsBkp
{
    public int WorkorderId { get; set; }

    public string WorkorderNo { get; set; } = null!;

    public string ProjectCode { get; set; } = null!;

    public DateTime? IssueDate { get; set; }

    public string? AgencyName { get; set; }

    public string? ResourceCategory { get; set; }

    public string? ResourceNo { get; set; }

    public string? NoofMonths { get; set; }

    public DateTime? WorkorderFrom { get; set; }

    public DateTime? WorkorderTo { get; set; }

    public string? DocName { get; set; }
}

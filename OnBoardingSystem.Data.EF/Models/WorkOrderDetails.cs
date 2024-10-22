using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class WorkOrderDetails
{
    public int WorkorderId { get; set; }

    /// <summary>
    /// alphanumeric,required
    /// </summary>
    public string WorkorderNo { get; set; } = null!;

    public string ProjectCode { get; set; } = null!;

    /// <summary>
    /// required
    /// </summary>
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? AgencyName { get; set; }

    /// <summary>
    /// alphanumeric,required
    /// </summary>
    public string? ResourceCategory { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? ResourceNo { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? NoofMonths { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public DateTime? WorkorderFrom { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public DateTime? WorkorderTo { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? DocName { get; set; }
}

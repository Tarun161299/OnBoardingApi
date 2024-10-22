using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstAgency
{
    /// <summary>
    /// Required
    /// </summary>
    public string AgencyId { get; set; } = null!;

    /// <summary>
    /// Alphanumeric
    /// </summary>
    public string AgencyName { get; set; } = null!;

    public string? AgencyAbbr { get; set; }

    /// <summary>
    /// Required
    /// </summary>
    public string? AgencyType { get; set; }

    /// <summary>
    /// Required
    /// </summary>
    public string? StateId { get; set; }

    public string? ServiceTypeId { get; set; }

    public string? Address { get; set; }

    /// <summary>
    /// Required
    /// </summary>
    public string? IsActive { get; set; }

    /// <summary>
    /// Only Number,Required
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// Required
    /// </summary>
    public byte[]? BoardRequestLetter { get; set; }
}

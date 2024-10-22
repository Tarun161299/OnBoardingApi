using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdAgency
{
    public int AgencyId { get; set; }

    /// <summary>
    /// required;pattern;maxlength;minlength
    /// </summary>
    public string AgencyName { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public string? AgencyType { get; set; }

    public string? StateId { get; set; }

    public string? ServiceTypeId { get; set; }

    public string? Address { get; set; }

    public string? IsActive { get; set; }

    public int? Priority { get; set; }
}

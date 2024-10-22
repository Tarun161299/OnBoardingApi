using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppProjectDetails
{
    public int Id { get; set; }

    public string RequestNo { get; set; } = null!;

    public string? RequestLetterNo { get; set; }

    public DateTime? RequestLetterDate { get; set; }

    /// <summary>
    /// System Generated;pattern;maxlength
    /// </summary>
    public string? ProjectCode { get; set; }

    /// <summary>
    /// required;pattern;maxlength;minlength
    /// </summary>
    public string ProjectName { get; set; } = null!;

    public int ProjectYear { get; set; }

    public bool? IsWorkOrderRequired { get; set; }

    public int AgencyId { get; set; }

    public string? AgencyName { get; set; }

    public string EfileNo { get; set; } = null!;

    public string PrizmId { get; set; } = null!;

    public string? Status { get; set; }

    public string Remarks { get; set; } = null!;

    public string Nicsipino { get; set; } = null!;

    public DateTime? Pidate { get; set; }

    public decimal? Piamount { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }

    public string? SubmitBy { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string? IsActive { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppOnboardingResponse
{
    public int? Id { get; set; }

    public string? RequestNo { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public int? Version { get; set; }

    public string? UserId { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }
}

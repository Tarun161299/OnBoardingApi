using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppOnboardingDetailsResponseLink
{
    public string? RequestNo { get; set; }

    public int? Version { get; set; }

    public string? ResponseId { get; set; }

    public string? Link { get; set; }

    public string? UserId { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }

    public string? Status { get; set; }

    public string? ExpiryDate { get; set; }
}

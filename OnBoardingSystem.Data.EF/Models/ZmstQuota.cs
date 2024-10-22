using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQuota
{
    public string QuotaId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

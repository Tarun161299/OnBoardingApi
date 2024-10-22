using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstState
{
    public string StateId { get; set; } = null!;

    public string? StateName { get; set; }

    public string? AlternateNames { get; set; }
}

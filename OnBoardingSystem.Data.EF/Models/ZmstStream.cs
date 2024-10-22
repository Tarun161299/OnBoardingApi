using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstStream
{
    public string StreamId { get; set; } = null!;

    public string StreamName { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

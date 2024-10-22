using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppSettings
{
    public string ReferenceKey { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;
}

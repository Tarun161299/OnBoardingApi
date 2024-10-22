using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstCategory
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public string? AlternateNames { get; set; }
}

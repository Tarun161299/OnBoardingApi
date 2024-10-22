using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSubCategoryPriority
{
    public string SubCategoryPriorityId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string SubCategoryId { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

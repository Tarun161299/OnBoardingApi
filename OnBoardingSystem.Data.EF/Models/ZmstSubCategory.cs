using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSubCategory
{
    public string SubCategoryId { get; set; } = null!;

    public string SubCategoryName { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

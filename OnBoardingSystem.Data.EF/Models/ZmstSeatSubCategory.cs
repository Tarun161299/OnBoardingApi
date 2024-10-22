using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSeatSubCategory
{
    public string SeatSubCategoryId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Alternatenames { get; set; }
}

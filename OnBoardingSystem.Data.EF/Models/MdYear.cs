using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdYear
{
    public string YearId { get; set; } = null!;

    public string? Description { get; set; }

    public string? Abbrivation { get; set; }

    public string? YearGroup { get; set; }

    public string? IsActive { get; set; }
}

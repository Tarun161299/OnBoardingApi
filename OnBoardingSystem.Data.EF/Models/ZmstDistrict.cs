using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstDistrict
{
    public string? StateId { get; set; }

    public string? StateName { get; set; }

    public string? DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public string? AlternateNames { get; set; }
}

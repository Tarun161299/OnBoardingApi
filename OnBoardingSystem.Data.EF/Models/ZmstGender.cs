using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstGender
{
    public string GenderId { get; set; } = null!;

    public string GenderName { get; set; } = null!;

    public string AlternateNames { get; set; } = null!;
}

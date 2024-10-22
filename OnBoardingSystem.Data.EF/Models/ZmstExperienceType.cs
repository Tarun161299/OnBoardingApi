using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstExperienceType
{
    public string Id { get; set; } = null!;

    public string? ExperienceType { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstActivity
{
    public string ActivityId { get; set; } = null!;

    public string ActivityName { get; set; } = null!;

    public int? DisplayPriority { get; set; }
}

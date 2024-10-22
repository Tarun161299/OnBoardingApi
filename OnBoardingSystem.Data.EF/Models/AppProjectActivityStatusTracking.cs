using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppProjectActivityStatusTracking
{
    public int Id { get; set; }

    public int ProjectActivityId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime SubmitTime { get; set; }
}

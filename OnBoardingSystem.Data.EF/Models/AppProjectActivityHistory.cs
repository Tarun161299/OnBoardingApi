using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppProjectActivityHistory
{
    public int Id { get; set; }

    public string ActivityParentRefId { get; set; } = null!;

    public int ActivityId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime SubmitTime { get; set; }

    public string IpAddress { get; set; } = null!;
}

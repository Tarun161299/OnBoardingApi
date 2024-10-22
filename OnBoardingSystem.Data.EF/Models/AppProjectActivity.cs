using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppProjectActivity
{
    public int Id { get; set; }

    /// <summary>
    /// RequestNo or Project Code etc
    /// </summary>
    public string ActivityParentRefId { get; set; } = null!;

    public int ActivityId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime SubmitTime { get; set; }

    public string? IpAddress { get; set; }
}

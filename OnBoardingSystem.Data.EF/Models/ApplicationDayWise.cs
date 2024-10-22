using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ApplicationDayWise
{
    public int Id { get; set; }

    public string AppId { get; set; } = null!;

    public string? Summary { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string? UpdatedBy { get; set; }

    public string? IpAddress { get; set; }

    public int? Priority { get; set; }

    public string? Status { get; set; }
}

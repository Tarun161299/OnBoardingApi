using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdModule
{
    public string ModuleId { get; set; } = null!;

    public string? Heading { get; set; }

    public string? SubHeading { get; set; }

    public string? Url { get; set; }

    public string? Path { get; set; }

    public string? Parent { get; set; }

    public string? MainModule { get; set; }

    public string? IsActive { get; set; }

    public int? DisplayPriority { get; set; }

    public string? Remarks { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class BkpMdMainModule
{
    public string MainModuleId { get; set; } = null!;

    public string? Heading { get; set; }

    public string? SubHeading { get; set; }

    public string? Path { get; set; }

    public string? IsActive { get; set; }

    public int? DisplayPriority { get; set; }

    public string? Remarks { get; set; }

    public string? Icon { get; set; }

    public string? CssClass { get; set; }
}

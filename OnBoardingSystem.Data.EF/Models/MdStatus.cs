using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdStatus
{
    public string StatusId { get; set; } = null!;

    public string ActivityId { get; set; } = null!;

    public string? Status { get; set; }

    public bool? IsActive { get; set; }
}

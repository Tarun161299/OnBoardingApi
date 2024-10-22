using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdActivityType
{
    public int ActivityId { get; set; }

    public string? Activity { get; set; }

    public string? ActivityGroup { get; set; }

    public bool? IsActive { get; set; }
}

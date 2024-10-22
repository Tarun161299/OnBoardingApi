using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstServiceType
{
    public string ServiceTypeId { get; set; } = null!;

    public string? ServiceTypeName { get; set; }
}

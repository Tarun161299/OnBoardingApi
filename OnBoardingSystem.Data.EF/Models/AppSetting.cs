using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppSetting
{
    public int Id { get; set; }

    public string ReferenceKey { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;
}

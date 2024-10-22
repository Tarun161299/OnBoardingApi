using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQuesPaper
{
    public string PaperId { get; set; } = null!;

    public string? PaperName { get; set; }
}

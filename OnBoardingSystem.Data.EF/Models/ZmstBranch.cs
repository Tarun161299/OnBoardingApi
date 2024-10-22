using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstBranch
{
    public string BrCd { get; set; } = null!;

    public string BrNm { get; set; } = null!;

    public string? Stream { get; set; }
}

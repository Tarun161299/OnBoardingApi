using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSymbol
{
    public string SymbolId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

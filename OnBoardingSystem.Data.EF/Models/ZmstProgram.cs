using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstProgram
{
    public int Id { get; set; }

    public string? Brcd { get; set; }

    public string? Brnm { get; set; }

    public string? Agencyid { get; set; }

    public string? BrcdOrg { get; set; }

    public string? Bshift { get; set; }

    public string? Btfw { get; set; }
}

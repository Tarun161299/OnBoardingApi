using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstInstituteType
{
    /// <summary>
    /// required
    /// </summary>
    public string InstituteTypeId { get; set; } = null!;

    /// <summary>
    /// required
    /// </summary>
    public string? InstituteType { get; set; }

    /// <summary>
    /// number,required
    /// </summary>
    public int Priority { get; set; }
}

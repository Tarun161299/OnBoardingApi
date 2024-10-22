using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstAgencyVirtualDirectoryMapping
{
    /// <summary>
    /// required
    /// </summary>
    public string AgencyId { get; set; } = null!;

    /// <summary>
    /// alphanumeric,required
    /// </summary>
    public string? BaseDirectory { get; set; }

    /// <summary>
    /// alphanumeric,required
    /// </summary>
    public string VirtualDirectory { get; set; } = null!;

    /// <summary>
    /// required
    /// </summary>
    public string VirtualDirectoryType { get; set; } = null!;

    public bool? IsActive { get; set; }
}

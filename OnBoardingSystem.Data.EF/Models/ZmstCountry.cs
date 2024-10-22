using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstCountry
{
    /// <summary>
    /// alphabet;maxlength
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// alphabet;maxlength
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// alphabet;maxlength
    /// </summary>
    public string SAarccode { get; set; } = null!;

    /// <summary>
    /// alphabet;maxlength
    /// </summary>
    public string SAarcname { get; set; } = null!;

    /// <summary>
    /// number;maxlength
    /// </summary>
    public string Isdcode { get; set; } = null!;

    /// <summary>
    /// number
    /// </summary>
    public int Priority { get; set; }
}

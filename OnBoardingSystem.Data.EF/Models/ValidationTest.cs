using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ValidationTest
{
    public int Id { get; set; }

    /// <summary>
    /// maxlength,minlength
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// email;maxlength;minlength
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// number;maxlength;minlength
    /// </summary>
    public string? Mobile { get; set; }

    public bool? Status { get; set; }

    /// <summary>
    /// url
    /// </summary>
    public string? Url { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstAuthenticationMode
{
    public string AuthCode { get; set; } = null!;

    public string Authmode { get; set; } = null!;
}

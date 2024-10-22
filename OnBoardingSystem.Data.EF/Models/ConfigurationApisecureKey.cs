using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ConfigurationApisecureKey
{
    public string? KeyName { get; set; }

    public string? SecretKey { get; set; }

    public string? Salt { get; set; }
}

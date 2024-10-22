using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ApiSubscriptionKey
{
    public int Id { get; set; }

    public string? ApplicationName { get; set; }

    public string? ApplicationKey { get; set; }
}

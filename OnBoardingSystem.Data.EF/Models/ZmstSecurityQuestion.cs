using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstSecurityQuestion
{
    public string SecurityQuesId { get; set; } = null!;

    public string SecurityQues { get; set; } = null!;
}

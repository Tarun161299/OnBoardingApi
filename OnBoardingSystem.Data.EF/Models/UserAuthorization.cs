using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class UserAuthorization
{
    public string? UserId { get; set; }

    public string? RefreshToken { get; set; }

    public string? Token { get; set; }
}

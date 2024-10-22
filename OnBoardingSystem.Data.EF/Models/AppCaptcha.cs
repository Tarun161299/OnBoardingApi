using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppCaptcha
{
    public long Id { get; set; }

    public string? CaptchaKey { get; set; }

    public string? CaptchBaseString { get; set; }

    public string? Md5Hash { get; set; }

    public string? Ip { get; set; }

    public DateTime? CreatedDate { get; set; }
}

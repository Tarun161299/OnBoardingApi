﻿using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class Administrator
{
    public string? AppFormId { get; set; }

    public string UserId { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? UserName { get; set; }

    public string? Designation { get; set; }

    public string? EmailId { get; set; }

    public string? MobileNo { get; set; }

    public string? SecurityQuesId { get; set; }

    public string? SecurityAns { get; set; }

    public string? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? IpAddress { get; set; }

    public string? RoleId { get; set; }

    public string? AuthMode { get; set; }

    public string? BoardId { get; set; }

    public byte[]? Photopath { get; set; }
}

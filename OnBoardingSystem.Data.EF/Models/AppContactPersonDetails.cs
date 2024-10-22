using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppContactPersonDetails
{
    public int Id { get; set; }

    public string? RequestNo { get; set; }

    public string? DepartmentId { get; set; }

    public string? RoleId { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }
}

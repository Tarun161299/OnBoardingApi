using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? CountryId { get; set; }

    public string? CountryName { get; set; }

    public int? Status { get; set; }

    public DateTime? SubmitTime { get; set; }
}

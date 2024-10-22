using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class CgconfigurationControleTest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public string? Dob { get; set; }

    public string? Email { get; set; }

    public int? Mobile { get; set; }

    public string? Qualification { get; set; }

    public string? Image { get; set; }

    public string? Addresss { get; set; }

    public string? State { get; set; }

    public string? District { get; set; }

    public decimal? Fee { get; set; }

    public string? Hobbies { get; set; }
}

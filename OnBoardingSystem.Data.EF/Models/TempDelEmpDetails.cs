using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class TempDelEmpDetails
{
    public string EmpCode { get; set; } = null!;

    public string? EmpName { get; set; }

    public string? Designation { get; set; }

    public string? FName { get; set; }

    public string? MName { get; set; }

    public string? Dob { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailId { get; set; }

    public string? Id { get; set; }

    public string? IdNumber { get; set; }

    public string? JoinDate { get; set; }

    public string? ReportingOfficer { get; set; }

    public string? Remarks { get; set; }

    public string? Photofilename { get; set; }

    public byte[]? Photo { get; set; }

    public string? Addressfilename { get; set; }

    public byte[]? Addressproof { get; set; }

    public string? EmpStatus { get; set; }

    public string? ResignDate { get; set; }

    public string? Idfilename { get; set; }

    public byte[]? Idproof { get; set; }

    public string? Division { get; set; }

    public int? RoleId { get; set; }
}

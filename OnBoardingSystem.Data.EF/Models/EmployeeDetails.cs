using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class EmployeeDetails
{
    public int EmpId { get; set; }

    public string? EmpCode { get; set; }

    /// <summary>
    /// required,alphanumeric
    /// </summary>
    public string? EmpName { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? Designation { get; set; }

    /// <summary>
    /// required,alphanumeric
    /// </summary>
    public string? FName { get; set; }

    /// <summary>
    /// required,alphanumeric
    /// </summary>
    public string? MName { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? Dob { get; set; }

    /// <summary>
    /// required,alphanumeric
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// number
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// required,number,maxlength,minlength
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// required,email
    /// </summary>
    public string? EmailId { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? JoinDate { get; set; }

    /// <summary>
    /// required,alphabet
    /// </summary>
    public string? ReportingOfficer { get; set; }

    public string? Remarks { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? EmpStatus { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? ResignDate { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? Division { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }
}

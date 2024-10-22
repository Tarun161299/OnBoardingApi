//-----------------------------------------------------------------------
// <copyright file="EmployeeDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class EmployeeDetails
    {
		public int? EmpId{ get; set; }
	    public string? EmpCode{ get; set; }
	    public string? EmpName{ get; set; }
	    public string? Designation{ get; set; }
	    public string? FName{ get; set; }
	    public string? MName{ get; set; }
	    public string? Dob{ get; set; }
	    public string? Address{ get; set; }
	    public string? PhoneNumber{ get; set; }
	    public string? MobileNumber{ get; set; }
	    public string? EmailId{ get; set; }
	    public string? Id{ get; set; }
	    public string? IdNumber{ get; set; }
	    public string? JoinDate{ get; set; }
	    public string? ReportingOfficer{ get; set; }
	    public string? Remarks{ get; set; }
	    public string? EmpStatus{ get; set; }
	    public string? ResignDate{ get; set; }
	    public string? Division{ get; set; }
	    public DateTime? SubmitTime{ get; set; }
	    public string? Ipaddress{ get; set; }
        public string? UploadaddressProof { get; set; }
	    public string? UploadIdDocument { get; set; }
        public string? UploadFile { get; set; }
		public DateTime? workorderTo { get; set;}
    }
}
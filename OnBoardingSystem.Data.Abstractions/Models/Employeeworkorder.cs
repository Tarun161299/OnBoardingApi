//-----------------------------------------------------------------------
// <copyright file="EmployeeWorkOrder.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class EmployeeWorkOrder
    {
        public string EmpCode { get; set; }
        public string WorkorderNo { get; set; }
        public string EmpName { get; set; }
        public string agencyName { get; set; }
        public DateTime? workorderFrom { get; set; }
        public DateTime? workorderTo { get; set; }
    }
}
//-----------------------------------------------------------------------
// <copyright file="WorkOrderDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class WorkOrderDetails
    {
        public int WorkorderId { get; set; }
        public string WorkorderNo { get; set; }
        public string ProjectCode { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? AgencyName { get; set; }
        public string? ResourceCategory { get; set; }
        public string? ResourceNo { get; set; }
        public string? NoofMonths { get; set; }
        public DateTime? WorkorderFrom { get; set; }
        public DateTime? WorkorderTo { get; set; }
        public string? DocName { get; set; }
        public string? Document { get; set; }

    }
}
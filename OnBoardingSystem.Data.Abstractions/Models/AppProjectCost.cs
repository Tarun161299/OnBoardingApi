//-----------------------------------------------------------------------
// <copyright file="AppProjectDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectCost
    {
        [Required(ErrorMessage = "ProjectCode is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "FinancialComponentId is required")]
        public int FinancialComponentId { get; set; }

        [RegularExpression("^[0-99999999]\\d*(\\.\\d+)?$", ErrorMessage = "Invalid Website")]
        public decimal Amount { get; set; }

        public string? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsActive { get; set; }

    }
}

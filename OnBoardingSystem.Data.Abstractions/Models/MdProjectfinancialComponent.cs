//-----------------------------------------------------------------------
// <copyright file="MdProjectfinancialComponent.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{

    public class MdProjectFinancialComponents
    {
        public int FinancialComponentId { get; set; }

        public string FinancialComponent { get; set; } = null!;

        public int ParentId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool? IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectExpenditure
    {
        public string? FinancialComponent { get; set; } = null!;

        public int? ProjectId { get; set; }

        public int? FinancialComponentId { get; set; }

        public decimal? Amount { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }

    }
}

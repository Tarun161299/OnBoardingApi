using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdProjectFinancialComponents
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

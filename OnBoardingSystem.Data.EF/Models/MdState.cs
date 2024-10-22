using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdState
{
    public string Id { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<MdDistrict> MdDistrict { get; } = new List<MdDistrict>();
}

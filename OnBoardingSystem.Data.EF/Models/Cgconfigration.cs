using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class Cgconfigration
{
    public string? TableName { get; set; }

    public string? CoulmnName { get; set; }

    public string? DisplayCaption { get; set; }

    public string? Validation { get; set; }

    public string? ControlType { get; set; }

    public bool? IsVisibleInGrid { get; set; }

    public bool? IsVisibleInForm { get; set; }

    public bool? IsReadOnlyForm { get; set; }

    public string? MasterDataProviderTable { get; set; }
}

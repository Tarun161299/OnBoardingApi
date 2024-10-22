using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdWorkOrderAgency
{
    public int Id { get; set; }

    public string? AgencyName { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class EmployeeWorkOrder
{
    public string EmpCode { get; set; } = null!;

    public string WorkorderNo { get; set; } = null!;
}

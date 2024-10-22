using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdIdType
{
    public string Id { get; set; } = null!;

    public string? IdType { get; set; }
}

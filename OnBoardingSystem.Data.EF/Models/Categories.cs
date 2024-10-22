using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class Categories
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime Created { get; set; }

    public long Author { get; set; }

    public DateTime? Modified { get; set; }

    public long Editor { get; set; }
}

using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstDocumentType
{
    public string DocumentTypeId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? AlternateNames { get; set; }
}

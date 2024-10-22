using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdSmsEmailTemplate
{
    public string TemplateId { get; set; } = null!;

    public string? Description { get; set; }

    public string? MessageTypeId { get; set; }

    public string? MessageSubject { get; set; }

    public string? MessageTemplate { get; set; }

    public string? RegisteredTemplateId { get; set; }
}

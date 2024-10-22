using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstQualifyingExamStream
{
    public string QualStreamId { get; set; } = null!;

    public string? QualStreamName { get; set; }
}

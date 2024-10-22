using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppDocumentUploadedDetail14102023
{
    public int DocumentId { get; set; }

    public string? Activityid { get; set; }

    public string RequestNo { get; set; } = null!;

    public string? CycleId { get; set; }

    public string DocType { get; set; } = null!;

    public string? DocId { get; set; }

    public string? DocSubject { get; set; }

    public string? DocContent { get; set; }

    public string? ObjectId { get; set; }

    public string? ObjectUrl { get; set; }

    public string? DocNatureId { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? SubTime { get; set; }

    public string? CreatedBy { get; set; }
}

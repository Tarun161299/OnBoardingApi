using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class RequestListInfo
{
    public int Id { get; set; }

    public string? RequestId { get; set; }

    public string? AgencyType { get; set; }

    public string? OranizationName { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }
}

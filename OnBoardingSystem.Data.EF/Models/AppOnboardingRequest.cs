using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppOnboardingRequest
{
    public string RequestNo { get; set; } = null!;

    public int? AgencyTypeId { get; set; }

    public string? Services { get; set; }

    public int? SessionYear { get; set; }

    public int? MinistryId { get; set; }

    public string? MinistryOther { get; set; }

    public int? OrganizationId { get; set; }

    public string? OrganizationOther { get; set; }

    public int? AgencyStateId { get; set; }

    public string? Address { get; set; }

    public int? StateId { get; set; }

    public int? DistrictId { get; set; }

    public string? PinCode { get; set; }

    public string? ContactPerson { get; set; }

    public string? Designation { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public string? CurrentStage { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }

    public string? Remarks { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string? Status { get; set; }

    public string? IsActive { get; set; }
}

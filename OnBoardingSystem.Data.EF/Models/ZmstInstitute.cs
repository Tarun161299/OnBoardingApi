using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class ZmstInstitute
{
    /// <summary>
    /// required
    /// </summary>
    public string InstCd { get; set; } = null!;

    /// <summary>
    /// required
    /// </summary>
    public string InstNm { get; set; } = null!;

    public string? AbbrNm { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string InstTypeId { get; set; } = null!;

    public string? SeatType { get; set; }

    public string? InstAdd { get; set; }

    /// <summary>
    /// required
    /// </summary>
    public string? State { get; set; }

    public string? District { get; set; }

    /// <summary>
    /// number,maxlength
    /// </summary>
    public string? Pincode { get; set; }

    /// <summary>
    /// number
    /// </summary>
    public string? InstPhone { get; set; }

    public string? InstFax { get; set; }

    /// <summary>
    /// url
    /// </summary>
    public string? InstWebSite { get; set; }

    /// <summary>
    /// email
    /// </summary>
    public string? EmailId { get; set; }

    public string? AltEmailId { get; set; }

    /// <summary>
    /// alphabet
    /// </summary>
    public string? ContactPerson { get; set; }

    /// <summary>
    /// alphabet
    /// </summary>
    public string? Designation { get; set; }

    /// <summary>
    /// number,maxlength
    /// </summary>
    public string? MobileNo { get; set; }

    public string? Aishe { get; set; }

    public string? OldInstituteCode { get; set; }
}

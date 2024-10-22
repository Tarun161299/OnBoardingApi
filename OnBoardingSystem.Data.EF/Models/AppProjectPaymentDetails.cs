using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppProjectPaymentDetails
{
    public int PaymentId { get; set; }

    /// <summary>
    /// Request No or any other 
    /// </summary>
    public string? PaymentParentRefId { get; set; }

    public decimal? Amount { get; set; }

    public string? Utrno { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? IncomeTax { get; set; }

    public decimal? Gst { get; set; }

    public decimal? Tds { get; set; }

    public string? Status { get; set; }

    public string? Ipaddress { get; set; }

    public DateTime? SubmitTime { get; set; }
}

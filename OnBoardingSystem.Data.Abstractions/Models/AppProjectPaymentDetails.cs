//-----------------------------------------------------------------------
// <copyright file="AppProjectPaymentDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectPaymentDetails
    {
        public int PaymentId { get; set; }
        public string? PaymentParentRefId { get; set; }
        public decimal? Amount { get; set; }
        public string? UTRNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? GST { get; set; }
        public decimal? TDS { get; set; }
        public string? Status { get; set; }
        public string? IPAddress { get; set; }
        public DateTime? SubmitTime { get; set; }

    }
}
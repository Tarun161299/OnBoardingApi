//-----------------------------------------------------------------------
// <copyright file="PIDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class PIDetails
    {
        public string RequestNo { get; set; }

        public string? ProjectCode { get; set; }

        public string Nicsipino { get; set; }

        public DateTime Pidate { get; set; }

        public decimal Piamount { get; set; }

        public string FileContent { get; set; }

        public string DocType { get; set; }

        public string fileContentCover { get; set; }

        public string docTypeCover { get; set; }

        public string fileContentProposal { get; set; }

        public string docTypeProposal { get; set; }

        public string? ModifyBy { get; set; }

        public string? ModifyOn { get; set; }
    }
}

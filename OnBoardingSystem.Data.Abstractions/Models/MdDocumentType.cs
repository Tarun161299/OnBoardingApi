//-----------------------------------------------------------------------
// <copyright file="MdDocumentType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdDocumentType
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Format { get; set; }

        public string? MinSize { get; set; }

        public string? MaxSize { get; set; }

        public int? DisplayPriority { get; set; }

        public string? DocumentNatureType { get; set; }

        public string? DocumentNatureTypeDesc { get; set; }

        public string? IsPasswordProtected { get; set; }

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }
}

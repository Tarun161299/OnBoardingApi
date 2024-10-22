//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadAndDocumentType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppDocumentUploadAndDocumentType
    {
        public string? DocType { get; set; }
        public string? IsVisible { get; set; }
        public string? DocTitle { get; set; }
        public int? DocumentId { get; set; }
        public string? DocContent { get; set; }

        public DateTime? submit { get; set; }

        public string? createdby { get; set; }

        public string? CycleId { get; set; }

        public string? CycleName { get; set; }

        public string? projectNo { get; set; }

        public string ? projectName { get; set;}
        public string? Activity { get; set; }

        public string? DocSubject { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailHistoty.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppDocumentUploadedDetailHistoty
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public string? Activityid { get; set; }

        public string ModuleRefId { get; set; } = null!;

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

        public int? VersionNo { get; set; }
    }
}

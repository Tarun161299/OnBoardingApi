//-----------------------------------------------------------------------
// <copyright file="ZmstDocumentType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstDocumentType
    {
        public string DocumentTypeId { get; set; }
        public string Title { get; set; }
        public string? AlternateNames { get; set; }

    }
}
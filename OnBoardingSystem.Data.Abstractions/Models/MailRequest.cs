//-----------------------------------------------------------------------
// <copyright file="MailRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? CCMail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? Attachment { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}

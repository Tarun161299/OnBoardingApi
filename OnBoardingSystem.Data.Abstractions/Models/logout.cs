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
    public class logout
    {
        public string? userId { get; set; }
        public string? refreshToken { get; set; }

        public string? token { get; set; }
    }
}

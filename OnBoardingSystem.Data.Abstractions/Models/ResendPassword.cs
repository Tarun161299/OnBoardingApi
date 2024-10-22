//-----------------------------------------------------------------------
// <copyright file="ResendPassword.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ResendPassword
    {
        /// <summary>
        /// Gets or sets MinistryId.
        /// </summary>
        public string? EncryptedRequestId { get; set; }

        /// <summary>
        /// Gets or sets MinistryName.
        /// </summary>
        public string? DecryptedEmail { get; set; }

        /// <summary>
        /// Gets or sets MinistryName.
        /// </summary>
        public string? NewPassword { get; set; }

        /// <summary>
        /// Gets or sets MinistryName.
        /// </summary>
        public string? ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets MinistryName.
        /// </summary>
        public string? RequestNumber { get; set; }

        public string? oldPassword { get; set; }

        public string? userid { get; set; }
    }
}

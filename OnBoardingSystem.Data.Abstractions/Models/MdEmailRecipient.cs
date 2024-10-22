//-----------------------------------------------------------------------
// <copyright file="MdEmailRecipient.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdEmailRecipient
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

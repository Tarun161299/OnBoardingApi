//-----------------------------------------------------------------------
// <copyright file="State.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class State
    {
        public string Id { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }
}

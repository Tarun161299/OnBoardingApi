//-----------------------------------------------------------------------
// <copyright file="MdMinistryUpsert.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdMinistryUpsert
    { /// <summary>
      /// Gets or sets MinistryId.
      /// </summary>
        public int MinistryId { get; set; }

        /// <summary>
        /// Gets or sets MinistryName.
        /// </summary>
        public string MinistryName { get; set; }

    }
}
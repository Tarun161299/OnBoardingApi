//-----------------------------------------------------------------------
// <copyright file="HashPassword.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class HashPassword
    {
        public long[] words { get; set; }
        public int sigBytes { get; set; }

    }
}
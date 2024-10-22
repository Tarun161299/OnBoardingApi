//-----------------------------------------------------------------------
// <copyright file="AppOnboardingRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
      public HashPassword hashPassword { get; set; }
     // public int sigBytes { get; set; }

    }
}

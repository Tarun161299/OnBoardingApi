//-----------------------------------------------------------------------
// <copyright file="AppLoginDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppLoginDetails
    {
		public string? RequestNo{ get; set; }
			public string? UserId{ get; set; }
			public string? UserName{ get; set; }
			public string? IsActive{ get; set; }
			public string? IsPasswordChanged{ get; set; }
			public DateTime? LastLoginTime{ get; set; }
			public string? LastLoginIP{ get; set; }
			public string? Password{ get; set; }
			public string? PasswordHistory1{ get; set; }
			public string? PasswordHistory2{ get; set; }
			public string? PasswordHistory3{ get; set; }
			public string? AuthenticationType{ get; set; }
			public string? SecurityQuestionId{ get; set; }
			public string? SecurityAnswer{ get; set; }
			public DateTime? LastFailedLoginAttemptTime{ get; set; }
			public string? LastFailedLoginAttemptIP{ get; set; }
			public int? FailedLoginAttemptCount{ get; set; }
			public DateTime? LastSuccessfulLoginTime{ get; set; }
			public string? LastSuccessfulLoginIP{ get; set; }
			public DateTime? LastPasswordChangeTime{ get; set; }
			public string? LastPasswordChangeIP{ get; set; }
			public DateTime? LastPasswordResetTime{ get; set; }
			public string? LastPasswordResetIP{ get; set; }
			public string? UserToken{ get; set; }
			public string? AccessToken{ get; set; }
            public string? Role { get; set; }
		    public string? RoleId { get; set; }
            public string? IsReadOnly { get; set; }
            public string? Assign { get; set; }

           }
		}
//-----------------------------------------------------------------------
// <copyright file="SignUp.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "UserID is required.")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequestNo is required.")]
        public string RequestNo { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        public string? Designation { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? AuthenticationType { get; set; }
        public string? SecurityQuestionId { get; set; }
        public string? SecurityAnswer { get; set; }
        public string? SecurityQuesdesc { get; set; }
        public string? AuthModedesc { get; set; }
        public string? photopath { get; set; }
    }
}

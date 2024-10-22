//-----------------------------------------------------------------------
// <copyright file="AppContactPersonDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppContactPersonDetails
    {
        public int Id { get; set; }

        public string RequestNo { get; set; }

        public string DepartmentId { get; set; }

        public string RoleId { get; set; }

        //[RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid name")]
        public string Name { get; set; }

        //[RegularExpression("^[a-zA-Z0-9_. -]*$", ErrorMessage = "Invalid designation")]
        public string Designation { get; set; }

        //[MaxLength(10)]
        //[MinLength(10)]
        public string MobileNo { get; set; }

        //[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,5}$", ErrorMessage = "Invalid emailId")]
        public string EmailId { get; set; }
    }
}

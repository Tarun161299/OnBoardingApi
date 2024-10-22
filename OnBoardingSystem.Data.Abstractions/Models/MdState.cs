//-----------------------------------------------------------------------
// <copyright file="MDModule.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdState
    {
        [MdStateValidation]
        public string Id { get; set; } = null!;

        [MdStateValidation]
        public string Description { get; set; } = null!;

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        //public string? IdHidden { get; set;   }
    }
    public class MdStateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var state = (MdState)validationContext.ObjectInstance;
            bool validator = false;
            if (
                (state.Id.Contains("<script>")) ||
                (state.Id.Contains("</script>")) ||
                (state.Id.Contains("<style>")) ||
                (state.Id.Contains("</style>")) ||
                (state.Description.Contains("<script>")) ||
                (state.Description.Contains("</script>")) ||
                (state.Description.Contains("<style>")) ||
                (state.Description.Contains("</style>"))
                )
            {
                validator = true;
                return new ValidationResult("script not allowed."); ;
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="District.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdDistrict// : EF.Models.MdDistrict
    {
        public string? StateName { get; set; } 

        public string StateId { get; set; }

        [MdDistrictValidation]
        public string Id { get; set; }

        [MdDistrictValidation]
        public string Description { get; set; }

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }

    public class MdDistrictValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var district = (MdDistrict)validationContext.ObjectInstance;
            bool validator = false;
            if (
                (district.Id.Contains("<script>")) || 
                (district.Id.Contains("</script>")) || 
                (district.Id.Contains("<style>")) ||
                (district.Id.Contains("</style>")) ||
                (district.Description.Contains("<script>")) || 
                (district.Description.Contains("</script>")) || 
                (district.Description.Contains("<style>")) || 
                (district.Description.Contains("</style>"))
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

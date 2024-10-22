//-----------------------------------------------------------------------
// <copyright file="MdOrganization.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdOrganization
    {
        [MdOrganizationValidation]
        public string OrganizationId { get; set; } = null!;

        [MdOrganizationValidation]
        public string OrganizationName { get; set; } = null!;

        public string? StateId { get; set; }
        public string? StateName { get; set; } = null;
       
    }

    public class MdOrganizationValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var organization = (MdOrganization)validationContext.ObjectInstance;
            bool validator = false;
            if (
                (organization.OrganizationId.Contains("<script>")) ||
                (organization.OrganizationId.Contains("</script>")) ||
                (organization.OrganizationId.Contains("<style>")) ||
                (organization.OrganizationId.Contains("</style>")) ||
                (organization.OrganizationName.Contains("<script>")) ||
                (organization.OrganizationName.Contains("</script>")) ||
                (organization.OrganizationName.Contains("<style>")) ||
                (organization.OrganizationName.Contains("</style>"))
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

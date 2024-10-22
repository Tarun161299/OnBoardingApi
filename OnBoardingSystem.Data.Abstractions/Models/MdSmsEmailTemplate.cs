//-----------------------------------------------------------------------
// <copyright file="MdSmsEmailTemplate.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdSmsEmailTemplate
    {
        [smsEmailScripValidation]
        public string TemplateId { get; set; } = null!;
        [smsEmailScripValidation]
        public string? Description { get; set; }
        [smsEmailScripValidation]
        public string? MessageTypeId { get; set; }
        [smsEmailScripValidation]
        public string? MessageSubject { get; set; }
        [smsEmailScripValidation]
        public string? MessageTemplate { get; set; }

        public string RegisteredTemplateId { get; set; }
    }

    public class smsEmailScripValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var msgtemplate = (MdSmsEmailTemplate)validationContext.ObjectInstance;
            bool validator = false;
            if (((msgtemplate.TemplateId.Contains("<script>")) || (msgtemplate.TemplateId.Contains("</script>")) || ((msgtemplate.TemplateId.Contains("<style>")) || msgtemplate.TemplateId.Contains("<style>")))||
                ((msgtemplate.Description.Contains("<script>")) || (msgtemplate.Description.Contains("</script>")) || ((msgtemplate.Description.Contains("<style>")) || msgtemplate.Description.Contains("<style>")))||
                ((msgtemplate.MessageTypeId.Contains("<script>")) || (msgtemplate.MessageTypeId.Contains("</script>")) || ((msgtemplate.MessageTypeId.Contains("<style>")) || msgtemplate.MessageTypeId.Contains("<style>")))||
                ((msgtemplate.MessageSubject.Contains("<script>")) || (msgtemplate.MessageSubject.Contains("</script>")) || ((msgtemplate.MessageSubject.Contains("<style>")) || msgtemplate.MessageSubject.Contains("<style>")))||
                ((msgtemplate.MessageTemplate.Contains("<script>")) || (msgtemplate.MessageTemplate.Contains("</script>")) || ((msgtemplate.MessageTemplate.Contains("<style>")) || msgtemplate.MessageTemplate.Contains("<style>"))))
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
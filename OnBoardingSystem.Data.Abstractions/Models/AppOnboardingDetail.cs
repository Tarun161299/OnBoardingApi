//-----------------------------------------------------------------------
// <copyright file="AppOnboardingRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public partial class AppOnboardingDetails
    {
        [Required(ErrorMessage = "RequestNo is required")]
        public string RequestNo { get; set; } = null!;

        //[RegularExpression("^((https?|ftp|smtp)://)?(www.)?[a-z0-9]+.[a-z]+(/[a-zA-Z0-9#]+/?)*$", ErrorMessage = "Invalid Website")]

        public string? Website { get; set; }

        public string? YearOfFirstTimeAffilitionSession { get; set; }

        public string? ExamLastSessionConductedIn { get; set; }

        [RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid Exam Last Session Tech SupportBy")]
        public string? ExamLastSessionTechSupportBy { get; set; }

        [ScriptValidations]
        public string? ExamLastSessionDescription { get; set; }

        public string? CounsLastSessionConductedIn { get; set; }

        [RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid Couns Last Session Tech SupportBy")]
        public string? CounsLastSessionTechSupportBy { get; set; }

        [ScriptValidation]
        public string? CounsLastSessionDescription { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Exam Expected Applicant is invalid")]
        public int? ExamExpectedApplicant { get; set; }

        //[RegularExpression("^([A-Za-z. ]+.,[a-zA-z. ])+1|[A-Za-z. ]+$", ErrorMessage = "Invalid Exam Course List")]
        public string? ExamCourseList { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Exam Total Course")]
        public int? ExamTotalCourse { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExamTentativeScheduleStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExamTentativeScheduleEnd { get; set; }

        public bool? ExamDissimilarityOfSchedule { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Couns Expected Applicant is invalid")]
        public int? CounsExpectedApplicant { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Couns Expected Seat is invalid")]
        public int? CounsExpectedSeat { get; set; }

       // [RegularExpression("^([A-Za-z. ]+.,[a-zA-z. ])+1|[A-Za-z. ]+$", ErrorMessage = "Invalid Couns Course List")]
        public string? CounsCourseList { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Couns Total Course")]
        public int? CounsTotalCourse { get; set; }

        public int? CounsExpectedRound { get; set; }

        public int? CounsExpectedSpotRound { get; set; }

        public int? CounsExpectedParticipatingInstitute { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CounsTentativeScheduleStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CounsTentativeScheduleEnd { get; set; }

        public bool? CounsDissimilarityOfSchedule { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SubmitTime { get; set; }

        public string? Ipaddress { get; set; }

        public string? Status { get; set; }

        public string? Remarks { get; set; }

        public string? IsActive { get; set; }

        [Required(ErrorMessage = "Mode is required")]
        public string? Mode { get; set; }

        public string? CoordinatorMail { get; set; }

        public string? HodMail { get; set; }

        [Required(ErrorMessage = "File Content is required")]
        public byte[] AttachFilecontent { get; set; }

        public AppContactPersonDetails[] contactdetails { get; set; }

    }

    public class ScriptValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var description = (AppOnboardingDetails)validationContext.ObjectInstance;
            bool validator = false;
            if (description.CounsLastSessionDescription != null)
            {
                if ((description.CounsLastSessionDescription.Contains("<script>")) || (description.CounsLastSessionDescription.Contains("</script>")) || ((description.CounsLastSessionDescription.Contains("<style>")) || description.CounsLastSessionDescription.Contains("<style>")))
                {
                    validator = true;
                    return new ValidationResult("script not allowed."); ;
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else { return ValidationResult.Success; }
        }
    }
    public class ScriptValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var description = (AppOnboardingDetails)validationContext.ObjectInstance;
            bool validator = false;
            if (description.ExamLastSessionDescription != null)
            {
                if ((description.ExamLastSessionDescription.Contains("<script>")) || (description.ExamLastSessionDescription.Contains("</script>")) || ((description.ExamLastSessionDescription.Contains("<style>")) || description.ExamLastSessionDescription.Contains("<style>")))
                {
                    validator = true;
                    return new ValidationResult("script not allowed.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="AppOnboardingResponse.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    using CommonModels = OnBoardingSystem.Common.Models;
    public class AppOnboardRequestAndDetail : OnBoardingRequestDetail
    {
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }

        public string? state { get; set; } = null;

        public string? MinistryName { get; set; }

        public string? AgencyType { get; set; }

        public string? docContent { get; set; }

        public string? Services { get; set; }

        public string? OranizationName { get; set; }

        public string? Designation { get; set; }

        public string? Website { get; set; }

        public int? MinistryId { get; set; }

        public int? ExamExpectedApplicant { get; set; }

        public DateTime? ExamTentativeScheduleStart { get; set; }

        public DateTime? ExamTentativeScheduleEnd { get; set; }

        public int? CounsExpectedApplicant { get; set; }

        public int? CounsExpectedSeat { get; set; }

        public int? CounsExpectedRound { get; set; }

        public int? CounsExpectedSpotRound { get; set; }

        public int? CounsExpectedParticipatingInstitute { get; set; }

        public DateTime? CounsTentativeScheduleStart { get; set; }

        public DateTime? CounsTentativeScheduleEnd { get; set; }

        public bool? CounsDissimilarityOfSchedule { get; set; }

        public string? Status { get; set; }

        public string? Remarks { get; set; }

        public string? IsActive { get; set; }
        public string? OnBoardingDetailsStatus { get; set; }

        public string? CoordinatorName { get; set; }
        public string? CoordinatorDesignation { get; set; }
        public string? CoordinatorEmail { get; set; }
        public string? CoordinatorPhone { get; set; }
        public List<CommonModels.MDStatus> MDStatusList { get; set; }

    }
}
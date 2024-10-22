using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppOnboardingDetails
{
    public string RequestNo { get; set; } = null!;

    public string? Website { get; set; }

    public string? YearOfFirstTimeAffilitionSession { get; set; }

    public string? ExamLastSessionConductedIn { get; set; }

    public string? ExamLastSessionTechSupportBy { get; set; }

    public string? ExamLastSessionDescription { get; set; }

    public string? CounsLastSessionConductedIn { get; set; }

    public string? CounsLastSessionTechSupportBy { get; set; }

    public string? CounsLastSessionDescription { get; set; }

    public int? ExamExpectedApplicant { get; set; }

    public string? ExamCourseList { get; set; }

    public int? ExamTotalCourse { get; set; }

    public DateTime? ExamTentativeScheduleStart { get; set; }

    public DateTime? ExamTentativeScheduleEnd { get; set; }

    public bool? ExamDissimilarityOfSchedule { get; set; }

    public int? CounsExpectedApplicant { get; set; }

    public int? CounsExpectedSeat { get; set; }

    public string? CounsCourseList { get; set; }

    public int? CounsTotalCourse { get; set; }

    public int? CounsExpectedRound { get; set; }

    public int? CounsExpectedSpotRound { get; set; }

    public int? CounsExpectedParticipatingInstitute { get; set; }

    public DateTime? CounsTentativeScheduleStart { get; set; }

    public DateTime? CounsTentativeScheduleEnd { get; set; }

    public bool? CounsDissimilarityOfSchedule { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Ipaddress { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public string? IsActive { get; set; }
}

//-----------------------------------------------------------------------
// <copyright file="MappingProfile.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business
{
    using AutoMapper;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using CommonModels = OnBoardingSystem.Common.Models;

    /// <summary>
    /// Automapper configuration.
    /// </summary>
	public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            OnBoardingMapping();
        }

        /// <summary>
        /// MdMinistry Mapping.
        /// </summary>
        private void OnBoardingMapping()
        {
            CreateMap<EF.Models.MdMinistry, AbsModels.MdMinistry>();
            CreateMap<AbsModels.MdMinistry, EF.Models.MdMinistry>();

            CreateMap<EF.Models.RequestListInfo, AbsModels.RequestList>();
            CreateMap<AbsModels.RequestList, EF.Models.RequestListInfo>();

            CreateMap<EF.Models.MdOrganization, AbsModels.MdOrganization>();
            CreateMap<AbsModels.MdOrganization, EF.Models.MdOrganization>();

            CreateMap<EF.Models.AppOnboardingRequest, AbsModels.OnBoardingRequestDetail>();
            CreateMap<AbsModels.OnBoardingRequestDetail, EF.Models.AppOnboardingRequest>();

            CreateMap<EF.Models.AppOnboardingResponse, AbsModels.AppOnboardingResponse>();
            CreateMap<AbsModels.AppOnboardingResponse, EF.Models.AppOnboardingResponse>();

            CreateMap<EF.Models.AppOnboardingRequest, AbsModels.AppOnboardingRequest>();
            CreateMap<AbsModels.AppOnboardingRequest, EF.Models.AppOnboardingRequest>();

            CreateMap<EF.Models.MdServiceType, AbsModels.MdServiceType>();
            CreateMap<AbsModels.MdServiceType, EF.Models.MdServiceType>();

            CreateMap<EF.Models.AppOnboardingDetails, AbsModels.AppOnboardingDetails>();
            CreateMap<AbsModels.AppOnboardingDetails, EF.Models.AppOnboardingDetails>();

            CreateMap<EF.Models.AppLoginDetails, AbsModels.AppAdminLoginDetails>();
            CreateMap<AbsModels.AppAdminLoginDetails, EF.Models.AppLoginDetails>();

            CreateMap<EF.Models.AppContactPersonDetails, AbsModels.AppContactPersonDetails>();
            CreateMap<AbsModels.AppContactPersonDetails, EF.Models.AppContactPersonDetails>();

            CreateMap<EF.Models.MdSmsEmailTemplate, AbsModels.MdSmsEmailTemplate>();
            CreateMap<AbsModels.MdSmsEmailTemplate, EF.Models.MdSmsEmailTemplate>();

            CreateMap<EF.Models.MdState, AbsModels.State>();
            CreateMap<AbsModels.State, EF.Models.MdState>();

            CreateMap<EF.Models.MdDistrict, AbsModels.MdDistrict>();
            CreateMap<AbsModels.MdDistrict, EF.Models.MdDistrict>();

            CreateMap<EF.Models.MdEmailRecipient, AbsModels.MdEmailRecipient>();
            CreateMap<AbsModels.MdEmailRecipient, EF.Models.MdEmailRecipient>();

            CreateMap<EF.Models.AppProjectDetails, AbsModels.AppProjectDetails>();
            CreateMap<AbsModels.AppProjectDetails, EF.Models.AppProjectDetails>();

            CreateMap<EF.Models.MdAgency, AbsModels.MdAgency>();
            CreateMap<AbsModels.MdAgency, EF.Models.MdAgency>();

            CreateMap<EF.Models.ZmstServiceType, AbsModels.ZmstServiceType>();
            CreateMap<AbsModels.ZmstServiceType, EF.Models.ZmstServiceType>();

            CreateMap<EF.Models.MdDocumentType, AbsModels.MdDocumentType>();
            CreateMap<AbsModels.MdDocumentType, EF.Models.MdDocumentType>();

            CreateMap<EF.Models.ZmstAgencyExamCouns, AbsModels.ZmstAgencyExamCouns>();
            CreateMap<AbsModels.ZmstAgencyExamCouns, EF.Models.ZmstAgencyExamCouns>();

            CreateMap<EF.Models.ZmstProjects, AbsModels.ZmstProjects>();
            CreateMap<AbsModels.ZmstProjects, EF.Models.ZmstProjects>();

            CreateMap<EF.Models.MdProjectFinancialComponents, AbsModels.MdProjectFinancialComponents>();
            CreateMap<AbsModels.MdProjectFinancialComponents, EF.Models.MdProjectFinancialComponents>();

            CreateMap<EF.Models.AppProjectCost, AbsModels.AppProjectCost>();
            CreateMap<AbsModels.AppProjectCost, EF.Models.AppProjectCost>();

            CreateMap<EF.Models.AppDocumentUploadedDetail, AbsModels.AppDocumentUploadedDetail>();
            CreateMap<AbsModels.AppDocumentUploadedDetail, EF.Models.AppDocumentUploadedDetail>();

            CreateMap<EF.Models.MdModule, AbsModels.MDModule>();
            CreateMap<AbsModels.MDModule, EF.Models.MdModule>();

            CreateMap<EF.Models.AppUserRoleMapping, AbsModels.AppUserRoleMapping>();
            CreateMap<AbsModels.AppUserRoleMapping, EF.Models.AppUserRoleMapping>();

            CreateMap<EF.Models.AppRoleModulePermission, AbsModels.AppRoleModulePermission>();
            CreateMap<AbsModels.AppRoleModulePermission, EF.Models.AppRoleModulePermission>();

            CreateMap<EF.Models.MdModule, AbsModels.MDModuleData>();
            CreateMap<AbsModels.MDModuleData, EF.Models.MdModule>();

            CreateMap<EF.Models.MdDistrict, AbsModels.MdDistrict>();
            CreateMap<AbsModels.MdDistrict, EF.Models.MdDistrict>();

            CreateMap<EF.Models.MdState, AbsModels.MdState>();
            CreateMap<AbsModels.MdState, EF.Models.MdState>();

            CreateMap<EF.Models.ZmstCountry, AbsModels.ZmstCountry>();
            CreateMap<AbsModels.ZmstCountry, EF.Models.ZmstCountry>();

            CreateMap<EF.Models.AppDocumentUploadedDetail, AbsModels.AppDocumentUploadAndDocumentType>();
            CreateMap<AbsModels.AppDocumentUploadAndDocumentType, EF.Models.AppDocumentUploadedDetail>();

            CreateMap<EF.Models.MdSmsEmailTemplate, AbsModels.MdSmsEmailTemplate>();
            CreateMap<AbsModels.MdSmsEmailTemplate, EF.Models.MdSmsEmailTemplate>();

            CreateMap<EF.Models.ApplicationSummary, AbsModels.ApplicationSummary>();
            CreateMap<AbsModels.ApplicationSummary, EF.Models.ApplicationSummary>();

            CreateMap<EF.Models.ApplicationSchedule, AbsModels.ApplicationSchedule>();
            CreateMap<AbsModels.ApplicationSchedule, EF.Models.ApplicationSchedule>();

            CreateMap<EF.Models.ApplicationDayWise, AbsModels.ApplicationDayWise>();
            CreateMap<AbsModels.ApplicationDayWise, EF.Models.ApplicationDayWise>();

            CreateMap<EF.Models.AppDocumentTypeRoleMapping, AbsModels.AppDocumentTypeRoleMapping>();
            CreateMap<AbsModels.AppDocumentTypeRoleMapping, EF.Models.AppDocumentTypeRoleMapping>();

            CreateMap<EF.Models.ZmstWillingness, AbsModels.ZmstWillingness>();
            CreateMap<AbsModels.ZmstWillingness, EF.Models.ZmstWillingness>();

            CreateMap<EF.Models.ZmstSubject, AbsModels.ZmstSubject>();
            CreateMap<AbsModels.ZmstSubject, EF.Models.ZmstSubject>();

            CreateMap<EF.Models.ZmstAgency, AbsModels.ZmstAgency>();
            CreateMap<AbsModels.ZmstAgency, EF.Models.ZmstAgency>();

            CreateMap<EF.Models.ZmstBranch, AbsModels.ZmstBranch>();
            CreateMap<AbsModels.ZmstBranch, EF.Models.ZmstBranch>();

            CreateMap<EF.Models.ZmstCategory, AbsModels.ZmstCategory>();
            CreateMap<AbsModels.ZmstCategory, EF.Models.ZmstCategory>();

            CreateMap<EF.Models.ZmstCourseApplied, AbsModels.ZmstCourseApplied>();
            CreateMap<AbsModels.ZmstCourseApplied, EF.Models.ZmstCourseApplied>();

            CreateMap<EF.Models.ZmstCourseAppliedLevel, AbsModels.ZmstCourseAppliedLevel>();
            CreateMap<AbsModels.ZmstCourseAppliedLevel, EF.Models.ZmstCourseAppliedLevel>();

            CreateMap<EF.Models.ZmstDistrict, AbsModels.ZmstDistrict>();
            CreateMap<AbsModels.ZmstDistrict, EF.Models.ZmstDistrict>();

            CreateMap<EF.Models.ZmstDocumentType, AbsModels.ZmstDocumentType>();
            CreateMap<AbsModels.ZmstDocumentType, EF.Models.ZmstDocumentType>();

            CreateMap<EF.Models.ZmstExamType, AbsModels.ZmstExamType>();
            CreateMap<AbsModels.ZmstExamType, EF.Models.ZmstExamType>();

            CreateMap<EF.Models.ZmstFeeType, AbsModels.ZmstFeeType>();
            CreateMap<AbsModels.ZmstFeeType, EF.Models.ZmstFeeType>();

            CreateMap<EF.Models.ZmstGender, AbsModels.ZmstGender>();
            CreateMap<AbsModels.ZmstGender, EF.Models.ZmstGender>();

            CreateMap<EF.Models.ZmstIdentityType, AbsModels.ZmstIdentityType>();
            CreateMap<AbsModels.ZmstIdentityType, EF.Models.ZmstIdentityType>();

            CreateMap<EF.Models.ZmstMinimumQualification, AbsModels.ZmstMinimumQualification>();
            CreateMap<AbsModels.ZmstMinimumQualification, EF.Models.ZmstMinimumQualification>();

            CreateMap<EF.Models.ZmstNationality, AbsModels.ZmstNationality>();
            CreateMap<AbsModels.ZmstNationality, EF.Models.ZmstNationality>();

            CreateMap<EF.Models.ZmstPassingStatus, AbsModels.ZmstPassingStatus>();
            CreateMap<AbsModels.ZmstPassingStatus, EF.Models.ZmstPassingStatus>();

            CreateMap<EF.Models.ZmstQualification, AbsModels.ZmstQualification>();
            CreateMap<AbsModels.ZmstQualification, EF.Models.ZmstQualification>();

            CreateMap<EF.Models.ZmstQualifyingCourse, AbsModels.ZmstQualifyingCourse>();
            CreateMap<AbsModels.ZmstQualifyingCourse, EF.Models.ZmstQualifyingCourse>();

            CreateMap<EF.Models.ZmstQualifyingExam, AbsModels.ZmstQualifyingExam>();
            CreateMap<AbsModels.ZmstQualifyingExam, EF.Models.ZmstQualifyingExam>();

            CreateMap<EF.Models.ZmstQualifyingExamBoard, AbsModels.ZmstQualifyingExamBoard>();
            CreateMap<AbsModels.ZmstQualifyingExamBoard, EF.Models.ZmstQualifyingExamBoard>();

            CreateMap<EF.Models.ZmstQualifyingExamFrom, AbsModels.ZmstQualifyingExamFrom>();
            CreateMap<AbsModels.ZmstQualifyingExamFrom, EF.Models.ZmstQualifyingExamFrom>();

            CreateMap<EF.Models.ZmstQualifyingExamLearningMode, AbsModels.ZmstQualifyingExamLearningMode>();
            CreateMap<AbsModels.ZmstQualifyingExamLearningMode, EF.Models.ZmstQualifyingExamLearningMode>();

            CreateMap<EF.Models.ZmstQualifyingExamResultMode, AbsModels.ZmstQualifyingExamResultMode>();
            CreateMap<AbsModels.ZmstQualifyingExamResultMode, EF.Models.ZmstQualifyingExamResultMode>();

            CreateMap<EF.Models.ZmstQualifyingExamStream, AbsModels.ZmstQualifyingExamStream>();
            CreateMap<AbsModels.ZmstQualifyingExamStream, EF.Models.ZmstQualifyingExamStream>();

            CreateMap<EF.Models.ZmstQuesPaper, AbsModels.ZmstQuesPaper>();
            CreateMap<AbsModels.ZmstQuesPaper, EF.Models.ZmstQuesPaper>();

            CreateMap<EF.Models.ZmstQuestionPaperMedium, AbsModels.ZmstQuestionPaperMedium>();
            CreateMap<AbsModels.ZmstQuestionPaperMedium, EF.Models.ZmstQuestionPaperMedium>();

            CreateMap<EF.Models.ZmstQuota, AbsModels.ZmstQuota>();
            CreateMap<AbsModels.ZmstQuota, EF.Models.ZmstQuota>();

            CreateMap<EF.Models.ZmstRankType, AbsModels.ZmstRankType>();
            CreateMap<AbsModels.ZmstRankType, EF.Models.ZmstRankType>();

            CreateMap<EF.Models.ZmstReligion, AbsModels.ZmstReligion>();
            CreateMap<AbsModels.ZmstReligion, EF.Models.ZmstReligion>();

            CreateMap<EF.Models.ZmstResidentialEligibility, AbsModels.ZmstResidentialEligibility>();
            CreateMap<AbsModels.ZmstResidentialEligibility, EF.Models.ZmstResidentialEligibility>();

            CreateMap<EF.Models.ZmstSeatCategory, AbsModels.ZmstSeatCategory>();
            CreateMap<AbsModels.ZmstSeatCategory, EF.Models.ZmstSeatCategory>();

            CreateMap<EF.Models.ZmstSeatGender, AbsModels.ZmstSeatGender>();
            CreateMap<AbsModels.ZmstSeatGender, EF.Models.ZmstSeatGender>();

            CreateMap<EF.Models.ZmstSpecialExamPaper, AbsModels.ZmstSpecialExamPaper>();
            CreateMap<AbsModels.ZmstSpecialExamPaper, EF.Models.ZmstSpecialExamPaper>();

            CreateMap<EF.Models.ZmstState, AbsModels.ZmstState>();
            CreateMap<AbsModels.ZmstState, EF.Models.ZmstState>();

            CreateMap<EF.Models.ZmstStream, AbsModels.ZmstStream>();
            CreateMap<AbsModels.ZmstStream, EF.Models.ZmstStream>();

            CreateMap<EF.Models.ZmstSubCategory, AbsModels.ZmstSubCategory>();
            CreateMap<AbsModels.ZmstSubCategory, EF.Models.ZmstSubCategory>();

            CreateMap<EF.Models.ZmstSubCategoryPriority, AbsModels.ZmstSubCategoryPriority>();
            CreateMap<AbsModels.ZmstSubCategoryPriority, EF.Models.ZmstSubCategoryPriority>();

            CreateMap<EF.Models.ZmstSymbol, AbsModels.ZmstSymbol>();
            CreateMap<AbsModels.ZmstSymbol, EF.Models.ZmstSymbol>();

            CreateMap<EF.Models.ZmstTypeofDisability, AbsModels.ZmstTypeofDisability>();
            CreateMap<AbsModels.ZmstTypeofDisability, EF.Models.ZmstTypeofDisability>();

            CreateMap<EF.Models.WorkOrderDetails, AbsModels.WorkOrderDetails>();
            CreateMap<AbsModels.WorkOrderDetails, EF.Models.WorkOrderDetails>();

            CreateMap<EF.Models.MdWorkOrderAgency, AbsModels.MdWorkOrderAgency>();
            CreateMap<AbsModels.MdWorkOrderAgency, EF.Models.MdWorkOrderAgency>();

            CreateMap<EF.Models.EmployeeDetails, AbsModels.EmployeeDetails>();
            CreateMap<AbsModels.EmployeeDetails, EF.Models.EmployeeDetails>();

            CreateMap<EF.Models.EmployeeWorkOrder, AbsModels.EmployeeWorkOrder>();
            CreateMap<AbsModels.EmployeeWorkOrder, EF.Models.EmployeeWorkOrder>();

            CreateMap<EF.Models.MdEmpStatus, AbsModels.MdEmpStatus>();
            CreateMap<AbsModels.MdEmpStatus, EF.Models.MdEmpStatus>();

            CreateMap<EF.Models.MdExam, AbsModels.MdExam>();
            CreateMap<AbsModels.MdExam, EF.Models.MdExam>();

            CreateMap<EF.Models.MdIdType, AbsModels.MdIdType>();
            CreateMap<AbsModels.MdIdType, EF.Models.MdIdType>();

            CreateMap<EF.Models.QualificationDetails, AbsModels.QualificationDetails>();
            CreateMap<AbsModels.QualificationDetails, EF.Models.QualificationDetails>();

            CreateMap<EF.Models.ZmstApplicantType, AbsModels.ZmstApplicantType>();
            CreateMap<AbsModels.ZmstApplicantType, EF.Models.ZmstApplicantType>();

            CreateMap<EF.Models.ZmstActivity, AbsModels.ZmstActivity>();
            CreateMap<AbsModels.ZmstActivity, EF.Models.ZmstActivity>();

            CreateMap<EF.Models.ZmstTrade, AbsModels.ZmstTrade>();
            CreateMap<AbsModels.ZmstTrade, EF.Models.ZmstTrade>();

            CreateMap<EF.Models.ZmstProgram, AbsModels.ZmstProgram>();
            CreateMap<AbsModels.ZmstProgram, EF.Models.ZmstProgram>();

            CreateMap<EF.Models.ZmstInstitute, AbsModels.ZmstInstitute>();
            CreateMap<AbsModels.ZmstInstitute, EF.Models.ZmstInstitute>();

            CreateMap<EF.Models.ZmstInstituteType, AbsModels.ZmstInstituteType>();
            CreateMap<AbsModels.ZmstInstituteType, EF.Models.ZmstInstituteType>();

            CreateMap<EF.Models.ZmstInstituteAgency, AbsModels.ZmstInstituteAgency>();
            CreateMap<AbsModels.ZmstInstituteAgency, EF.Models.ZmstInstituteAgency>();

            CreateMap<EF.Models.ZmstInstituteStream, AbsModels.ZmstInstituteStream>();
            CreateMap<AbsModels.ZmstInstituteStream, EF.Models.ZmstInstituteStream>();

            CreateMap<EF.Models.ZmstAgencyVirtualDirectoryMapping, AbsModels.ZmstAgencyVirtualDirectoryMapping>();
            CreateMap<AbsModels.ZmstAgencyVirtualDirectoryMapping, EF.Models.ZmstAgencyVirtualDirectoryMapping>();

            CreateMap<EF.Models.AppCaptcha, AbsModels.AppCaptcha>();
            CreateMap<AbsModels.AppCaptcha, EF.Models.AppCaptcha>();

            CreateMap<EF.Models.ZmstSeatGroup, AbsModels.ZmstSeatGroup>();
            CreateMap<AbsModels.ZmstSeatGroup, EF.Models.ZmstSeatGroup>();

            CreateMap<EF.Models.ZmstSeatSubCategory, AbsModels.ZmstSeatSubCategory>();
            CreateMap<AbsModels.ZmstSeatSubCategory, EF.Models.ZmstSeatSubCategory>();

            CreateMap<EF.Models.ZmstSeatType, AbsModels.ZmstSeatType>();
            CreateMap<AbsModels.ZmstSeatType, EF.Models.ZmstSeatType>();

            CreateMap<EF.Models.ZmstExperienceType, AbsModels.ZmstExperienceType>();
            CreateMap<AbsModels.ZmstExperienceType, EF.Models.ZmstExperienceType>();

            CreateMap<EF.Models.Administrator, AbsModels.Administrator>();
            CreateMap<AbsModels.Administrator, EF.Models.Administrator>();

            CreateMap<EF.Models.ZmstAuthenticationMode, AbsModels.ZmstAuthenticationMode>();
            CreateMap<AbsModels.ZmstAuthenticationMode, EF.Models.ZmstAuthenticationMode>();

            CreateMap<EF.Models.ZmstSecurityQuestion, AbsModels.ZmstSecurityQuestion>();
            CreateMap<AbsModels.ZmstSecurityQuestion, EF.Models.ZmstSecurityQuestion>();

            CreateMap<EF.Models.MdRole, AbsModels.MdRole>();
            CreateMap<AbsModels.MdRole, EF.Models.MdRole>();

            CreateMap<EF.Models.AppLoginDetails, AbsModels.AppLoginDetails>();
            CreateMap<AbsModels.AppLoginDetails, EF.Models.AppLoginDetails>();
            CreateMap<EF.Models.ZmstExperienceType, AbsModels.ZmstExperienceType>();
            CreateMap<AbsModels.ZmstExperienceType, EF.Models.ZmstExperienceType>();

            CreateMap<EF.Models.MdMainModule, AbsModels.MdMainModule>();
            CreateMap<AbsModels.MdMainModule, EF.Models.MdMainModule>();

            CreateMap<EF.Models.AppProjectActivity, AbsModels.AppProjectActivity>();
            CreateMap<AbsModels.AppProjectActivity, EF.Models.AppProjectActivity>();

            CreateMap<EF.Models.AppProjectActivityHistory, AbsModels.AppProjectActivityHistory>();
            CreateMap<AbsModels.AppProjectActivityHistory, EF.Models.AppProjectActivityHistory>();

            CreateMap<EF.Models.AppProjectActivityStatusTracking, AbsModels.AppProjectActivityStatusTracking>();
            CreateMap<AbsModels.AppProjectActivityStatusTracking, EF.Models.AppProjectActivityStatusTracking>();

            CreateMap<EF.Models.MdMainModule, AbsModels.MdMainModule>();
            CreateMap<AbsModels.MdMainModule, EF.Models.MdMainModule>();

            CreateMap<EF.Models.AppProjectPaymentDetails, AbsModels.AppProjectPaymentDetails>();
            CreateMap<AbsModels.AppProjectPaymentDetails, EF.Models.AppProjectPaymentDetails>();

            CreateMap<AbsModels.AppDocumentUploadedDetailHistoty, AbsModels.AppDocumentUploadedDetail>();
            CreateMap<AbsModels.AppDocumentUploadedDetail, AbsModels.AppDocumentUploadedDetailHistoty>();

            CreateMap<EF.Models.AppDocumentUploadedDetailHistoty, EF.Models.AppDocumentUploadedDetail>();
            CreateMap<EF.Models.AppDocumentUploadedDetail, EF.Models.AppDocumentUploadedDetailHistoty>();

            CreateMap<EF.Models.AppDocumentUploadedDetailHistoty, AbsModels.AppDocumentUploadedDetailHistoty>();
            CreateMap<AbsModels.AppDocumentUploadedDetailHistoty, EF.Models.AppDocumentUploadedDetailHistoty>();
            CreateMap<AbsModels.AppProjectPaymentDetails, EF.Models.AppProjectPaymentDetails>();

            CreateMap<EF.Models.MdStatus, AbsModels.MdStatus>();
            CreateMap<AbsModels.MdStatus, EF.Models.MdStatus>();

            CreateMap<EF.Models.MdActivityType, AbsModels.MdActivityType>();
            CreateMap<AbsModels.MdActivityType, EF.Models.MdActivityType>();

            CreateMap<EF.Models.ConfigurationEnvironment, AbsModels.ConfigurationEnvironment>();
            CreateMap<AbsModels.ConfigurationEnvironment, EF.Models.ConfigurationEnvironment>();

            CreateMap<EF.Models.MdYear, AbsModels.MdYear>();
            CreateMap<AbsModels.MdYear, EF.Models.MdYear>();

            CreateMap<EF.Models.MdAgencyType, AbsModels.MdAgencyType>();
            CreateMap<AbsModels.MdAgencyType, EF.Models.MdAgencyType>();

            CreateMap<EF.Models.AppProjectExpenditure, AbsModels.AppProjectExpenditure>();
            CreateMap<AbsModels.AppProjectExpenditure, EF.Models.AppProjectExpenditure>();

            CreateMap<EF.Models.ConfigurationApisecureKey, AbsModels.ConfigurationApisecureKey>();
            CreateMap<AbsModels.ConfigurationApisecureKey, EF.Models.ConfigurationApisecureKey>();

            CreateMap<AbsModels.ZmstInstitute, AbsModels.UpdateInstitute>();
        }
    }
}
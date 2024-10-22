//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using OnBoardingSystem.Data.EF.Models;
    using CommonModels = OnBoardingSystem.Common.Models;

    /// <summary>
    /// Interface for Unit of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets MD Ministry repository.
        /// </summary>
        IGenericRepository<EF.Models.MdMinistry> MdMinistryRepository { get; }

        /// <summary>
        /// Gets App OnBoarding repository.
        /// </summary>
        IGenericRepository<EF.Models.AppOnboardingRequest> AppOnboardingRequestRepository { get; }

        /// <summary>
        /// Gets RequestListInfoRepository repository.
        /// </summary>
        IGenericRepository<RequestListInfo> RequestListInfoRepository { get; }

        /// <summary>
        /// Gets MD Organization repository.
        /// </summary>
        IGenericRepository<EF.Models.MdOrganization> MdOrganizationRepository { get; }

        /// <summary>
        /// Gets MD AgencyType repository.
        /// </summary>
        IGenericRepository<EF.Models.MdAgencyType> MdAgencyTypeRepository { get; }

        /// <summary>
        /// Gets MD ServiceType repository.
        /// </summary>
        IGenericRepository<EF.Models.MdServiceType> MdServiceTypeRepository { get; }

        /// <summary>
        /// Gets MD ServiceType repository.
        /// </summary>
        IGenericRepository<EF.Models.AppOnboardingDetails> AppOnboardingDetailRepository { get; }

        /// <summary>
        /// Gets AppLoginDetailRepository Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppLoginDetails> AppLoginDetailRepository { get; }

        /// <summary>
        /// Gets AppDocumentUploadedDetailRepository Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppDocumentUploadedDetail> AppDocumentUploadedDetailRepository { get; }

        /// <summary>
        /// Gets AppOnboardingDetailResponseRepository Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppOnboardingDetailsResponse> AppOnboardingDetailResponseRepository { get; }

        /// <summary>
        /// Gets AppContactPersonDetailRepository Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppContactPersonDetails> AppContactPersonDetailRepository { get; }

        /// <summary>
        /// Gets AppOnboardingResponse Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppOnboardingResponse> AppOnboardingResponseRepository { get; }

        /// <summary>
        /// Gets AppOnboardingResponse Propery.
        /// </summary>
        IGenericRepository<EF.Models.MdSmsEmailTemplate> MdSmsEmailTemplateRepository { get; }

        /// <summary>
        /// Gets AppOnboardingResponse Propery.
        /// </summary>
        IGenericRepository<EF.Models.MdState> StateRepository { get; }

        /// <summary>
        /// Gets MdEmailRecipient Propery.
        /// </summary>
        IGenericRepository<EF.Models.MdEmailRecipient> EmailRecipientRepository { get; }

        /// <summary>
        /// Gets OBSDBContext Propery.
        /// </summary>
        OBSDBContext OBSDBContext { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Number of rows.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken);

        ///// <summary>
        ///// Gets MdStatus Propery.
        ///// </summary>
        //IGenericRepository<EF.Models.MdStatus> MdStatusRepository { get; }

        /// <summary>
        /// Gets AppProjectDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectDetails> AppProjectDetailsRepository { get; }

        /// <summary>
        /// Gets Project List Property.
        /// </summary>
        IGenericRepository<CommonModels.MDStatus> MDStatusRepository { get; }

        /// <summary>
        /// Gets Project List Property.
        IGenericRepository<EF.Models.MdAgency> MDAgencyRepository { get; }

        /// <summary>
        /// Gets ServiceType List Property.
        IGenericRepository<EF.Models.ZmstServiceType> ZmstServiceTypeRepository { get; }

        /// <summary>
        /// Gets MdDocument Type Repository Property.
        /// </summary>
        IGenericRepository<EF.Models.MdDocumentType> MdDocumentTypeRepository { get; }

        /// <summary>
        /// Gets ZmstAgencyExamCouns Propery.
        /// </summary>
        IGenericRepository<EF.Models.ZmstAgencyExamCouns> ZmstAgencyExamCounRepository { get; }

        /// <summary>
        /// Gets MDProjectFinancialComponent Propery.
        /// </summary>
        IGenericRepository<EF.Models.MdProjectFinancialComponents> MdProjectFinancialComponentRepository { get; }

        /// <summary>
        /// Gets ZmstAgencyExamCouns Propery.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectCost> AppProjectCostRepository { get; }

        /// <summary>
        /// Gets ZmstProjects Type Repository Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstProjects> ZmstProjectRepository { get; }

        /// <summary>
        /// Gets AppUserRoleMapping Type Repository Property.
        /// </summary>
        IGenericRepository<EF.Models.AppUserRoleMapping> AppUserRoleMappingRepository { get; }

        /// <summary>
        /// Gets AppRoleModulePermission Type Repository Property.
        /// </summary>
        IGenericRepository<EF.Models.AppRoleModulePermission> AppRoleModulePermissionRepository { get; }

        /// <summary>
        /// Gets ZmstProjects Type Repository Property.
        /// </summary>
        IGenericRepository<EF.Models.MdModule> MDModuleRepository { get; }

        /// <summary>
        /// Gets MdDistrict List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdDistrict> MdDistrictRepository { get; }

        ///// <summary>
        ///// Gets MdExamType List Property.
        ///// </summary>
        //IGenericRepository<EF.Models.MdExamType> MdExamTypeRepository { get; }

        /// <summary>
        /// Gets MdState List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdState> MdStateRepository { get; }

        /// <summary>
        /// Gets ZmstCountry List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ZmstCountry> ZmstCountryRepository { get; }

        /// <summary>
        /// Gets ZmstApplicationSummary List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ApplicationSummary> ApplicationSummaryRepository { get; }

        /// <summary>
        /// Gets ApplicationSchedule List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ApplicationSchedule> ApplicationScheduleRepository { get; }

        /// <summary>
        /// Gets ApplicationDayWise List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ApplicationDayWise> ApplicationDayWiseRepository { get; }

        /// <summary>
        /// Gets ApplicationDayWise List Property. 
        /// </summary>
        IGenericRepository<EF.Models.AppDocumentTypeRoleMapping> AppDocumentTypeRoleMapping { get; }
        /// <summary>
        /// Gets ApplicationDayWise List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ZmstWillingness> ZmstWillingnessRepository { get; }
        /// <summary>
        /// Gets ApplicationDayWise List Property. 
        /// </summary>
        IGenericRepository<EF.Models.ZmstSubject> ZmstSubjectRepository { get; }

        /// <summary>
        /// Gets ZmstAgency List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstAgency> ZmstAgencyRepository { get; }

        /// <summary>
        /// Gets ZmstBranch List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstBranch> ZmstBranchRepository { get; }

        /// <summary>
        /// Gets ZmstCategory List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstCategory> ZmstCategoryRepository { get; }

        /// <summary>
        /// Gets ZmstCourseApplied List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstCourseApplied> ZmstCourseAppliedRepository { get; }

        /// <summary>
        /// Gets ZmstCourseAppliedLevel List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstCourseAppliedLevel> ZmstCourseAppliedLevelRepository { get; }

        /// <summary>
        /// Gets ZmstDistrict List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstDistrict> ZmstDistrictRepository { get; }

        /// <summary>
        /// Gets ZmstDocumentType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstDocumentType> ZmstDocumentTypeRepository { get; }

        /// <summary>
        /// Gets ZmstExamType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstExamType> ZmstExamTypeRepository { get; }

        /// <summary>
        /// Gets ZmstFeeType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstFeeType> ZmstFeeTypeRepository { get; }

        /// <summary>
        /// Gets ZmstGender List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstGender> ZmstGenderRepository { get; }

        /// <summary>
        /// Gets ZmstIdentityType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstIdentityType> ZmstIdentityTypeRepository { get; }

        /// <summary>
        /// Gets ZmstMinimumQualification List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstMinimumQualification> ZmstMinimumQualificationRepository { get; }

        /// <summary>
        /// Gets ZmstNationality List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstNationality> ZmstNationalityRepository { get; }

        /// <summary>
        /// Gets ZmstPassingStatus List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstPassingStatus> ZmstPassingStatusRepository { get; }

        /// <summary>
        /// Gets ZmstQualification List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualification> ZmstQualificationRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingCourse List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingCourse> ZmstQualifyingCourseRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExam List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExam> ZmstQualifyingExamRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExamBoard List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExamBoard> ZmstQualifyingExamBoardRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExamFrom List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExamFrom> ZmstQualifyingExamFromRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExamLearningMode List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExamLearningMode> ZmstQualifyingExamLearningModeRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExamResultMode List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExamResultMode> ZmstQualifyingExamResultModeRepository { get; }

        /// <summary>
        /// Gets ZmstQualifyingExamStream List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQualifyingExamStream> ZmstQualifyingExamStreamRepository { get; }

        /// <summary>
        /// Gets ZmstQuesPaper List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQuesPaper> ZmstQuesPaperRepository { get; }

        /// <summary>
        /// Gets ZmstQuestionPaperMedium List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQuestionPaperMedium> ZmstQuestionPaperMediumRepository { get; }

        /// <summary>
        /// Gets ZmstQuota List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstQuota> ZmstQuotaRepository { get; }

        /// <summary>
        /// Gets ZmstRankType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstRankType> ZmstRankTypeRepository { get; }

        /// <summary>
        /// Gets ZmstReligion List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstReligion> ZmstReligionRepository { get; }

        /// <summary>
        /// Gets ZmstResidentialEligibility List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstResidentialEligibility> ZmstResidentialEligibilityRepository { get; }

        /// <summary>
        /// Gets ZmstSeatCategory List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSeatCategory> ZmstSeatCategoryRepository { get; }

        /// <summary>
        /// Gets ZmstSeatGender List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSeatGender> ZmstSeatGenderRepository { get; }

        /// <summary>
        /// Gets ZmstSpecialExamPaper List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSpecialExamPaper> ZmstSpecialExamPaperRepository { get; }

        /// <summary>
        /// Gets ZmstState List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstState> ZmstStateRepository { get; }

        /// <summary>
        /// Gets ZmstStream List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstStream> ZmstStreamRepository { get; }

        /// <summary>
        /// Gets ZmstSubCategory List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSubCategory> ZmstSubCategoryRepository { get; }

        /// <summary>
        /// Gets ZmstSubCategoryPriority List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSubCategoryPriority> ZmstSubCategoryPriorityRepository { get; }

        /// <summary>
        /// Gets ZmstSymbol List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSymbol> ZmstSymbolRepository { get; }

        /// <summary>
        /// Gets ZmstTypeofDisability List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstTypeofDisability> ZmstTypeofDisabilityRepository { get; }

        /// <summary>
        /// Gets WorkOrderDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.WorkOrderDetails> WorkOrderDetailsRepository { get; }

        /// <summary>
        /// Gets WorkOrderDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdWorkOrderAgency> MdWorkOrderAgencyRepository { get; }

        /// <summary>
        /// Gets EmployeeDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.EmployeeDetails> EmployeeDetailsRepository { get; }

        /// <summary>
        /// Gets employeeWorkOrder List Property.
        /// </summary>
        IGenericRepository<EF.Models.EmployeeWorkOrder> EmployeeWorkOrderRepository { get; }

        /// <summary>
        /// Gets MdEmpStatus List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdEmpStatus> MdEmpStatusRepository { get; }

        /// <summary>
        /// Gets MdExam List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdExam> MdExamRepository { get; }

        /// <summary>
        /// Gets MdIdType List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdIdType> MdIdTypeRepository { get; }

        /// <summary>
        /// Gets qualificationDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.QualificationDetails> QualificationDetailsRepository { get; }

        /// <summary>
        /// Gets ZmstProgram List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstProgram> ZmstProgramRepository { get; }

        /// <summary>
        /// Gets ZmstApplicantType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstApplicantType> ZmstApplicantTypeRepository { get; }

        /// <summary>
        /// Gets ZmstActivity List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstActivity> ZmstActivityRepository { get; }

        /// <summary>
        /// Gets ZmstTrade List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstTrade> ZmstTradeRepository { get; }

        /// <summary>
        /// Gets ZmstInstitute List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstInstitute> ZmstInstituteRepository { get; }

        /// <summary>
        /// Gets ZmstInstituteType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstInstituteType> ZmstInstituteTypeRepository { get; }

        /// <summary>
        /// Gets ZmstInstituteType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstInstituteAgency> ZmstInstituteAgencyRepository { get; }
        /// <summary>
        /// Gets ZmstInstituteStream List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstInstituteStream> ZmstInstituteStreamRepository { get; }
        /// <summary>
        /// Gets ZmstAgencyVirtualDirectoryMapping List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstAgencyVirtualDirectoryMapping> ZmstAgencyVirtualDirectoryMappingRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.AppCaptcha> AppCaptchaRepository { get; }

        /// <summary>
        /// Gets ZmstSeatGroup List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSeatGroup> ZmstSeatGroupRepository { get; }

        /// <summary>
        /// Gets ZmstSeatSubCategory List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSeatSubCategory> ZmstSeatSubCategoryRepository { get; }

        /// <summary>
        /// Gets ZmstSeatType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstSeatType> ZmstSeatTypeRepository { get; }

        /// <summary>
        /// Gets ZmstExperienceType List Property.
        /// </summary>
        IGenericRepository<EF.Models.ZmstExperienceType> ZmstExperienceTypeRepository { get; }
        /// <summary>
        /// Gets MdMainModule List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdMainModule> MdMainModuleRepository { get; }

        /// <summary>
        /// Gets AppProjectPaymentDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectPaymentDetails> AppProjectPaymentDetailsRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Number of rows.</returns>

        /// <summary>
        /// Gets AppProjectActivity List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectActivity> AppProjectActivityRepository { get; }

        /// <summary>
        /// Gets AppProjectActivityHistory List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectActivityHistory> AppProjectActivityHistoryRepository { get; }

        /// <summary>
        /// Gets AppProjectActivityStatusTracking List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppProjectActivityStatusTracking> AppProjectActivityStatusTrackingRepository { get; }
        /// <summary>
		/// Gets AppProjectActivityStatusTracking List Property.
		/// </summary>

		IGenericRepository<EF.Models.MdStatus> mdstatusDirectorRepository { get; }

        /// <summary>
        /// Gets MdActivityType List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdActivityType> MdActivityTypeRepository { get; }

        /// <summary>
        /// Gets AppDocumentUploadedDetailHistoty List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppDocumentUploadedDetailHistoty> AppDocumentUploadedDetailHistotyRepository { get; }

        IGenericRepository<EF.Models.Administrator> AdministratorRepository { get; }

        IGenericRepository<EF.Models.ZmstAuthenticationMode> ZmstAuthenticationModeRepository { get; }

        IGenericRepository<EF.Models.ZmstSecurityQuestion> ZmstSecurityQuestionRepository { get; }

        IGenericRepository<EF.Models.MdRole> MdRoleRepository { get; }

        IGenericRepository<EF.Models.AppLoginDetails> AppLoginDetailsRepository { get; }

        IGenericRepository<EF.Models.ConfigurationEnvironment> ConfigurationEnvironmentRepository { get; }

        IGenericRepository<EF.Models.MdYear> MdYearRepository { get; }

        IGenericRepository<EF.Models.AppProjectExpenditure> AppProjectExpenditureRepository { get; }
        
        IGenericRepository<EF.Models.ConfigurationApisecureKey> ConfigurationAPISecureKeyRepository { get; }
    }
}
//-----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Server.IIS.Core;
    //using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using CommonModels = OnBoardingSystem.Common.Models;

    /// <inheritdoc/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OBSDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="oBSDBContext">Onboarding System DbContext.</param>
        /// <param name="mdminsirtySetRepository">entityContract Repository.</param>
        /// <param name="requestListRepository">requestListRepository Repository.</param>
        /// <param name="mdorganizationSetRepository">mdorganizationSetRepository Repository.</param>
        /// <param name="appSettingRepository">appSettingRepository Repository.</param>
        /// <param name="appOnboardingRequest">appOnboardingRequest Repository.</param>
        /// <param name="mdservicetypeRepository">mdservicetypeRepository Repository.</param>
        /// <param name="mdagencytypeRepository">mdagencytypeRepository Repository.</param>
        /// <param name="appOnboardingDetailRepository">appOnboardingDetailRepository Repository.</param>
        /// <param name="apploginDetailsRepository">apploginDetailsRepository Repository.</param>
        /// <param name="appOnboardingDetailsResponseRepository">appOnboardingDetailsResponse Repository.</param>
        /// <param name="appContactPersonDetailRepository">appContactPersonDetailRepository Repository.</param>
        /// <param name="appOnboardingResponseRepository">appOnboardingResponseRepository Repository.</param>
        /// <param name="appDocumentUploadedDetail">appDocumentUploadedDetail Repository.</param>
        /// <param name="signUp">signUp Repository.</param>
        /// <param name="state">MdState.</param>
        /// <param name="mdDistrictRepository">mdDistrictRepository Repository.</param>
        /// <param name="mdSmsEmailTemplateRepositor">mdSmsEmailTemplate Repository.</param>
        /// <param name="mdStatusRepository">mdStatus Repository.</param>
        /// <param name="appProjectDetailRepository">Project List Repository.</param>
        /// <param name="mdStatusRNDRepository">mdStatusRNDRepository Repository.</param>
        /// <param name="mdEmailRecipientRepository">mdEmailRecipientRepository Repository.</param>
        /// <param name="mdAgencyRepository">mdAgencyRepository Repository.</param>
        /// <param name="zmstServiceTypeRepository">zmstServiceTypeRepository Repository.</param>
        /// <param name="appUserRoleMappingRepository">appUserRoleMappingRepository Repository.</param>
        /// <param name="appRoleModulePermissionRepository">appRoleModulePermissionRepository Repository.</param>
        /// <param name="mddistrictRepository">mddistrictRepository Repository.</param>
        ///	<param name = "mdexamtypeRepository" > mdexamtypeRepository Repository.</param>
        /// <param name = "mdstateRepository" > mdstateRepository Repository.</param>
        /// <param name="zmstcountryRepository">zmstcountryRepository Repository.</param>
        /// <param name="validationtestRepository">validationtestRepository Repository.</param>
        /// <param name="mdsmsemailtemplateRepository">mdsmsemailtemplateRepository Repository.</param>
        /// /// <param name="applicationScheduleRepository">mdsmsemailtemplateRepository Repository.</param>
        /// <param name="zmstWillingnessRepository">mdsmsemailtemplateRepository Repository.</param>
        /// <param name="zmstSubjectRepository">mdsmsemailtemplateRepository Repository.</param>
        /// /// <param name="zmstagencyRepository">zmstagencyRepository Repository.</param>
        /// <param name="zmstbranchRepository">zmstbranchRepository Repository.</param>
        /// <param name="zmstcategoryRepository">zmstcategoryRepository Repository.</param>
        /// <param name="zmstcourseappliedRepository">zmstcourseappliedRepository Repository.</param>
        /// <param name="zmstcourseappliedlevelRepository">zmstcourseappliedlevelRepository Repository.</param>
        /// <param name="zmstdistrictRepository">zmstdistrictRepository Repository.</param>
        /// <param name="zmstdocumenttypeRepository">zmstdocumenttypeRepository Repository.</param>
        /// <param name="zmstexamtypeRepository">zmstexamtypeRepository Repository.</param>
        /// <param name="zmstfeetypeRepository">zmstfeetypeRepository Repository.</param>
        /// <param name="zmstgenderRepository">zmstgenderRepository Repository.</param>
        /// <param name="zmstidentitytypeRepository">zmstidentitytypeRepository Repository.</param>
        /// <param name="zmstminimumqualificationRepository">zmstminimumqualificationRepository Repository.</param>
        /// <param name="zmstnationalityRepository">zmstnationalityRepository Repository.</param>
        /// <param name="zmstpassingstatusRepository">zmstpassingstatusRepository Repository.</param>
        /// <param name="zmstqualificationRepository">zmstqualificationRepository Repository.</param>
        /// <param name="zmstqualifyingcourseRepository">zmstqualifyingcourseRepository Repository.</param>
        /// <param name="zmstqualifyingexamRepository">zmstqualifyingexamRepository Repository.</param>
        /// <param name="zmstqualifyingexamboardRepository">zmstqualifyingexamboardRepository Repository.</param>
        /// <param name="zmstqualifyingexamfromRepository">zmstqualifyingexamfromRepository Repository.</param>
        /// <param name="zmstqualifyingexamlearningmodeRepository">zmstqualifyingexamlearningmodeRepository Repository.</param>
        /// <param name="zmstqualifyingexamresultmodeRepository">zmstqualifyingexamresultmodeRepository Repository.</param>
        /// <param name="zmstqualifyingexamstreamRepository">zmstqualifyingexamstreamRepository Repository.</param>
        /// <param name="zmstquespaperRepository">zmstquespaperRepository Repository.</param>
        /// <param name="zmstquestionpapermediumRepository">zmstquestionpapermediumRepository Repository.</param>
        /// <param name="zmstquotaRepository">zmstquotaRepository Repository.</param>
        /// <param name="zmstranktypeRepository">zmstranktypeRepository Repository.</param>
        /// <param name="zmstreligionRepository">zmstreligionRepository Repository.</param>
        /// <param name="zmstresidentialeligibilityRepository">zmstresidentialeligibilityRepository Repository.</param>
        /// <param name="zmstseatcategoryRepository">zmstseatcategoryRepository Repository.</param>
        /// <param name="zmstseatgenderRepository">zmstseatgenderRepository Repository.</param>
        /// <param name="zmstspecialexampaperRepository">zmstspecialexampaperRepository Repository.</param>
        /// <param name="zmststateRepository">zmststateRepository Repository.</param>
        /// <param name="zmststreamRepository">zmststreamRepository Repository.</param>
        /// <param name="zmstsubcategoryRepository">zmstsubcategoryRepository Repository.</param>
        /// <param name="zmstsubcategorypriorityRepository">zmstsubcategorypriorityRepository Repository.</param>
        /// <param name="zmstsymbolRepository">zmstsymbolRepository Repository.</param>
        /// <param name="zmsttypeofdisabilityRepository">zmsttypeofdisabilityRepository Repository.</param>
        /// /// <param name="employeedetailsRepository">employeedetailsRepository Repository.</param>
        /// <param name="employeeworkorderRepository">employeeworkorderRepository Repository.</param>
        /// <param name="mdempstatusRepository">mdempstatusRepository Repository.</param>
        /// <param name="mdexamRepository">mdexamRepository Repository.</param>
        /// <param name="mdidtypeRepository">mdidtypeRepository Repository.</param>
        /// <param name="mdworkorderagencyRepository">mdworkorderagencyRepository Repository.</param>
        /// <param name="qualificationdetailsRepository">qualificationdetailsRepository Repository.</param>
        /// <param name="zmstapplicanttypeRepository">zmstapplicanttypeRepository Repository.</param>
        /// <param name="zmstactivityRepository">zmstactivityRepository Repository.</param>
        /// <param name="zmsttradeRepository">zmsttradeRepository Repository.</param>
        /// <param name="zmstseatgroupRepository">zmstseatgroupRepository Repository.</param>
        /// <param name="zmstseatsubcategoryRepository">zmstseatsubcategoryRepository Repository.</param>
        /// <param name="zmstseattypeRepository">zmstseattypeRepository Repository.</param>
        /// <param name="appProjectActivityRepository">zmstseattypeRepository Repository.</param>
        /// <param name="appProjectActivityHistoryRepository">zmstseattypeRepository Repository.</param>
        /// <param name="appProjectActivityStatusTrackingRepository">zmstseattypeRepository Repository.</param>
        /// <param name="mdActivityTypeRepository">zmstseattypeRepository Repository.</param>
        ///  <param name="appProjectPaymentDetailsRepository">zmstseattypeRepository Repository.</param>
        /// <param name="configurationApisecureKeyRepository">zmstseattypeRepository Repository.</param>
        public UnitOfWork(
        OBSDBContext oBSDBContext,
        IGenericRepository<MdMinistry> mdminsirtySetRepository,
        IGenericRepository<RequestListInfo> requestListRepository,
        IGenericRepository<MdOrganization> mdorganizationSetRepository,
        IGenericRepository<AppOnboardingRequest> appOnboardingRequest,
        IGenericRepository<MdServiceType> mdservicetypeRepository,
        IGenericRepository<MdAgencyType> mdagencytypeRepository,
        IGenericRepository<AppLoginDetails> apploginDetailsRepository,
        IGenericRepository<AppDocumentUploadedDetail> appDocumentUploadedDetail,
        IGenericRepository<AppOnboardingDetails> appOnboardingDetailRepository,
        IGenericRepository<AppOnboardingDetailsResponse> appOnboardingDetailsResponseRepository,
        IGenericRepository<AppContactPersonDetails> appContactPersonDetailRepository,
        IGenericRepository<AppOnboardingResponse> appOnboardingResponseRepository,
        IGenericRepository<MdState> state,
        IGenericRepository<MdDistrict> mdDistrictRepository,
        IGenericRepository<MdSmsEmailTemplate> mdSmsEmailTemplateRepositor,
        IGenericRepository<MdEmailRecipient> mdEmailRecipientRepository,
        IGenericRepository<AppProjectDetails> appProjectDetailRepository,
        IGenericRepository<CommonModels.MDStatus> mdStatusRepository,
        IGenericRepository<MdAgency> mdAgencyRepository,
        IGenericRepository<ZmstServiceType> zmstserviceTypeRepository,
        IGenericRepository<MdDocumentType> mdDocumentTypeRepository,
        IGenericRepository<ZmstAgencyExamCouns> zmstAgencyExamCounRepository,
        IGenericRepository<ZmstProjects> zmstprojectRepository,
        IGenericRepository<MdProjectFinancialComponents> mdProjectFinancialComponentRepository,
        IGenericRepository<AppProjectCost> appProjectCostRepository,
        IGenericRepository<AppUserRoleMapping> appUserRoleMappingRepository,
        IGenericRepository<AppRoleModulePermission> appRoleModulePermissionRepository,
        IGenericRepository<MdModule> mdModuleRepository,
        IGenericRepository<MdDistrict> mddistrictRepository,
        //IGenericRepository<MdExamType> mdexamtypeRepository,
        IGenericRepository<MdState> mdstateRepository,
        IGenericRepository<ZmstCountry> zmstcountryRepository,
        IGenericRepository<MdSmsEmailTemplate> mdsmsemailtemplateRepository,
        IGenericRepository<ApplicationSummary> applicationSummaryRepository,
        IGenericRepository<ApplicationSchedule> applicationScheduleRepository,
        IGenericRepository<ApplicationDayWise> applicationDayWiseRepository,
        IGenericRepository<AppDocumentTypeRoleMapping> appDocumentTypeRoleMappingRepository,
        IGenericRepository<ZmstWillingness> zmstWillingnessRepository,
        IGenericRepository<ZmstSubject> zmstSubjectRepository,
        IGenericRepository<ZmstAgency> zmstagencyRepository,
        IGenericRepository<ZmstBranch> zmstbranchRepository,
        IGenericRepository<ZmstCategory> zmstcategoryRepository,
        IGenericRepository<ZmstCourseApplied> zmstcourseappliedRepository,
        IGenericRepository<ZmstCourseAppliedLevel> zmstcourseappliedlevelRepository,
        IGenericRepository<ZmstDistrict> zmstdistrictRepository,
        IGenericRepository<ZmstDocumentType> zmstdocumenttypeRepository,
        IGenericRepository<ZmstExamType> zmstexamtypeRepository,
        IGenericRepository<ZmstFeeType> zmstfeetypeRepository,
        IGenericRepository<ZmstGender> zmstgenderRepository,
        IGenericRepository<ZmstIdentityType> zmstidentitytypeRepository,
        IGenericRepository<ZmstMinimumQualification> zmstminimumqualificationRepository,
        IGenericRepository<ZmstNationality> zmstnationalityRepository,
        IGenericRepository<ZmstPassingStatus> zmstpassingstatusRepository,
        IGenericRepository<ZmstQualification> zmstqualificationRepository,
        IGenericRepository<ZmstQualifyingCourse> zmstqualifyingcourseRepository,
        IGenericRepository<ZmstQualifyingExam> zmstqualifyingexamRepository,
        IGenericRepository<ZmstQualifyingExamBoard> zmstqualifyingexamboardRepository,
        IGenericRepository<ZmstQualifyingExamFrom> zmstqualifyingexamfromRepository,
        IGenericRepository<ZmstQualifyingExamLearningMode> zmstqualifyingexamlearningmodeRepository,
        IGenericRepository<ZmstQualifyingExamResultMode> zmstqualifyingexamresultmodeRepository,
        IGenericRepository<ZmstQualifyingExamStream> zmstqualifyingexamstreamRepository,
        IGenericRepository<ZmstQuesPaper> zmstquespaperRepository,
        IGenericRepository<ZmstQuestionPaperMedium> zmstquestionpapermediumRepository,
        IGenericRepository<ZmstQuota> zmstquotaRepository,
        IGenericRepository<ZmstRankType> zmstranktypeRepository,
        IGenericRepository<ZmstReligion> zmstreligionRepository,
        IGenericRepository<ZmstResidentialEligibility> zmstresidentialeligibilityRepository,
        IGenericRepository<ZmstSeatCategory> zmstseatcategoryRepository,
        IGenericRepository<ZmstSeatGender> zmstseatgenderRepository,
        IGenericRepository<ZmstSpecialExamPaper> zmstspecialexampaperRepository,
        IGenericRepository<ZmstState> zmststateRepository,
        IGenericRepository<ZmstStream> zmststreamRepository,
        IGenericRepository<ZmstSubCategory> zmstsubcategoryRepository,
        IGenericRepository<ZmstSubCategoryPriority> zmstsubcategorypriorityRepository,
        IGenericRepository<ZmstSymbol> zmstsymbolRepository,
        IGenericRepository<ZmstTypeofDisability> zmsttypeofdisabilityRepository,
        IGenericRepository<WorkOrderDetails> workorderdetailsRepository,
        IGenericRepository<MdWorkOrderAgency> mdworkorderagencyRepository,
        IGenericRepository<EmployeeDetails> employeedetailsRepository,
        IGenericRepository<EmployeeWorkOrder> employeeworkorderRepository,
        IGenericRepository<MdEmpStatus> mdempstatusRepository,
        IGenericRepository<MdExam> mdexamRepository,
        IGenericRepository<MdIdType> mdidtypeRepository,
        IGenericRepository<ZmstProgram> zmstProgramRepository,
        IGenericRepository<QualificationDetails> qualificationdetailsRepository,
        IGenericRepository<ZmstApplicantType> zmstapplicanttypeRepository,
        IGenericRepository<ZmstActivity> zmstactivityRepository,
        IGenericRepository<ZmstTrade> zmsttradeRepository,
        IGenericRepository<ZmstInstitute> zmstinstituteRepository,
        IGenericRepository<ZmstInstituteType> zmstinstitutetypeRepository,
        IGenericRepository<ZmstInstituteAgency> zmstInstituteAgencyRepository,
        IGenericRepository<ZmstInstituteStream> zmstinstitutestreamRepository,
        IGenericRepository<ZmstAgencyVirtualDirectoryMapping> zmstagencyvirtualdirectorymappingRepository,
        IGenericRepository<AppCaptcha> appCaptchaRepository,
        IGenericRepository<ZmstSeatGroup> zmstseatgroupRepository,
        IGenericRepository<ZmstSeatSubCategory> zmstseatsubcategoryRepository,
        IGenericRepository<ZmstSeatType> zmstseattypeRepository,
        IGenericRepository<ZmstExperienceType> zmstExperienceTypeRepository,
        IGenericRepository<MdMainModule> mdmainmoduleRepository,
        IGenericRepository<AppProjectActivity> appProjectActivityRepository,
        IGenericRepository<AppProjectActivityHistory> appProjectActivityHistoryRepository,
        IGenericRepository<AppProjectActivityStatusTracking> appProjectActivityStatusTrackingRepository,
        IGenericRepository<MdActivityType> mdActivityTypeRepository,
        IGenericRepository<AppProjectPaymentDetails> appProjectPaymentDetailsRepository,

        IGenericRepository<AppDocumentUploadedDetailHistoty> appDocumentUploadedDetailHistotyRepository,
        IGenericRepository<MdStatus> mdstatusDirectorRepository,

        IGenericRepository<Administrator> AdministratorRepository,
        IGenericRepository<ZmstAuthenticationMode> zmstauthenticationmodeRepository,
        IGenericRepository<ZmstSecurityQuestion> zmstsecurityquestionRepository,
        IGenericRepository<MdRole> mdroleRepository,
        IGenericRepository<AppLoginDetails> applogindetailsRepository,
        IGenericRepository<ConfigurationEnvironment> configurationEnvironmentRepository,
        IGenericRepository<MdYear> mdYearRepository,
        IGenericRepository<AppProjectExpenditure> appProjectExpenditureRepository,
        IGenericRepository<ConfigurationApisecureKey> configurationApisecureKeyRepository
        )
        {
            this.dbContext = oBSDBContext;
            this.OBSDBContext = oBSDBContext;
            this.MdMinistryRepository = mdminsirtySetRepository;
            this.RequestListInfoRepository = requestListRepository;
            this.MdOrganizationRepository = mdorganizationSetRepository;
            this.AppOnboardingRequestRepository = appOnboardingRequest;
            this.MdAgencyTypeRepository = mdagencytypeRepository;
            this.MdServiceTypeRepository = mdservicetypeRepository;
            this.AppOnboardingDetailRepository = appOnboardingDetailRepository;
            this.AppDocumentUploadedDetailRepository = appDocumentUploadedDetail;
            this.AppLoginDetailRepository = apploginDetailsRepository;
            this.AppOnboardingDetailResponseRepository = appOnboardingDetailsResponseRepository;
            this.AppContactPersonDetailRepository = appContactPersonDetailRepository;
            this.AppOnboardingResponseRepository = appOnboardingResponseRepository;
            this.MdSmsEmailTemplateRepository = mdSmsEmailTemplateRepositor;
            this.StateRepository = state;
            this.MdDistrictRepository = mdDistrictRepository;
            this.EmailRecipientRepository = mdEmailRecipientRepository;
            this.AppProjectDetailsRepository = appProjectDetailRepository;
            this.MDStatusRepository = mdStatusRepository;
            this.MDAgencyRepository = mdAgencyRepository;
            this.ZmstServiceTypeRepository = zmstserviceTypeRepository;
            this.MdDocumentTypeRepository = mdDocumentTypeRepository;
            this.ZmstAgencyExamCounRepository = zmstAgencyExamCounRepository;
            this.ZmstProjectRepository = zmstprojectRepository;
            this.MdProjectFinancialComponentRepository = mdProjectFinancialComponentRepository;
            this.AppProjectCostRepository = appProjectCostRepository;
            this.AppUserRoleMappingRepository = appUserRoleMappingRepository;
            this.AppRoleModulePermissionRepository = appRoleModulePermissionRepository;
            this.MDModuleRepository = mdModuleRepository;
            //this.MdExamTypeRepository = mdexamtypeRepository;
            this.MdStateRepository = mdstateRepository;
            this.ZmstCountryRepository = zmstcountryRepository;
            this.MdSmsEmailTemplateRepository = mdsmsemailtemplateRepository;
            this.ApplicationSummaryRepository = applicationSummaryRepository;
            this.ApplicationScheduleRepository = applicationScheduleRepository;
            this.ApplicationDayWiseRepository = applicationDayWiseRepository;
            this.AppDocumentTypeRoleMapping = appDocumentTypeRoleMappingRepository;
            this.ZmstWillingnessRepository = zmstWillingnessRepository;
            this.ZmstSubjectRepository = zmstSubjectRepository;
            this.ZmstAgencyRepository = zmstagencyRepository;
            this.ZmstBranchRepository = zmstbranchRepository;
            this.ZmstCategoryRepository = zmstcategoryRepository;
            this.ZmstCourseAppliedRepository = zmstcourseappliedRepository;
            this.ZmstCourseAppliedLevelRepository = zmstcourseappliedlevelRepository;
            this.ZmstDistrictRepository = zmstdistrictRepository;
            this.ZmstDocumentTypeRepository = zmstdocumenttypeRepository;
            this.ZmstExamTypeRepository = zmstexamtypeRepository;
            this.ZmstFeeTypeRepository = zmstfeetypeRepository;
            this.ZmstGenderRepository = zmstgenderRepository;
            this.ZmstIdentityTypeRepository = zmstidentitytypeRepository;
            this.ZmstMinimumQualificationRepository = zmstminimumqualificationRepository;
            this.ZmstNationalityRepository = zmstnationalityRepository;
            this.ZmstPassingStatusRepository = zmstpassingstatusRepository;
            this.ZmstQualificationRepository = zmstqualificationRepository;
            this.ZmstQualifyingCourseRepository = zmstqualifyingcourseRepository;
            this.ZmstQualifyingExamRepository = zmstqualifyingexamRepository;
            this.ZmstQualifyingExamBoardRepository = zmstqualifyingexamboardRepository;
            this.ZmstQualifyingExamFromRepository = zmstqualifyingexamfromRepository;
            this.ZmstQualifyingExamLearningModeRepository = zmstqualifyingexamlearningmodeRepository;
            this.ZmstQualifyingExamResultModeRepository = zmstqualifyingexamresultmodeRepository;
            this.ZmstQualifyingExamStreamRepository = zmstqualifyingexamstreamRepository;
            this.ZmstQuesPaperRepository = zmstquespaperRepository;
            this.ZmstQuestionPaperMediumRepository = zmstquestionpapermediumRepository;
            this.ZmstQuotaRepository = zmstquotaRepository;
            this.ZmstRankTypeRepository = zmstranktypeRepository;
            this.ZmstReligionRepository = zmstreligionRepository;
            this.ZmstResidentialEligibilityRepository = zmstresidentialeligibilityRepository;
            this.ZmstSeatCategoryRepository = zmstseatcategoryRepository;
            this.ZmstSeatGenderRepository = zmstseatgenderRepository;
            this.ZmstSpecialExamPaperRepository = zmstspecialexampaperRepository;
            this.ZmstStateRepository = zmststateRepository;
            this.ZmstStreamRepository = zmststreamRepository;
            this.ZmstSubCategoryRepository = zmstsubcategoryRepository;
            this.ZmstSubCategoryPriorityRepository = zmstsubcategorypriorityRepository;
            this.ZmstSymbolRepository = zmstsymbolRepository;
            this.ZmstTypeofDisabilityRepository = zmsttypeofdisabilityRepository;
            this.WorkOrderDetailsRepository = workorderdetailsRepository;
            this.MdWorkOrderAgencyRepository = mdworkorderagencyRepository;
            this.EmployeeDetailsRepository = employeedetailsRepository;
            this.EmployeeWorkOrderRepository = employeeworkorderRepository;
            this.MdEmpStatusRepository = mdempstatusRepository;
            this.MdExamRepository = mdexamRepository;
            this.MdIdTypeRepository = mdidtypeRepository;
            this.QualificationDetailsRepository = qualificationdetailsRepository;
            this.ZmstProgramRepository = zmstProgramRepository;
            this.ZmstApplicantTypeRepository = zmstapplicanttypeRepository;
            this.ZmstActivityRepository = zmstactivityRepository;
            this.ZmstTradeRepository = zmsttradeRepository;
            this.ZmstInstituteRepository = zmstinstituteRepository;
            this.ZmstInstituteTypeRepository = zmstinstitutetypeRepository;
            this.ZmstInstituteAgencyRepository = zmstInstituteAgencyRepository;
            this.ZmstInstituteStreamRepository = zmstinstitutestreamRepository;
            this.ZmstAgencyVirtualDirectoryMappingRepository = zmstagencyvirtualdirectorymappingRepository;
            this.AppCaptchaRepository = appCaptchaRepository;
            this.ZmstSeatGroupRepository = zmstseatgroupRepository;
            this.ZmstSeatSubCategoryRepository = zmstseatsubcategoryRepository;
            this.ZmstSeatTypeRepository = zmstseattypeRepository;
            this.ZmstExperienceTypeRepository = zmstExperienceTypeRepository;
            this.MdMainModuleRepository = mdmainmoduleRepository;
            this.AppProjectActivityRepository = appProjectActivityRepository;
            this.AppProjectActivityHistoryRepository = appProjectActivityHistoryRepository;
            this.AppProjectActivityStatusTrackingRepository = appProjectActivityStatusTrackingRepository;
            this.mdstatusDirectorRepository = mdstatusDirectorRepository;
            this.MdActivityTypeRepository = mdActivityTypeRepository;
            this.AppProjectPaymentDetailsRepository = appProjectPaymentDetailsRepository;
            this.AppDocumentUploadedDetailHistotyRepository = appDocumentUploadedDetailHistotyRepository;
            this.AdministratorRepository = AdministratorRepository;
            this.ZmstAuthenticationModeRepository = zmstauthenticationmodeRepository;
            this.ZmstSecurityQuestionRepository = zmstsecurityquestionRepository;
            this.MdRoleRepository = mdroleRepository;
            this.AppLoginDetailsRepository = applogindetailsRepository;
            this.ConfigurationEnvironmentRepository = configurationEnvironmentRepository;
            this.MdYearRepository = mdYearRepository;
            this.AppProjectExpenditureRepository = appProjectExpenditureRepository;
            this.ConfigurationAPISecureKeyRepository = configurationApisecureKeyRepository;
        }

        /// <inheritdoc/>
        public OBSDBContext OBSDBContext { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdMinistry> MdMinistryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<RequestListInfo> RequestListInfoRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppOnboardingRequest> AppOnboardingRequestRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdOrganization> MdOrganizationRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdAgencyType> MdAgencyTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdServiceType> MdServiceTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppDocumentUploadedDetail> AppDocumentUploadedDetailRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppOnboardingDetails> AppOnboardingDetailRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppLoginDetails> AppLoginDetailRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppOnboardingDetailsResponse> AppOnboardingDetailResponseRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppContactPersonDetails> AppContactPersonDetailRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppOnboardingResponse> AppOnboardingResponseRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdState> StateRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdDistrict> MdDistrictRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdSmsEmailTemplate> MdSmsEmailTemplateRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdEmailRecipient> EmailRecipientRepository { get; }

        ///// <inheritdoc/>
        public IGenericRepository<MdStatus> mdstatusDirectorRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectDetails> AppProjectDetailsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<CommonModels.MDStatus> MDStatusRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdAgency> MDAgencyRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdDocumentType> MdDocumentTypeRepository { get; }

        /// <inheritdoc/>
        public virtual Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return this.dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public IGenericRepository<ZmstServiceType> ZmstServiceTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstAgencyExamCouns> ZmstAgencyExamCounRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstProjects> ZmstProjectRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdProjectFinancialComponents> MdProjectFinancialComponentRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectCost> AppProjectCostRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdModule> MDModuleRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppUserRoleMapping> AppUserRoleMappingRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppRoleModulePermission> AppRoleModulePermissionRepository { get; }

        /// <inheritdoc/>
        //public IGenericRepository<MdExamType> MdExamTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdState> MdStateRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstCountry> ZmstCountryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ApplicationSummary> ApplicationSummaryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ApplicationSchedule> ApplicationScheduleRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ApplicationDayWise> ApplicationDayWiseRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppDocumentTypeRoleMapping> AppDocumentTypeRoleMapping { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstWillingness> ZmstWillingnessRepository { get; }
        /// <inheritdoc/>
        public IGenericRepository<ZmstSubject> ZmstSubjectRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstAgency> ZmstAgencyRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstBranch> ZmstBranchRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstCategory> ZmstCategoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstCourseApplied> ZmstCourseAppliedRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstCourseAppliedLevel> ZmstCourseAppliedLevelRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstDistrict> ZmstDistrictRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstDocumentType> ZmstDocumentTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstExamType> ZmstExamTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstFeeType> ZmstFeeTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstGender> ZmstGenderRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstIdentityType> ZmstIdentityTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstMinimumQualification> ZmstMinimumQualificationRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstNationality> ZmstNationalityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstPassingStatus> ZmstPassingStatusRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualification> ZmstQualificationRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingCourse> ZmstQualifyingCourseRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExam> ZmstQualifyingExamRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExamBoard> ZmstQualifyingExamBoardRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExamFrom> ZmstQualifyingExamFromRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExamLearningMode> ZmstQualifyingExamLearningModeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExamResultMode> ZmstQualifyingExamResultModeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQualifyingExamStream> ZmstQualifyingExamStreamRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQuesPaper> ZmstQuesPaperRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQuestionPaperMedium> ZmstQuestionPaperMediumRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstQuota> ZmstQuotaRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstRankType> ZmstRankTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstReligion> ZmstReligionRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstResidentialEligibility> ZmstResidentialEligibilityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSeatCategory> ZmstSeatCategoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSeatGender> ZmstSeatGenderRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSpecialExamPaper> ZmstSpecialExamPaperRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstState> ZmstStateRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstStream> ZmstStreamRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSubCategory> ZmstSubCategoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSubCategoryPriority> ZmstSubCategoryPriorityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSymbol> ZmstSymbolRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstTypeofDisability> ZmstTypeofDisabilityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<WorkOrderDetails> WorkOrderDetailsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdWorkOrderAgency> MdWorkOrderAgencyRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<EmployeeDetails> EmployeeDetailsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<EmployeeWorkOrder> EmployeeWorkOrderRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdEmpStatus> MdEmpStatusRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdExam> MdExamRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdIdType> MdIdTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<QualificationDetails> QualificationDetailsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<EF.Models.ZmstProgram> ZmstProgramRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstApplicantType> ZmstApplicantTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstActivity> ZmstActivityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstTrade> ZmstTradeRepository { get; }
        /// <inheritdoc/>
        public IGenericRepository<ZmstInstitute> ZmstInstituteRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstInstituteType> ZmstInstituteTypeRepository { get; }
        /// <inheritdoc/>
        public IGenericRepository<ZmstInstituteAgency> ZmstInstituteAgencyRepository { get; }
        /// <inheritdoc/>
        public IGenericRepository<ZmstInstituteStream> ZmstInstituteStreamRepository { get; }
        /// <inheritdoc/>
        public IGenericRepository<ZmstAgencyVirtualDirectoryMapping> ZmstAgencyVirtualDirectoryMappingRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppCaptcha> AppCaptchaRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSeatGroup> ZmstSeatGroupRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSeatSubCategory> ZmstSeatSubCategoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstSeatType> ZmstSeatTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstExperienceType> ZmstExperienceTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdMainModule> MdMainModuleRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectActivity> AppProjectActivityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectActivityHistory> AppProjectActivityHistoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectActivityStatusTracking> AppProjectActivityStatusTrackingRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppProjectPaymentDetails> AppProjectPaymentDetailsRepository { get; }

        /// <inheritdoc/>
		public IGenericRepository<MdActivityType> MdActivityTypeRepository { get; }
        
        /// <inheritdoc/>
		public IGenericRepository<AppDocumentUploadedDetailHistoty> AppDocumentUploadedDetailHistotyRepository { get; }

        public IGenericRepository<EF.Models.Administrator> AdministratorRepository { get; }

        public IGenericRepository<ZmstAuthenticationMode> ZmstAuthenticationModeRepository { get; }

        public IGenericRepository<ZmstSecurityQuestion> ZmstSecurityQuestionRepository { get; }

        public IGenericRepository<MdRole> MdRoleRepository { get; }

        public IGenericRepository<AppLoginDetails> AppLoginDetailsRepository { get; }

        public IGenericRepository<ConfigurationEnvironment> ConfigurationEnvironmentRepository { get; }

        public IGenericRepository<MdYear> MdYearRepository { get; }

        public IGenericRepository<AppProjectExpenditure> AppProjectExpenditureRepository { get; }

        public IGenericRepository<ConfigurationApisecureKey> ConfigurationAPISecureKeyRepository { get; }
    }
}
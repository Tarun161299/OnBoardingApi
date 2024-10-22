using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnBoardingSystem.Data.EF.Models;

public partial class OBSDBContext : DbContext
{
    public OBSDBContext()
    {
    }

    public OBSDBContext(DbContextOptions<OBSDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiSubscriptionKey> ApiSubscriptionKey { get; set; }

    public virtual DbSet<AppCaptcha> AppCaptcha { get; set; }

    public virtual DbSet<AppContactPersonDetails> AppContactPersonDetails { get; set; }

    public virtual DbSet<AppDocumentTypeRoleMapping> AppDocumentTypeRoleMapping { get; set; }

    public virtual DbSet<AppDocumentUploadedDetail> AppDocumentUploadedDetail { get; set; }

    public virtual DbSet<AppDocumentUploadedDetail14102023> AppDocumentUploadedDetail14102023 { get; set; }

    public virtual DbSet<AppDocumentUploadedDetailHistoty> AppDocumentUploadedDetailHistoty { get; set; }

    public virtual DbSet<AppLoginDetails> AppLoginDetails { get; set; }

    public virtual DbSet<AppOnboardingDetails> AppOnboardingDetails { get; set; }

    public virtual DbSet<AppOnboardingDetailsResponse> AppOnboardingDetailsResponse { get; set; }

    public virtual DbSet<AppOnboardingDetailsResponseLink> AppOnboardingDetailsResponseLink { get; set; }

    public virtual DbSet<AppOnboardingRequest> AppOnboardingRequest { get; set; }

    public virtual DbSet<AppOnboardingResponse> AppOnboardingResponse { get; set; }

    public virtual DbSet<AppOnboardingResponseLink> AppOnboardingResponseLink { get; set; }

    public virtual DbSet<AppProjectActivity> AppProjectActivity { get; set; }

    public virtual DbSet<AppProjectActivityHistory> AppProjectActivityHistory { get; set; }

    public virtual DbSet<AppProjectCost> AppProjectCost { get; set; }

    public virtual DbSet<AppProjectDetails> AppProjectDetails { get; set; }

    public virtual DbSet<AppProjectExpenditure> AppProjectExpenditure { get; set; }

    public virtual DbSet<AppProjectPaymentDetails> AppProjectPaymentDetails { get; set; }

    public virtual DbSet<AppRoleModulePermission> AppRoleModulePermission { get; set; }

    public virtual DbSet<AppUserRoleMapping> AppUserRoleMapping { get; set; }

    public virtual DbSet<ApplicationDayWise> ApplicationDayWise { get; set; }

    public virtual DbSet<ApplicationSchedule> ApplicationSchedule { get; set; }

    public virtual DbSet<ApplicationSummary> ApplicationSummary { get; set; }

    public virtual DbSet<BkpAppRoleModulePermission> BkpAppRoleModulePermission { get; set; }

    public virtual DbSet<BkpDel> BkpDel { get; set; }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Cgconfigration> Cgconfigration { get; set; }

    public virtual DbSet<CgconfigurationControleTest> CgconfigurationControleTest { get; set; }

    public virtual DbSet<ConfigurationApisecureKey> ConfigurationApisecureKey { get; set; }

    public virtual DbSet<ConfigurationEnvironment> ConfigurationEnvironment { get; set; }

    public virtual DbSet<EmployeeDetails> EmployeeDetails { get; set; }

    public virtual DbSet<EmployeeWorkOrder> EmployeeWorkOrder { get; set; }

    public virtual DbSet<Log> Log { get; set; }

    public virtual DbSet<MdActivityType> MdActivityType { get; set; }

    public virtual DbSet<MdAgency> MdAgency { get; set; }

    public virtual DbSet<MdAgencyType> MdAgencyType { get; set; }

    public virtual DbSet<MdDistrict> MdDistrict { get; set; }

    public virtual DbSet<MdDocumentType> MdDocumentType { get; set; }

    public virtual DbSet<MdEmailRecipient> MdEmailRecipient { get; set; }

    public virtual DbSet<MdEmpStatus> MdEmpStatus { get; set; }

    public virtual DbSet<MdExam> MdExam { get; set; }

    public virtual DbSet<MdIdType> MdIdType { get; set; }

    public virtual DbSet<MdMainModule> MdMainModule { get; set; }

    public virtual DbSet<MdMinistry> MdMinistry { get; set; }

    public virtual DbSet<MdModule> MdModule { get; set; }

    public virtual DbSet<MdModule24082023> MdModule24082023 { get; set; }

    public virtual DbSet<MdOnboardingActivity> MdOnboardingActivity { get; set; }

    public virtual DbSet<MdOrganization> MdOrganization { get; set; }

    public virtual DbSet<MdProjectFinancialComponents> MdProjectFinancialComponents { get; set; }

    public virtual DbSet<MdRole> MdRole { get; set; }

    public virtual DbSet<MdSmsEmailTemplate> MdSmsEmailTemplate { get; set; }

    public virtual DbSet<MdState> MdState { get; set; }

    public virtual DbSet<MdStatus> MdStatus { get; set; }

    public virtual DbSet<MdWorkOrderAgency> MdWorkOrderAgency { get; set; }

    public virtual DbSet<MdYear> MdYear { get; set; }

    public virtual DbSet<QualificationDetails> QualificationDetails { get; set; }

    public virtual DbSet<Registration> Registration { get; set; }

    public virtual DbSet<RequestListInfo> RequestListInfo { get; set; }

    public virtual DbSet<UserAuthorization> UserAuthorization { get; set; }

    public virtual DbSet<ValidationTest> ValidationTest { get; set; }

    public virtual DbSet<WorkOrderDetails> WorkOrderDetails { get; set; }

    public virtual DbSet<WorkorderdetailsBkp> WorkorderdetailsBkp { get; set; }

    public virtual DbSet<ZmstActivity> ZmstActivity { get; set; }

    public virtual DbSet<ZmstAgency> ZmstAgency { get; set; }

    public virtual DbSet<ZmstAgencyExamCouns> ZmstAgencyExamCouns { get; set; }

    public virtual DbSet<ZmstAgencyVirtualDirectoryMapping> ZmstAgencyVirtualDirectoryMapping { get; set; }

    public virtual DbSet<ZmstApplicantType> ZmstApplicantType { get; set; }

    public virtual DbSet<ZmstApplicationSummaryBkp> ZmstApplicationSummaryBkp { get; set; }

    public virtual DbSet<ZmstAuthenticationMode> ZmstAuthenticationMode { get; set; }

    public virtual DbSet<ZmstBranch> ZmstBranch { get; set; }

    public virtual DbSet<ZmstCategory> ZmstCategory { get; set; }

    public virtual DbSet<ZmstCountry> ZmstCountry { get; set; }

    public virtual DbSet<ZmstCourseApplied> ZmstCourseApplied { get; set; }

    public virtual DbSet<ZmstCourseAppliedLevel> ZmstCourseAppliedLevel { get; set; }

    public virtual DbSet<ZmstDistrict> ZmstDistrict { get; set; }

    public virtual DbSet<ZmstDocumentType> ZmstDocumentType { get; set; }

    public virtual DbSet<ZmstExamType> ZmstExamType { get; set; }

    public virtual DbSet<ZmstExperienceType> ZmstExperienceType { get; set; }

    public virtual DbSet<ZmstFeeType> ZmstFeeType { get; set; }

    public virtual DbSet<ZmstGender> ZmstGender { get; set; }

    public virtual DbSet<ZmstIdentityType> ZmstIdentityType { get; set; }

    public virtual DbSet<ZmstInstitute> ZmstInstitute { get; set; }

    public virtual DbSet<ZmstInstituteAgency> ZmstInstituteAgency { get; set; }

    public virtual DbSet<ZmstInstituteStream> ZmstInstituteStream { get; set; }

    public virtual DbSet<ZmstInstituteType> ZmstInstituteType { get; set; }

    public virtual DbSet<ZmstMinimumQualification> ZmstMinimumQualification { get; set; }

    public virtual DbSet<ZmstNationality> ZmstNationality { get; set; }

    public virtual DbSet<ZmstPassingStatus> ZmstPassingStatus { get; set; }

    public virtual DbSet<ZmstProgram> ZmstProgram { get; set; }

    public virtual DbSet<ZmstProjects> ZmstProjects { get; set; }

    public virtual DbSet<ZmstQualification> ZmstQualification { get; set; }

    public virtual DbSet<ZmstQualifyingCourse> ZmstQualifyingCourse { get; set; }

    public virtual DbSet<ZmstQualifyingExam> ZmstQualifyingExam { get; set; }

    public virtual DbSet<ZmstQualifyingExamBoard> ZmstQualifyingExamBoard { get; set; }

    public virtual DbSet<ZmstQualifyingExamFrom> ZmstQualifyingExamFrom { get; set; }

    public virtual DbSet<ZmstQualifyingExamLearningMode> ZmstQualifyingExamLearningMode { get; set; }

    public virtual DbSet<ZmstQualifyingExamResultMode> ZmstQualifyingExamResultMode { get; set; }

    public virtual DbSet<ZmstQualifyingExamStream> ZmstQualifyingExamStream { get; set; }

    public virtual DbSet<ZmstQuesPaper> ZmstQuesPaper { get; set; }

    public virtual DbSet<ZmstQuestionPaperMedium> ZmstQuestionPaperMedium { get; set; }

    public virtual DbSet<ZmstQuota> ZmstQuota { get; set; }

    public virtual DbSet<ZmstRankType> ZmstRankType { get; set; }

    public virtual DbSet<ZmstReligion> ZmstReligion { get; set; }

    public virtual DbSet<ZmstResidentialEligibility> ZmstResidentialEligibility { get; set; }

    public virtual DbSet<ZmstSeatCategory> ZmstSeatCategory { get; set; }

    public virtual DbSet<ZmstSeatGender> ZmstSeatGender { get; set; }

    public virtual DbSet<ZmstSeatGroup> ZmstSeatGroup { get; set; }

    public virtual DbSet<ZmstSeatSubCategory> ZmstSeatSubCategory { get; set; }

    public virtual DbSet<ZmstSeatType> ZmstSeatType { get; set; }

    public virtual DbSet<ZmstSecurityQuestion> ZmstSecurityQuestion { get; set; }

    public virtual DbSet<ZmstServiceType> ZmstServiceType { get; set; }

    public virtual DbSet<ZmstSpecialExamPaper> ZmstSpecialExamPaper { get; set; }

    public virtual DbSet<ZmstState> ZmstState { get; set; }

    public virtual DbSet<ZmstStream> ZmstStream { get; set; }

    public virtual DbSet<ZmstSubCategory> ZmstSubCategory { get; set; }

    public virtual DbSet<ZmstSubCategoryPriority> ZmstSubCategoryPriority { get; set; }

    public virtual DbSet<ZmstSubject> ZmstSubject { get; set; }

    public virtual DbSet<ZmstSymbol> ZmstSymbol { get; set; }

    public virtual DbSet<ZmstTrade> ZmstTrade { get; set; }

    public virtual DbSet<ZmstTypeofDisability> ZmstTypeofDisability { get; set; }

    public virtual DbSet<ZmstWillingness> ZmstWillingness { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiSubscriptionKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApiSubsc__3214EC07461B6496");

            entity.Property(e => e.ApplicationKey)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppCaptcha>(entity =>
        {
            entity.ToTable("App_Captcha");

            entity.Property(e => e.CaptchBaseString).HasColumnName("Captch_BaseString");
            entity.Property(e => e.CaptchaKey)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Captcha_Key");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Md5Hash)
                .IsUnicode(false)
                .HasColumnName("Md5_Hash");
        });

        modelBuilder.Entity<AppContactPersonDetails>(entity =>
        {
            entity.ToTable("App_ContactPersonDetails");

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppDocumentTypeRoleMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_DocumentTypeRoleMapping");

            entity.Property(e => e.DocumentTypeId)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppDocumentUploadedDetail>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("App_DocumentUploadedDetail");

            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.Activityid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("activityid");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.DocContent)
                .IsUnicode(false)
                .HasColumnName("docContent");
            entity.Property(e => e.DocId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docId");
            entity.Property(e => e.DocNatureId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("docNatureId");
            entity.Property(e => e.DocSubject)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("docSubject");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docType");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.ModuleRefId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Mater Table Id");
            entity.Property(e => e.ObjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("objectId");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(1500)
                .IsUnicode(false)
                .HasColumnName("objectUrl");
            entity.Property(e => e.SubTime)
                .HasColumnType("datetime")
                .HasColumnName("subTime");
            entity.Property(e => e.VersionNo).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<AppDocumentUploadedDetail14102023>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_DocumentUploadedDetail_14102023");

            entity.Property(e => e.Activityid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("activityid");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.CycleId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cycleId");
            entity.Property(e => e.DocContent)
                .IsUnicode(false)
                .HasColumnName("docContent");
            entity.Property(e => e.DocId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docId");
            entity.Property(e => e.DocNatureId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("docNatureId");
            entity.Property(e => e.DocSubject)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("docSubject");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docType");
            entity.Property(e => e.DocumentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("documentId");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.ObjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("objectId");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(1500)
                .IsUnicode(false)
                .HasColumnName("objectUrl");
            entity.Property(e => e.RequestNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("requestNo");
            entity.Property(e => e.SubTime)
                .HasColumnType("datetime")
                .HasColumnName("subTime");
        });

        modelBuilder.Entity<AppDocumentUploadedDetailHistoty>(entity =>
        {
            entity.ToTable("App_DocumentUploadedDetail_Histoty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activityid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("activityid");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.DocContent)
                .IsUnicode(false)
                .HasColumnName("docContent");
            entity.Property(e => e.DocId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docId");
            entity.Property(e => e.DocNatureId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("docNatureId");
            entity.Property(e => e.DocSubject)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("docSubject");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("docType");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.ModuleRefId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ObjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("objectId");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(1500)
                .IsUnicode(false)
                .HasColumnName("objectUrl");
            entity.Property(e => e.SubTime)
                .HasColumnType("datetime")
                .HasColumnName("subTime");
            entity.Property(e => e.VersionNo).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<AppLoginDetails>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("App_LoginDetails");

            entity.Property(e => e.UserId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("userId");
            entity.Property(e => e.AccessToken)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("accessToken");
            entity.Property(e => e.AuthenticationType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("authenticationType");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emailId");
            entity.Property(e => e.FailedLoginAttemptCount).HasColumnName("failedLoginAttemptCount");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isActive");
            entity.Property(e => e.IsPasswordChanged)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isPasswordChanged");
            entity.Property(e => e.LastFailedLoginAttemptIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastFailedLoginAttemptIP");
            entity.Property(e => e.LastFailedLoginAttemptTime)
                .HasColumnType("datetime")
                .HasColumnName("lastFailedLoginAttemptTime");
            entity.Property(e => e.LastLoginIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lastLoginIP");
            entity.Property(e => e.LastLoginTime)
                .HasColumnType("datetime")
                .HasColumnName("lastLoginTime");
            entity.Property(e => e.LastPasswordChangeIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastPasswordChangeIP");
            entity.Property(e => e.LastPasswordChangeTime)
                .HasColumnType("datetime")
                .HasColumnName("lastPasswordChangeTime");
            entity.Property(e => e.LastPasswordResetIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastPasswordResetIP");
            entity.Property(e => e.LastPasswordResetTime)
                .HasColumnType("datetime")
                .HasColumnName("lastPasswordResetTime");
            entity.Property(e => e.LastSuccessfulLoginIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastSuccessfulLoginIP");
            entity.Property(e => e.LastSuccessfulLoginTime)
                .HasColumnType("datetime")
                .HasColumnName("lastSuccessfulLoginTime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("mobileNo");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PasswordHistory1)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("passwordHistory1");
            entity.Property(e => e.PasswordHistory2)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("passwordHistory2");
            entity.Property(e => e.PasswordHistory3)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("passwordHistory3");
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.SecurityAnswer)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("securityAnswer");
            entity.Property(e => e.SecurityQuestionId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("securityQuestionId");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.UserToken)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("userToken");
        });

        modelBuilder.Entity<AppOnboardingDetails>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_OnboardingDetails");

            entity.Property(e => e.CounsCourseList).IsUnicode(false);
            entity.Property(e => e.CounsLastSessionConductedIn)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CounsLastSessionDescription).IsUnicode(false);
            entity.Property(e => e.CounsLastSessionTechSupportBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CounsTentativeScheduleEnd).HasColumnType("datetime");
            entity.Property(e => e.CounsTentativeScheduleStart).HasColumnType("datetime");
            entity.Property(e => e.ExamCourseList).IsUnicode(false);
            entity.Property(e => e.ExamLastSessionConductedIn)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.ExamLastSessionDescription).IsUnicode(false);
            entity.Property(e => e.ExamLastSessionTechSupportBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ExamTentativeScheduleEnd).HasColumnType("datetime");
            entity.Property(e => e.ExamTentativeScheduleStart).HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.Website)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.YearOfFirstTimeAffilitionSession)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppOnboardingDetailsResponse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_OnboardingDetailsResponse");

            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppOnboardingDetailsResponseLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_OnboardingDetailsResponseLink");

            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Link)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.ResponseId).HasMaxLength(128);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppOnboardingRequest>(entity =>
        {
            entity.HasKey(e => e.RequestNo);

            entity.ToTable("App_OnboardingRequest");

            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(1024);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.CurrentStage)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.MinistryOther)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifyOn).HasColumnType("datetime");
            entity.Property(e => e.OrganizationOther)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.Services)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppOnboardingResponse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_OnboardingResponse");

            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppOnboardingResponseLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_OnboardingResponseLink");

            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Link)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.ResponseId).HasMaxLength(128);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppProjectActivity>(entity =>
        {
            entity.ToTable("App_ProjectActivity");

            entity.Property(e => e.ActivityParentRefId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("RequestNo or Project Code etc");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppProjectActivityHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_App_ProjectActivity_Histoty");

            entity.ToTable("App_ProjectActivity_History");

            entity.Property(e => e.ActivityParentRefId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppProjectCost>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.FinancialComponentId }).HasName("PK_App_ProjectCost_1");

            entity.ToTable("App_ProjectCost");

            entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppProjectDetails>(entity =>
        {
            entity.ToTable("App_ProjectDetails");

            entity.Property(e => e.AgencyName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EfileNo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EFileNo");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.ModifyBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifyOn).HasColumnType("datetime");
            entity.Property(e => e.Nicsipino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NICSIPINo");
            entity.Property(e => e.Piamount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PIAmount");
            entity.Property(e => e.Pidate)
                .HasColumnType("datetime")
                .HasColumnName("PIDate");
            entity.Property(e => e.PrizmId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProjectCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("System Generated;pattern;maxlength");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("required;pattern;maxlength;minlength");
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.RequestLetterDate).HasColumnType("datetime");
            entity.Property(e => e.RequestLetterNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RequestNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SubmitBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppProjectExpenditure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_App_ProjectExpenditure");

            entity.ToTable("App_ProjectExpenditure");

            entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppProjectPaymentDetails>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("App_ProjectPaymentDetails");

            entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Gst)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("GST");
            entity.Property(e => e.IncomeTax).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentParentRefId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Request No or any other ");
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("TDS");
            entity.Property(e => e.Utrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UTRNo");
        });

        modelBuilder.Entity<AppRoleModulePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModuleId });

            entity.ToTable("App_RoleModulePermission");

            entity.Property(e => e.RoleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModuleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsReadOnly)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<AppUserRoleMapping>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.ToTable("App_UserRoleMapping");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RoleID");
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.IsReadOnly)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
        });

        modelBuilder.Entity<ApplicationDayWise>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__Applicat__C00006D58DF4DA34");

            entity.Property(e => e.AppId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("appId");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Summary)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedTime");
        });

        modelBuilder.Entity<ApplicationSchedule>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__Applicat__C00006D509ED8C66");

            entity.Property(e => e.AppId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("appId");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Summary)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedTime");
        });

        modelBuilder.Entity<ApplicationSummary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3213E83F5FD20F21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("AdminURL");
            entity.Property(e => e.AgencyId)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ApiLink)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("apiLink");
            entity.Property(e => e.AppId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("appId");
            entity.Property(e => e.AppTitle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("appTitle");
            entity.Property(e => e.AppType)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("appType");
            entity.Property(e => e.AppUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("appURL");
            entity.Property(e => e.AppYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("appYear");
            entity.Property(e => e.ContactDetail)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Summary)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.TotalRound).HasColumnName("totalRound");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedTime");
        });

        modelBuilder.Entity<BkpAppRoleModulePermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BKP_App_RoleModulePermission");

            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsReadOnly)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModuleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BkpDel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("bkp_Del");

            entity.Property(e => e.Aaa)
                .IsUnicode(false)
                .HasColumnName("aaa");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<Cgconfigration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("__CGConfigration");

            entity.Property(e => e.ControlType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CoulmnName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisplayCaption)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MasterDataProviderTable)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Validation)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CgconfigurationControleTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK___ConfigurationTest");

            entity.ToTable("CGConfigurationControleTest");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Addresss).IsUnicode(false);
            entity.Property(e => e.District)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Gender)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Hobbies)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConfigurationApisecureKey>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ConfigurationAPISecureKey");

            entity.Property(e => e.KeyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.SecretKey)
                .HasMaxLength(800)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConfigurationEnvironment>(entity =>
        {
            entity.HasKey(e => e.ApplicationId);

            entity.Property(e => e.ApplicationId).ValueGeneratedNever();
            entity.Property(e => e.AdminKey)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("adminKey");
            entity.Property(e => e.AgencyKey)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("agencyKey");
            entity.Property(e => e.AuthMode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CaptchaMode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("captchaMode");
            entity.Property(e => e.CaptchaModeDesc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("captchaModeDesc");
            entity.Property(e => e.CaptchaValue)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("captchaValue");
            entity.Property(e => e.EnvironmentMode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("environmentMode");
            entity.Property(e => e.EnvironmentModeDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("environmentModeDesc");
            entity.Property(e => e.IsDataCached)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isDataCached");
            entity.Property(e => e.IsOfflineEnabled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isOfflineEnabled");
            entity.Property(e => e.IsOfflineEnabledAdmin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isOfflineEnabledAdmin");
            entity.Property(e => e.OfflineModeMessage)
                .HasMaxLength(1000)
                .HasColumnName("offlineModeMessage");
            entity.Property(e => e.OfflineModeMessageAdmin)
                .HasMaxLength(1000)
                .HasColumnName("offlineModeMessageAdmin");
            entity.Property(e => e.OtpMedium)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("otpMedium");
        });

        modelBuilder.Entity<EmployeeDetails>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK_EmpDetails");

            entity.Property(e => e.EmpId).HasColumnName("empId");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("required,alphanumeric")
                .HasColumnName("address");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("designation");
            entity.Property(e => e.Division)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("division");
            entity.Property(e => e.Dob)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("dob");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required,email")
                .HasColumnName("emailId");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("empCode");
            entity.Property(e => e.EmpName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required,alphanumeric")
                .HasColumnName("empName");
            entity.Property(e => e.EmpStatus)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("empStatus");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required,alphanumeric")
                .HasColumnName("fName");
            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("id");
            entity.Property(e => e.IdNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("idNumber");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("ipaddress");
            entity.Property(e => e.JoinDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("joinDate");
            entity.Property(e => e.MName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required,alphanumeric")
                .HasColumnName("mName");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("required,number,maxlength,minlength")
                .HasColumnName("mobileNumber");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("number")
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("remarks");
            entity.Property(e => e.ReportingOfficer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required,alphabet")
                .HasColumnName("reportingOfficer");
            entity.Property(e => e.ResignDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("resignDate");
            entity.Property(e => e.SubmitTime)
                .HasColumnType("datetime")
                .HasColumnName("submitTime");
        });

        modelBuilder.Entity<EmployeeWorkOrder>(entity =>
        {
            entity.HasKey(e => new { e.EmpCode, e.WorkorderNo });

            entity.ToTable("employeeWorkOrder");

            entity.Property(e => e.EmpCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("empCode");
            entity.Property(e => e.WorkorderNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("workorderNo");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.Ip)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("IP Address Of Matchin")
                .HasColumnName("IP");
            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.Properties).HasColumnType("xml");
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<MdActivityType>(entity =>
        {
            entity.HasKey(e => e.ActivityId);

            entity.ToTable("MD_ActivityType");

            entity.Property(e => e.ActivityId).ValueGeneratedNever();
            entity.Property(e => e.Activity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ActivityGroup)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdAgency>(entity =>
        {
            entity.HasKey(e => e.AgencyId);

            entity.ToTable("Md_Agency");

            entity.Property(e => e.AgencyId).ValueGeneratedNever();
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.AgencyName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("required;pattern;maxlength;minlength");
            entity.Property(e => e.AgencyType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isActive");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.ServiceTypeId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StateId)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdAgencyType>(entity =>
        {
            entity.HasKey(e => e.AgencyTypeId);

            entity.ToTable("MD_AgencyType");

            entity.Property(e => e.AgencyTypeId).ValueGeneratedNever();
            entity.Property(e => e.AgencyType)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdDistrict>(entity =>
        {
            entity.HasKey(e => new { e.StateId, e.Id });

            entity.ToTable("MD_District");

            entity.Property(e => e.StateId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("stateId");
            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modified_date");

            entity.HasOne(d => d.State).WithMany(p => p.MdDistrict)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MD_District_MD_State");
        });

        modelBuilder.Entity<MdDocumentType>(entity =>
        {
            entity.ToTable("MD_DocumentType");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_date");
            entity.Property(e => e.DisplayPriority).HasColumnName("displayPriority");
            entity.Property(e => e.DocumentNatureType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("documentNatureType");
            entity.Property(e => e.DocumentNatureTypeDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documentNatureTypeDesc");
            entity.Property(e => e.Format)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("format");
            entity.Property(e => e.IsPasswordProtected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isPasswordProtected");
            entity.Property(e => e.MaxSize)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("maxSize");
            entity.Property(e => e.MinSize)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("minSize");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modified_date");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<MdEmailRecipient>(entity =>
        {
            entity.ToTable("MD_EmailRecipient");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdEmpStatus>(entity =>
        {
            entity.ToTable("MD_EmpStatus");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdExam>(entity =>
        {
            entity.ToTable("MD_Exam");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Exam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("exam");
        });

        modelBuilder.Entity<MdIdType>(entity =>
        {
            entity.ToTable("MD_IdType");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IdType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idType");
        });

        modelBuilder.Entity<MdMainModule>(entity =>
        {
            entity.HasKey(e => e.MainModuleId);

            entity.ToTable("MD_MainModule");

            entity.Property(e => e.MainModuleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CssClass).HasMaxLength(100);
            entity.Property(e => e.Heading)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SubHeading)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdMinistry>(entity =>
        {
            entity.HasKey(e => e.MinistryId);

            entity.ToTable("MD_Ministry");

            entity.Property(e => e.MinistryId).ValueGeneratedNever();
            entity.Property(e => e.MinistryName)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdModule>(entity =>
        {
            entity.HasKey(e => e.ModuleId);

            entity.ToTable("MD_Module");

            entity.Property(e => e.ModuleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Heading)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MainModule)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Parent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SubHeading)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdModule24082023>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MD_Module_24082023");

            entity.Property(e => e.Heading)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModuleId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Parent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SubHeading)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdOnboardingActivity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MD_OnboardingActivity");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<MdOrganization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId);

            entity.ToTable("MD_Organization");

            entity.Property(e => e.OrganizationId)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.StateId)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdProjectFinancialComponents>(entity =>
        {
            entity.HasKey(e => e.FinancialComponentId);

            entity.ToTable("Md_ProjectFinancialComponents");

            entity.Property(e => e.FinancialComponentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FinancialComponent)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MdRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("MD_Role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("roleId");
            entity.Property(e => e.Description)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<MdSmsEmailTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId);

            entity.ToTable("MD_SmsEmailTemplate");

            entity.Property(e => e.TemplateId)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MessageSubject)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("messageSubject");
            entity.Property(e => e.MessageTemplate)
                .IsUnicode(false)
                .HasColumnName("messageTemplate");
            entity.Property(e => e.MessageTypeId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("messageTypeId");
            entity.Property(e => e.RegisteredTemplateId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdState>(entity =>
        {
            entity.ToTable("MD_State");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modified_date");
        });

        modelBuilder.Entity<MdStatus>(entity =>
        {
            entity.HasKey(e => new { e.StatusId, e.ActivityId });

            entity.ToTable("MD_Status");

            entity.Property(e => e.StatusId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ActivityId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MdWorkOrderAgency>(entity =>
        {
            entity.ToTable("MD_WorkOrderAgency");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgencyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("agencyName");
        });

        modelBuilder.Entity<MdYear>(entity =>
        {
            entity.HasKey(e => e.YearId).HasName("PK__Md_Year__C33A18CD3E9B9C9F");

            entity.ToTable("Md_Year");

            entity.Property(e => e.YearId)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Abbrivation)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.YearGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QualificationDetails>(entity =>
        {
            entity.ToTable("qualificationDetails");

            entity.Property(e => e.QualificationDetailsId).HasColumnName("qualificationDetailsId");
            entity.Property(e => e.BoardUniv)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("boardUniv");
            entity.Property(e => e.Division)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("division");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("empCode");
            entity.Property(e => e.ExamPassed)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("examPassed");
            entity.Property(e => e.PassYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("passYear");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubmitTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<RequestListInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestL__3214EC07F1945E8D");

            entity.Property(e => e.AgencyType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OranizationName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAuthorization>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User_Authorization");

            entity.Property(e => e.RefreshToken)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("refreshToken");
            entity.Property(e => e.Token)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<ValidationTest>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("email;maxlength;minlength");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("number;maxlength;minlength");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("maxlength,minlength");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("url")
                .HasColumnName("URL");
        });

        modelBuilder.Entity<WorkOrderDetails>(entity =>
        {
            entity.HasKey(e => e.WorkorderId);

            entity.Property(e => e.WorkorderId).HasColumnName("workorderId");
            entity.Property(e => e.AgencyName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("agencyName");
            entity.Property(e => e.DocName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("docName");
            entity.Property(e => e.IssueDate)
                .HasComment("required")
                .HasColumnType("datetime")
                .HasColumnName("issueDate");
            entity.Property(e => e.NoofMonths)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("noofMonths");
            entity.Property(e => e.ProjectCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("")
                .HasColumnName("projectCode");
            entity.Property(e => e.ResourceCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("alphanumeric,required")
                .HasColumnName("resourceCategory");
            entity.Property(e => e.ResourceNo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("required")
                .HasColumnName("resourceNo");
            entity.Property(e => e.WorkorderFrom)
                .HasComment("required")
                .HasColumnType("datetime")
                .HasColumnName("workorderFrom");
            entity.Property(e => e.WorkorderNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("alphanumeric,required")
                .HasColumnName("workorderNo");
            entity.Property(e => e.WorkorderTo)
                .HasComment("required")
                .HasColumnType("datetime")
                .HasColumnName("workorderTo");
        });

        modelBuilder.Entity<WorkorderdetailsBkp>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AgencyName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("agencyName");
            entity.Property(e => e.DocName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("docName");
            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime")
                .HasColumnName("issueDate");
            entity.Property(e => e.NoofMonths)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("noofMonths");
            entity.Property(e => e.ProjectCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("projectCode");
            entity.Property(e => e.ResourceCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("resourceCategory");
            entity.Property(e => e.ResourceNo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("resourceNo");
            entity.Property(e => e.WorkorderFrom)
                .HasColumnType("datetime")
                .HasColumnName("workorderFrom");
            entity.Property(e => e.WorkorderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("workorderId");
            entity.Property(e => e.WorkorderNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("workorderNo");
            entity.Property(e => e.WorkorderTo)
                .HasColumnType("datetime")
                .HasColumnName("workorderTo");
        });

        modelBuilder.Entity<ZmstActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId);

            entity.ToTable("ZMst_Activity");

            entity.Property(e => e.ActivityId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("activityId");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("activityName");
        });

        modelBuilder.Entity<ZmstAgency>(entity =>
        {
            entity.HasKey(e => e.AgencyId).HasName("PK_ZMST_Board");

            entity.ToTable("ZMST_Agency");

            entity.Property(e => e.AgencyId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("Required");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.AgencyAbbr)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AgencyName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Alphanumeric");
            entity.Property(e => e.AgencyType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Required");
            entity.Property(e => e.BoardRequestLetter)
                .HasComment("Required")
                .HasColumnName("boardRequestLetter");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Required")
                .HasColumnName("isActive");
            entity.Property(e => e.Priority)
                .HasComment("Only Number,Required")
                .HasColumnName("priority");
            entity.Property(e => e.ServiceTypeId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StateId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("Required");
        });

        modelBuilder.Entity<ZmstAgencyExamCouns>(entity =>
        {
            entity.HasKey(e => new { e.AgencyId, e.ExamCounsId });

            entity.ToTable("Zmst_AgencyExamCouns");

            entity.Property(e => e.AgencyId)
                .HasComment("Required")
                .HasColumnName("agencyId");
            entity.Property(e => e.ExamCounsId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("Required")
                .HasColumnName("examCounsId");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Required")
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstAgencyVirtualDirectoryMapping>(entity =>
        {
            entity.HasKey(e => new { e.AgencyId, e.VirtualDirectoryType });

            entity.ToTable("Zmst_AgencyVirtualDirectoryMapping");

            entity.Property(e => e.AgencyId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.VirtualDirectoryType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.BaseDirectory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("alphanumeric,required");
            entity.Property(e => e.VirtualDirectory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("alphanumeric,required");
        });

        modelBuilder.Entity<ZmstApplicantType>(entity =>
        {
            entity.ToTable("ZMst_ApplicantType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstApplicationSummaryBkp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ZMST_ApplicationSummaryBKP");

            entity.Property(e => e.AdminUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("AdminURL");
            entity.Property(e => e.AgencyId)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ApiLink)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("apiLink");
            entity.Property(e => e.AppId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("appId");
            entity.Property(e => e.AppTitle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("appTitle");
            entity.Property(e => e.AppType)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("appType");
            entity.Property(e => e.AppUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("appURL");
            entity.Property(e => e.AppYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("appYear");
            entity.Property(e => e.ContactDetail)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Summary)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.TotalRound).HasColumnName("totalRound");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedTime");
        });

        modelBuilder.Entity<ZmstAuthenticationMode>(entity =>
        {
            entity.HasKey(e => e.AuthCode);

            entity.ToTable("ZMst_AuthenticationMode");

            entity.Property(e => e.AuthCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Authmode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstBranch>(entity =>
        {
            entity.HasKey(e => e.BrCd).HasName("PK_Zmst_Branch_1");

            entity.ToTable("Zmst_Branch");

            entity.Property(e => e.BrCd)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.BrNm)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Stream)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ZMst_Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("categoryId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<ZmstCountry>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Zmst_Country");

            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("alphabet;maxlength")
                .HasColumnName("code");
            entity.Property(e => e.Isdcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("number;maxlength")
                .HasColumnName("isdcode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("alphabet;maxlength")
                .HasColumnName("name");
            entity.Property(e => e.Priority)
                .HasComment("number")
                .HasColumnName("priority");
            entity.Property(e => e.SAarccode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("alphabet;maxlength")
                .HasColumnName("sAARCCode");
            entity.Property(e => e.SAarcname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("alphabet;maxlength")
                .HasColumnName("sAARCName");
        });

        modelBuilder.Entity<ZmstCourseApplied>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.ToTable("ZMst_CourseApplied");

            entity.Property(e => e.CourseId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.AlternameNames)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("alternameNames");
            entity.Property(e => e.CourseName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("courseName");
        });

        modelBuilder.Entity<ZmstCourseAppliedLevel>(entity =>
        {
            entity.HasKey(e => e.CourseLevelId);

            entity.ToTable("Zmst_CourseAppliedLevel");

            entity.Property(e => e.CourseLevelId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("courseLevelId");
            entity.Property(e => e.CourseLevelName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("courseLevelName");
        });

        modelBuilder.Entity<ZmstDistrict>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Zmst_District");

            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.DistrictId)
                .HasMaxLength(255)
                .HasColumnName("districtId");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .HasColumnName("districtName");
            entity.Property(e => e.StateId)
                .HasMaxLength(255)
                .HasColumnName("stateId");
            entity.Property(e => e.StateName)
                .HasMaxLength(255)
                .HasColumnName("stateName");
        });

        modelBuilder.Entity<ZmstDocumentType>(entity =>
        {
            entity.HasKey(e => e.DocumentTypeId).HasName("PK__Zmst_Doc__A48A012EF81B1E89");

            entity.ToTable("Zmst_DocumentType");

            entity.Property(e => e.DocumentTypeId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("documentTypeId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ZmstExamType>(entity =>
        {
            entity.ToTable("Zmst_ExamType");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstExperienceType>(entity =>
        {
            entity.ToTable("Zmst_ExperienceType");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ExperienceType)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstFeeType>(entity =>
        {
            entity.HasKey(e => e.ActivityId);

            entity.ToTable("Zmst_FeeType");

            entity.Property(e => e.ActivityId)
                .ValueGeneratedNever()
                .HasColumnName("activityId");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstGender>(entity =>
        {
            entity.HasKey(e => e.GenderId);

            entity.ToTable("Zmst_Gender");

            entity.Property(e => e.GenderId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("genderId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.GenderName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("genderName");
        });

        modelBuilder.Entity<ZmstIdentityType>(entity =>
        {
            entity.HasKey(e => e.IdentityTypeId);

            entity.ToTable("Zmst_IdentityType");

            entity.Property(e => e.IdentityTypeId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("identityTypeId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstInstitute>(entity =>
        {
            entity.HasKey(e => e.InstCd);

            entity.ToTable("Zmst_Institute");

            entity.Property(e => e.InstCd)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.AbbrNm)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Aishe)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("AISHE");
            entity.Property(e => e.AltEmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("alphabet");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("alphabet");
            entity.Property(e => e.District)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("email");
            entity.Property(e => e.InstAdd)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InstFax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InstNm)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.InstPhone)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("number");
            entity.Property(e => e.InstTypeId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.InstWebSite)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("url");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("number,maxlength");
            entity.Property(e => e.OldInstituteCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("oldInstituteCode");
            entity.Property(e => e.Pincode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("number,maxlength");
            entity.Property(e => e.SeatType)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("required");
        });

        modelBuilder.Entity<ZmstInstituteAgency>(entity =>
        {
            entity.HasKey(e => new { e.InstCd, e.AgencyId });

            entity.ToTable("Zmst_InstituteAgency");

            entity.Property(e => e.InstCd)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.AgencyId)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstInstituteStream>(entity =>
        {
            entity.HasKey(e => new { e.InstCd, e.StreamId });

            entity.ToTable("Zmst_InstituteStream");

            entity.Property(e => e.InstCd)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.StreamId)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstInstituteType>(entity =>
        {
            entity.HasKey(e => e.InstituteTypeId);

            entity.ToTable("Zmst_InstituteType");

            entity.Property(e => e.InstituteTypeId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.InstituteType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("required");
            entity.Property(e => e.Priority).HasComment("number,required");
        });

        modelBuilder.Entity<ZmstMinimumQualification>(entity =>
        {
            entity.HasKey(e => e.MinimumQualId);

            entity.ToTable("Zmst_MinimumQualification");

            entity.Property(e => e.MinimumQualId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("minimumQualId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstNationality>(entity =>
        {
            entity.HasKey(e => e.NationalityId);

            entity.ToTable("Zmst_Nationality");

            entity.Property(e => e.NationalityId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("nationalityId");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstPassingStatus>(entity =>
        {
            entity.HasKey(e => e.PassingStatusId);

            entity.ToTable("Zmst_PassingStatus");

            entity.Property(e => e.PassingStatusId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("passingStatusId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstProgram>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ZMST_Program");

            entity.Property(e => e.Agencyid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agencyid");
            entity.Property(e => e.Brcd)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("brcd");
            entity.Property(e => e.BrcdOrg)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("brcdOrg");
            entity.Property(e => e.Brnm)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("brnm");
            entity.Property(e => e.Bshift)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("bshift");
            entity.Property(e => e.Btfw)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("btfw");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<ZmstProjects>(entity =>
        {
            entity.HasKey(e => e.ProjectId);

            entity.ToTable("Zmst_Projects");

            entity.Property(e => e.ProjectId)
                .ValueGeneratedNever()
                .HasColumnName("projectId");
            entity.Property(e => e.AcademicYear).HasColumnName("academicYear");
            entity.Property(e => e.AgencyId).HasColumnName("agencyId");
            entity.Property(e => e.Attempt).HasColumnName("attempt");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ExamCounsid)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("examCounsid");
            entity.Property(e => e.IsLive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modified_date");
            entity.Property(e => e.Pinitiated)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PInitiated");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("projectName");
            entity.Property(e => e.RequestLetter).HasColumnName("requestLetter");
            entity.Property(e => e.ServiceType).HasColumnName("serviceType");
        });

        modelBuilder.Entity<ZmstQualification>(entity =>
        {
            entity.HasKey(e => e.QualificationId);

            entity.ToTable("Zmst_Qualification");

            entity.Property(e => e.QualificationId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualificationId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ZmstQualifyingCourse>(entity =>
        {
            entity.HasKey(e => new { e.QualificationCourseId, e.QualificationId });

            entity.ToTable("ZMst_QualifyingCourse");

            entity.Property(e => e.QualificationCourseId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualificationCourseId");
            entity.Property(e => e.QualificationId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualificationId");
            entity.Property(e => e.QualificationCourseName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("qualificationCourseName");
        });

        modelBuilder.Entity<ZmstQualifyingExam>(entity =>
        {
            entity.HasKey(e => e.QualifyingExamId);

            entity.ToTable("ZMst_QualifyingExam");

            entity.Property(e => e.QualifyingExamId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualifyingExamId");
            entity.Property(e => e.QualifyingExamName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("qualifyingExamName");
        });

        modelBuilder.Entity<ZmstQualifyingExamBoard>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.QualificationId });

            entity.ToTable("Zmst_QualifyingExamBoard");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.QualificationId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualificationId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstQualifyingExamFrom>(entity =>
        {
            entity.HasKey(e => e.QualExamFromId);

            entity.ToTable("Zmst_QualifyingExamFrom");

            entity.Property(e => e.QualExamFromId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("qualExamFromId");
            entity.Property(e => e.QualExamFromName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("qualExamFromName");
        });

        modelBuilder.Entity<ZmstQualifyingExamLearningMode>(entity =>
        {
            entity.ToTable("Zmst_QualifyingExamLearningMode");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstQualifyingExamResultMode>(entity =>
        {
            entity.ToTable("Zmst_QualifyingExamResultMode");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Alternatenames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternatenames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstQualifyingExamStream>(entity =>
        {
            entity.HasKey(e => e.QualStreamId);

            entity.ToTable("Zmst_QualifyingExamStream");

            entity.Property(e => e.QualStreamId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualStreamId");
            entity.Property(e => e.QualStreamName)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("qualStreamName");
        });

        modelBuilder.Entity<ZmstQuesPaper>(entity =>
        {
            entity.HasKey(e => e.PaperId);

            entity.ToTable("ZMst_QuesPaper");

            entity.Property(e => e.PaperId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("paperId");
            entity.Property(e => e.PaperName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("paperName");
        });

        modelBuilder.Entity<ZmstQuestionPaperMedium>(entity =>
        {
            entity.HasKey(e => e.MediumId);

            entity.ToTable("ZMst_QuestionPaperMedium");

            entity.Property(e => e.MediumId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("mediumId");
            entity.Property(e => e.MediumName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mediumName");
        });

        modelBuilder.Entity<ZmstQuota>(entity =>
        {
            entity.HasKey(e => e.QuotaId);

            entity.ToTable("Zmst_Quota");

            entity.Property(e => e.QuotaId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("quotaId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ZmstRankType>(entity =>
        {
            entity.ToTable("Zmst_RankType");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstReligion>(entity =>
        {
            entity.HasKey(e => e.ReligionId);

            entity.ToTable("Zmst_Religion");

            entity.Property(e => e.ReligionId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("religionId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstResidentialEligibility>(entity =>
        {
            entity.HasKey(e => e.ResidentialEligibilityId);

            entity.ToTable("ZMst_ResidentialEligibility");

            entity.Property(e => e.ResidentialEligibilityId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("residentialEligibilityId");
            entity.Property(e => e.ResidentialEligibilityName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("residentialEligibilityName");
        });

        modelBuilder.Entity<ZmstSeatCategory>(entity =>
        {
            entity.HasKey(e => e.SeatCategoryId);

            entity.ToTable("Zmst_SeatCategory");

            entity.Property(e => e.SeatCategoryId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("seatCategoryId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSeatGender>(entity =>
        {
            entity.HasKey(e => e.SeatGenderId);

            entity.ToTable("Zmst_SeatGender");

            entity.Property(e => e.SeatGenderId)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSeatGroup>(entity =>
        {
            entity.ToTable("Zmst_SeatGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSeatSubCategory>(entity =>
        {
            entity.HasKey(e => e.SeatSubCategoryId);

            entity.ToTable("Zmst_SeatSubCategory");

            entity.Property(e => e.SeatSubCategoryId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("seatSubCategoryId");
            entity.Property(e => e.Alternatenames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternatenames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSeatType>(entity =>
        {
            entity.ToTable("Zmst_SeatType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSecurityQuestion>(entity =>
        {
            entity.HasKey(e => e.SecurityQuesId);

            entity.ToTable("ZMst_SecurityQuestion");

            entity.Property(e => e.SecurityQuesId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("securityQuesId");
            entity.Property(e => e.SecurityQues)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("securityQues");
        });

        modelBuilder.Entity<ZmstServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId);

            entity.ToTable("Zmst_ServiceType");

            entity.Property(e => e.ServiceTypeId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("serviceTypeId");
            entity.Property(e => e.ServiceTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serviceTypeName");
        });

        modelBuilder.Entity<ZmstSpecialExamPaper>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SpecialExamId });

            entity.ToTable("Zmst_SpecialExamPaper");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.SpecialExamId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("specialExamId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstState>(entity =>
        {
            entity.HasKey(e => e.StateId);

            entity.ToTable("ZMst_State");

            entity.Property(e => e.StateId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.StateName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZmstStream>(entity =>
        {
            entity.HasKey(e => e.StreamId);

            entity.ToTable("Zmst_Stream");

            entity.Property(e => e.StreamId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("streamId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.StreamName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("streamName");
        });

        modelBuilder.Entity<ZmstSubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId);

            entity.ToTable("ZMst_SubCategory");

            entity.Property(e => e.SubCategoryId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("subCategoryId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("subCategoryName");
        });

        modelBuilder.Entity<ZmstSubCategoryPriority>(entity =>
        {
            entity.HasKey(e => new { e.SubCategoryPriorityId, e.SubCategoryId });

            entity.ToTable("Zmst_SubCategoryPriority");

            entity.Property(e => e.SubCategoryPriorityId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("subCategoryPriorityId");
            entity.Property(e => e.SubCategoryId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("subCategoryId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstSubject>(entity =>
        {
            entity.HasKey(e => new { e.QualificationId, e.SubjectId });

            entity.ToTable("Zmst_Subject");

            entity.Property(e => e.QualificationId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("qualificationId");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("subjectId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("subjectName");
        });

        modelBuilder.Entity<ZmstSymbol>(entity =>
        {
            entity.HasKey(e => e.SymbolId);

            entity.ToTable("Zmst_Symbol");

            entity.Property(e => e.SymbolId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("symbolId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstTrade>(entity =>
        {
            entity.ToTable("Zmst_Trade");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstTypeofDisability>(entity =>
        {
            entity.ToTable("Zmst_TypeofDisability");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ZmstWillingness>(entity =>
        {
            entity.HasKey(e => e.WillingnessId);

            entity.ToTable("Zmst_Willingness");

            entity.Property(e => e.WillingnessId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("willingnessId");
            entity.Property(e => e.AlternateNames)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("alternateNames");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

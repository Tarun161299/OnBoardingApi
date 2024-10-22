using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnBoardingSystem.Data;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;
using OnBoardingSystem.Data.Business.Behaviors;
using OnBoardingSystem.Data.Business.Services;
using OnBoardingSystem.Data.EF.Models;
using OnBoardingSystem.Data.Interfaces;
using OnBoardingSystem.Data.Repositories;
using OnBoardingSystem.Service.Controllers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using SixLaborsCaptcha.Mvc.Core;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;
using EfModels = OnBoardingSystem.Data.EF.Models;

var builder = WebApplication.CreateBuilder(args);
// DD
var connectionString = builder.Configuration.GetConnectionString("OnBoardingSystem");
string domain = builder.Configuration.GetSection("Domain").Value.ToString();
var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar)
                 },
};
////
var logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
               , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
               .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddDbContext<EfModels.OBSDBContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
//builder.Services.AddTransient<EfModels.OBSDBContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
builder.Services.Configure<MailService>(builder.Configuration.GetSection("MailSettings"));

// Add services to the container.
builder.Services.AddSixLabCaptcha(x =>
{
    x.DrawLines = 4;
});
builder.Services.AddControllers(config =>
{   //*to allow Authorize Globally*
    //var policy = new AuthorizationPolicyBuilder()
    //                 .RequireAuthenticatedUser()
    //                 .Build();
    //config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 6.0 Web API"
    });
    //To Enable authorization using Swagger (JWT)  
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: Bearer 12345abcdef",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
    c.OperationFilter<AddCustomHeaderService>();

});

builder.Services.AddSwaggerGenWithConventionalRoutes(options =>
{
    options.IgnoreTemplateFunc = (template) => template.StartsWith("api/");
    options.SkipDefaults = true;
});

// DD
////builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
////builder.Services.AddTransient<IMdMinistryDirector, MdMinistryDirector>();
////builder.Services.AddTransient<IAppOnboardingRequestDirector, AppOnboardingRequestDirector>();
////builder.Services.AddTransient<IRequestListInfoDirector, RequestListInfoDirector>();
////builder.Services.AddTransient<IMdOrganizationDirector, MdOrganizationDirector>();
////builder.Services.AddTransient<IMdAgencyTypeDirector, MdAgencyTypeDirector>();
////builder.Services.AddTransient<IMdServiceTypeDirector, MdServiceTypeDirector>();
////builder.Services.AddTransient<IAppOnboardingDetailDirector, AppOnboardingDetailDirector>();
////builder.Services.AddScoped<IAppOnboardingAdminloginDirector, AppOnboardingAdminloginDirector>();
////builder.Services.AddTransient<IAppOnboardingDetailDirector, AppOnboardingDetailDirector>();
////builder.Services.AddTransient<IAppOnboardingResponseDirector, AppOnboardingResponseDirector>();
////builder.Services.AddTransient<IAppContactPersonDetailDirector, AppContactPersonDetailDirector>();
////builder.Services.AddTransient<IStateDirector, StateDirector>();
////builder.Services.AddTransient<IAppProjectDetailsDirector, AppProjectDetailsDirector>();
////builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
////builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
////builder.Services.AddTransient<IMailServiceDirector, MailServiceDirector>();
////builder.Services.AddTransient<UtilityService, UtilityService>();
////builder.Services.AddSingleton<JWTTokenService, JWTTokenService>();
////builder.Services.AddTransient<SMSService, SMSService>();
////builder.Services.AddTransient<IMdStateDirector, MdStateDirector>();
////builder.Services.AddTransient<IAgencyDirector, AgencyDirector>();
////builder.Services.AddTransient<IMdDocumentTypeDirector, MdDocumentTypeDirector>();
////builder.Services.AddTransient<IZmstServiceTypeDirector, ZmstServiceTypeDirector>();
////builder.Services.AddTransient<IZmstAgencyExamCounsDirector, ZmstAgencyExamCounsDirector>();
////builder.Services.AddTransient<IZmstProjectsDirector, ZmstProjectsDirector>();
////builder.Services.AddTransient<IMdFinancialComponentDirector, MdProjectFinancialComponentDirector>();
////builder.Services.AddTransient<IAppProjectCostDiector, AppProjectCostDirector>();
////builder.Services.AddTransient<IAppDocumentUploadedDetailDirector, AppDocumentUploadedDetailDirector>();
////builder.Services.AddTransient<IMDModuleDirector, MDModuleDirector>();
////builder.Services.AddTransient<IMdDistrictDirector, MdDistrictDirector>();
////builder.Services.AddTransient<IMdExamTypeDirector, MdExamTypeDirector>();
//////builder.Services.AddTransient<IMdStateDirector, MdStateDirector>();
////builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
////builder.Services.AddTransient<IZmstCountryDirector, ZmstCountryDirector>();
////builder.Services.AddTransient<IApplicationSummaryDirector, ApplicationSummaryDirector>();
////builder.Services.AddTransient<IZmstWillingnessDirector, ZmstWillingnessDirector>();
////builder.Services.AddTransient<IMdSmsEmailTemplateDirector, MdSmsEmailTemplateDirector>();
////builder.Services.AddTransient<EncryptionDecryptionService, EncryptionDecryptionService>();
////builder.Services.AddTransient<IApplicationScheduleDirector, ApplicationScheduleDirector>();
////builder.Services.AddTransient<IDaywiseRegistrationDirector, DaywiseRegistrationDirector>();
////builder.Services.AddTransient<IZmstAgencyDirector, ZmstAgencyDirector>();
////builder.Services.AddTransient<IWorkOrderDetailsDirector, WorkOrderDetailsDirector>();
////builder.Services.AddTransient<IMdWorkOrderAgencyDirector, MdWorkOrderAgencyDirector>();
////builder.Services.AddTransient<IZmstSubjectDirector, ZmstSubjectDirector>();
////builder.Services.AddTransient<IZmstQuotaDirector, ZmstQuotaDirector>();
////builder.Services.AddTransient<IZmstRankTypeDirector, ZmstRankTypeDirector>();
////builder.Services.AddTransient<IZmstReligionDirector, ZmstReligionDirector>();
////builder.Services.AddTransient<IZmstResidentialEligibilityDirector, ZmstResidentialEligibilityDirector>();
////builder.Services.AddTransient<IZmstSeatCategoryDirector, ZmstSeatCategoryDirector>();
////builder.Services.AddTransient<IZmstSeatGenderDirector, ZmstSeatGenderDirector>();
////builder.Services.AddTransient<IZmstSpecialExamPaperDirector, ZmstSpecialExamPaperDirector>();
////builder.Services.AddTransient<IZmstStreamDirector, ZmstStreamDirector>();
////builder.Services.AddTransient<IZmstSubCategoryDirector, ZmstSubCategoryDirector>();
////builder.Services.AddTransient<IZmstSymbolDirector, ZmstSymbolDirector>();
////builder.Services.AddTransient<IZmstTypeofDisabilityDirector, ZmstTypeofDisabilityDirector>();
////builder.Services.AddTransient<IZmstBranchDirector, ZmstBranchDirector>();
////builder.Services.AddTransient<IZmstCategoryDirector, ZmstCategoryDirector>();
////builder.Services.AddTransient<IZmstCourseAppliedDirector, ZmstCourseAppliedDirector>();
////builder.Services.AddTransient<IZmstCourseAppliedLevelDirector, ZmstCourseAppliedLevelDirector>();
////builder.Services.AddTransient<IZmstDocumentTypeDirector, ZmstDocumentTypeDirector>();
////builder.Services.AddTransient<IZmstFeeTypeDirector, ZmstFeeTypeDirector>();
////builder.Services.AddTransient<IZmstGenderDirector, ZmstGenderDirector>();
////builder.Services.AddTransient<IZmstIdentityTypeDirector, ZmstIdentityTypeDirector>();
////builder.Services.AddTransient<IZmstMinimumQualificationDirector, ZmstMinimumQualificationDirector>();
////builder.Services.AddTransient<IZmstNationalityDirector, ZmstNationalityDirector>();
////builder.Services.AddTransient<IZmstPassingStatusDirector, ZmstPassingStatusDirector>();
////builder.Services.AddTransient<IZmstProjectsDirector, ZmstProjectsDirector>();
////builder.Services.AddTransient<IZmstQualificationDirector, ZmstQualificationDirector>();
////builder.Services.AddTransient<IZmstQualifyingCourseDirector, ZmstQualifyingCourseDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamDirector, ZmstQualifyingExamDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamBoardDirector, ZmstQualifyingExamBoardDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamFromDirector, ZmstQualifyingExamFromDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamResultModeDirector, ZmstQualifyingExamResultModeDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamLearningModeDirector, ZmstQualifyingExamLearningModeDirector>();
////builder.Services.AddTransient<IZmstQualifyingExamStreamDirector, ZmstQualifyingExamStreamDirector>();
////builder.Services.AddTransient<IZmstQuesPaperDirector, ZmstQuesPaperDirector>();
////builder.Services.AddTransient<IZmstQuestionPaperMediumDirector, ZmstQuestionPaperMediumDirector>();
////builder.Services.AddTransient<IZmstAgencyDirector, ZmstAgencyDirector>();
////builder.Services.AddTransient<ZmstAgencyExamCounsDirector, ZmstAgencyExamCounsDirector>();
////builder.Services.AddTransient<IEmployeeDetailsDirector, EmployeeDetailsDirector>();
////

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAppOnboardingAdminloginDirector, AppOnboardingAdminloginDirector>();
builder.Services.AddSingleton<JWTTokenService, JWTTokenService>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<EncryptionDecryptionService, EncryptionDecryptionService>();
builder.Services.AddTransient<ExcelUtilityServices, ExcelUtilityServices>();
builder.Services.AddTransient<IAgencyDirector, AgencyDirector>();
builder.Services.AddTransient<IAppContactPersonDetailDirector, AppContactPersonDetailDirector>();
//builder.Services.AddTransient<IAppContactPersonDetailsDirector, AppContactPersonDetailsDirector>();
//builder.Services.AddTransient<IAppDocumentTypeRoleMappingDirector, AppDocumentTypeRoleMappingDirector>();
builder.Services.AddTransient<IAppDocumentUploadedDetailDirector, AppDocumentUploadedDetailDirector>();
//builder.Services.AddTransient<IApplicationDayWiseDirector, ApplicationDayWiseDirector>();
builder.Services.AddTransient<IApplicationScheduleDirector, ApplicationScheduleDirector>();
//builder.Services.AddTransient<IAppLoginDetailsDirector, AppLoginDetailsDirector>();
builder.Services.AddTransient<IAppOnboardingDetailDirector, AppOnboardingDetailDirector>();
//builder.Services.AddTransient<IAppOnboardingDetailsDirector, AppOnboardingDetailsDirector>();
//builder.Services.AddTransient<IAppOnboardingDetailsResponseDirector, AppOnboardingDetailsResponseDirector>();
//builder.Services.AddTransient<IAppOnboardingDetailsResponseLinkDirector, AppOnboardingDetailsResponseLinkDirector>();
builder.Services.AddTransient<IAppOnboardingRequestDirector, AppOnboardingRequestDirector>();
builder.Services.AddTransient<IAppOnboardingResponseDirector, AppOnboardingResponseDirector>();
//builder.Services.AddTransient<IAppOnboardingResponseLinkDirector, AppOnboardingResponseLinkDirector>();
builder.Services.AddTransient<IAppProjectCostDiector, AppProjectCostDirector>();
//builder.Services.AddTransient<IAppProjectCostDirector, AppProjectCostDirector>();
builder.Services.AddTransient<IAppProjectDetailsDirector, AppProjectDetailsDirector>();
//builder.Services.AddTransient<IAppRoleModulePermissionDirector, AppRoleModulePermissionDirector>();
//builder.Services.AddTransient<IAppUserRoleMappingDirector, AppUserRoleMappingDirector>();
//builder.Services.AddTransient<ICategoriesDirector, CategoriesDirector>();
builder.Services.AddTransient<IDaywiseRegistrationDirector, DaywiseRegistrationDirector>();
builder.Services.AddTransient<IEmployeeDetailsDirector, EmployeeDetailsDirector>();
builder.Services.AddTransient<IEmployeeWorkOrderDirector, EmployeeWorkOrderDirector>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddTransient<ILogDirector, LogDirector>();
builder.Services.AddTransient<IMailServiceDirector, MailServiceDirector>();
//builder.Services.AddTransient<IMdAgencyDirector, MdAgencyDirector>();
builder.Services.AddTransient<IMdAgencyTypeDirector, MdAgencyTypeDirector>();
builder.Services.AddTransient<IMdDistrictDirector, MdDistrictDirector>();
builder.Services.AddTransient<IMdDocumentTypeDirector, MdDocumentTypeDirector>();
//builder.Services.AddTransient<IMdEmailRecipientDirector, MdEmailRecipientDirector>();
builder.Services.AddTransient<IMdEmpStatusDirector, MdEmpStatusDirector>();
builder.Services.AddTransient<IMdExamDirector, MdExamDirector>();
builder.Services.AddTransient<IMdFinancialComponentDirector, MdProjectFinancialComponentDirector>();
builder.Services.AddTransient<IMdIdTypeDirector, MdIdTypeDirector>();
builder.Services.AddTransient<IMdMinistryDirector, MdMinistryDirector>();
builder.Services.AddTransient<IMDModuleDirector, MDModuleDirector>();
//builder.Services.AddTransient<IMdOnboardingActivityDirector, MdOnboardingActivityDirector>();
builder.Services.AddTransient<IMdOrganizationDirector, MdOrganizationDirector>();
//builder.Services.AddTransient<IMdProjectFinancialComponentsDirector, MdProjectFinancialComponentsDirector>();
//builder.Services.AddTransient<IMdRoleDirector, MdRoleDirector>();
builder.Services.AddTransient<IMdServiceTypeDirector, MdServiceTypeDirector>();
builder.Services.AddTransient<IMdSmsEmailTemplateDirector, MdSmsEmailTemplateDirector>();
builder.Services.AddTransient<IMdStateDirector, MdStateDirector>();
builder.Services.AddTransient<IMdWorkOrderAgencyDirector, MdWorkOrderAgencyDirector>();
builder.Services.AddTransient<IQualificationDetailsDirector, QualificationDetailsDirector>();
//builder.Services.AddTransient<IRegistrationDirector, RegistrationDirector>();
builder.Services.AddTransient<IRequestListInfoDirector, RequestListInfoDirector>();
builder.Services.AddTransient<IStateDirector, StateDirector>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient<IUserAuthorizationDirector, UserAuthorizationDirector>();
//builder.Services.AddTransient<IValidationTestDirector, ValidationTestDirector>();
builder.Services.AddTransient<IWorkOrderDetailsDirector, WorkOrderDetailsDirector>();
builder.Services.AddTransient<IApplicationSummaryDirector, ApplicationSummaryDirector>();
builder.Services.AddTransient<IZmstAgencyDirector, ZmstAgencyDirector>();
builder.Services.AddTransient<IZmstAgencyExamCounsDirector, ZmstAgencyExamCounsDirector>();
//builder.Services.AddTransient<IZmstApplicationSummaryBKPDirector, ZmstApplicationSummaryBKPDirector>();
//builder.Services.AddTransient<IZmstApplicationSummaryDirector, ZmstApplicationSummaryDirector>();
builder.Services.AddTransient<IZmstBranchDirector, ZmstBranchDirector>();
builder.Services.AddTransient<IZmstCategoryDirector, ZmstCategoryDirector>();
builder.Services.AddTransient<IZmstCountryDirector, ZmstCountryDirector>();
builder.Services.AddTransient<IZmstCourseAppliedDirector, ZmstCourseAppliedDirector>();
builder.Services.AddTransient<IZmstCourseAppliedLevelDirector, ZmstCourseAppliedLevelDirector>();
builder.Services.AddTransient<IZmstDistrictDirector, ZmstDistrictDirector>();
builder.Services.AddTransient<IZmstDocumentTypeDirector, ZmstDocumentTypeDirector>();
builder.Services.AddTransient<IZmstExamTypeDirector, ZmstExamTypeDirector>();
builder.Services.AddTransient<IZmstFeeTypeDirector, ZmstFeeTypeDirector>();
builder.Services.AddTransient<IZmstGenderDirector, ZmstGenderDirector>();
builder.Services.AddTransient<IZmstIdentityTypeDirector, ZmstIdentityTypeDirector>();
builder.Services.AddTransient<IZmstMinimumQualificationDirector, ZmstMinimumQualificationDirector>();
builder.Services.AddTransient<IZmstNationalityDirector, ZmstNationalityDirector>();
builder.Services.AddTransient<IZmstPassingStatusDirector, ZmstPassingStatusDirector>();
builder.Services.AddTransient<IZmstProjectsDirector, ZmstProjectsDirector>();
builder.Services.AddTransient<IZmstQualificationDirector, ZmstQualificationDirector>();
builder.Services.AddTransient<IZmstQualifyingCourseDirector, ZmstQualifyingCourseDirector>();
builder.Services.AddTransient<IZmstQualifyingExamBoardDirector, ZmstQualifyingExamBoardDirector>();
builder.Services.AddTransient<IZmstQualifyingExamDirector, ZmstQualifyingExamDirector>();
builder.Services.AddTransient<IZmstQualifyingExamFromDirector, ZmstQualifyingExamFromDirector>();
builder.Services.AddTransient<IZmstQualifyingExamLearningModeDirector, ZmstQualifyingExamLearningModeDirector>();
builder.Services.AddTransient<IZmstQualifyingExamResultModeDirector, ZmstQualifyingExamResultModeDirector>();
builder.Services.AddTransient<IZmstQualifyingExamStreamDirector, ZmstQualifyingExamStreamDirector>();
builder.Services.AddTransient<IZmstQuesPaperDirector, ZmstQuesPaperDirector>();
builder.Services.AddTransient<IZmstQuestionPaperMediumDirector, ZmstQuestionPaperMediumDirector>();
builder.Services.AddTransient<IZmstQuotaDirector, ZmstQuotaDirector>();
builder.Services.AddTransient<IZmstRankTypeDirector, ZmstRankTypeDirector>();
builder.Services.AddTransient<IZmstReligionDirector, ZmstReligionDirector>();
builder.Services.AddTransient<IZmstResidentialEligibilityDirector, ZmstResidentialEligibilityDirector>();
builder.Services.AddTransient<IZmstSeatCategoryDirector, ZmstSeatCategoryDirector>();
builder.Services.AddTransient<IZmstSeatGenderDirector, ZmstSeatGenderDirector>();
builder.Services.AddTransient<IZmstServiceTypeDirector, ZmstServiceTypeDirector>();
builder.Services.AddTransient<IZmstSpecialExamPaperDirector, ZmstSpecialExamPaperDirector>();
builder.Services.AddTransient<IZmstStateDirector, ZmstStateDirector>();
builder.Services.AddTransient<IZmstStreamDirector, ZmstStreamDirector>();
builder.Services.AddTransient<IZmstSubCategoryDirector, ZmstSubCategoryDirector>();
builder.Services.AddTransient<IZmstSubCategoryPriorityDirector, ZmstSubCategoryPriorityDirector>();
builder.Services.AddTransient<IZmstSubjectDirector, ZmstSubjectDirector>();
builder.Services.AddTransient<IZmstSymbolDirector, ZmstSymbolDirector>();
builder.Services.AddTransient<IZmstTypeofDisabilityDirector, ZmstTypeofDisabilityDirector>();
builder.Services.AddTransient<IZmstWillingnessDirector, ZmstWillingnessDirector>();
builder.Services.AddTransient<SMSService, SMSService>();
builder.Services.AddTransient<UtilityService, UtilityService>();
builder.Services.AddTransient<ZmstAgencyExamCounsDirector, ZmstAgencyExamCounsDirector>();
builder.Services.AddTransient<IEmployeeDetailsDirector, EmployeeDetailsDirector>();
builder.Services.AddTransient<IEmployeeWorkOrderDirector, EmployeeWorkOrderDirector>();
builder.Services.AddTransient<IMdEmpStatusDirector, MdEmpStatusDirector>();
builder.Services.AddTransient<IMdExamDirector, MdExamDirector>();
builder.Services.AddTransient<IMdIdTypeDirector, MdIdTypeDirector>();
builder.Services.AddTransient<IQualificationDetailsDirector, QualificationDetailsDirector>();
builder.Services.AddTransient<IZmstProgramDirector, ZmstProgramDirector>();
//builder.Services.AddTransient<IManageProgramDirector, ManageProgramDirector>();
builder.Services.AddTransient<IZmstApplicantTypeDirector, ZmstApplicantTypeDirector>();
builder.Services.AddTransient<IZmstActivityDirector, ZmstActivityDirector>();
builder.Services.AddTransient<IZmstTradeDirector, ZmstTradeDirector>();
builder.Services.AddTransient<IZmstInstituteDirector, ZmstInstituteDirector>();
builder.Services.AddTransient<IZmstInstituteTypeDirector, ZmstInstituteTypeDirector>();
builder.Services.AddTransient<IZmstInstituteAgencyDirector, ZmstInstituteAgencyDirector>();
builder.Services.AddTransient<IZmstInstituteStreamDirector, ZmstInstituteStreamDirector>();
builder.Services.AddTransient<IZmstAgencyVirtualDirectoryMappingDirector, ZmstAgencyVirtualDirectoryMappingDirector>();
builder.Services.AddTransient<ICaptchaDirector, CaptchaDirector>();
builder.Services.AddTransient<IMdStateDirector, MdStateDirector>();
builder.Services.AddTransient<MDApiKeyValidateService, MDApiKeyValidateService>();
builder.Services.AddTransient<IZmstSeatTypeDirector, ZmstSeatTypeDirector>();
builder.Services.AddTransient<IZmstSeatGroupDirector, ZmstSeatGroupDirector>();
builder.Services.AddTransient<IZmstSeatSubCategoryDirector, ZmstSeatSubCategoryDirector>();
builder.Services.AddTransient<IZmstExperienceTypeDirector, ZmstExperienceTypeDirector>();
builder.Services.AddTransient<IAdministratorDirector, AdministratorDirector>();
builder.Services.AddTransient<IZmstAuthenticationModeDirector, ZmstAuthenticationModeDirector>();
builder.Services.AddTransient<IZmstSecurityQuestionDirector, ZmstSecurityQuestionDirector>();
builder.Services.AddTransient<IAppRoleModulePermissionDirector, AppRoleModulePermissionDirector>();
builder.Services.AddTransient<IAppUserRoleMappingDirector, AppUserRoleMappingDirector>();
builder.Services.AddTransient<IMdRoleDirector, MdRoleDirector>();
builder.Services.AddTransient<IAppLoginDetailsDirector, AppLoginDetailsDirector>();

builder.Services.AddTransient<IMdMainModuleDirector, MdMainModuleDirector>();
builder.Services.AddTransient<IAppProjectPaymentDetailsDirector, AppProjectPaymentDetailsDirector>();
builder.Services.AddTransient<IAppProjectActivityDirector, AppProjectActivityDirector>();
builder.Services.AddTransient<IMdStatusDirector, MdStatusDirector>();
builder.Services.AddTransient<IMdActivityTypeDirector, MdActivityTypeDirector>();
builder.Services.AddTransient<IAppDocumentUploadedDetailHistoryDirector, App_DocumentUplodedDetailHistoryDirector>();
builder.Services.AddTransient<IConfigurationEnvironmentDirector, ConfigurationEnvironmentDirector>();
builder.Services.AddTransient<IMdYearDirector, MdYearDirector>();
builder.Services.AddTransient<IAppProjectExpenditureDirector, AppProjectExpenditureDirector>();
builder.Services.AddTransient<IConfigurationAPISecureKeyDirector, ConfigurationAPISecureKeyDirector>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:44351", "http://localhost:4200", domain)
//                                .AllowAnyHeader()
//                                .AllowAnyMethod();
//        });
//});
//builder.Configuration.GetConnectionString("OnBoardingSystem")
var key = "onBoarding12345$$$$$$$$";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ClockSkew = TimeSpan.Zero,
    };
}).AddCookie();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DD E
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//start region
builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});
//end region
var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1;mode=block");
    //context.Response.Headers.Add("Strict-Transport-Security", "max-age=3153600000");Access-Control-Allow-Origin": "*",
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    //context.Response.Headers.Add("Strict-Transport-Security", "max-age=3153600000");
    await next();
});

app.UseReferrerPolicy(options => options.NoReferrer());
app.UseXContentTypeOptions();
app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseXfo(options => options.Deny());
app.Use(async (context, next) =>
{
    if (!context.Response.Headers.ContainsKey("Feature-Policy"))
    {
        context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; microphone 'none';");
    }
    await next();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "OBS");
    });
    app.UseCors();
    //Change
}
app.UseHttpsRedirection();
app.UseMiddleware<RefreshTokenMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "/api/{controller}/{action}/{id?}");
    endpoints.MapControllerRoute(name: "ABC", pattern: "sendTest/UA", defaults: new { controller = "AAA", action = "Send" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "sendb", defaults: new { controller = "AAA", action = "Send" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/genderlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllGenederList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SeatCategoryList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncSeatCategoryList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/subcategorylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncSubCategoryList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/Nationalitylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncNationality" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/CountryList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncCountry" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/Examtypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllExamTypeAsync" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/ranktypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncRankType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/streamlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncStreamList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/streamlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncStreamList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SymbolList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncSymbol" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/categorylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllZmstCategory" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SubcategoryPrioritylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncSubcategoryPriorityList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/StateList", defaults: new { controller = "MasterDataDirectory", action = "GetState" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/Qualificationlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllQualification" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/passingStatuslist", defaults: new { controller = "MasterDataDirectory", action = "GetAllPassingStatus" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/QualifyingExamResultModeList", defaults: new { controller = "MasterDataDirectory", action = "GetAllQualifyingExamResultMode" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/QualifyingExamLearningModeList", defaults: new { controller = "MasterDataDirectory", action = "GetAllQualifyingExamLearningMode" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/MinimumQualificationList", defaults: new { controller = "MasterDataDirectory", action = "GetAllMinimumQualification" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/QualifyingExamBoardList", defaults: new { controller = "MasterDataDirectory", action = "GetAllQualifyingExamBoard" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/List/QualifyingCourse", defaults: new { controller = "MasterDataDirectory", action = "GetAllQualifyingCourse" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/documentTypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAlldocumenttype" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/willingnessList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncwillingness" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/IdentityTypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllZmstIdentityType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/Institutelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllInstituteList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SubjectList", defaults: new { controller = "MasterDataDirectory", action = "GetAllSubjects" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SeatType", defaults: new { controller = "MasterDataDirectory", action = "GetAllSubjects" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/ServiceTypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllServiceType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/QuotaList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncQuotaList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/religionlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllZmstReligion" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/TypeofDisabilitylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncTypeOfDisabilityList" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SeatGroup", defaults: new { controller = "MasterDataDirectory", action = "GetAllseatgroup" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/subcategorylist", defaults: new { controller = "MasterDataDirectory", action = "GetAllSeatSubCategory" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SeatType", defaults: new { controller = "MasterDataDirectory", action = "GetAllSeatType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/BranchList", defaults: new { controller = "MasterDataDirectory", action = "GetAllBranchType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/InstituteTypelist", defaults: new { controller = "MasterDataDirectory", action = "GetAllInstituteType" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SMSTemplate", defaults: new { controller = "MasterDataDirectory", action = "GetAllSmsEmailTemplate" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/ExperienceType", defaults: new { controller = "MasterDataDirectory", action = "GetAllExperienceType" });

    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/projectlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllZmstProjects" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/AgencyList", defaults: new { controller = "MasterDataDirectory", action = "GetAllZmstAgency" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/DistrictList", defaults: new { controller = "MasterDataDirectory", action = "GetAllAsyncDistrict" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/SpecialExamPaperList", defaults: new { controller = "MasterDataDirectory", action = "GetAllSpecialExam" });

    endpoints.MapControllerRoute(name: "default", pattern: "/api/{controller}/{action}/{id?}");
    endpoints.MapControllerRoute(name: "ABC", pattern: "sendTest/UA", defaults: new { controller = "AAA", action = "Send" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "sendb", defaults: new { controller = "AAA", action = "Send" });
    endpoints.MapControllerRoute(name: "ABC", pattern: "api/list/genderlist", defaults: new { controller = "MasterDataDirectory", action = "GetAllGenderList" });
    endpoints.MapControllerRoute(name: "ABCD", pattern: "takea", defaults: new { controller = "AAA", action = "Take" });
    endpoints.MapControllerRoute(name: "checkpost", pattern: "checkposta", defaults: new { controller = "AAA", action = "CheckPost" });
    endpoints.MapControllerRoute(name: "ABCD", pattern: "takea", defaults: new { controller = "AAA", action = "Take" });
    endpoints.MapControllerRoute(name: "checkpost", pattern: "checkposta", defaults: new { controller = "AAA", action = "CheckPost" });
    ConventionalRoutingSwaggerGen.UseRoutes(endpoints);
});
//app.UseMiddleware<ApiKeyMiddleWare>();

app.Run();

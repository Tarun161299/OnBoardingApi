//-----------------------------------------------------------------------
// <copyright file="AppOnboardingRequestDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Data.Entity.Infrastructure;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Azure.Core;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using OnBoardingSystem.Common.enums;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.EF.Models;
    using Abs = OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using Serilog;
    /// <inheritdoc />
    public class AppOnboardingRequestDirector : IAppOnboardingRequestDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        private readonly EncryptionDecryptionService decryptionService;
        private readonly SMSService sMSService;
        private readonly ILogger<AppOnboardingRequestDirector> logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingRequestDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppOnboardingRequestDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, ILogger<AppOnboardingRequestDirector> logger, EncryptionDecryptionService _encryptionDecryptionService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.utilityService = _utilityService;
            this.sMSService = _sMSService;
            this.logger = logger;
            this._httpContextAccessor = httpContextAccessor;
            this.decryptionService = _encryptionDecryptionService;
        }
        //public List<Abstractions.Models.MdMinistry> MdMinistries { get; set; }

        /// <inheritdoc />
        public virtual async Task<List<OnBoardingRequestDetailUpsert>> GetAllAsync(CancellationToken cancellationToken)
        {
            var agencylist = this.unitOfWork.MdAgencyTypeRepository.GetAll();
            //var serviceTypeList = this.unitOfWork.MdServiceTypeRepository.GetAll();
            var organizationList = this.unitOfWork.MdOrganizationRepository.GetAll();
            var minitryList = this.unitOfWork.MdMinistryRepository.GetAll();
            var onboardingDetails = this.unitOfWork.AppOnboardingDetailRepository.GetAll();
            var onboardingRequestDetail = this.unitOfWork.AppOnboardingRequestRepository.GetAll();

            var result = from requestDetails in onboardingRequestDetail
                             //join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId
                         join agency in agencylist on requestDetails.AgencyTypeId equals agency.AgencyTypeId
                         join organization in organizationList on requestDetails.OrganizationId equals Convert.ToInt32(organization.OrganizationId)
                         join status in onboardingDetails on requestDetails.RequestNo equals status.RequestNo into finalstatus
                         from status in finalstatus.DefaultIfEmpty()
                         join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId into ministries
                         from ministry in ministries.DefaultIfEmpty()
                         select new OnBoardingRequestDetailUpsert
                         {
                             RequestNo = requestDetails.RequestNo,
                             AgencyType = agency.AgencyType,
                             //  OranizationName = organization.OrganizationName,
                             OranizationName = organization.OrganizationId!="999"? organization.OrganizationName : requestDetails.OrganizationOther,
                             SubmitTime = requestDetails.SubmitTime,
                             Status = (status.Status == null ? requestDetails.Status : status.Status),
                             //Status = requestDetails.Status,
                             ShowStatus = requestDetails.Status,
                             CurrentStage = requestDetails.CurrentStage,
                             ModifyOn = (requestDetails.ModifyOn == null ? requestDetails.SubmitTime : requestDetails.ModifyOn),
                         };

            return await Task.FromResult(result.OrderByDescending(rs => rs.ModifyOn).ToList()).ConfigureAwait(false);
        }
        /// <inheritdoc />
        public virtual async Task<List<OnBoardingRequestDetailUpsert>> GetAllByStatusAsync(string Status, CancellationToken cancellationToken)
        {
            var agencylist = this.unitOfWork.MdAgencyTypeRepository.GetAll();
            //var serviceTypeList = this.unitOfWork.MdServiceTypeRepository.GetAll();
            var organizationList = this.unitOfWork.MdOrganizationRepository.GetAll();
            var minitryList = this.unitOfWork.MdMinistryRepository.GetAll();
            var onboardingDetails = this.unitOfWork.AppOnboardingDetailRepository.GetAll();
            var onboardingRequestDetail = this.unitOfWork.AppOnboardingRequestRepository.GetAll();

            var result = from requestDetails in onboardingRequestDetail
                             //join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId
                         join agency in agencylist on requestDetails.AgencyTypeId equals agency.AgencyTypeId
                         join organization in organizationList on requestDetails.OrganizationId equals Convert.ToInt32(organization.OrganizationId)

                         join status in onboardingDetails on requestDetails.RequestNo equals status.RequestNo into finalstatus
                         from status in finalstatus.DefaultIfEmpty()

                         join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId into ministries
                         from ministry in ministries.DefaultIfEmpty()

                         select new OnBoardingRequestDetailUpsert
                         {
                             RequestNo = requestDetails.RequestNo,
                             AgencyType = agency.AgencyType,
                             //OranizationName = organization.OrganizationName,
                             OranizationName = organization.OrganizationId != "999" ? organization.OrganizationName : requestDetails.OrganizationOther,
                             SubmitTime = requestDetails.SubmitTime,
                             Status = (status.Status == null ? requestDetails.Status : status.Status),
                             //Status = requestDetails.Status,
                             ShowStatus = requestDetails.Status,
                             CurrentStage = requestDetails.CurrentStage,
                             ModifyOn = (requestDetails.ModifyOn == null ? requestDetails.SubmitTime : requestDetails.ModifyOn),
                         };
            var resultfilterByStatus = await Task.FromResult(result.OrderByDescending(rs => rs.ModifyOn).ToList()).ConfigureAwait(false);
            return resultfilterByStatus.FindAll(x => x.Status == Status);
        }

        //private static readonly Func<AppOnboardingRequestDirector, string, OnBoardingRequestDetailUpsert> GetbyId =
        //    Microsoft.EntityFrameworkCore.EF.CompileQuery(
        //        (AppOnboardingRequestDirector request, int id) => request.Set<OnBoardingRequestDetailUpsert>().ToString());

        //public  object Set<T>()
        //{
        //    throw new NotImplementedException();
        //}

        //private static Func<AppOnboardingRequestDirector, Guid, IQueryable<Category>> _getCategory =
        //   EF.CompileQuery((AppOnboardingRequestDirector context, Guid id) =>
        //       context.MdMinistries.Include(c => c.Translations)
        //                         .ThenInclude(c => c.Language)
        //                         .Include(c => c.Parent)
        //                         .Where(c => c.Id == id)
        //                         .toList());
        //public IList<Category> GetCategory(Guid id)
        //{
        //    return MdMinistries.Include(c => c.Translations)
        //                     .ThenInclude(c => c.Language)
        //                     .Include(c => c.Parent)
        //                     .Where(c => c.Id == id)
        //                     .FirstOrDefault();
        //}

        /// <inheritdoc/>
        public virtual async Task<string> SendOTP(OTPModal otpModal, CancellationToken cancellationToken)
        {
            string key = "37ZA3D89D64C115122DF9178C8R99c1x";                            //This needs to be in Handled Properly
            string iv = "213A26DBB4A358C5";
            byte[] KeyBytes = Encoding.ASCII.GetBytes(key);
            byte[] ivByte = Encoding.ASCII.GetBytes(iv);
            Encoding unicode = Encoding.Unicode;
            //string decryptedOTP = decryptionService.Decryption(otpModal.Otp, KeyBytes, ivByte);
            // string decryptedOTPsms = decryptionService.Decryption(otpModal.OtpSms, KeyBytes, ivByte);
            string decryptedOTP = Encoding.UTF8.GetString(Convert.FromBase64String(otpModal.Otp));
            string decryptedOTPsms = Encoding.UTF8.GetString(Convert.FromBase64String(otpModal.OtpSms));
            string coodinatorName = Encoding.UTF8.GetString(Convert.FromBase64String(otpModal.coodinatorName));
            string email = Encoding.UTF8.GetString(Convert.FromBase64String(otpModal.Email));
            string mobileno = Encoding.UTF8.GetString(Convert.FromBase64String(otpModal.Mobile));
            string response = "";
            var emaiTemplateOTP = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0006", cancellationToken).ConfigureAwait(false);
            var smsTemplateOTP = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0001", cancellationToken).ConfigureAwait(false);

            string isdCode = "91";
            string Subject = emaiTemplateOTP.MessageSubject;

            string Body = emaiTemplateOTP.MessageTemplate;
            Body = Body.Replace("#OTP#", decryptedOTP);
            Body = Body.Replace("#USER#", coodinatorName);
            MailRequest request = new MailRequest();
            request.ToEmail = email;
            request.Subject = Subject;
            request.Body = Body;
            //string messagRPH = "";
            string TemplateId = smsTemplateOTP.RegisteredTemplateId;
            //string TemplateId = "1107168630317451475";
            string message = smsTemplateOTP.MessageTemplate;
            //message = message.Replace("@activity@", "submit request");
            message = message.Replace("#OTP#", decryptedOTPsms);
            message = message.Replace("#EMAIL#", "counselling-pmu@nic.in");
            if (decryptedOTP != "NA" && decryptedOTPsms != "NA")
            {var confEnv=await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(x=>x.ApplicationId==1, cancellationToken).ConfigureAwait(false);
                if (confEnv.OtpMedium=="B" || confEnv.OtpMedium == "A")
                {
                    var mail = utilityService.SendEmailAsync(request);
                    var sms = sMSService.SendAsync(isdCode + mobileno, decryptedOTPsms, TemplateId, message);
                    response = "OTP has been sent";// to " + email + " and " + mobileno;
                   
                }
                else if (confEnv.OtpMedium == "E")
                {
                    var mail = utilityService.SendEmailAsync(request);
                    response = "OTP has been sent on Email";// to " + email + " and " + mobileno;
                   
                }
                else if (confEnv.OtpMedium == "M")
                {
                    var sms = sMSService.SendAsync(isdCode + mobileno, decryptedOTPsms, TemplateId, message);
                    response = "OTP has been sent on Mobile";// to " + email + " and " + mobileno;
                   
                }
                else
                {
                    response = "OTp Medium not valid";
                }
                return response;

            }
            else if (decryptedOTP == "NA")
            {
                var sms = sMSService.SendAsync(isdCode + mobileno, decryptedOTPsms, TemplateId, message);
                response = "OTP has been sent";// to " + mobileno;
                return response;
            }
            else if (decryptedOTPsms == "NA")
            {
                var mail = utilityService.SendEmailAsync(request);
                response = "OTP has been sent";// to " + email;
                return response;
            }
            else
            {
                response = "OTP not send";
                return response;
            }

        }

        /// <inheritdoc/>
        public virtual async Task<string> SaveAppOnboardingRequestDataAsync(Abstractions.Models.AppOnboardingRequest appOnboardingRequestData, CancellationToken cancellationToken)
        {
            var emailbyRoleId = await this.unitOfWork.EmailRecipientRepository.FindAllByAsync(x => x.RoleId == '3', cancellationToken).ConfigureAwait(false);

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@AgencyID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appOnboardingRequestData.AgencyTypeId,
                },
                new SqlParameter()
                {
                    ParameterName = "@MinistryId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appOnboardingRequestData.MinistryId,
                },
                new SqlParameter()
                {
                    ParameterName = "@MinistryOther",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.MinistryOther,
                },
                new SqlParameter()
                {
                    ParameterName = "@NameOfOrganisation",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appOnboardingRequestData.OrganizationId,
                },
                new SqlParameter()
                {
                    ParameterName = "@OtherOganisation",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.OrganizationOther,
                },
                new SqlParameter()
                {
                    ParameterName = "@SessionYear",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appOnboardingRequestData.SessionYear,
                },
                new SqlParameter()
                {
                    ParameterName = "@Address",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = appOnboardingRequestData.Address,
                },
                new SqlParameter()
                {
                    ParameterName = "@PinCode",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.PinCode,
                },
                new SqlParameter()
                {
                    ParameterName = "@HeadOfOrganisation",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.ContactPerson,
                },
                new SqlParameter()
                {
                    ParameterName = "@Designation",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.Designation,
                },
                new SqlParameter()
                {
                    ParameterName = "@Services",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.Services,
                },
                new SqlParameter()
                {
                    ParameterName = "@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.hodEncryptedMail,
                },
                new SqlParameter()
                {
                    ParameterName = "@Mobileno",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.MobileNo,
                },
                new SqlParameter()
                {
                    ParameterName = "@filename",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.FileName,
                },
                new SqlParameter()
                {
                    ParameterName = "@fileextension",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.FileExtension,
                },
                new SqlParameter()
                {
                    ParameterName = "@modifieddate",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.ModifiedDate,
                },
                new SqlParameter()
                {
                    ParameterName = "@fileContent",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.Content,
                },
                new SqlParameter()
                {
                    ParameterName = "@format",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.Format,
                },
                new SqlParameter()
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.Id,
                },
                new SqlParameter()
                {
                    ParameterName = "@IpAddress",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),

                },
                new SqlParameter()
                {
                    ParameterName = "@coordinatorName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.CoodinatorName,
                },
                new SqlParameter()
                {
                    ParameterName = "@coordinatorEmail",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.cordinatiorEncryptedMail,
                },
                new SqlParameter()
                {
                    ParameterName = "@coordinatorMobile",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.CoodinatorMobile,
                },
                new SqlParameter()
                {
                    ParameterName = "@coordinatorDesignation",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.CoodinatorDesignation,
                },
                new SqlParameter()
                {
                    ParameterName = "@stateID",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.StateID,
                },
                new SqlParameter()
                {
                    ParameterName = "@districtID",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.DistrictID,
                },
                new SqlParameter()
                {
                    ParameterName = "@AgencyStateId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.AgencyStateId,
                },
                new SqlParameter()
                {
                    ParameterName = "@CurrentStage",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingRequestData.CurrentStage,
                },
                new SqlParameter()
                {
                    ParameterName = "@requestId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size= 50,
                },
            };
            var storedProcedureName = $"{"USP_InsertOnboardingRequest"}  @AgencyID,@MinistryId,@MinistryOther,@NameOfOrganisation,@OtherOganisation,@SessionYear,@Address,@PinCode,@HeadOfOrganisation,@Designation,@Services,@Email,@Mobileno,@IpAddress,@filename,@fileextension,@modifieddate,@fileContent,@format,@id,@coordinatorName,@coordinatorEmail,@coordinatorMobile,@coordinatorDesignation,@stateID,@districtID,@AgencyStateId,@CurrentStage,@requestId output";
            int data = await this.unitOfWork.AppOnboardingRequestRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);

            var requestId = param[28].Value;
            if (requestId != null && requestId != "")
            {
                {
                    MailRequest mailContentUser = new MailRequest();
                    var emaiTemplateUser = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0007", cancellationToken).ConfigureAwait(false);
                    string Body = emaiTemplateUser.MessageTemplate;
                    Body = Body.Replace("#REQNO#", requestId.ToString());
                    Body = Body.Replace("#USER#", appOnboardingRequestData.CoodinatorName);
                    mailContentUser.Body = Body;
                    mailContentUser.Subject = emaiTemplateUser.MessageSubject.Replace("#REQNO#", requestId.ToString());
                    mailContentUser.ToEmail = appOnboardingRequestData.CoodinatorEmail;
                    mailContentUser.CCMail = appOnboardingRequestData.Email;
                    //mailContentUser.Attachment = appOnboardingRequestData.PdfContent;
                    mailContentUser.Body = Body;
                    var mail = utilityService.SendEmailAsync(mailContentUser);

                    var smsTemplateCoordinator = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0008", cancellationToken).ConfigureAwait(false);
                    string isdCode = "91";
                    string mobile = decryptionService.Decryption(appOnboardingRequestData.CoodinatorMobile);
                    string smsMessageCoordinator = "";
                    string TemplateId = smsTemplateCoordinator.RegisteredTemplateId;
                    string message = smsTemplateCoordinator.MessageTemplate;
                    smsMessageCoordinator = message.Replace("#USER#", appOnboardingRequestData.CoodinatorName);
                    smsMessageCoordinator = smsMessageCoordinator.Replace("#REQNO#", requestId.ToString());
                    var sms = sMSService.SendStatusSmsAsync(isdCode + mobile, TemplateId, smsMessageCoordinator);

                    var saveDocument = new OnBoardingSystem.Data.Abstractions.Models.AppDocumentUploadedDetail();
                    saveDocument.DocContent = appOnboardingRequestData.PdfContent;
                    saveDocument.Activityid = ((int)Enumactivity.OnboardingRequest).ToString();
                    saveDocument.DocType = "RP";
                    saveDocument.ModuleRefId = requestId.ToString();
                    saveDocument.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    saveDocument.SubTime = DateTime.Now;
                    var SaveDocument = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(saveDocument);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(SaveDocument, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                }

                {
                    MailRequest mailContentPMU = new MailRequest();
                    var emaiTemplatePMU = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0008", cancellationToken).ConfigureAwait(false);
                    mailContentPMU.Body = emaiTemplatePMU.MessageTemplate.Replace("#REQNO#", requestId.ToString());
                    mailContentPMU.Subject = emaiTemplatePMU.MessageSubject.Replace("#REQNO#", requestId.ToString());
                    foreach (var pmumail in emailbyRoleId)
                    {
                        mailContentPMU.ToEmail = pmumail.Email;
                        var mail = utilityService.SendEmailAsync(mailContentPMU);
                    }

                    var smsTemplatePMU = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0003", cancellationToken).ConfigureAwait(false);
                    string isdCode = "91";
                    string pmuContactNumber = "8377090312";
                    string smsMessagePMU = "";
                    string TemplateId = smsTemplatePMU.RegisteredTemplateId;
                    string message = smsTemplatePMU.MessageTemplate;
                    smsMessagePMU = message.Replace("#USER#", "Admin");
                    smsMessagePMU = smsMessagePMU.Replace("#STATUS#", "received");
                    smsMessagePMU = smsMessagePMU.Replace("#REQNO#", requestId.ToString());
                    var sms = sMSService.SendStatusSmsAsync(isdCode + pmuContactNumber, TemplateId, smsMessagePMU);
                }
            }

            return requestId.ToString();
        }

        /// <inheritdoc/>
        public virtual async Task<AppOnboardRequestAndDetail> GetByIdAsync(string requestId, CancellationToken cancellationToken)
        {
            try
            {
                var OnboardRequest = new AppOnboardRequestAndDetail();
                var param = new SqlParameter[]
                {
                new SqlParameter()
                {
                    ParameterName = "@RequestNo",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = requestId,
                },
                };

                var storedProcedureName = $"{"Proc_GetStatusByRequstId"}  @RequestNo";
                var statusRusult = await this.unitOfWork.MDStatusRepository.FromSqlRawAsync(storedProcedureName, param, cancellationToken).ConfigureAwait(false);
                //List<Abstractions.Models.MDStatus> statusList = this.mapper.Map<List<Abstractions.Models.MDStatus>>(statusRusult);
                /* To be discussed
                //List<MDStatus> mdstatus = new List<MDStatus>();
                //mdstatus[0].StatusId = "RP"; mdstatus[0].Status = "Pending";
                //mdstatus[1].StatusId = "RR"; mdstatus[1].Status = "Rejected";
                //mdstatus[2].StatusId = "RH"; mdstatus[2].Status = "Hold";
                //mdstatus[3].StatusId = "RA"; mdstatus[3].Status = "Accepted";
                //var statusList = from status in mdstatus
                //                 where status.StatusId != onboardingRequest.Status
                //                 select new MDStatus
                //                 {
                //                     StatusId = status.StatusId,
                //                     Status = status.Status,
                //                 };*/

                var agencylist =await this.unitOfWork.MdAgencyTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var appUploadedDetails =await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(x => x.ModuleRefId == requestId && x.Activityid == ((int)Enumactivity.OnboardingRequest).ToString() && x.DocType == "01", cancellationToken).ConfigureAwait(false);
                var organizationList =await this.unitOfWork.MdOrganizationRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                //var a = Enumactivity.OnboardingRequest;
                var minitryList = await this.unitOfWork.MdMinistryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var onboardingDetail =  await this.unitOfWork.AppOnboardingDetailRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var onboardingRequestDetail =  await this.unitOfWork.AppOnboardingRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                //var onboardingRequest = this.mapper.Map < List<Abs.AppOnboardingRequest >>( await this.unitOfWork.AppOnboardingRequestRepository.FindByAsync(re => re.RequestNo == requestId, cancellationToken).ConfigureAwait(false));
                var stateList =  await this.unitOfWork.StateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var contactPerson = await this.unitOfWork.AppContactPersonDetailRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var onboardingResposesDetails =  await this.unitOfWork.AppOnboardingResponseRepository.FindAllByAsync(re => re.RequestNo == requestId, cancellationToken).ConfigureAwait(false);

                var requestlistdata = from requestDetails in onboardingRequestDetail
                                      join agency in agencylist on requestDetails.AgencyTypeId equals agency.AgencyTypeId
                                      join organization in organizationList on requestDetails.OrganizationId equals Convert.ToInt32(organization.OrganizationId)
                                      join appDocumentUploadedDetail in appUploadedDetails on requestDetails.RequestNo equals appDocumentUploadedDetail.ModuleRefId
                                      //join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId
                                      join onboardingResposes in onboardingResposesDetails on requestDetails.RequestNo equals requestId into onboardingRsp

                                      from onboardingResposes in onboardingRsp.DefaultIfEmpty()
                                      join onboarddetail in onboardingDetail on requestDetails.RequestNo equals onboarddetail.RequestNo into temp
                                      from onboarddetail in temp.DefaultIfEmpty()
                                      join state in stateList on requestDetails.AgencyStateId equals Convert.ToInt32(state.Id) into states
                                      from state in states.DefaultIfEmpty()

                                      join contact in contactPerson on requestDetails.RequestNo equals contact.RequestNo into contacts
                                      from contact in contacts.DefaultIfEmpty()

                                      join ministry in minitryList on requestDetails.MinistryId equals ministry.MinistryId into ministries
                                      from ministry in ministries.DefaultIfEmpty()

                                      where requestDetails.RequestNo == requestId
                                      select new AppOnboardRequestAndDetail
                                      {
                                          RequestNo = requestDetails.RequestNo,
                                          Services = requestDetails.Services,
                                          MinistryName = (requestDetails.MinistryId == 0) ? null: (requestDetails.MinistryId == 999) ? requestDetails.MinistryOther : ministry.MinistryName,
                                          Address = requestDetails.Address,
                                          PinCode = requestDetails.PinCode,
                                          ContactPerson = requestDetails.ContactPerson,
                                          Designation = requestDetails.Designation,
                                          state = state == null ? null : state.Description,
                                          Email = requestDetails.Email,
                                          MobileNo = requestDetails.MobileNo,
                                          AgencyType = agency.AgencyType,
                                          OranizationName = (requestDetails.OrganizationId == 999) ? requestDetails.OrganizationOther : organization.OrganizationName,
                                          docContent = appDocumentUploadedDetail.DocContent,
                                          MinistryId = requestDetails.MinistryId,
                                          Website = (onboarddetail == null) ? null : onboarddetail.Website,
                                          YearOfFirstTimeAffilitionSession = (onboarddetail == null) ? null : onboarddetail.YearOfFirstTimeAffilitionSession,//onboarddetail.YearOfFirstTimeAffilitionSession,
                                          CounsLastSessionConductedIn = (onboarddetail == null) ? null : onboarddetail.CounsLastSessionConductedIn,// onboarddetail.CounsLastSessionConductedIn,
                                          CounsLastSessionTechSupportBy = (onboarddetail == null) ? null : onboarddetail.CounsLastSessionTechSupportBy,// onboarddetail.CounsLastSessionTechSupportBy,
                                          CounsLastSessionDescription = (onboarddetail == null) ? null : onboarddetail.CounsLastSessionDescription,// onboarddetail.CounsLastSessionDescription,
                                          CounsCourseList = (onboarddetail == null) ? null : onboarddetail.CounsCourseList,// onboarddetail.CounsCourseList,
                                          CounsTotalCourse = (onboarddetail == null) ? null : onboarddetail.CounsTotalCourse,// onboarddetail.CounsTotalCourse,
                                          ExamLastSessionConductedIn = (onboarddetail == null) ? null : onboarddetail.ExamLastSessionConductedIn,// onboarddetail.ExamLastSessionConductedIn,
                                          ExamLastSessionTechSupportBy = (onboarddetail == null) ? null : onboarddetail.ExamLastSessionTechSupportBy,// onboarddetail.ExamLastSessionTechSupportBy,
                                          ExamLastSessionDescription = (onboarddetail == null) ? null : onboarddetail.ExamLastSessionDescription,// onboarddetail.ExamLastSessionDescription,
                                          ExamCourseList = (onboarddetail == null) ? null : onboarddetail.ExamCourseList,// onboarddetail.ExamCourseList,
                                          ExamTotalCourse = (onboarddetail == null) ? null : onboarddetail.ExamTotalCourse,// onboarddetail.ExamTotalCourse,
                                          ExamDissimilarityOfSchedule = (onboarddetail == null) ? null : onboarddetail.ExamDissimilarityOfSchedule,// onboarddetail.ExamDissimilarityOfSchedule,
                                          ExamExpectedApplicant = (onboarddetail == null) ? null : onboarddetail.ExamExpectedApplicant,// onboarddetail.ExamExpectedApplicant,
                                          ExamTentativeScheduleStart = (onboarddetail == null) ? null : onboarddetail.ExamTentativeScheduleStart,// onboarddetail.ExamTentativeScheduleStart,
                                          ExamTentativeScheduleEnd = (onboarddetail == null) ? null : onboarddetail.ExamTentativeScheduleEnd,// onboarddetail.ExamTentativeScheduleEnd,
                                          CounsExpectedApplicant = (onboarddetail == null) ? null : onboarddetail.CounsExpectedApplicant,// onboarddetail.CounsExpectedApplicant,
                                          CounsExpectedSeat = (onboarddetail == null) ? null : onboarddetail.CounsExpectedSeat,// onboarddetail.CounsExpectedSeat,
                                          CounsExpectedRound = (onboarddetail == null) ? null : onboarddetail.CounsExpectedRound,// onboarddetail.CounsExpectedRound,
                                          CounsExpectedSpotRound = (onboarddetail == null) ? null : onboarddetail.CounsExpectedSpotRound,// onboarddetail.CounsExpectedSpotRound,
                                          CounsExpectedParticipatingInstitute = (onboarddetail == null) ? null : onboarddetail.CounsExpectedParticipatingInstitute,// onboarddetail.CounsExpectedParticipatingInstitute,
                                          CounsTentativeScheduleStart = (onboarddetail == null) ? null : onboarddetail.CounsTentativeScheduleStart,// onboarddetail.CounsTentativeScheduleStart,
                                          CounsTentativeScheduleEnd = (onboarddetail == null) ? null : onboarddetail.CounsTentativeScheduleEnd,// onboarddetail.CounsTentativeScheduleEnd,
                                          CounsDissimilarityOfSchedule = (onboarddetail == null) ? null : onboarddetail.CounsDissimilarityOfSchedule,//onboarddetail.CounsDissimilarityOfSchedule,
                                          Status = requestDetails.Status.ToString(),
                                          OnBoardingDetailsStatus = (onboarddetail == null) ? null : onboarddetail.Status, //onboarddetail.Status
                                          Remarks = requestDetails.Remarks.ToString(),
                                          CoordinatorName = contact.Name,
                                          CoordinatorDesignation = contact.Designation,
                                          CoordinatorEmail = contact.EmailId,
                                          CoordinatorPhone = contact.MobileNo
                                      };
                OnboardRequest = requestlistdata.FirstOrDefault();
                OnboardRequest.MDStatusList = statusRusult.ToList();
                return OnboardRequest;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<bool> GetOnBoardingRequestLink(string requestId, CancellationToken cancellationToken)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@RequestNo",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = requestId,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
            };

            var storedProcedureName = $"{"USP_OnBoardingRequestLinkCheck"}  @RequestNo,@IsError output";
            int result = await this.unitOfWork.AppOnboardingDetailResponseRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
            bool s = (bool)param[1].Value;
            return s;
        }

        /// <inheritdoc/>
        public virtual async Task<Abstractions.Models.Status> GetStatusByIdAsync(string RequestId, CancellationToken cancellationToken)
        {
            //var param = new SqlParameter[]
            //{
            //    new SqlParameter()
            //    {
            //        ParameterName = "@RequestId",
            //        SqlDbType = System.Data.SqlDbType.VarChar,
            //        Value = RequestId,
            //    },
            //};
            var Status = new Status();

            try
            {
                /* To be removed
                using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "EXEC " + "USP_GetStatusById @RequestId";
                    foreach (var parameterDefinition in param)
                    {
                        command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                    }

                    List<Abstractions.Models.StatusOnboardingRequest> requestStatus = new List<Abstractions.Models.StatusOnboardingRequest>();
                    List<Abstractions.Models.StatusOnboardingDetail> detailStatus = new List<Abstractions.Models.StatusOnboardingDetail>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requestStatus.Add(new Abstractions.Models.StatusOnboardingRequest
                            {
                                RequestNo = reader.GetString(reader.GetOrdinal("RequestNo")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                Remarks = reader.GetString(reader.GetOrdinal("Remarks")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                            });
                            detailStatus.Add(new Abstractions.Models.StatusOnboardingDetail
                            {
                                RequestNo = reader.GetString(reader.GetOrdinal("RequestNOD")),
                                Remarks = reader.GetString(reader.GetOrdinal("RemarksD")),
                                Status = reader.GetString(reader.GetOrdinal("StatusD")),
                                CordEmail = reader.GetString(reader.GetOrdinal("cordmail")),
                                CordName = reader.GetString(reader.GetOrdinal("Name")),
                            });
                        }
                    }

                    Status.StatusRequest = requestStatus;
                    Status.StatusDetail = detailStatus;
                }*/
                var onboardingRequestDetail = await this.unitOfWork.AppOnboardingRequestRepository.FindAllByAsync(x => x.RequestNo == RequestId, cancellationToken).ConfigureAwait(false);
                var appContactPersonDetail = await this.unitOfWork.AppContactPersonDetailRepository.FindAllByAsync(x => x.RequestNo == RequestId, cancellationToken).ConfigureAwait(false);
                var appOnboardingDetails = await this.unitOfWork.AppOnboardingDetailRepository.FindAllByAsync(x => x.RequestNo == RequestId,
                  cancellationToken).ConfigureAwait(false);
                var requestStatus = from onboardingRequest in onboardingRequestDetail
                                    join appContactPerson in appContactPersonDetail on onboardingRequest.RequestNo equals appContactPerson.RequestNo
                                    where appContactPerson.RoleId == "11"
                                    select new StatusOnboardingRequest
                                    {
                                        RequestNo = onboardingRequest.RequestNo,
                                        Status = onboardingRequest.Status,
                                        Remarks = onboardingRequest.Remarks,
                                        Email = onboardingRequest.Email,
                                    };
                var details = from onboardingRequest in onboardingRequestDetail
                              join appContactPerson in appContactPersonDetail on onboardingRequest.RequestNo equals appContactPerson.RequestNo
                              where appContactPerson.RoleId == "11"
                              join appOnboardingDetail in appOnboardingDetails on onboardingRequest.RequestNo equals appOnboardingDetail.RequestNo into onboardingDetails
                              from appOnboardingDetail in onboardingDetails.DefaultIfEmpty()
                              select new StatusOnboardingDetail
                              {
                                  RequestNo = (appOnboardingDetail == null) ? "NA" : appOnboardingDetail.RequestNo,
                                  Remarks = (appOnboardingDetail == null) ? "NA" : appOnboardingDetail.Remarks,
                                  Status = (appOnboardingDetail== null) ? "NA" : appOnboardingDetail.Status ,
                                  CordEmail = (appContactPerson.EmailId.Length > 0) ? appContactPerson.EmailId : "NA",
                                  CordName = (appContactPerson.Name.Length > 0) ? appContactPerson.Name : "NA",
                              };
                Status.StatusRequest = requestStatus.ToList();
                Status.StatusDetail = details.ToList();
            }
            catch (Exception Ex)
            {
                logger.LogError(Ex.Message);
            }

            return Status;
        }
        /// <inheritdoc/>
        //public virtual Task<Abstractions.Models.DashboardCount> GetStatusCountAsync( CancellationToken cancellationToken)
        //{
        //    var onBoardingList = this.unitOfWork.AppOnboardingRequestRepository.GetAll();
        //    var onBoardingDetail = this.unitOfWork.AppOnboardingDetailRepository.GetAll();
        //    var List = new DashboardCount();
        //    using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
        //    {
        //        connection.Open();
        //        var command = connection.CreateCommand();
        //        command.CommandText = "EXEC " + "USP_StatuscCount";
        //        List<Abstractions.Models.DashboardCount> countlist = new List<Abstractions.Models.DashboardCount>();
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                countlist.Add(new Abstractions.Models.DashboardCount
        //                {
        //                    TotalRequest = onBoardingList.Count(),
        //                    ApprovedRequest = (from requestList in onBoardingList where requestList.Status == "RA" select requestList).Count(),
        //                    PendingRequest = (from requestList in onBoardingList where requestList.Status == "RP" select requestList).Count(),
        //                    HoldRequest = (from requestList in onBoardingList where requestList.Status == "RH" select requestList).Count(),
        //                    RejectRequest = (from requestList in onBoardingList where requestList.Status == "RR" select requestList).Count(),
        //                    ApprovedDetails = (from detailList in onBoardingDetail where detailList.Status == "DA" select detailList).Count(),
        //                    PendingDetails = (from detailList in onBoardingDetail where detailList.Status == "DP" select detailList).Count(),
        //                    ReturnDetails = (from detailList in onBoardingDetail where detailList.Status == "DT" select detailList).Count(),
        //                    RejectDetails = (from detailList in onBoardingDetail where detailList.Status == "DR" select detailList).Count(),
        //                    EligileDetails = (from requestList in onBoardingList where requestList.Status == "RA" select requestList).Count(),
        //                }); 
        //            }
        //        }

        //        List.StatusDetail = countlist;

        //    }

        //    return Task.FromResult(List);
        //}
        public virtual Task<Abstractions.Models.DashboardCount> GetStatusCountAsync(CancellationToken cancellationToken)
        {
            try
            {
                var onBoardingList = this.unitOfWork.AppOnboardingRequestRepository.GetAll();
                var onBoardingDetail = this.unitOfWork.AppOnboardingDetailRepository.GetAll();
                var List = new DashboardCount();
                List<Abstractions.Models.DashboardCount> countlist = new List<Abstractions.Models.DashboardCount>();
                countlist.Add(new Abstractions.Models.DashboardCount
                {
                    TotalRequest = onBoardingList.Count(),
                    ApprovedRequest = (from requestList in onBoardingList where requestList.Status == "RA" select requestList).Count(),
                    PendingRequest = (from requestList in onBoardingList where requestList.Status == "RP" select requestList).Count(),
                    HoldRequest = (from requestList in onBoardingList where requestList.Status == "RH" select requestList).Count(),
                    RejectRequest = (from requestList in onBoardingList where requestList.Status == "RR" select requestList).Count(),
                    ApprovedDetails = (from detailList in onBoardingDetail where detailList.Status == "DA" select detailList).Count(),
                    PendingDetails = (from detailList in onBoardingDetail where detailList.Status == "DP" select detailList).Count(),
                    ReturnDetails = (from detailList in onBoardingDetail where detailList.Status == "DT" select detailList).Count(),
                    RejectDetails = (from detailList in onBoardingDetail where detailList.Status == "DR" select detailList).Count(),
                    EligileDetails = (from requestList in onBoardingList where requestList.Status == "RA" select requestList).Count(),
                });
                List.StatusDetail = countlist;
                return Task.FromResult(List);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
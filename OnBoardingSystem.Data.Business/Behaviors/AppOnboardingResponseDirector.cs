//-----------------------------------------------------------------------
// <copyright file="AppOnboardingResponseDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Azure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.Interfaces;

    public class AppOnboardingResponseDirector : IAppOnboardingResponseDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SMSService sMSService;

        public AppOnboardingResponseDirector(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.utilityService = _utilityService;
            this._httpContextAccessor = httpContextAccessor;
            this.sMSService = _sMSService;
        }

        /// <inheritdoc/>
        public virtual async Task<bool> SaveAppOnboardingResponseData(AppOnboardingResponse appOnboardingResponse, CancellationToken cancellationToken)
        {
            
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@RequestNo",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingResponse.RequestNo,
                },
                new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingResponse.Status,
                },
                new SqlParameter()
                {
                    ParameterName = "@Remarks",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingResponse.Remarks,
                },
                new SqlParameter()
                {
                    ParameterName = "@UserId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingResponse.UserId,
                },
                new SqlParameter()
                {
                    ParameterName = "@IpAddress",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                },
                new SqlParameter()
                {
                    ParameterName = "@EncryptedRequestNumber",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appOnboardingResponse.EncryptedRequestNumber,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
            };

            var storedProcedureName = $"{"USP_InsertAppOnboardingResponse"}  @RequestNo,@Status,@Remarks,@UserId,@IpAddress,@EncryptedRequestNumber,@IsError output";
            int result = await this.unitOfWork.AppOnboardingDetailResponseRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
            bool s = (bool)param[6].Value;
            string decryptedCoordinatorMobile = Encoding.UTF8.GetString(Convert.FromBase64String(appOnboardingResponse.CordMobileNo));
            var smsTemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0004", cancellationToken).ConfigureAwait(false);
            string isdCode = "91";
            string smsMessage = "";
            if (s = true && appOnboardingResponse.Status == "RA")
            {
                var emaiTemplateApproval = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0001", cancellationToken).ConfigureAwait(false);
                string Subject = emaiTemplateApproval.MessageSubject;
                Subject = Subject.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                string Body = emaiTemplateApproval.MessageTemplate;
                Body = Body.Replace("#STATUS#","Approved");
                Body = Body.Replace("#REMARKS#", appOnboardingResponse.Remarks);
                Body = Body.Replace("#REQUESTNUMBER#", appOnboardingResponse.RequestNo);
                Body = Body.Replace("#REQNO#", appOnboardingResponse.EncryptedRequestNumber);
                Body = Body.Replace("#USER#", appOnboardingResponse.cordinatorName);
                MailRequest request = new MailRequest();
                request.ToEmail = appOnboardingResponse.MailingEmail;
                request.Subject = Subject;
                request.Body = Body;
                request.CCMail = appOnboardingResponse.cordMail;
                var mail = utilityService.SendEmailAsync(request);
                string TemplateId = smsTemplate.RegisteredTemplateId;
                string message = smsTemplate.MessageTemplate;
                smsMessage = message.Replace("#USER#", appOnboardingResponse.cordinatorName);
                smsMessage = smsMessage.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                smsMessage = smsMessage.Replace("#STATUS#", "Initiated");
                var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile,TemplateId, smsMessage);
            }
            else if (s = true && appOnboardingResponse.Status == "RR")
            {
                var emaiTemplateReject = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0010", cancellationToken).ConfigureAwait(false);
                string Subject = emaiTemplateReject.MessageSubject;
                Subject = Subject.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                string Body = emaiTemplateReject.MessageTemplate;
                Body = Body.Replace("#STATUS#", "Rejected");
                Body = Body.Replace("#REMARKS#", appOnboardingResponse.Remarks);
                Body = Body.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                Body = Body.Replace("#USER#", appOnboardingResponse.cordinatorName);
                MailRequest request = new MailRequest();
                request.ToEmail = appOnboardingResponse.MailingEmail;
                request.Subject = Subject;
                request.Body = Body;
                request.CCMail = appOnboardingResponse.cordMail;
                var mail = utilityService.SendEmailAsync(request);
                string TemplateId = smsTemplate.RegisteredTemplateId;
                string message = smsTemplate.MessageTemplate;
                smsMessage = message.Replace("#USER#", appOnboardingResponse.cordinatorName);
                smsMessage = smsMessage.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                smsMessage = smsMessage.Replace("#STATUS#", "Rejected");
                var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);

            }
            else if(s= true && appOnboardingResponse.Status == "RP")
            {
                var emaiTemplatePending = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0002", cancellationToken).ConfigureAwait(false);
                string Subject = emaiTemplatePending.MessageSubject;
                Subject = Subject.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                string Body = emaiTemplatePending.MessageTemplate;
                Body = Body.Replace("#STATUS#", "Pending");
                Body = Body.Replace("#REMARKS#", appOnboardingResponse.Remarks);
                Body = Body.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                Body = Body.Replace("#USER#", appOnboardingResponse.cordinatorName);
                MailRequest request = new MailRequest();
                request.ToEmail = appOnboardingResponse.MailingEmail;
                request.Subject = Subject;
                request.Body = Body;
                request.CCMail = appOnboardingResponse.cordMail;
                var mail = utilityService.SendEmailAsync(request);
                string TemplateId = smsTemplate.RegisteredTemplateId;
                string message = smsTemplate.MessageTemplate;
                smsMessage = message.Replace("#USER#", appOnboardingResponse.cordinatorName);
                smsMessage = smsMessage.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                smsMessage = smsMessage.Replace("#STATUS#", "Pending");
                var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                ////Changes
            }
            else if(s = true && appOnboardingResponse.Status == "RH")
            {
                var emaiTemplateHold = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0009", cancellationToken).ConfigureAwait(false);
                string Subject = emaiTemplateHold.MessageSubject;
                Subject = Subject.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                string Body = emaiTemplateHold.MessageTemplate;
                Body = Body.Replace("#STATUS#", "On Hold");
                Body = Body.Replace("#REMARKS#", appOnboardingResponse.Remarks);
                Body = Body.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                Body = Body.Replace("#USER#", appOnboardingResponse.cordinatorName);
                MailRequest request = new MailRequest();
                request.ToEmail = appOnboardingResponse.MailingEmail;
                request.Subject = Subject;
                request.Body = Body;
                request.CCMail = appOnboardingResponse.cordMail;
                var mail = utilityService.SendEmailAsync(request);
                string TemplateId = smsTemplate.RegisteredTemplateId;
                string message = smsTemplate.MessageTemplate;
                smsMessage = message.Replace("#USER#", appOnboardingResponse.cordinatorName);
                smsMessage = smsMessage.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                smsMessage = smsMessage.Replace("#STATUS#", "On Hold");
                var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
            }

            return s;
        }

        /// <inheritdoc/>
        public virtual async Task<List<AppOnboardingResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdagencyTypelist = await this.unitOfWork.AppOnboardingResponseRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AppOnboardingResponse>>(mdagencyTypelist);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<List<AppOnboardingResponse>> GetAllByRequestIdAsync(string requestId, CancellationToken cancellationToken)
        {
            var list = await this.unitOfWork.AppOnboardingResponseRepository.FindAllByAsync(x => x.RequestNo == requestId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AppOnboardingResponse>>(list);

            return result;
        }
    }
}
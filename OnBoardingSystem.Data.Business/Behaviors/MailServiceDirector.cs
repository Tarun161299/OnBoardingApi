//-----------------------------------------------------------------------
// <copyright file="MailServiceDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{

    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using System.Net.Mail;
    using System.Net;
    using OnBoardingSystem.Data.Business.Services;
    using AutoMapper;
    using OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class MailServiceDirector : IMailServiceDirector
    {
        private readonly MailService _mailSettings;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        public MailServiceDirector(IOptions<MailService> options, IUnitOfWork unitOfWork, UtilityService _utilityService)
        {
            _mailSettings = options.Value;
            this.unitOfWork = unitOfWork;
            this.utilityService = _utilityService;
        }
        /// <inheritdoc />
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            //SmtpClient _smtpClient = new SmtpClient(_mailSettings.Host);
            //var isEnableSSL = Convert.ToBoolean(_mailSettings.Port);
            //_smtpClient.EnableSsl = isEnableSSL;
            //var mailMsg = new MailMessage(
            //                from: _mailSettings.Username,
            //                to: mailRequest.ToEmail,
            //                subject: mailRequest.Subject,
            //                body: mailRequest.Body
            //                );
            //mailMsg.IsBodyHtml = true;
            //_smtpClient.Port = Convert.ToInt32(587);
            //_smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //_smtpClient.UseDefaultCredentials = false;
            //string emailFrom = _mailSettings.Username;
            //string pwd = _mailSettings.Password;
            //_smtpClient.Credentials = new NetworkCredential(emailFrom, pwd);
            //await _smtpClient.SendMailAsync(mailMsg);
            //return true;
        }

        /// <inheritdoc />
        public virtual async Task<bool> sendRequestStatusEmailAsync(string Email,string requestno,string statusReq,string statusdetail,string Cordmail,string cordName, CancellationToken cancellationToken)
        {
            var emaiTemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0011", cancellationToken).ConfigureAwait(false);
            var detailattachedpdf= await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == requestno && x.DocType=="DL", cancellationToken).ConfigureAwait(false);
            var requestAttachpdf = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == requestno && x.DocType == "RP", cancellationToken).ConfigureAwait(false);
            //var emaiTemplateA = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0001", cancellationToken).ConfigureAwait(false);
            string Subject = emaiTemplate.MessageSubject;
            Subject = Subject.Replace("#STATUS#", statusReq);
            Subject = Subject.Replace("#REQNO#", requestno);
            string Body = emaiTemplate.MessageTemplate;
            Body= Body.Replace("#STATUS#", statusReq);
            Body = Body.Replace("#REQNO#", requestno);
            Body = Body.Replace("#STATUSDetail#", statusdetail);
            Body = Body.Replace("#USER#", cordName);
            MailRequest request = new MailRequest();
            request.ToEmail = Cordmail;
            request.CCMail = Email;
            request.Subject = Subject;
            request.Body = Body;
            if (statusdetail == "NA")
            {
                if (requestAttachpdf != null) {
                    request.Attachment = requestAttachpdf.DocContent;
                }
             
            }
            else
            {
                if (detailattachedpdf!=null) { request.Attachment = detailattachedpdf.DocContent; }
                }
            var mail =  utilityService.SendEmailAsync(request);
            return true;
        }
        }
    }

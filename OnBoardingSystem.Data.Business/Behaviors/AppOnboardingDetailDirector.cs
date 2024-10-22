//-----------------------------------------------------------------------
// <copyright file="AppOnboardingDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Net.Mail;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using Newtonsoft.Json.Linq;
    using OnBoardingSystem.Common.enums;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.EF.Models;
    //using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using AbsModel = OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class AppOnboardingDetailDirector : IAppOnboardingDetailDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        private readonly SMSService sMSService;
        private readonly EncryptionDecryptionService decryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingDetailDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppOnboardingDetailDirector(IMapper mapper, IUnitOfWork unitOfWork, EncryptionDecryptionService _encryptionDecryptionService, UtilityService _utilityService, SMSService _sMSService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.utilityService = _utilityService;
            this.sMSService = _sMSService;
            this.decryptionService = _encryptionDecryptionService;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModel.AppOnboardingDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await this.unitOfWork.AppOnboardingRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.AppOnboardingDetails>>(list);
            return result;
        }

        /// <inheritdoc/>
        public Task<int> SaveAsync(AbsModel.AppOnboardingDetails appOnboardingRequestData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        Task<AbsModel.AppOnboardingDetails> IAppOnboardingDetailDirector.GetListByIdAsync(string requestId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

         /// <inheritdoc />
        public virtual async Task<bool> Save(string AppOnboardingDetails, CancellationToken cancellationToken)
        {
            var emailbyRoleId = await this.unitOfWork.EmailRecipientRepository.FindAllByAsync(x => x.RoleId == '1', cancellationToken).ConfigureAwait(false);

            var details = JObject.Parse(AppOnboardingDetails);
            string requestNo = (string)details["RequestNo"];
            string mode= (string)details["Mode"];
            string AttachmentFile = (string)details["AttachFilecontent"];

            string coordinatormail= (string)details["CoordinatorMail"]; ;
            string hodmail= (string)details["HodMail"];
            string ipaddress = (string)details["Ipaddress"];
            var CoordinatorNamedetail = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == requestNo && x.RoleId=="11", cancellationToken).ConfigureAwait(false);
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@InputJson",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = AppOnboardingDetails,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
                //new SqlParameter()
                //{
                //    ParameterName = "@Message",   
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Direction = System.Data.ParameterDirection.Output,
                //},
            };
            var storedProcedureName = $"{"USP_SaveOnboardingDetail"}  @InputJson,@IsError output";
            int result = await this.unitOfWork.AppOnboardingDetailRepository.ExecuteSqlRawAsync(storedProcedureName,ref param, cancellationToken).ConfigureAwait(false);
            bool s = (bool)param[1].Value;
            var saveDocument = new AbsModel.AppDocumentUploadedDetail();
            if (s == true && mode== "FinalSubmit")
            {
                //var smsTemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0005", cancellationToken).ConfigureAwait(false);
                MailRequest mailContentPMU = new MailRequest();
                var emaiTemplatePMU = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0013", cancellationToken).ConfigureAwait(false);
                mailContentPMU.Body = emaiTemplatePMU.MessageTemplate.Replace("#REQNO#", requestNo.ToString());
                mailContentPMU.Subject = emaiTemplatePMU.MessageSubject.Replace("#REQNO#", requestNo.ToString());
                foreach (var pmumail in emailbyRoleId)
                {
                    mailContentPMU.ToEmail = pmumail.Email;
                    var mail = utilityService.SendEmailAsync(mailContentPMU);
                }
                MailRequest mailCoodinator = new MailRequest();
                var emaiTemplatCoord = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0012", cancellationToken).ConfigureAwait(false);
                // var emaiTemplatePMU = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0013", cancellationToken).ConfigureAwait(false);
                mailCoodinator.Body = emaiTemplatCoord.MessageTemplate.Replace("#REQNO#", requestNo.ToString());
                mailCoodinator.Body = emaiTemplatCoord.MessageTemplate.Replace("#USER#", CoordinatorNamedetail.Name);
                mailCoodinator.Subject = emaiTemplatCoord.MessageSubject.Replace("#REQNO#", requestNo.ToString());

                mailCoodinator.ToEmail = coordinatormail;
                mailCoodinator.CCMail = hodmail;
                mailCoodinator.Attachment = AttachmentFile;
                saveDocument.DocContent = AttachmentFile;
                saveDocument.Activityid =((int)Enumactivity.OnboardingDetails).ToString();
                saveDocument.DocType = "DL";
                saveDocument.ModuleRefId = requestNo;
                saveDocument.IpAddress = ipaddress;
                saveDocument.SubTime = DateTime.Now;
                var SaveDocument= this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(saveDocument);
                await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(SaveDocument, cancellationToken).ConfigureAwait(false);
                await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                var mailsave = utilityService.SendEmailAsync(mailCoodinator);
                var userCredentials = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == requestNo && x.RoleId == "11", cancellationToken).ConfigureAwait(false); ;
                //var sendSmsandemail = this.mapper.Map<List<OnBoardingSystem.Data.Abstractions.Models.AppContactPersonDetails>>(userCredentials);
                // string userEmail= Encoding.UTF8.GetString(sendSmsandemail[0].emailId)
                var smsTemplatedetailSubmit = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0005", cancellationToken).ConfigureAwait(false);
                string userEmail = decryptionService.Decryption(userCredentials.EmailId);
                string mobile = decryptionService.Decryption(userCredentials.MobileNo);
                string coordinatorName = userCredentials.Name;
                string isdCode = "91";
                string smsMessage = "";
                string TemplateId = smsTemplatedetailSubmit.RegisteredTemplateId;
                string message = smsTemplatedetailSubmit.MessageTemplate;
                smsMessage = message.Replace("#USER#", userCredentials.Name);
                smsMessage = smsMessage.Replace("#STATUS#", "submitted");
                smsMessage = smsMessage.Replace("#REQNO#", requestNo);
                var sms = sMSService.SendStatusSmsAsync(isdCode + mobile, TemplateId, smsMessage);
            }
            return s;
        }

        public virtual async Task<int> Updatestatus(AbsModel.AppOnboardingDetailStatus statusupdate, CancellationToken cancellationToken)
        {
            string decryptedCoordinatorMobile = Encoding.UTF8.GetString(Convert.FromBase64String(statusupdate.cordNumber));
            var smsTemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0006", cancellationToken).ConfigureAwait(false);
            int result=-1;
            if (statusupdate.Activity == "Update")
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@RequestNo",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = statusupdate.requestNo,
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Status",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = statusupdate.Status,
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Remarks",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = statusupdate.Remarks,
                    },
                };
                var storedProcedureName = $"{"Usp_OnboardingDetailsUpdateStatus"}  @RequestNo,@Status,@Remarks";
                result = await this.unitOfWork.AppOnboardingDetailRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
                if (result > 0 && statusupdate.Status == "DA")
                {
                    var emaiTemplateA = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0003", cancellationToken).ConfigureAwait(false);
                    string Subject = emaiTemplateA.MessageSubject;
                    Subject = Subject.Replace("#REQNO#", statusupdate.requestNo);
                    string Body = emaiTemplateA.MessageTemplate;
                    Body = Body.Replace("#USER#", statusupdate.cordName);
                    Body = Body.Replace("#REMARKS#", statusupdate.Remarks);
                    Body = Body.Replace("#STATUS#", "Approved");
                    Body = Body.Replace("#REQNO#", statusupdate.requestNo);
                    Body = Body.Replace("#ENCREQNO#", statusupdate.EncryptedRequestNumber);
                    MailRequest request = new MailRequest();
                    request.ToEmail = statusupdate.Email;
                    request.Subject = Subject;
                    request.Body = Body;
                    var mail = utilityService.SendEmailAsync(request);

                    string isdCode = "91";
                    string smsMessage = smsTemplate.MessageTemplate; ;
                    string TemplateId = smsTemplate.RegisteredTemplateId;
                    smsMessage = smsMessage.Replace("#USER#", statusupdate.cordName);
                    smsMessage = smsMessage.Replace("#REQNO#", statusupdate.requestNo);
                    smsMessage = smsMessage.Replace("#STATUS#", "Approved");
                    var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                }

                if (result > 0 && statusupdate.Status == "DT")
                {
                    var emaiTemplateT = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0004", cancellationToken).ConfigureAwait(false);
                    string Subject = emaiTemplateT.MessageSubject;
                    Subject = Subject.Replace("#REQNO#", statusupdate.requestNo);
                    string Body = emaiTemplateT.MessageTemplate;
                    Body = Body.Replace("#USER#", statusupdate.cordName);
                    Body = Body.Replace("#STATUS#", "Returned");
                    Body = Body.Replace("#ENCREQNO#", statusupdate.EncryptedRequestNumber);
                    Body = Body.Replace("#REMARKS#", statusupdate.Remarks);
                    Body = Body.Replace("#REQNO#", statusupdate.requestNo);
                    MailRequest request = new MailRequest();
                    request.ToEmail = statusupdate.Email;
                    request.Subject = Subject;
                    request.Body = Body;
                    var mail = utilityService.SendEmailAsync(request);

                    string isdCode = "91";
                    string smsMessage = "";
                    string TemplateId = smsTemplate.RegisteredTemplateId;
                    string message = smsTemplate.MessageTemplate;
                    smsMessage = message.Replace("#USER#", statusupdate.cordName);
                    smsMessage = smsMessage.Replace("#REQNO#", statusupdate.requestNo);
                    smsMessage = smsMessage.Replace("#STATUS#", "Returned");
                    var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                }

                if (result > 0 && statusupdate.Status == "DR")
                {
                    var emaiTemplateR = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0005", cancellationToken).ConfigureAwait(false);
                    string Subject = emaiTemplateR.MessageSubject;
                    Subject = Subject.Replace("#REQNO#", statusupdate.requestNo);
                    string Body = emaiTemplateR.MessageTemplate;
                    Body = Body.Replace("#USER#", statusupdate.cordName);
                    Body = Body.Replace("#STATUS#", "Rejected");
                    Body = Body.Replace("#REMARKS#", statusupdate.Remarks);
                    Body = Body.Replace("#REQNO#", statusupdate.requestNo);
                    MailRequest request = new MailRequest();
                    request.ToEmail = statusupdate.Email;
                    request.Subject = Subject;
                    request.Body = Body;
                    var mail = utilityService.SendEmailAsync(request);

                    string isdCode = "91";
                    string smsMessage = "";
                    string TemplateId = smsTemplate.RegisteredTemplateId;
                    string message = smsTemplate.MessageTemplate;
                    smsMessage = message.Replace("#USER#", statusupdate.cordName);
                    smsMessage = smsMessage.Replace("#REQNO#", statusupdate.requestNo);
                    smsMessage = smsMessage.Replace("#STATUS#", "Rejected");
                    var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                }
                return result;
            }

            if (statusupdate.Activity == "ResendRegistrationLink")
            {
                if(statusupdate.Status == "DA")
                {
                    var emaiTemplateA = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0003", cancellationToken).ConfigureAwait(false);
                    string Subject = emaiTemplateA.MessageSubject;
                    Subject = Subject.Replace("#REQNO#", statusupdate.requestNo);
                    string Body = emaiTemplateA.MessageTemplate;
                    Body = Body.Replace("#USER#", statusupdate.cordName);
                    Body = Body.Replace("#REMARKS#", statusupdate.Remarks);
                    Body = Body.Replace("#STATUS#", "Approved");
                    Body = Body.Replace("#REQNO#", statusupdate.requestNo);
                    Body = Body.Replace("#ENCREQNO#", statusupdate.EncryptedRequestNumber);
                    MailRequest request = new MailRequest();
                    request.ToEmail = statusupdate.Email;
                    request.Subject = Subject;
                    request.Body = Body;
                    var mail = utilityService.SendEmailAsync(request);

                    string isdCode = "91";
                    string smsMessage = smsTemplate.MessageTemplate; ;
                    string TemplateId = smsTemplate.RegisteredTemplateId;
                    smsMessage = smsMessage.Replace("#USER#", statusupdate.cordName);
                    smsMessage = smsMessage.Replace("#REQNO#", statusupdate.requestNo);
                    smsMessage = smsMessage.Replace("#STATUS#", "Approved");
                    var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                    result = 11;
                }

                if(statusupdate.Status == "DT")
                {
                    var emaiTemplateT = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0004", cancellationToken).ConfigureAwait(false);
                    string Subject = emaiTemplateT.MessageSubject;
                    Subject = Subject.Replace("#REQNO#", statusupdate.requestNo);
                    string Body = emaiTemplateT.MessageTemplate;
                    Body = Body.Replace("#USER#", statusupdate.cordName);
                    Body = Body.Replace("#STATUS#", "Returned");
                    Body = Body.Replace("#ENCREQNO#", statusupdate.EncryptedRequestNumber);
                    Body = Body.Replace("#REMARKS#", statusupdate.Remarks);
                    Body = Body.Replace("#REQNO#", statusupdate.requestNo);
                    MailRequest request = new MailRequest();
                    request.ToEmail = statusupdate.Email;
                    request.Subject = Subject;
                    request.Body = Body;
                    var mail = utilityService.SendEmailAsync(request);

                    string isdCode = "91";
                    string smsMessage = "";
                    string TemplateId = smsTemplate.RegisteredTemplateId;
                    string message = smsTemplate.MessageTemplate;
                    smsMessage = message.Replace("#USER#", statusupdate.cordName);
                    smsMessage = smsMessage.Replace("#REQNO#", statusupdate.requestNo);
                    smsMessage = smsMessage.Replace("#STATUS#", "Returned");
                    var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);
                    result = 11;
                }
            }
            return result;
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="UtilityService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Services
{
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Microsoft.Extensions.Options;
    using OnBoardingSystem.Data.Abstractions.Models;
    using Microsoft.AspNetCore.Http;
    using static System.Net.WebRequestMethods;
    using System;

    /// <inheritdoc />
    public class UtilityService
    {
        private readonly Random _random = new Random();
        private readonly MailService _mailSettings;
        //private readonly Random _random = new Random();
        // public Session Session { get;  set; }
        // public string salt { get;set; }

        public UtilityService(IOptions<MailService> options, IHttpContextAccessor httpContextAccessor)
        {
            _mailSettings = options.Value;
            //this._httpContextAccessor = httpContextAccessor;

        }

        /// <inheritdoc />
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            //New Code Start
            ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //New Code end

            byte[] file;
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = _mailSettings.Host;
            MailMessage MailMsg = new MailMessage(new MailAddress(_mailSettings.Username), new MailAddress(mailRequest.ToEmail));        
            MailMsg.BodyEncoding = Encoding.Default;
            MailMsg.Subject = mailRequest.Subject.Trim();
            MailMsg.Body = mailRequest.Body.Trim();

            MailMsg.Priority = MailPriority.High;
            MailMsg.IsBodyHtml = true;
            if (mailRequest.Attachment != null)
            {
               file= Convert.FromBase64String(mailRequest.Attachment);
               Attachment mailAttachment = new Attachment(new MemoryStream(file),"OnboardingSystem.pdf");
               MailMsg.Attachments.Add(mailAttachment);
            }
            if (!string.IsNullOrEmpty(mailRequest.CCMail))
                MailMsg.CC.Add(mailRequest.CCMail);

            // New Code Start
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Port = _mailSettings.Port;
            SmtpMail.EnableSsl = _mailSettings.IsEnableSSL;
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            string emailFrom = _mailSettings.Username;
            string pwd = _mailSettings.Password;
            SmtpMail.Credentials = new NetworkCredential(emailFrom, pwd);
            SmtpMail.Send(MailMsg);

            /*
            SmtpClient _smtpClient = new SmtpClient(_mailSettings.Host);
            var isEnableSSL = Convert.ToBoolean(_mailSettings.Port);
            _smtpClient.EnableSsl = isEnableSSL;
            var mailMsg = new MailMessage(
                            from: _mailSettings.Username,
                            to: mailRequest.ToEmail,
                            subject: mailRequest.Subject,
                            body: mailRequest.Body
                            );
            mailMsg.IsBodyHtml = true;
            _smtpClient.Port = _mailSettings.Port;  //Convert.ToInt32(587);
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.UseDefaultCredentials = false;
            string emailFrom = _mailSettings.Username;
            string pwd = _mailSettings.Password;
            _smtpClient.Credentials = new NetworkCredential(emailFrom, pwd);
            await _smtpClient.SendMailAsync(mailMsg);
            */

           ////byte[] file;
           ////SmtpClient SmtpMail = new SmtpClient();
           ////SmtpMail.Host = _mailSettings.Host;
           ////MailMessage MailMsg = new MailMessage(new MailAddress(_mailSettings.Username), new MailAddress(mailRequest.ToEmail));
           ////MailMsg.BodyEncoding = Encoding.Default;
           ////MailMsg.Subject = mailRequest.Subject.Trim();
           ////MailMsg.Body = mailRequest.Body.Trim();
           ////
           ////MailMsg.Priority = MailPriority.High;
           ////MailMsg.IsBodyHtml = true;
           ////if (mailRequest.Attachment != null)
           ////{
           ////    file = Convert.FromBase64String(mailRequest.Attachment);
           ////    Attachment mailAttachment = new Attachment(new MemoryStream(file), "OnboardingSystem.pdf");
           ////    MailMsg.Attachments.Add(mailAttachment);
           ////}
           ////if (!string.IsNullOrEmpty(mailRequest.CCMail))
           ////    MailMsg.CC.Add(mailRequest.CCMail);
           ////
           ////// New Code Start
           ////SmtpMail.UseDefaultCredentials = false;
           ////SmtpMail.EnableSsl = true;
           ////SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
           ////SmtpMail.Send(MailMsg);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }

}

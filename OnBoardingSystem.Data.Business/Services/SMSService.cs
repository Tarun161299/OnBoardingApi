//-----------------------------------------------------------------------
// <copyright file="SMSService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Services
{
    using System.Net;
    using System.Net.Mail;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Microsoft.Extensions.Options;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class SMSService
    {
        /// <inheritdoc />
        public async Task SendAsync(string mobileno,string OTP,string TemplateId,string messagRPH)
        {
            string SMSPriority = string.Empty;
            StreamReader reader = null;
            Stream dataStream = null;
            HttpWebRequest request = null;
            HttpWebResponse respons = default(HttpWebResponse);
            string url = string.Empty;
            string responseFromServer = string.Empty;
            string priority = "P";
            try
            {
                Check_SSL_Certificate();
                if (priority.Trim().ToUpper().Equals("P"))
                {
                    SMSPriority = "H";
                    url = "https://ecounssms.nic.in/ems/api/PSMS/SendTemplateSMS?ApplicationId=admissions&MobileNo=" + mobileno.ToString() + "&Message=" + messagRPH.Trim() + "&custRef=admissions&APIKey=0987656789123456&templateid=" + TemplateId;
                }
                else if (priority.Trim().ToUpper().Equals("N"))
                {
                    SMSPriority = "N";
                    url = "http://ecounssms.nic.in/ems/api/SMS/SendTemplateSMS?ApplicationId=admissions&MobileNo=" + mobileno.ToString() + "&Message=" + messagRPH.Trim() + "&custRef=admissions&APIKey=0987656789123456&templateId=" + TemplateId;
                }

                request = (HttpWebRequest)WebRequest.Create(url);
                respons = (HttpWebResponse)request.GetResponse();
                dataStream = respons.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                respons.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendStatusSmsAsync(string mobileno, string TemplateId, string smsStatusMessage)
        {
            string SMSPriority = string.Empty;
            StreamReader reader = null;
            Stream dataStream = null;
            HttpWebRequest request = null;
            HttpWebResponse respons = default(HttpWebResponse);
            string url = string.Empty;
            string responseFromServer = string.Empty;
            string priority = "P";
            try
            {
                Check_SSL_Certificate();
                if (priority.Trim().ToUpper().Equals("P"))
                {
                    SMSPriority = "H";
                    url = "https://ecounssms.nic.in/ems/api/PSMS/SendTemplateSMS?ApplicationId=admissions&MobileNo=" + mobileno.ToString() + "&Message=" + smsStatusMessage.Trim() + "&custRef=admissions&APIKey=0987656789123456&templateid=" + TemplateId;
                }
                else if (priority.Trim().ToUpper().Equals("N"))
                {
                    SMSPriority = "N";
                    url = "http://ecounssms.nic.in/ems/api/SMS/SendTemplateSMS?ApplicationId=admissions&MobileNo=" + mobileno.ToString() + "&Message=" + smsStatusMessage.Trim() + "&custRef=admissions&APIKey=0987656789123456&templateId=" + TemplateId;
                }

                request = (HttpWebRequest)WebRequest.Create(url);
                respons = (HttpWebResponse)request.GetResponse();
                dataStream = respons.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                respons.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Check_SSL_Certificate()
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;//(SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

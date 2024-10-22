using AutoMapper;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;
using OnBoardingSystem.Data.Business.Services;
using OnBoardingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    public class CaptchaDirector : ICaptchaDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// /// Initializes a new instance of the <see cref="CaptchaDirector"/> class.
		/// </summary>
		/// <param name="mapper">Automapper.</param>
		/// <param name="unitOfWork">Unit of Work.</param>
        public CaptchaDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;

        }

        public virtual async Task<AppCaptcha> InsertAsync(string key, string base64String, string hashvalue,CancellationToken cancellationToken)
        {
            var ConfigEnv = await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(x => x.ApplicationId == 1, cancellationToken).ConfigureAwait(false);
            string keyCaptcha = ConfigEnv.CaptchaValue;
            string hashvalueCaptcha;
            var cc = new AppCaptcha();
            cc.Md5Hash = hashvalue;
            cc.Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            cc.CaptchaKey = key;
            cc.CaptchBaseString = base64String;
            var captchaCode = this.mapper.Map<Data.EF.Models.AppCaptcha>(cc);
            if (ConfigEnv.CaptchaMode == "F")
            {
                return cc;

            }
            else
            {
                await this.unitOfWork.AppCaptchaRepository.InsertAsync(captchaCode, cancellationToken).ConfigureAwait(false);

            }
            var effectedRows = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            if (effectedRows > 0)
            {
                return cc;
            }
            else
            {
                return null;
            }
        }
        public virtual async Task<int> checkCaptcha(Check_captcha captcha, CancellationToken cancellationToken)
        {

            try
            {
                int s = 0;
                var ConfigEnv = await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(x => x.ApplicationId == 1, cancellationToken).ConfigureAwait(false);
                string keyCaptcha = ConfigEnv.CaptchaValue;
                if (ConfigEnv.CaptchaMode == "F")
                {
                    if (captcha.key == keyCaptcha)
                    {
                        return s = 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    var isCaptchaTrue = await this.unitOfWork.AppCaptchaRepository.FindAllByAsync(x => x.CaptchaKey == captcha.key && x.Md5Hash == captcha.hash, cancellationToken).ConfigureAwait(false);
                    if (isCaptchaTrue.Count() != 0)
                    {
                        await this.unitOfWork.AppCaptchaRepository.DeleteAsync(x => x.CaptchaKey == captcha.key && x.Md5Hash == captcha.hash, cancellationToken).ConfigureAwait(false);
                        await this.unitOfWork.AppCaptchaRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
                        s = 1;
                    }

                    return s;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}

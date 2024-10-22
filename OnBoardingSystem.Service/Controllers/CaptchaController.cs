namespace OnBoardingSystem.Service.Controllers
{
    using Extensions = SixLaborsCaptcha.Core.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using SixLaborsCaptcha.Core;

    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaDirector ecaptchaDirector;
        private readonly IConfigurationEnvironmentDirector configurationenvironmentDirector;
        private readonly ILogger<CaptchaController> logger;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="CaptchaController">CaptchaController.</param>
        /// <param name="logger">logger.</param>

        public CaptchaController(ICaptchaDirector captchaDirector,  IConfigurationEnvironmentDirector _configurationenvironmentDirector, ILogger<CaptchaController> logger, IConfiguration configuration)
        {
            this.ecaptchaDirector = captchaDirector;
            _config = configuration;
            this.configurationenvironmentDirector = _configurationenvironmentDirector;
        }

        [HttpGet]
        public async Task<ActionResult<AppCaptcha>> GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)
        {
            string hashvalue;
            string key = Extensions.GetUniqueKey(6);
            //string key = "123456";
            string base64String;
            var imgText = sixLaborsCaptcha.Generate(key);
            base64String = Convert.ToBase64String(imgText, 0, imgText.Length);
            //ConfigurationEnvironment data = new ConfigurationEnvironment();
            //var captchaImg=await this.configurationenvironmentDirector.GetByIdAsync(1, default).ConfigureAwait(false);
            //if (captchaImg.CaptchaMode=="F")
            //{
            //    key = captchaImg.CaptchaValue;
            //     imgText = sixLaborsCaptcha.Generate(key);
            //     base64String = Convert.ToBase64String(imgText, 0, imgText.Length);
            //}

            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                hashvalue = Convert.ToHexString(hashBytes);
            }
            var response = await ecaptchaDirector.InsertAsync(key, base64String, hashvalue,  default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CheckCaptcha([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha, string input)
        {
            string hashvalue;
            string key = input;
            var imgText = sixLaborsCaptcha.Generate(key);
            string base64String = Convert.ToBase64String(imgText, 0, imgText.Length);
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                hashvalue = Convert.ToHexString(hashBytes);
            }

            Check_captcha captcha = new Check_captcha();
            captcha.key = key;
            captcha.hash = hashvalue;
            var response = await ecaptchaDirector.checkCaptcha(captcha, default).ConfigureAwait(false);
            return response;
        }

    }
}
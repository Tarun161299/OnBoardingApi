using Azure.Core;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Business.Services
{
    public class MDApiKeyValidateService
    {
        private const string ApiKey = "ApiSettings:MDApiKey";

        private readonly IConfiguration _configuration;

        public MDApiKeyValidateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateApiKeyForMaster(HttpContext context)
        {
            var headers = context.Request.Headers;
            var objCollection = new Dictionary<string, string>();
            foreach (var item in headers)
            {
                objCollection.Add(item.Key, string.Join(string.Empty, item.Value));
            }
            var ApiKeys = context.RequestServices.GetRequiredService<IConfiguration>();

            var webConfigApiKey = ApiKeys.GetValue<string>(ApiKey);
            string resultValue;
            if (objCollection["apikey"] == webConfigApiKey || objCollection["ApiKey"] == webConfigApiKey)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

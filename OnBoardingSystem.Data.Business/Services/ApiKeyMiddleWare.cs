using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Core;
using DocumentFormat.OpenXml.InkML;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace OnBoardingSystem.Data.Business.Services
{
    public class ApiKeyMiddleWare
    {
        private const string ConnectionString = "ConnectionStrings:OnBoardingSystem";
        private const string ApiKey = "ApiKey";
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiKeyMiddleWare(RequestDelegate next, IHttpContextAccessor httpContextAccessor)//, IAppOnboardingAdminloginDirector _AppAdminLoginDirector)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            //this.JWTTokenService = _JWTTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
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
                if (ValidateApiKeyForMaster(objCollection["apikey"], context) || ValidateApiKeyForMaster(objCollection["ApiKey"],context))
                {

                    await this._next(context);
                }
                else
                {
                    //throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    throw new ArgumentException("This request is unauthorized");
                }
            }
            catch (Exception ex)
            {
                 throw new HttpResponseException(HttpStatusCode.BadRequest);
                //throw new ArgumentException("This request is unauthorized");
            }
        }

        public bool ValidateApiKeyForMaster(string apiKey, HttpContext context)
        {
            bool hasApiKey=false;

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@Apikey",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = apiKey,
                },
            };

            using (SqlConnection conn = new SqlConnection())
            {
                var ConnectionStrings = context.RequestServices.GetRequiredService<IConfiguration>();
                var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                conn.ConnectionString = webConfigConnectionString;
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC " + "usp_ValidateApiKey @Apikey";

                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hasApiKey = reader.GetBoolean(reader.GetOrdinal("message"));
                    }
                }
                conn.Close();
                return hasApiKey;
            }
        }
    }
}

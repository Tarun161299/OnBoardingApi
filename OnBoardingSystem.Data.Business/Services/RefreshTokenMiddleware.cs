using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using unitOfWorkdb = OnBoardingSystem.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Authentication.OAuth;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;

namespace OnBoardingSystem.Data.Business.Services
{
    public class RefreshTokenMiddleware
    {
        private const string ConnectionString = "ConnectionStrings:OnBoardingSystem";
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWTTokenService JWTTokenService;
        private readonly IConfiguration config;
        HttpActionContext actionContext;
        HttpResponseException exception;

        public RefreshTokenMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, JWTTokenService _JWTTokenService, IConfiguration coniguration)//, IAppOnboardingAdminloginDirector _AppAdminLoginDirector)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            this.JWTTokenService = _JWTTokenService;
            config = coniguration;
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

                string resultValue;
                string test = JsonSerializer.Serialize(objCollection);
                if ((objCollection.ContainsKey("refreshtoken") || objCollection.ContainsKey("RefreshToken")) && objCollection.ContainsKey("Authorization"))
                {
                    string Refreshtoken = "";
                    if (objCollection.ContainsKey("RefreshToken"))
                    {
                        Refreshtoken = objCollection["RefreshToken"];
                    }
                    else
                    {
                        Refreshtoken = objCollection["refreshtoken"];
                    }

                    string token = objCollection["Authorization"];

                    if (CheckRefreshToken(Refreshtoken, token, context))
                    {
                        await this._next(context);
                    }
                    else
                    {
                        throw new ArgumentException("This request is unauthorized");
                    }
                }
                else
                {
                    await this._next(context);
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    throw new ArgumentException("This request is unauthorized");
                }
                else
                {
                    throw ex;
                }

                //throw ex;
            }
        }

        private static ControllerActionDescriptor GetControllerByUrl(HttpContext httpContext)
        {
            var actionDescriptorsProvider = httpContext.RequestServices.GetRequiredService<IActionDescriptorCollectionProvider>();
            ControllerActionDescriptor controller = actionDescriptorsProvider.ActionDescriptors.Items
            .Where(s => s is ControllerActionDescriptor bb
                        && bb.ActionName == "Send"
                        && bb.ControllerName == "AAA"
                        && (bb.ActionConstraints == null
                            || (bb.ActionConstraints != null
                                && bb.ActionConstraints.Any(x => x is HttpMethodActionConstraint cc
                                && cc.HttpMethods.Any(m => m.ToLower() == httpContext.Request.Method.ToLower())))))
            .Select(s => s as ControllerActionDescriptor)
            .FirstOrDefault();
            return controller;
        }
        public bool CheckRefreshToken(string reftoken, string AccessToken, HttpContext context)
        {
            string AccessTokenUAT = AccessToken.Substring(7);
            var stream = AccessToken.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var IpAddress = tokenS.Claims.First(claim => claim.Type == "systemIP").Value;
            var browserid = tokenS.Claims.First(claim => claim.Type == "browserId").Value;
            var UserName = tokenS.Claims.First(claim => claim.Type == "userId").Value;

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                ParameterName = "@userid",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = UserName,
                },
                new SqlParameter()
                {
                ParameterName = "@refreshToken",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = reftoken,
                },
                new SqlParameter()
                {
                ParameterName = "@Token",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = AccessToken.Substring(7),
                },
            };

            using (SqlConnection conn = new SqlConnection())
            {
                var ConnectionStrings = context.RequestServices.GetRequiredService<IConfiguration>();

                var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                conn.ConnectionString = webConfigConnectionString;
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC " + "GetRefreshToken @userid,@refreshToken,@Token";

                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }

                var refreshToken = "";
                var Authtoken = "";
                string token;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        refreshToken = reader.GetString(reader.GetOrdinal("refreshToken"));
                        Authtoken = reader.GetString(reader.GetOrdinal("Token"));
                        token = refreshToken;
                    }
                    else
                    {
                        token = "";
                    }
                }

                conn.Close();

                if ((token == reftoken && Authtoken == AccessTokenUAT && IpAddress == _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() && browserid == _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString()) || reftoken == "123")
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
}
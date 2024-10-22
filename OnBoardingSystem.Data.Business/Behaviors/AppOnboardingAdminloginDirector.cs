//-----------------------------------------------------------------------
// <copyright file="AppOnboardingAdminloginDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
	using Microsoft.Data.SqlClient;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using OnBoardingSystem.Data.Abstractions.Behaviors;
	using OnBoardingSystem.Data.Abstractions.Models;
	using OnBoardingSystem.Data.Interfaces;
	using OnBoardingSystem.Data.Business.Services;
	using EFModel = OnBoardingSystem.Data.EF.Models;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using System.Text;
	using Microsoft.Extensions.Options;
	using System.Security.Cryptography;
	using EF = OnBoardingSystem.Data.EF.Models;
	using OnBoardingSystem.Common.enums;
    using Microsoft.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using OnBoardingSystem.Data.Business.Services;
    using Azure.Core;
    using System.Text.Json;
    using System.Reflection;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using Azure;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Identity;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using System.Security.Cryptography;
    using System.Linq;
    using Abp.Collections.Extensions;
    using DocumentFormat.OpenXml.Spreadsheet;
    //using OnBoardingSystem.Data.EF.Models;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using OnBoardingSystem.Data.Abstractions.Exceptions;

	/// <inheritdoc />
	public class AppOnboardingAdminloginDirector : IAppOnboardingAdminloginDirector
	{
		public static string Salt;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly JWTTokenService jwtTokenServices;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly EncryptionDecryptionService decryptionService;
		private readonly UtilityService utilityService;
		private readonly JWT _jwtSetting;

		/// <summary>
		/// Initializes a new instance of the <see cref="AppOnboardingAdminloginDirector"/> class.
		/// </summary>
		/// <param name="mapper">Automapper.</param>
		/// <param name="unitOfWork">Unit of Work.</param>

		public AppOnboardingAdminloginDirector(UtilityService _utilityService, IOptions<JWT> options, EncryptionDecryptionService _encryptionDecryptionService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork _unitOfWork, JWTTokenService jwtTokenServices)
		{
			this.mapper = mapper;
			this.unitOfWork = _unitOfWork;
			this.jwtTokenServices = jwtTokenServices;
			_httpContextAccessor = httpContextAccessor;
			this.decryptionService = _encryptionDecryptionService;
			this.utilityService = _utilityService;
			_jwtSetting = options.Value;
		}

		/// <inheritdoc />
		public virtual async Task<string> GetSalt(CancellationToken cancellationToken)
		{
			var response = this.utilityService.RandomString(10, true);
			//_httpContextAccessor.HttpContext.Session.SetString(Abstractions.Models.SessionVariable.Salt, "asasas");
			Salt = response;
			return response;
		}

		/// <inheritdoc />
		public virtual async Task<string> GetCaptcha(CancellationToken cancellationToken)
		{
			string captcha;
			var response = this.utilityService.RandomString(6, true);
			return response;
		}
		/*
		public virtual async Task<Session> CheckAdminLoginAsync(Abstractions.Models.UserInfo admincredentials, CancellationToken cancellationToken)
		{
		    var token = new Token();
		    var session=new Session();
		    var details= await this.unitOfWork.AppLoginDetailRepository.FindByAsync(x => x.UserId == admincredentials.Username, cancellationToken).ConfigureAwait(false);
		    var Applogindetails = this.mapper.Map<Abstractions.Models.AppAdminLoginDetails>(details);
		    string password = admincredentials.Password;
		    //var abc = _httpContextAccessor.HttpContext.Session.GetString(Abstractions.Models.SessionVariable.Salt);
		    string salt = Salt;
		    string saltedpassword = decryptionService.getHashSha256(Applogindetails.Password+Salt);
		    if (password == saltedpassword && details.UserId== admincredentials.Username)
		    {
		        var param = new SqlParameter[]
		        {
		        new SqlParameter()
		        {
		            ParameterName = "@UserId",
		            SqlDbType = System.Data.SqlDbType.VarChar,
		            Value = admincredentials.Username,
		        },
		        new SqlParameter()
		        {
		            ParameterName = "@password",
		            SqlDbType = System.Data.SqlDbType.VarChar,
		            Value =Applogindetails.Password,
		        },
		        };

		        var storedProcedureName = $"{"USP_OnboardingAdminLogin"}  @UserId,@password";
		        var result = await this.unitOfWork.AppLoginDetailRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
		        var appLogInDetailsList = await this.unitOfWork.AppLoginDetailRepository.FindAllByAsync(x => x.UserId == admincredentials.Username, cancellationToken).ConfigureAwait(false);
		        var appUserRoleMappinglist = await this.unitOfWork.AppUserRoleMappingRepository.GetAllAsync(cancellationToken);
		        var appRoleModulePermission = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken);

		        var userRoles = from app_UserRoleMapping in appUserRoleMappinglist
		                        join app_RoleModule in appRoleModulePermission on app_UserRoleMapping.RoleId equals app_RoleModule.RoleId
		                        where app_UserRoleMapping.UserId == admincredentials.Username
		                        select new Abstractions.Models.AppUserRoleMapping
		                        {
		                            UserId = app_UserRoleMapping.UserId,
		                            RoleId = app_UserRoleMapping.RoleId,
		                            IsReadOnly = app_UserRoleMapping.IsReadOnly,
		                            IsActive = app_UserRoleMapping.IsActive,
		                            ModuleId = app_RoleModule.ModuleId,
		                        };

		        var appUserRoleMapping = this.mapper.Map<List<Abstractions.Models.AppUserRoleMapping>>(appUserRoleMappinglist);
		        var appRoleModulePermissions = this.mapper.Map<List<Abstractions.Models.AppRoleModulePermission>>(appRoleModulePermission);
		        var results = this.mapper.Map<List<Abstractions.Models.AppAdminLoginDetails>>(appLogInDetailsList);
		        if (result > 0)
		        {
		            token = jwtTokenServices.TokenGenerate(admincredentials.Username, Applogindetails.Password,"Login");
		            UpdateRefreshToken(token.RefreshToken, token.CreatedToken, admincredentials.Username,"login", default);
		            session.adminLogIn = results;
		            session.token = token;
		            session.AppUserRoleMappingList = userRoles.ToList();
		        }
		        return session;
		    }
		    else
		    {
		        return session;
		    }
		}
		*/
		public virtual async Task<Session> CheckAdminLoginAsync(Abstractions.Models.UserInfo admincredentials, CancellationToken cancellationToken)
		{
			var token = new Token();
			var session = new Session();
			var details = await this.unitOfWork.AppLoginDetailRepository.FindByAsync(x => x.UserId == admincredentials.Username, cancellationToken).ConfigureAwait(false);
			var Applogindetails = this.mapper.Map<Abstractions.Models.AppAdminLoginDetails>(details);
			string password = admincredentials.Password;
			//var abc = _httpContextAccessor.HttpContext.Session.GetString(Abstractions.Models.SessionVariable.Salt);
			string salt = Salt;
			string saltedpassword = decryptionService.getHashSha256(Applogindetails.Password + Salt);
			if (password == saltedpassword && details.UserId == admincredentials.Username)
			{
				details.LastSuccessfulLoginTime = DateTime.Now;
				await this.unitOfWork.AppLoginDetailRepository.UpdateAsync(details, cancellationToken).ConfigureAwait(false);
				var result = await this.unitOfWork.AppLoginDetailRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
				var appLogInDetailsList = await this.unitOfWork.AppLoginDetailRepository.FindAllByAsync(x => x.UserId == admincredentials.Username, cancellationToken).ConfigureAwait(false);
				var appUserRoleMappinglist = await this.unitOfWork.AppUserRoleMappingRepository.GetAllAsync(cancellationToken);
				var appRoleModulePermission = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken);

				var userRoles = from app_UserRoleMapping in appUserRoleMappinglist
								join app_RoleModule in appRoleModulePermission on app_UserRoleMapping.RoleId equals app_RoleModule.RoleId
								where app_UserRoleMapping.UserId == admincredentials.Username
								select new Abstractions.Models.AppUserRoleMapping
								{
									UserId = app_UserRoleMapping.UserId,
									RoleId = app_UserRoleMapping.RoleId,
									IsReadOnly = app_UserRoleMapping.IsReadOnly,
									IsActive = app_UserRoleMapping.IsActive,
									ModuleId = app_RoleModule.ModuleId,
								};

				var appUserRoleMapping = this.mapper.Map<List<Abstractions.Models.AppUserRoleMapping>>(appUserRoleMappinglist);
				var appRoleModulePermissions = this.mapper.Map<List<Abstractions.Models.AppRoleModulePermission>>(appRoleModulePermission);
				var results = this.mapper.Map<List<Abstractions.Models.AppAdminLoginDetails>>(appLogInDetailsList);
				if (result > 0)
				{
					token = jwtTokenServices.TokenGenerate(admincredentials.Username, Applogindetails.Password, "Login");
					UpdateRefreshToken(token.RefreshToken, token.CreatedToken, admincredentials.Username, "login", default);
					session.adminLogIn = results;
					session.token = token;
					session.AppUserRoleMappingList = userRoles.ToList();
				}
				return session;
			}
			else
			{
				return session;
			}
		}

		/// <inheritdoc/>
		public virtual async Task<int> SaveSignUpDetailsAsync(Abstractions.Models.SignUp signUpData, CancellationToken cancellationToken)
		{
			try
			{
                int result = 0;
                AppAdminLoginDetails userLoginDetail = new AppAdminLoginDetails();
                userLoginDetail.UserId = signUpData.UserID;
                userLoginDetail.Password = signUpData.Password;
                userLoginDetail.RequestNo = signUpData.RequestNo;
                userLoginDetail.LastSuccessfulLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                userLoginDetail.LastLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                userLoginDetail.UserName = signUpData.UserName;
                var loginDetail = this.mapper.Map<EF.AppLoginDetails>(userLoginDetail);

                AppUserRoleMapping userRoleMapping = new AppUserRoleMapping();
                userRoleMapping.RoleId = "USER";
                userRoleMapping.UserId = signUpData.UserID;
                userRoleMapping.IsReadOnly = "Y";
                userRoleMapping.IsActive = "Y";
                var efUserRoleMapping = this.mapper.Map<EFModel.AppUserRoleMapping>(userRoleMapping);
                var mdActivityType = await this.unitOfWork.MdActivityTypeRepository.FindAllByAsync(x => x.ActivityGroup == "onboarding", cancellationToken).ConfigureAwait(false);
                List<AppProjectActivity> activityList = new List<AppProjectActivity>();
                AppProjectActivity activity;
                foreach (var mdActivity in mdActivityType)
                {
                    activity = new AppProjectActivity();
                    activity.ActivityParentRefId =signUpData.RequestNo;
                    activity.SubmitTime = DateTime.Now;
                    activity.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    activity.ActivityId = Convert.ToInt32(mdActivity.ActivityId);
                    switch (Convert.ToInt32(mdActivity.ActivityId))
                    {
                        case (int)Enumactivity.OnboardingRequest:
                            activity.Status = MdStatusEnum.Completed.Value.ToString();
                            break;
                        case (int)Enumactivity.OnboardingDetails:
                            activity.Status = MdStatusEnum.Completed.Value.ToString();
                            break;
                        case (int)Enumactivity.MOU:
                            activity.Status = MdStatusEnum.Pending.Value.ToString();
                            break;
                        case (int)Enumactivity.ProposalAndPI:
                            activity.Status = MdStatusEnum.Pending.Value.ToString();
                            break;
                        case (int)Enumactivity.Payment:
                            activity.Status = MdStatusEnum.Pending.Value.ToString();
                            break;
                        case (int)Enumactivity.SignOffAndDataHandover:
                            activity.Status = MdStatusEnum.Pending.Value.ToString();
                            break;
                        case (int)Enumactivity.UC:
                            activity.Status = MdStatusEnum.Pending.Value.ToString();
                            break;
                    }
                    activityList.Add(activity);
                }
                var projectActivity = this.mapper.Map<List<Data.EF.Models.AppProjectActivity>>(activityList);
                using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        await this.unitOfWork.AppUserRoleMappingRepository.InsertAsync(efUserRoleMapping, cancellationToken).ConfigureAwait(false);
                        await this.unitOfWork.AppLoginDetailRepository.InsertAsync(loginDetail, cancellationToken).ConfigureAwait(false);
                        await this.unitOfWork.AppProjectActivityRepository.InsertAsync(projectActivity, cancellationToken).ConfigureAwait(false);
                        result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
                if (result > 0)
                {
                    string isdCode = "91";
                    string smsMessage = "";                    
                    var userCredentials = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == signUpData.RequestNo && x.RoleId == "11", cancellationToken).ConfigureAwait(false); ;
                    var emaiTemplateApproval = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0016", cancellationToken).ConfigureAwait(false);
                    var coordinatorData = await this.unitOfWork.AppOnboardingRequestRepository.FindByAsync(x => x.RequestNo == signUpData.RequestNo, cancellationToken).ConfigureAwait(false);
                    string userEmail = decryptionService.Decryption(userCredentials.EmailId);
                    string mobile = decryptionService.Decryption(userCredentials.MobileNo);
                    string Subject = emaiTemplateApproval.MessageSubject;
                    string Body = emaiTemplateApproval.MessageTemplate;
                    Body = Body.Replace("#USER#", signUpData.UserName);
                    Body = Body.Replace("#USERCREATIONID#", signUpData.UserID);
                    MailRequest request = new MailRequest();
                    request.ToEmail = userEmail;
                    request.Subject = Subject;
                    request.Body = Body;
                    request.CCMail = coordinatorData.ContactPerson;
                    var mail = utilityService.SendEmailAsync(request);
                }
                //string TemplateId = smsTemplate.RegisteredTemplateId;
                //string message = smsTemplate.MessageTemplate;
                //smsMessage = message.Replace("#USER#", appOnboardingResponse.cordinatorName);
                //smsMessage = smsMessage.Replace("#REQNO#", appOnboardingResponse.RequestNo);
                //smsMessage = smsMessage.Replace("#STATUS#", "Initiated");
                //var sms = sMSService.SendStatusSmsAsync(isdCode + decryptedCoordinatorMobile, TemplateId, smsMessage);  

                return result;
            }
			catch (Exception ex)
			{

				throw ex;
			}
			
		}
		/// <inheritdoc/>
		public virtual async Task<int> UpdateRefreshToken(string refreshToken, string token, string username, string Mode, CancellationToken cancellationToken)
		{
			//if (mddistrict.Id == "0")
			//{
			//    throw new ArgumentException(nameof(mddistrict.Id));
			//}
			var param = new SqlParameter[]
			{
				new SqlParameter()
				{
					ParameterName = "@RefreshToken",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = refreshToken,
				},
				new SqlParameter()
				{
					ParameterName = "@UserId",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = username,
				},
				new SqlParameter()
				{
					ParameterName = "@token",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = token,
				},
				new SqlParameter()
				{
					ParameterName = "@Mode",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = Mode,
				},
			};
			using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "EXEC " + "RefreshToken @UserId,@RefreshToken,@token,@Mode";
				foreach (var parameterDefinition in param)
				{
					command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
				}

				using (var reader = command.ExecuteReader())
				{ }
				return 1;
			}
		}

		/// <inheritdoc/>
		public virtual async Task<bool> CheckUserIdAvailibity(string userID, CancellationToken cancellationToken)
		{

			var param = new SqlParameter[]
			{
				new SqlParameter()
				{
					ParameterName = "@UserID",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = userID,
				},
				new SqlParameter()
				{
					ParameterName = "@IsError",
					SqlDbType = System.Data.SqlDbType.Bit,
					Direction = System.Data.ParameterDirection.Output,
				},
			};

			var storedProcedureName = $"{"USP_CheckUserIdAvailibilty"}  @UserID,@IsError output";
			int result = await this.unitOfWork.AppLoginDetailRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
			bool s = (bool)param[1].Value;
			return s;
		}

		public virtual async Task<List<UserRole>> GetRoleByIdAsync(string userID, CancellationToken cancellationToken)
		{
            string doc = "";
            var appLoginDetail = await this.unitOfWork.AppLoginDetailRepository.FindByAsync(x => x.UserId == userID, cancellationToken).ConfigureAwait(false);
            Data.EF.Models.AppDocumentUploadedDetail doclist = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.ModuleRefId == userID, cancellationToken).ConfigureAwait(false);
            if (doclist == null)
            {
                doc = "";
            }
            else
            {
                doc = doclist.DocContent;
            }
           
            var _userrole = new UserRole();
			_userrole.username = appLoginDetail.UserName;
			_userrole.userrole = "";
            _userrole.DocContent = doc;
            _userrole.RequestNo = appLoginDetail.RequestNo;
            _userrole.userId = appLoginDetail.UserId;
            _userrole.authenticationType = appLoginDetail.AuthenticationType;
            _userrole.securityQuestionId = appLoginDetail.SecurityQuestionId;
            _userrole.securityAnswer = appLoginDetail.SecurityAnswer;
            _userrole.designation = appLoginDetail.Designation;
            _userrole.emailId = appLoginDetail.EmailId;
            _userrole.mobileNo = appLoginDetail.MobileNo;
            _userrole.Password = appLoginDetail.Password;

            List<Abstractions.Models.UserRole> userrole = new List<Abstractions.Models.UserRole>();
			userrole.Add(_userrole);
			return userrole;
		}

		public virtual async Task<string> SendForgotPasswordMail(string requestId, CancellationToken cancellationToken)
		{
			string response;			
			string decryptedRequestId = decryptionService.Decryption(requestId);
			var contactPersonDetail = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == decryptedRequestId && x.RoleId == "11", cancellationToken).ConfigureAwait(false);
			response = contactPersonDetail.EmailId;
			return response;
		}

		public virtual async Task<string> ForgotPasswordMail(ResendPassword resendPassword, CancellationToken cancellationToken)
		{
			string response;
			string decryptedRequestId = decryptionService.Decryption(resendPassword.EncryptedRequestId);
			var contactPersonDetail = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == decryptedRequestId && x.RoleId == "11", cancellationToken).ConfigureAwait(false);
			var emaiTemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0014", cancellationToken).ConfigureAwait(false);
			if (resendPassword.EncryptedRequestId.Contains("=="))
			{
				string reqId = resendPassword.EncryptedRequestId.Replace("==", "@tn");
				string Subject = emaiTemplate.MessageSubject;
				string Body = emaiTemplate.MessageTemplate;
				Body = Body.Replace("#USER#", contactPersonDetail.Name);
				Body = Body.Replace("#REQNO#", reqId);
				MailRequest request = new MailRequest();
				request.ToEmail = resendPassword.DecryptedEmail;
				request.Subject = Subject;
				request.Body = Body;
				var mail = utilityService.SendEmailAsync(request);
			}
			else
			{
				string Subject = emaiTemplate.MessageSubject;
				string Body = emaiTemplate.MessageTemplate;
				Body = Body.Replace("#USER#", contactPersonDetail.Name);
				Body = Body.Replace("#REQNO#", resendPassword.EncryptedRequestId);
				MailRequest request = new MailRequest();
				request.ToEmail = resendPassword.DecryptedEmail;
				request.Subject = Subject;
				request.Body = Body;
				var mail = utilityService.SendEmailAsync(request);
			}
			response = "Mail has been sent to " + resendPassword.DecryptedEmail + " Please check the corresponding mail";
			return response;
		}

		public virtual async Task<int> UpdateAsync(ResendPassword confirmPassword, CancellationToken cancellationToken)
		{
			string response;
			string decryptedRequestId = decryptionService.Decryption(confirmPassword.RequestNumber);
			Data.EF.Models.AppLoginDetails data = await unitOfWork.AppLoginDetailRepository.FindByAsync(e => e.RequestNo == decryptedRequestId, cancellationToken);
			data.PasswordHistory1 = data.Password;
			data.Password = confirmPassword.NewPassword;
			data.LastPasswordResetTime = DateTime.Now;
			data.LastPasswordResetIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			await unitOfWork.AppLoginDetailRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);

			return await unitOfWork.CommitAsync(cancellationToken);
		}

		public virtual async Task<int> UpdateByUseridAsync(ResendPassword confirmPassword, CancellationToken cancellationToken)
		{
			string response;
			string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(confirmPassword.userid));
			Data.EF.Models.AppLoginDetails data = await unitOfWork.AppLoginDetailRepository.FindByAsync(e => e.UserId == decryptedUserId, cancellationToken);
			data.PasswordHistory1 = data.Password;
			data.Password = confirmPassword.NewPassword;
			data.LastPasswordResetTime = DateTime.Now;
			data.LastPasswordResetIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			if (data.PasswordHistory1 == confirmPassword.oldPassword)
			{
				await unitOfWork.AppLoginDetailRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);
			}
			return await unitOfWork.CommitAsync(cancellationToken);
		}

		public virtual async Task<string> Logout(logout logout, CancellationToken cancellationToken)
		{
			var refreshtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
			UpdateRefreshToken(logout.refreshToken, logout.token, logout.userId, "logout", default);
			return "Logout Successfully";
		}

		public virtual async Task<string> Token(string token, string username, CancellationToken cancellationToken)
		{
			var appRefreshToken = await this.unitOfWork.AppLoginDetailRepository.FindAllByAsync(x => x.UserToken == token && x.UserName == username, cancellationToken).ConfigureAwait(false);
			return appRefreshToken.ToString();
		}

        public virtual async Task<string> GetIPAddress(CancellationToken cancellationToken)
        {
            var refreshtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip.ToString();
        }
        public virtual async Task<int> SaveUserDetailsAsync(Abstractions.Models.SignUp signUpData, CancellationToken cancellationToken)
        {
            int result = 0;
            AppAdminLoginDetails userLoginDetail = new AppAdminLoginDetails();
            userLoginDetail.UserId = signUpData.UserID;
            userLoginDetail.Password = signUpData.Password;
            userLoginDetail.RequestNo = signUpData.RequestNo;
            userLoginDetail.LastSuccessfulLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            userLoginDetail.LastLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            userLoginDetail.UserName = signUpData.UserName;
            userLoginDetail.Designation = signUpData.Designation;
            userLoginDetail.EmailId = signUpData.EmailId;
            userLoginDetail.MobileNo = signUpData.MobileNo;
            userLoginDetail.SecurityQuestionId = signUpData.SecurityQuestionId;
            userLoginDetail.SecurityAnswer = signUpData.SecurityAnswer;
            userLoginDetail.AuthenticationType = signUpData.AuthenticationType;
            //=================================================================
            userLoginDetail.IsPasswordChanged = "N";
            userLoginDetail.IsActive = "Y";
            userLoginDetail.LastLoginTime = DateTime.Now;
            var loginDetail = this.mapper.Map<EF.AppLoginDetails>(userLoginDetail);

            //Data.Abstractions.Models.AppUserRoleMapping userRoleMapping = new Data.Abstractions.Models.AppUserRoleMapping();
            //userRoleMapping.RoleId = "USER";
            //userRoleMapping.UserId = signUpData.UserID;
            //userRoleMapping.IsReadOnly = "Y";
            //userRoleMapping.IsActive = "Y";
            //var efUserRoleMapping = this.mapper.Map<EFModel.AppUserRoleMapping>(userRoleMapping);
            Abstractions.Models.AppDocumentUploadedDetail appdoc = new Data.Abstractions.Models.AppDocumentUploadedDetail();
            appdoc.Activityid = "501";
            appdoc.ModuleRefId = signUpData.UserID;
            appdoc.DocContent = signUpData.photopath;
            appdoc.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            appdoc.SubTime = null;
            appdoc.DocType = "";
            appdoc.CreatedBy = signUpData.UserID;
            var efdoc = this.mapper.Map<EFModel.AppDocumentUploadedDetail>(appdoc);
            try
            {
                await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(efdoc, cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //try
            //{
            //    await this.unitOfWork.AppUserRoleMappingRepository.InsertAsync(efUserRoleMapping, cancellationToken).ConfigureAwait(false);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            try
            {
                await this.unitOfWork.AppLoginDetailRepository.InsertAsync(loginDetail, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public virtual async Task<List<Abstractions.Models.SignUp>> GetAllAsync(CancellationToken cancellationToken)
        {
            var userlist = this.unitOfWork.AppLoginDetailRepository.GetAll();
            var userlistzmstmode = this.unitOfWork.ZmstAuthenticationModeRepository.GetAll();
            var userlistzmstsequrityq = this.unitOfWork.ZmstSecurityQuestionRepository.GetAll();
            var appuserlist = this.unitOfWork.AppUserRoleMappingRepository.GetAll();
            // var doclist = this.unitOfWork.AppDocumentUploadedDetailRepository.GetAll();

            var query = from user in userlist
                        join userzmstsequrity in userlistzmstsequrityq on user.SecurityQuestionId equals userzmstsequrity.SecurityQuesId into AS
                        from ls in AS.DefaultIfEmpty()
                        join userlistzmst in userlistzmstmode on user.AuthenticationType equals userlistzmst.AuthCode into temp
                        from m in temp.DefaultIfEmpty()
                        join appulist in appuserlist on user.UserId equals appulist.UserId into temp1
                        from t in temp1.DefaultIfEmpty()
                        where ((t == null || t.RoleId.ToUpper() != "USER") && user.UserName != null)
                        select new Abstractions.Models.SignUp
                        {
                            UserName = user.UserName,
                            Designation = user.Designation,
                            EmailId = user.EmailId,
                            MobileNo = user.MobileNo,
                            UserID = user.UserId,
                            Password = user.Password,
                            SecurityQuestionId = user.SecurityQuestionId,
                            SecurityQuesdesc = ls.SecurityQues,
                            SecurityAnswer = user.SecurityAnswer,
                            AuthenticationType = user.AuthenticationType,
                            AuthModedesc = m.Authmode,
                         };

            return query.ToList();
        }

        public virtual async Task<List<Abstractions.Models.SignUp>> BoardUserGetAllAsync(CancellationToken cancellationToken)
        {
            var userlist = this.unitOfWork.AppLoginDetailRepository.GetAll();
            var userlistzmstmode = this.unitOfWork.ZmstAuthenticationModeRepository.GetAll();
            var userlistzmstsequrityq = this.unitOfWork.ZmstSecurityQuestionRepository.GetAll();
            var appuserlist = this.unitOfWork.AppUserRoleMappingRepository.GetAll();
            // var doclist = this.unitOfWork.AppDocumentUploadedDetailRepository.GetAll();

            var query = from user in userlist
                        join userzmstsequrity in userlistzmstsequrityq on user.SecurityQuestionId equals userzmstsequrity.SecurityQuesId into AS
                        from ls in AS.DefaultIfEmpty()
                        join userlistzmst in userlistzmstmode on user.AuthenticationType equals userlistzmst.AuthCode into temp
                        from m in temp.DefaultIfEmpty()
                        join appulist in appuserlist on user.UserId equals appulist.UserId into temp1
                        from t in temp1.DefaultIfEmpty()
                        where ((t == null || t.RoleId.ToUpper() == "USER") && user.UserName != null)
                        select new Abstractions.Models.SignUp
                        {
                            UserName = user.UserName,
                            Designation = user.Designation,
                            EmailId = user.EmailId,
                            MobileNo = user.MobileNo,
                            UserID = user.UserId,
                            Password = user.Password,
                            SecurityQuestionId = user.SecurityQuestionId,
                            SecurityQuesdesc = ls.SecurityQues,
                            SecurityAnswer = user.SecurityAnswer,
                            AuthenticationType = user.AuthenticationType,
                            AuthModedesc = m.Authmode,
                        };

            return query.ToList();
        }

        public virtual async Task<List<Abstractions.Models.SignUp>> GetBoardUserdetailByIdAsync(string id, CancellationToken cancellationToken)
        {
            var userlist = this.unitOfWork.AppLoginDetailRepository.GetAll();
            var userlistzmstmode = this.unitOfWork.ZmstAuthenticationModeRepository.GetAll();
            var userlistzmstsequrityq = this.unitOfWork.ZmstSecurityQuestionRepository.GetAll();
            var appuserlist = this.unitOfWork.AppUserRoleMappingRepository.GetAll();
            // var doclist = this.unitOfWork.AppDocumentUploadedDetailRepository.GetAll();

            var query = from user in userlist
                        join userzmstsequrity in userlistzmstsequrityq on user.SecurityQuestionId equals userzmstsequrity.SecurityQuesId into AS
                        from ls in AS.DefaultIfEmpty()
                        join userlistzmst in userlistzmstmode on user.AuthenticationType equals userlistzmst.AuthCode into temp
                        from m in temp.DefaultIfEmpty()
                        join appulist in appuserlist on user.UserId equals appulist.UserId into temp1
                        from t in temp1.DefaultIfEmpty()
                        where ((t == null || t.RoleId.ToUpper() == "USER") && user.UserName != null && user.UserId == id)
                        select new Abstractions.Models.SignUp
                        {
                            UserName = user.UserName,
                            Designation = user.Designation,
                            EmailId = user.EmailId,
                            MobileNo = user.MobileNo,
                            UserID = user.UserId,
                            Password = user.Password,
                            SecurityQuestionId = user.SecurityQuestionId,
                            SecurityQuesdesc = ls.SecurityQues,
                            SecurityAnswer = user.SecurityAnswer,
                            AuthenticationType = user.AuthenticationType,
                            AuthModedesc = m.Authmode,
                        };

            return query.ToList();
        }
        public virtual async Task<List<Abstractions.Models.SignUp>> GetDocumentByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                string doc = "";
                var userlist = await this.unitOfWork.AppLoginDetailRepository.GetAllAsync(cancellationToken);
                var userlistzmstmode = await this.unitOfWork.ZmstAuthenticationModeRepository.GetAllAsync(cancellationToken);
                var userlistzmstsequrityq = await this.unitOfWork.ZmstSecurityQuestionRepository.GetAllAsync(cancellationToken);
                Data.EF.Models.AppDocumentUploadedDetail doclist = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.ModuleRefId == id, cancellationToken);
                if (doclist == null)
                {
                    doc = "";
                }
                else
                {
                    doc = doclist.DocContent;
                }

                var query = from user in userlist
                            join userzmstsequrity in userlistzmstsequrityq on user.SecurityQuestionId equals userzmstsequrity.SecurityQuesId into AS
                            from ls in AS.DefaultIfEmpty()
                            join userlistzmst in userlistzmstmode on user.AuthenticationType equals userlistzmst.AuthCode into temp
                            from m in temp.DefaultIfEmpty()
                            where user.UserId == id

                            select new Abstractions.Models.SignUp
                            {
                                UserName = string.IsNullOrEmpty(user.UserName) ? "" : user.UserName,
                                Designation = string.IsNullOrEmpty(user.Designation) ? "" : user.Designation,
                                EmailId = string.IsNullOrEmpty(user.EmailId) ? "" : user.EmailId,
                                MobileNo = string.IsNullOrEmpty(user.MobileNo) ? "" : user.MobileNo,
                                UserID = string.IsNullOrEmpty(user.UserId) ? "" : user.UserId,
                                Password = string.IsNullOrEmpty(user.Password) ? "" : user.Password,
                                SecurityQuestionId = string.IsNullOrEmpty(user.SecurityQuestionId) ? "" : user.SecurityQuestionId,
                                SecurityQuesdesc = (ls==null) ? "" : ls.SecurityQues,
                                SecurityAnswer = string.IsNullOrEmpty(user.SecurityAnswer) ? "" : user.SecurityAnswer,
                                AuthenticationType = string.IsNullOrEmpty(user.AuthenticationType) ? "" : user.AuthenticationType,
                                AuthModedesc = (m == null) ? "" : m.Authmode,
                                photopath = doc,
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<int> UpdateUserDetailsAsync(Abstractions.Models.SignUp signUpData, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                Data.EF.Models.AppDocumentUploadedDetail efdoc = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == signUpData.UserID, cancellationToken).ConfigureAwait(false);
                if (efdoc != null)
                {
                    efdoc.Activityid = "501";
                    efdoc.DocContent = signUpData.photopath;
                    efdoc.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    efdoc.SubTime = null;
                    efdoc.DocType = "";
                    efdoc.CreatedBy = signUpData.UserID;
                    efdoc.ModuleRefId = signUpData.UserID;
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(efdoc, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    Abstractions.Models.AppDocumentUploadedDetail appdoc = new Abstractions.Models.AppDocumentUploadedDetail();
                    appdoc.Activityid = "501";
                    appdoc.ModuleRefId = signUpData.UserID;
                    appdoc.DocContent = signUpData.photopath;
                    appdoc.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    appdoc.SubTime = null;
                    appdoc.DocType = "";
                    appdoc.CreatedBy = signUpData.UserID;
                    // var efdoc1 = this.mapper.Map<EFModel.AppDocumentUploadedDetail>(appdoc);

                    var AppDocuments = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(appdoc);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(AppDocuments, cancellationToken).ConfigureAwait(false);
                   // await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                }

                await this.unitOfWork.AppDocumentUploadedDetailRepository.CommitAsync(cancellationToken).ConfigureAwait(false);

                //Data.EF.Models.AppUserRoleMapping efUserRoleMapping = await this.unitOfWork.AppUserRoleMappingRepository.FindByAsync(x => x.UserId == signUpData.UserID && x.RoleId == "USER", cancellationToken).ConfigureAwait(false);
                //if (efUserRoleMapping != null)
                //{
                //    efUserRoleMapping.RoleId = "USER";
                //    efUserRoleMapping.IsReadOnly = "Y";
                //    efUserRoleMapping.IsActive = "Y";
                //    await this.unitOfWork.AppUserRoleMappingRepository.UpdateAsync(efUserRoleMapping, cancellationToken).ConfigureAwait(false);

                //}
                //else
                //{
                //    Abstractions.Models.AppUserRoleMapping approle = new Abstractions.Models.AppUserRoleMapping();
                //    approle.RoleId = "USER";
                //    approle.IsReadOnly = "Y";
                //    approle.IsActive = "Y";
                //    approle.UserId = signUpData.UserID;
                //    var Approlemap = this.mapper.Map<Data.EF.Models.AppUserRoleMapping>(approle);
                //    await this.unitOfWork.AppUserRoleMappingRepository.InsertAsync(Approlemap, cancellationToken).ConfigureAwait(false);
                //}

                //await this.unitOfWork.AppUserRoleMappingRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
                //await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                Data.EF.Models.AppLoginDetails loginDetail = await this.unitOfWork.AppLoginDetailRepository.FindByAsync(x => x.UserId == signUpData.UserID, cancellationToken).ConfigureAwait(false);
                if (loginDetail != null)
                {
                    loginDetail.Password = signUpData.Password;
                    loginDetail.RequestNo = signUpData.RequestNo;
                    loginDetail.LastSuccessfulLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    loginDetail.LastLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    loginDetail.UserName = signUpData.UserName;
                    loginDetail.Designation = signUpData.Designation;
                    loginDetail.EmailId = signUpData.EmailId;
                    loginDetail.MobileNo = signUpData.MobileNo;
                    loginDetail.SecurityQuestionId = signUpData.SecurityQuestionId;
                    loginDetail.SecurityAnswer = signUpData.SecurityAnswer;
                    loginDetail.AuthenticationType = signUpData.AuthenticationType;
                    loginDetail.IsPasswordChanged = "Y";
                    loginDetail.IsActive = "Y";
                    loginDetail.LastLoginTime = DateTime.Now;
                    loginDetail.LastPasswordResetIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    loginDetail.LastPasswordResetTime = DateTime.Now;
                    loginDetail.LastPasswordChangeIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    loginDetail.LastPasswordChangeTime = DateTime.Now;
                }

                await this.unitOfWork.AppLoginDetailRepository.UpdateAsync(loginDetail, cancellationToken).ConfigureAwait(false);

                result = await this.unitOfWork.AppLoginDetailRepository.CommitAsync(cancellationToken).ConfigureAwait(false);

              //  result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            int result = 0;
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == Id, cancellationToken).ConfigureAwait(false);
            //var entity1 = await this.unitOfWork.AppUserRoleMappingRepository.FindByAsync(x => x.UserId == Id, cancellationToken).ConfigureAwait(false);
            var entity2 = await this.unitOfWork.AppLoginDetailRepository.FindByAsync(x => x.UserId == Id, cancellationToken).ConfigureAwait(false);

            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    if (entity != null)
                    {
                        await this.unitOfWork.AppDocumentUploadedDetailRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
                    }
                   // await this.unitOfWork.AppUserRoleMappingRepository.DeleteAsync(entity1, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.AppLoginDetailRepository.DeleteAsync(entity2, cancellationToken).ConfigureAwait(false);
                    result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }

            return result;
          }

        public virtual async Task<List<AppUserRoleMapping>> CheckExistUserIdAsync(string userID, CancellationToken cancellationToken)
        {
            string doc = "";
            var appUserDetail = await this.unitOfWork.AppUserRoleMappingRepository.FindByAsync(x => x.UserId == userID && x.RoleId.ToUpper() == "SUPERADMIN", cancellationToken).ConfigureAwait(false);
            
            var _userrole = new AppUserRoleMapping();
            if(appUserDetail!=null)
            { 
            _userrole.UserId = appUserDetail.UserId;
            _userrole.RoleId = appUserDetail.RoleId.ToUpper();
            _userrole.IsReadOnly = appUserDetail.IsReadOnly;
            _userrole.IsActive = appUserDetail.IsActive;
            }
            List<Abstractions.Models.AppUserRoleMapping> userrole = new List<Abstractions.Models.AppUserRoleMapping>();
            userrole.Add(_userrole);

            return userrole;
        }
    }
}

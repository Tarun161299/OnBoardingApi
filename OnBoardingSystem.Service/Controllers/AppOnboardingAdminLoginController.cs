//-----------------------------------------------------------------------
// <copyright file="AppOnboardingAdminLoginController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    //using OnBoardingSystem.Data.EF.Models;
    using SignUp = Data.Abstractions.Models.SignUp;
    using Serilog;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    /// <summary>
    /// AppOnboardingAdminLoginController.
    /// </summary>
    public class AppOnboardingAdminLoginController : ControllerBase
    {
        private readonly IAppOnboardingAdminloginDirector iappOnboardingAdminlogin;
        // private readonly IHttpContextAccessor _httpContextAccessor;
        //public readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingAdminLoginController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingAdminlogin">iAppOnboardingAdminlogin.</param>
        public AppOnboardingAdminLoginController(IAppOnboardingAdminloginDirector iAppOnboardingAdminlogin)
        {
            this.iappOnboardingAdminlogin = iAppOnboardingAdminlogin;
            // _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// Onboarding Admin Login.
        /// </summary>
        /// <param name="adminCredentials">adminCredentials.</param>
        /// <returns>AppAdminLoginDetails.</returns>

        ///[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserInfo), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Session>> USP_OnboardingAdminLoginAsync([FromBody] UserInfo adminCredentials)
        {
            //byte[] sal1t = _session.Get("Salt");
            var response = await iappOnboardingAdminlogin.CheckAdminLoginAsync(adminCredentials, default).ConfigureAwait(false);
            return response;

        }

        /// <summary>
        /// Save SignUp Detail.
        /// </summary>
        /// <param name="signUp">signUp.</param>
        /// <returns>SignUp.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SaveSignUpAsync([FromBody] SignUp signUp)
        {
            var response = await iappOnboardingAdminlogin.SaveSignUpDetailsAsync(signUp, default).ConfigureAwait(false);
            string status;
            if (response > 0)
            {
                status = "\"Data Stored Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        /// <summary>
        /// Check userID Availibility.
        /// </summary>
        /// <param name="userID">Check UserID Availibility.</param>
        /// <returns>get by UserID.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> CheckUserIDAvailibility(string userID)
        {
            var response = await iappOnboardingAdminlogin.CheckUserIdAvailibity(userID, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// userId.
        /// </summary>
        /// <param name="userId">ministriesId.</param>
        /// /// <param name="userId">requestid.</param>
        /// <returns>get by id.</returns>

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserRole>> GetByUserIdAsync(string userId)
        {
            var response = await iappOnboardingAdminlogin.GetRoleByIdAsync(userId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SendForgotPasswordMail(string requestId)
        {
            var response = await iappOnboardingAdminlogin.SendForgotPasswordMail(requestId, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SendPasswordMail([FromBody] ResendPassword resendPassword)
        {
            var response = await iappOnboardingAdminlogin.ForgotPasswordMail(resendPassword, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdatePasswordAsync([FromBody] ResendPassword confirmPassword)
        {
            if (confirmPassword == null)
            {
                return BadRequest(nameof(confirmPassword));
            }

            try
            {
                var response = await iappOnboardingAdminlogin.UpdateAsync(confirmPassword, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Serilog.Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdatePasswordByUaseridAsync([FromBody] ResendPassword confirmPassword)
        {
            if (confirmPassword == null || confirmPassword.NewPassword != confirmPassword.ConfirmPassword)
            {
                return BadRequest(nameof(confirmPassword));
            }
            try
            {
                var response = await iappOnboardingAdminlogin.UpdateByUseridAsync(confirmPassword, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Serilog.Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetSalt()
        {
            var response = await iappOnboardingAdminlogin.GetSalt(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetCaptcha()
        {
            var response = await iappOnboardingAdminlogin.GetCaptcha(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> LogOut([FromBody] logout logout)
        {
            var response = await iappOnboardingAdminlogin.Logout(logout, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetIP()
        {
            var response = await iappOnboardingAdminlogin.GetIPAddress(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SaveUserDetailsAsync([FromBody] SignUp signUp)
        {
            var response = await iappOnboardingAdminlogin.SaveUserDetailsAsync(signUp, default).ConfigureAwait(false);
            string status;
            switch (response)
            {
                case 1: status = "\"Data Stored Successfully\""; break;
                case 2: status = "\"Data Stored Successfully\""; break;
                case 3: status = "\"Data Stored Successfully\""; break;
                default: status = "\"Try Again\""; break;
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<SignUp>>> GetAllAsync()
        {
            return await iappOnboardingAdminlogin.GetAllAsync(default).ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<SignUp>>> BoardUserGetAllAsync()
        {
            return await iappOnboardingAdminlogin.BoardUserGetAllAsync(default).ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<SignUp>>> GetDocumentAsync(string id)
        {
            var response = await iappOnboardingAdminlogin.GetDocumentByIdAsync(id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<SignUp>>> GetBoardUserdetailAsync(string id)
        {
            var response = await iappOnboardingAdminlogin.GetBoardUserdetailByIdAsync(id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] SignUp signUp)
        {
            if (signUp == null)
            {
                return BadRequest(nameof(signUp));
            }

            if (signUp.UserID == "0")
            {
                return BadRequest(nameof(signUp.UserID));
            }

            try
            {
                var response = await iappOnboardingAdminlogin.UpdateUserDetailsAsync(signUp, default).ConfigureAwait(false);
                string status;
                switch (response)
                {
                    case 1: status = "\"Updated Successfully\""; break;
                    case 2: status = "\"Updated Successfully\""; break;
                    case 3: status = "\"Updated Successfully\""; break;
                    default: status = "\"Try Again\""; break;
                }

                return response > 0 ? this.Ok(status) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await iappOnboardingAdminlogin.DeleteAsync(Id, default).ConfigureAwait(false);

            if (response > 0)
            {
                status = "\"Delete Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AppUserRoleMapping>> checkExistUserIdAsync(string id)
        {
            var response = await iappOnboardingAdminlogin.CheckExistUserIdAsync(id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

    }
}

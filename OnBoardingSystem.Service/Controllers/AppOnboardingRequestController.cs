//-----------------------------------------------------------------------
// <copyright file="AppOnboardingRequestController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using Castle.MicroKernel.Registration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    // using OnBoardingSystem.Data.EF.Models;
    using System.Text.Json;

    //using AbsModels = Data.Abstractions.Models.AppOnboardingRequest;

    /// <summary>
    /// OnBoardingRegistrationController.
    /// </summary>
    public class AppOnboardingRequestController : ControllerBase
    {
        private readonly IAppOnboardingRequestDirector iappOnboardingRequest;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingRequestController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingRequest">iAppOnboardingRequest.</param>
        public AppOnboardingRequestController(IHttpContextAccessor httpContextAccessor, IAppOnboardingRequestDirector iAppOnboardingRequest)
        {
            this.iappOnboardingRequest = iAppOnboardingRequest;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.OnBoardingRequestDetailUpsert>>> GetAllAsync()
        {

            return await iappOnboardingRequest.GetAllAsync(default).ConfigureAwait(false);
        }
        /// <summary>
        /// Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.OnBoardingRequestDetailUpsert>>> GetAllByStatusAsync(string Status)
        {

            return await iappOnboardingRequest.GetAllByStatusAsync(Status, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Save AppOnboarding Request Data.
        /// </summary>
        /// <param name="appOnboardingRequest">appOnboardingRequest.</param>
        /// <returns>AppOnboardingRequest.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] Data.Abstractions.Models.AppOnboardingRequest appOnboardingRequest)
        {
            var response = await iappOnboardingRequest.SaveAppOnboardingRequestDataAsync(appOnboardingRequest, default).ConfigureAwait(false);
            string status;
            response = JsonSerializer.Serialize(response);
            return response;
        }
        /// <summary>
        /// Save AppOnboarding Request Data.
        /// </summary>
        /// <param name="appOnboardingRequest">appOnboardingRequest.</param>
        /// <returns>AppOnboardingRequest.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppOnboardingRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> OTPAsync([FromBody] OTPModal otpModal)
        {
            var response = await iappOnboardingRequest.SendOTP(otpModal, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        /// <summary>
        /// Get appOnboardingRequest List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AppOnboardRequestAndDetail>> GetByIdAsync(string requestId)
        {
            var response = await iappOnboardingRequest.GetByIdAsync(requestId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get appOnboardingRequest LinkDetails by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest LinkDetails by id.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AppOnboardRequestAndDetail>> GetOnBoardingRequestLink(string requestId)
        {
            var response = await iappOnboardingRequest.GetOnBoardingRequestLink(requestId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// GetList by id.
        /// </summary>
        /// <param name="ministriesId">ministriesId.</param>
        /// /// <param name="requestid">requestid.</param>
        /// <returns>get by id.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Status>> GetStatusByIdAsync(string RequestId)
        {
            var response = await iappOnboardingRequest.GetStatusByIdAsync(RequestId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// GetStatus Count.
        /// </summary>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DashboardCount), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DashboardCount), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DashboardCount>> GetStatusCountAsync()
        {
            var response = await iappOnboardingRequest.GetStatusCountAsync(default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
    }
}

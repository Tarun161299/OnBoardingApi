//-----------------------------------------------------------------------
// <copyright file="AppOnboardingDetailsController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    /// <summary>
    /// AppOnboardingDetailsController.
    /// </summary>
    public class AppOnboardingDetailsController : ControllerBase
    {
        private readonly IAppOnboardingDetailDirector iappOnboardingdetail;

        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingdetail">iAppOnboardingdetail.</param>

        public AppOnboardingDetailsController(IHttpContextAccessor httpContextAccessor, IAppOnboardingDetailDirector iAppOnboardingdetail)
        {
            this.iappOnboardingdetail = iAppOnboardingdetail;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Onboarding Details.
        /// </summary>
        /// <param name="appOnboardingDetail">adminCredentials.</param>
        /// <returns>AppAdminLoginDetails.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardingDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardingDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] AppOnboardingDetails appOnboardingDetail)
        {
            string status;
            appOnboardingDetail.Ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var jsonString = JsonSerializer.Serialize(appOnboardingDetail);
            var response = await iappOnboardingdetail.Save(jsonString, default).ConfigureAwait(false);
            if (response == true)
            {
                status = "\"Data Stored Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }

            return response == true ? Created(string.Empty, status) : Ok(status);
        }

        /// <summary>
        /// Update to AppOnboardingDetails.
        /// </summary>
        /// <param name="statusupdate">statusupdate.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardingDetailStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardingDetailStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> ChangeStatusAsync([FromBody] AppOnboardingDetailStatus statusupdate)
        {
            var response = await iappOnboardingdetail.Updatestatus(statusupdate, default).ConfigureAwait(false);
            string status;
            if (response > 0)
            {
                return status = "\"Success\"";
            }
            else
            {
                return status = "\"Try Again\"";
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ProjectCreationController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;

    /// <summary>
    /// AppProjectDetailsController.
    /// </summary>
    public class AppProjectDetailsController : ControllerBase
    {
        private readonly IAppProjectDetailsDirector iAppProjectDetails;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppProjectDetails">IAppProjectDetails.</param>
        public AppProjectDetailsController(IAppProjectDetailsDirector iAppProjectDetails)
        {
            this.iAppProjectDetails = iAppProjectDetails;
        }

        /// <summary>
        /// Get App_ProjectDetails List.
        /// </summary>
        /// <returns>GetAll.</returns>

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppProjectDetails>>> GetAllAsync()
        {
            return await iAppProjectDetails.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appProjectDetails">iAppOnboardingdetail.</param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] OnBoardingSystem.Data.Abstractions.Models.AppProjectDetails appProjectDetails)
        {
            string status;
            var response = await iAppProjectDetails.Save(appProjectDetails, default).ConfigureAwait(false);
            if (response == 1)
            {
                status = "\"Data Stored Successfully\"";

            }
            else
            {
                status = "\"Already Exists\"";
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appProjectDetails">iAppOnboardingdetail.</param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> UpdateCreationAsync([FromBody] OnBoardingSystem.Data.Abstractions.Models.AppProjectDetails appProjectDetails)
        {
            string status;
            var response = await iAppProjectDetails.Update(appProjectDetails, default).ConfigureAwait(false);
            if (response == 1)
            {
                status = "\"Data Update Successfully\"";
            }
            else
            {
                status = "\"Already Exists\"";
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        /// <summary>
        /// Get App Project Details List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="id">id.</param>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.AppProjectDetails>>> GetByIdAsync(string id)
        {
            return await iAppProjectDetails.GetById(id, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get App Project Details List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="RequestNo">RequestNo.</param>

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.CounsellingDocs>>> GetByRequestNoAsync(string Requestno)
         {
            return await iAppProjectDetails.GetByRequestNoAsync(Requestno, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get App Project Details List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="id">id.</param>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OnBoardingSystem.Data.Abstractions.Models.AppProjectDetails>> GetByProjectIdAsync(int id)
        {
            return await iAppProjectDetails.GetByProjectId(id, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCreationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appProjectDetails">AppProjectDetails.</param>+

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SaveProjectDetailsAsync([FromBody] OnBoardingSystem.Data.Abstractions.Models.AppProjectDetails appProjectDetails)
        {
            string status;
            var response = await iAppProjectDetails.SaveProjectDetails(appProjectDetails, default).ConfigureAwait(false);
            if (response > 0)
            {
                status = "\"Project Details Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCreationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appProjectDetails">AppProjectDetails.</param>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppProjectDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SavePIDetailsAsync([FromBody] OnBoardingSystem.Data.Abstractions.Models.PIDetails PIDetails)
        {
            string status;
            var response = await iAppProjectDetails.SavePIDetails(PIDetails, default).ConfigureAwait(false);
            if (response > 0)
            {
                status = "\"PI Details Saved Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }
    }
}

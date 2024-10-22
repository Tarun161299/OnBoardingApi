
//-----------------------------------------------------------------------
// <copyright file="AppProjectActivityController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// AppProjectActivityController.
    /// </summary>
    public class AppProjectActivityController : ControllerBase
    {
        private readonly IAppProjectActivityDirector appprojectactivityDirector;
        private readonly ILogger<AppProjectActivityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectActivityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appprojectactivityDirector">appprojectactivityDirector.</param>
        /// <param name="logger">logger.</param>
        public AppProjectActivityController(IAppProjectActivityDirector appprojectactivityDirector, ILogger<AppProjectActivityController> logger)
        {
            this.appprojectactivityDirector = appprojectactivityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppProjectActivity List.
        /// </summary>
        /// <returns>Get All AppProjectActivity List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppProjectActivity>>> GetAllAsync()
        {
            return await appprojectactivityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get AppProjectActivity List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppProjectActivity>> GetByIdAsync([FromBody] AppDocumentFilter getByRefIdNActivity)
        {
            var response = await appprojectactivityDirector.GetByIdAsync(getByRefIdNActivity, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get AppProjectActivity List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectActivity), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppProjectActivity>>> GetByParentRefIdAsync(string ParentRefId)
        {
            var response = await appprojectactivityDirector.GetByParentRefIdAsync(ParentRefId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert AppProjectActivity.
        /// </summary>
        /// <param name="appprojectactivity">appprojectactivity.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.AppProjectActivity appprojectactivity)
        {
            if (appprojectactivity == null)
            {
                return BadRequest(appprojectactivity);
            }
            try
            {
                var response = await appprojectactivityDirector.InsertAsync(appprojectactivity, default).ConfigureAwait(false);
                return response > 0 ? Created(string.Empty, response) : Ok(response);
            }
            catch (EntityFoundException entityFoundEx)
            {
                Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update to AppProjectActivity.
        /// </summary>
        /// <param name="appprojectactivity">appprojectactivity.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.AppProjectActivity appprojectactivity)
        {
            if (appprojectactivity == null)
            {
                return BadRequest(nameof(appprojectactivity));
            }

            //if (appprojectactivity.Id == 0)
            //{
            //    return BadRequest(nameof(appprojectactivity.Id));
            //}

            try
            {
                var response = await appprojectactivityDirector.UpdateAsync(appprojectactivity, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppProjectActivity.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int Id)
        {
            string status;
            var response = await appprojectactivityDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

    }
}

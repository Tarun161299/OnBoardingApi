
//-----------------------------------------------------------------------
// <copyright file="AppProjectExpenditureController.cs" company="NIC">
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
    /// AppProjectExpenditureController.
    /// </summary>
    public class AppProjectExpenditureController : ControllerBase
    {
        private readonly IAppProjectExpenditureDirector appprojectexpenditureDirector;
        private readonly ILogger<AppProjectExpenditureController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectExpenditureController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appprojectexpenditureDirector">appprojectexpenditureDirector.</param>
        /// <param name="logger">logger.</param>
        public AppProjectExpenditureController(IAppProjectExpenditureDirector appprojectexpenditureDirector, ILogger<AppProjectExpenditureController> logger)
        {
            this.appprojectexpenditureDirector = appprojectexpenditureDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppProjectExpenditure List.
        /// </summary>
        /// <returns>Get All AppProjectExpenditure List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectExpenditure), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectExpenditure), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppProjectExpenditure>>> GetAllAsync()
        {
            return await appprojectexpenditureDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppProjectExpenditure List By Id.
        /// </summary>
        /// <param name="ProjectId">ProjectId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectExpenditure), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectExpenditure), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppProjectExpenditure>>> GetByIdAsync(int ProjectId)
        {
            var response = await appprojectexpenditureDirector.GetByIdAsync(ProjectId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppProjectExpenditure.
        /// </summary>
        /// <param name="appprojectexpenditure">appprojectexpenditure.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.AppProjectExpenditure appprojectexpenditure)
        {
            if (appprojectexpenditure == null)
            {
                return BadRequest(appprojectexpenditure);
            }

            try
            {
                var response = await appprojectexpenditureDirector.InsertAsync(appprojectexpenditure, default).ConfigureAwait(false);
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
        /// Update to AppProjectExpenditure.
        /// </summary>
        /// <param name="appprojectexpenditure">appprojectexpenditure.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.AppProjectExpenditure appprojectexpenditure)
        {
            string status;
            if (appprojectexpenditure == null)
            {
                return BadRequest(nameof(appprojectexpenditure));
            }

            if (appprojectexpenditure.ProjectId == 0)
            {
                return BadRequest(nameof(appprojectexpenditure.ProjectId));
            }

            try
            {
				var response = await appprojectexpenditureDirector.UpdateAsync(appprojectexpenditure, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Update Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppProjectExpenditure.
        /// </summary>
        /// <param name="ProjectId">ProjectId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int ProjectId)
        {
            string status;
            var response = await appprojectexpenditureDirector.DeleteAsync(ProjectId, default).ConfigureAwait(false);

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
	
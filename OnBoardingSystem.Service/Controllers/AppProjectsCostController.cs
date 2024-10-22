//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
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
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    public class AppProjectsCostController : ControllerBase
    {
        private readonly IAppProjectCostDiector iAppProjectCost;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public AppProjectsCostController(IAppProjectCostDiector _iAppProjectCost)
        {
            this.iAppProjectCost = _iAppProjectCost;    
        }
        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="appProjectCost">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] AbsModels.AppProjectCost appProjectCost)
        {
            string status = "";
            if (appProjectCost == null)
            {
                return BadRequest(appProjectCost);
            }

            try
            {
                var response = await iAppProjectCost.SaveAsync(appProjectCost, default).ConfigureAwait(false);
                if (response == 1)
                {
                    return status = "\"Success\"";
                }
                if (response == -2)
                {
                    return status = "\"Already Exist\"";
                }
                else
                {
                    return status = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, response) : Ok(response);
            }
            catch (EntityFoundException entityFoundEx)
            {
               return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
               return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Get appOnboardingRequest List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppProjectCostList>>> GetByIdAsync(int projectid)
        {
            var response = await iAppProjectCost.GetByIdAsync(projectid, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Deletes Rule.
        /// </summary>
        /// <param name="financialComponentId","projectCode">ministrytId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync([FromBody] AbsModels.AppProjectCost appProjectCost)
        {
            string status;
            var response = await iAppProjectCost.DeleteAsync(appProjectCost.FinancialComponentId , appProjectCost.ProjectId, default).ConfigureAwait(false);

            if (response > 0)
            {
                status = "\"Deleted Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return response > 0 ? Created(string.Empty, status) : Ok(status);

            }
        /// <summary>
        /// Update to Ministry.
        /// </summary>
        /// <param name="ministry">ministry.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] Data.Abstractions.Models.AppProjectCost appProjectCost)
        {
            string status;
            if (appProjectCost == null)
            {
                return BadRequest(nameof(appProjectCost));
            }

            try
            {
                var response = await this.iAppProjectCost.UpdateAsync(appProjectCost, default).ConfigureAwait(false);
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
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

    }
}


//-----------------------------------------------------------------------
// <copyright file="WorkOrderDetailsController.cs" company="NIC">
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
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// WorkOrderDetailsController.
    /// </summary>
    public class WorkOrderDetailsController : ControllerBase
    {
        private readonly IWorkOrderDetailsDirector workorderdetailsDirector;
        private readonly ILogger<WorkOrderDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkOrderDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="workorderdetailsDirector">workorderdetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public WorkOrderDetailsController(IWorkOrderDetailsDirector workorderdetailsDirector, ILogger<WorkOrderDetailsController> logger)
        {
            this.workorderdetailsDirector = workorderdetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get WorkOrderDetails List.
        /// </summary>
        /// <returns>Get All WorkOrderDetails List.</returns>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.WorkOrderDetails>>> GetAllAsync()
        {
            return await workorderdetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get WorkOrderDetails List By Id.
        /// </summary>
        /// <param name="WorkorderId">WorkorderId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.WorkOrderDetails>> GetByIdAsync(int WorkorderId)
        {
            var response = await workorderdetailsDirector.GetByIdAsync(WorkorderId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Get WorkOrderDetails List By Id.
        /// </summary>
        /// <param name="WorkorderId">WorkorderId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.WorkOrderDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.WorkOrderDetails>>> GetByProjectCodeAsync(string projectCode)
        {
            var response = await workorderdetailsDirector.GetByProjectCodeAsync(projectCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert WorkOrderDetails.
        /// </summary>
        /// <param name="workorderdetails">workorderdetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.WorkOrderDetails workorderdetails)
        {
            if (workorderdetails == null)
            {
                return BadRequest(workorderdetails);
            }

            try
            {
                var response = await workorderdetailsDirector.InsertAsync(workorderdetails, default).ConfigureAwait(false);
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
        /// Update to WorkOrderDetails.
        /// </summary>
        /// <param name="workorderdetails">workorderdetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.WorkOrderDetails workorderdetails)
        {
            string message = "";
            if (workorderdetails == null)
            {
                return BadRequest(nameof(workorderdetails));
            }

            if (workorderdetails.WorkorderId == 0)
            {
                return BadRequest(nameof(workorderdetails.WorkorderId));
            }

            try
            {
                var response = await workorderdetailsDirector.UpdateAsync(workorderdetails, default).ConfigureAwait(false);
                if (response > 0)
                {
                    message = "\"Data Update Successfully\"";

                }
                else
                {
                    message = "\"Try Again\"";
                }
                return message;
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete WorkOrderDetails.
        /// </summary>
        /// <param name="WorkorderId">WorkorderId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int WorkorderId)
        {
            string status;
            var response = await workorderdetailsDirector.DeleteAsync(WorkorderId, default).ConfigureAwait(false);

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

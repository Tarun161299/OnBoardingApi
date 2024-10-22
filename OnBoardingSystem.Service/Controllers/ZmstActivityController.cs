
//-----------------------------------------------------------------------
// <copyright file="ZmstActivityController.cs" company="NIC">
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
    /// ZmstActivityController.
    /// </summary>
    public class ZmstActivityController : ControllerBase
    {
        private readonly IZmstActivityDirector zmstactivityDirector;
        private readonly ILogger<ZmstActivityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstActivityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstactivityDirector">zmstactivityDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstActivityController(IZmstActivityDirector zmstactivityDirector, ILogger<ZmstActivityController> logger)
        {
            this.zmstactivityDirector = zmstactivityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstActivity List.
        /// </summary>
        /// <returns>Get All ZmstActivity List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstActivity), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstActivity), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstActivity>>> GetAllAsync()
        {
            return await zmstactivityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstActivity List By Id.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstActivity), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstActivity), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstActivity>> GetByIdAsync(string ActivityId)
        {
            var response = await zmstactivityDirector.GetByIdAsync(ActivityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstActivity.
        /// </summary>
        /// <param name="zmstactivity">zmstactivity.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstActivity zmstactivity)
        {
            if (zmstactivity == null)
            {
                return BadRequest(zmstactivity);
            }

            try
            {
                var response = await zmstactivityDirector.InsertAsync(zmstactivity, default).ConfigureAwait(false);
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
        /// Update to ZmstActivity.
        /// </summary>
        /// <param name="zmstactivity">zmstactivity.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstActivity zmstactivity)
        {
            if (zmstactivity == null)
            {
                return BadRequest(nameof(zmstactivity));
            }

            if (zmstactivity.ActivityId == "0")
            {
                return BadRequest(nameof(zmstactivity.ActivityId));
            }

            try
            {
                var response = await zmstactivityDirector.UpdateAsync(zmstactivity, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstActivity.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string ActivityId)
        {
            string status;
            var response = await zmstactivityDirector.DeleteAsync(ActivityId, default).ConfigureAwait(false);

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


//-----------------------------------------------------------------------
// <copyright file="ZmstFeeTypeController.cs" company="NIC">
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
    /// ZmstFeeTypeController.
    /// </summary>
    public class ZmstFeeTypeController : ControllerBase
    {
        private readonly IZmstFeeTypeDirector zmstfeetypeDirector;
        private readonly ILogger<ZmstFeeTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstFeeTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstfeetypeDirector">zmstfeetypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstFeeTypeController(IZmstFeeTypeDirector zmstfeetypeDirector, ILogger<ZmstFeeTypeController> logger)
        {
            this.zmstfeetypeDirector = zmstfeetypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstFeeType List.
        /// </summary>
        /// <returns>Get All ZmstFeeType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstFeeType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstFeeType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstFeeType>>> GetAllAsync()
        {
            return await zmstfeetypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstFeeType List By Id.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstFeeType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstFeeType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstFeeType>> GetByIdAsync(int ActivityId)
        {
            var response = await zmstfeetypeDirector.GetByIdAsync(ActivityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstFeeType.
        /// </summary>
        /// <param name="zmstfeetype">zmstfeetype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstFeeType zmstfeetype)
        {
            if (zmstfeetype == null)
            {
                return BadRequest(zmstfeetype);
            }

            try
            {
                var response = await zmstfeetypeDirector.InsertAsync(zmstfeetype, default).ConfigureAwait(false);
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
        /// Update to ZmstFeeType.
        /// </summary>
        /// <param name="zmstfeetype">zmstfeetype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstFeeType zmstfeetype)
        {
            if (zmstfeetype == null)
            {
                return BadRequest(nameof(zmstfeetype));
            }

            if (zmstfeetype.ActivityId == 0)
            {
                return BadRequest(nameof(zmstfeetype.ActivityId));
            }

            try
            {
				var response = await zmstfeetypeDirector.UpdateAsync(zmstfeetype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstFeeType.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int ActivityId)
        {
            string status;
            var response = await zmstfeetypeDirector.DeleteAsync(ActivityId, default).ConfigureAwait(false);

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
	
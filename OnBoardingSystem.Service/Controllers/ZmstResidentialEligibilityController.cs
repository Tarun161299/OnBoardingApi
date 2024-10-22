
//-----------------------------------------------------------------------
// <copyright file="ZmstResidentialEligibilityController.cs" company="NIC">
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
    /// ZmstResidentialEligibilityController.
    /// </summary>
    public class ZmstResidentialEligibilityController : ControllerBase
    {
        private readonly IZmstResidentialEligibilityDirector zmstresidentialeligibilityDirector;
        private readonly ILogger<ZmstResidentialEligibilityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstResidentialEligibilityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstresidentialeligibilityDirector">zmstresidentialeligibilityDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstResidentialEligibilityController(IZmstResidentialEligibilityDirector zmstresidentialeligibilityDirector, ILogger<ZmstResidentialEligibilityController> logger)
        {
            this.zmstresidentialeligibilityDirector = zmstresidentialeligibilityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstResidentialEligibility List.
        /// </summary>
        /// <returns>Get All ZmstResidentialEligibility List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstResidentialEligibility), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstResidentialEligibility), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstResidentialEligibility>>> GetAllAsync()
        {
            return await zmstresidentialeligibilityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstResidentialEligibility List By Id.
        /// </summary>
        /// <param name="ResidentialEligibilityId">ResidentialEligibilityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstResidentialEligibility), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstResidentialEligibility), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstResidentialEligibility>> GetByIdAsync(string ResidentialEligibilityId)
        {
            var response = await zmstresidentialeligibilityDirector.GetByIdAsync(ResidentialEligibilityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstResidentialEligibility.
        /// </summary>
        /// <param name="zmstresidentialeligibility">zmstresidentialeligibility.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstResidentialEligibility zmstresidentialeligibility)
        {
            if (zmstresidentialeligibility == null)
            {
                return BadRequest(zmstresidentialeligibility);
            }

            try
            {
                var response = await zmstresidentialeligibilityDirector.InsertAsync(zmstresidentialeligibility, default).ConfigureAwait(false);
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
        /// Update to ZmstResidentialEligibility.
        /// </summary>
        /// <param name="zmstresidentialeligibility">zmstresidentialeligibility.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstResidentialEligibility zmstresidentialeligibility)
        {
            if (zmstresidentialeligibility == null)
            {
                return BadRequest(nameof(zmstresidentialeligibility));
            }

            if (zmstresidentialeligibility.ResidentialEligibilityId == "0")
            {
                return BadRequest(nameof(zmstresidentialeligibility.ResidentialEligibilityId));
            }

            try
            {
				var response = await zmstresidentialeligibilityDirector.UpdateAsync(zmstresidentialeligibility, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstResidentialEligibility.
        /// </summary>
        /// <param name="ResidentialEligibilityId">ResidentialEligibilityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string ResidentialEligibilityId)
        {
            string status;
            var response = await zmstresidentialeligibilityDirector.DeleteAsync(ResidentialEligibilityId, default).ConfigureAwait(false);

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
	
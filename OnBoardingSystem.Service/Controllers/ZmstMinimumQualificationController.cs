
//-----------------------------------------------------------------------
// <copyright file="ZmstMinimumQualificationController.cs" company="NIC">
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
    /// ZmstMinimumQualificationController.
    /// </summary>
    public class ZmstMinimumQualificationController : ControllerBase
    {
        private readonly IZmstMinimumQualificationDirector zmstminimumqualificationDirector;
        private readonly ILogger<ZmstMinimumQualificationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstMinimumQualificationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstminimumqualificationDirector">zmstminimumqualificationDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstMinimumQualificationController(IZmstMinimumQualificationDirector zmstminimumqualificationDirector, ILogger<ZmstMinimumQualificationController> logger)
        {
            this.zmstminimumqualificationDirector = zmstminimumqualificationDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstMinimumQualification List.
        /// </summary>
        /// <returns>Get All ZmstMinimumQualification List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstMinimumQualification), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstMinimumQualification), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstMinimumQualification>>> GetAllAsync()
        {
            return await zmstminimumqualificationDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstMinimumQualification List By Id.
        /// </summary>
        /// <param name="MinimumQualId">MinimumQualId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstMinimumQualification), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstMinimumQualification), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstMinimumQualification>> GetByIdAsync(string MinimumQualId)
        {
            var response = await zmstminimumqualificationDirector.GetByIdAsync(MinimumQualId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstMinimumQualification.
        /// </summary>
        /// <param name="zmstminimumqualification">zmstminimumqualification.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstMinimumQualification zmstminimumqualification)
        {
            if (zmstminimumqualification == null)
            {
                return BadRequest(zmstminimumqualification);
            }

            try
            {
                var response = await zmstminimumqualificationDirector.InsertAsync(zmstminimumqualification, default).ConfigureAwait(false);
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
        /// Update to ZmstMinimumQualification.
        /// </summary>
        /// <param name="zmstminimumqualification">zmstminimumqualification.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstMinimumQualification zmstminimumqualification)
        {
            if (zmstminimumqualification == null)
            {
                return BadRequest(nameof(zmstminimumqualification));
            }

            if (zmstminimumqualification.MinimumQualId == "0")
            {
                return BadRequest(nameof(zmstminimumqualification.MinimumQualId));
            }

            try
            {
				var response = await zmstminimumqualificationDirector.UpdateAsync(zmstminimumqualification, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstMinimumQualification.
        /// </summary>
        /// <param name="MinimumQualId">MinimumQualId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string MinimumQualId)
        {
            string status;
            var response = await zmstminimumqualificationDirector.DeleteAsync(MinimumQualId, default).ConfigureAwait(false);

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
	
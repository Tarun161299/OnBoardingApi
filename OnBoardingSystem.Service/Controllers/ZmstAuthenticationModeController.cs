
//-----------------------------------------------------------------------
// <copyright file="ZmstAuthenticationModeController.cs" company="NIC">
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
    /// ZmstAuthenticationModeController.
    /// </summary>
    public class ZmstAuthenticationModeController : ControllerBase
    {
        private readonly IZmstAuthenticationModeDirector zmstauthenticationmodeDirector;
        private readonly ILogger<ZmstAuthenticationModeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAuthenticationModeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstauthenticationmodeDirector">zmstauthenticationmodeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstAuthenticationModeController(IZmstAuthenticationModeDirector zmstauthenticationmodeDirector, ILogger<ZmstAuthenticationModeController> logger)
        {
            this.zmstauthenticationmodeDirector = zmstauthenticationmodeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstAuthenticationMode List.
        /// </summary>
        /// <returns>Get All ZmstAuthenticationMode List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAuthenticationMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAuthenticationMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstAuthenticationMode>>> GetAllAsync()
        {
            return await zmstauthenticationmodeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstAuthenticationMode List By Id.
        /// </summary>
        /// <param name="AuthCode">AuthCode.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAuthenticationMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAuthenticationMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstAuthenticationMode>> GetByIdAsync(string AuthCode)
        {
            var response = await zmstauthenticationmodeDirector.GetByIdAsync(AuthCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstAuthenticationMode.
        /// </summary>
        /// <param name="zmstauthenticationmode">zmstauthenticationmode.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstAuthenticationMode zmstauthenticationmode)
        {
            if (zmstauthenticationmode == null)
            {
                return BadRequest(zmstauthenticationmode);
            }

            try
            {
                var response = await zmstauthenticationmodeDirector.InsertAsync(zmstauthenticationmode, default).ConfigureAwait(false);
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
        /// Update to ZmstAuthenticationMode.
        /// </summary>
        /// <param name="zmstauthenticationmode">zmstauthenticationmode.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstAuthenticationMode zmstauthenticationmode)
        {
            if (zmstauthenticationmode == null)
            {
                return BadRequest(nameof(zmstauthenticationmode));
            }

            if (zmstauthenticationmode.AuthCode == "0")
            {
                return BadRequest(nameof(zmstauthenticationmode.AuthCode));
            }

            try
            {
				var response = await zmstauthenticationmodeDirector.UpdateAsync(zmstauthenticationmode, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstAuthenticationMode.
        /// </summary>
        /// <param name="AuthCode">AuthCode.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string AuthCode)
        {
            string status;
            var response = await zmstauthenticationmodeDirector.DeleteAsync(AuthCode, default).ConfigureAwait(false);

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
	
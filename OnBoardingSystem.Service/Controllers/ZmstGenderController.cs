
//-----------------------------------------------------------------------
// <copyright file="ZmstGenderController.cs" company="NIC">
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
    /// ZmstGenderController.
    /// </summary>
    public class ZmstGenderController : ControllerBase
    {
        private readonly IZmstGenderDirector zmstgenderDirector;
        private readonly ILogger<ZmstGenderController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstGenderController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstgenderDirector">zmstgenderDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstGenderController(IZmstGenderDirector zmstgenderDirector, ILogger<ZmstGenderController> logger)
        {
            this.zmstgenderDirector = zmstgenderDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstGender List.
        /// </summary>
        /// <returns>Get All ZmstGender List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstGender), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstGender), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstGender>>> GetAllAsync()
        {
            return await zmstgenderDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstGender List By Id.
        /// </summary>
        /// <param name="GenderId">GenderId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstGender), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstGender), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstGender>> GetByIdAsync(string GenderId)
        {
            var response = await zmstgenderDirector.GetByIdAsync(GenderId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstGender.
        /// </summary>
        /// <param name="zmstgender">zmstgender.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstGender zmstgender)
        {
            if (zmstgender == null)
            {
                return BadRequest(zmstgender);
            }

            try
            {
                var response = await zmstgenderDirector.InsertAsync(zmstgender, default).ConfigureAwait(false);
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
        /// Update to ZmstGender.
        /// </summary>
        /// <param name="zmstgender">zmstgender.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstGender zmstgender)
        {
            if (zmstgender == null)
            {
                return BadRequest(nameof(zmstgender));
            }

            if (zmstgender.GenderId == "0")
            {
                return BadRequest(nameof(zmstgender.GenderId));
            }
            try
            {
				var response = await zmstgenderDirector.UpdateAsync(zmstgender, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstGender.
        /// </summary>
        /// <param name="GenderId">GenderId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string GenderId)
        {
            string status;
            var response = await zmstgenderDirector.DeleteAsync(GenderId, default).ConfigureAwait(false);
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
	
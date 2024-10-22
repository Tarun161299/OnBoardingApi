
//-----------------------------------------------------------------------
// <copyright file="ZmstStreamController.cs" company="NIC">
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
    /// ZmstStreamController.
    /// </summary>
    public class ZmstStreamController : ControllerBase
    {
        private readonly IZmstStreamDirector zmststreamDirector;
        private readonly ILogger<ZmstStreamController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstStreamController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmststreamDirector">zmststreamDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstStreamController(IZmstStreamDirector zmststreamDirector, ILogger<ZmstStreamController> logger)
        {
            this.zmststreamDirector = zmststreamDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstStream List.
        /// </summary>
        /// <returns>Get All ZmstStream List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstStream>>> GetAllAsync()
        {
            return await zmststreamDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstStream List.
        /// </summary>
        /// <returns>Get All ZmstStream List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstStream>>> GetListByInstcdAsync(string instcd)
        {
            return await zmststreamDirector.GetListByInstcdAsync(instcd,default).ConfigureAwait(false);
        }
        /// <summary>
        /// Get ZmstStream List By Id.
        /// </summary>
        /// <param name="StreamId">StreamId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstStream>> GetByIdAsync(string StreamId)
        {
            var response = await zmststreamDirector.GetByIdAsync(StreamId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstStream.
        /// </summary>
        /// <param name="zmststream">zmststream.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstStream zmststream)
        {
            if (zmststream == null)
            {
                return BadRequest(zmststream);
            }

            try
            {
                var response = await zmststreamDirector.InsertAsync(zmststream, default).ConfigureAwait(false);
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
        /// Update to ZmstStream.
        /// </summary>
        /// <param name="zmststream">zmststream.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstStream zmststream)
        {
            if (zmststream == null)
            {
                return BadRequest(nameof(zmststream));
            }

            if (zmststream.StreamId == "0")
            {
                return BadRequest(nameof(zmststream.StreamId));
            }

            try
            {
				var response = await zmststreamDirector.UpdateAsync(zmststream, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstStream.
        /// </summary>
        /// <param name="StreamId">StreamId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string StreamId)
        {
            string status;
            var response = await zmststreamDirector.DeleteAsync(StreamId, default).ConfigureAwait(false);

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
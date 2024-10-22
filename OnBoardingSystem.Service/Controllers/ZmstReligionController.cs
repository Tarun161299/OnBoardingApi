
//-----------------------------------------------------------------------
// <copyright file="ZmstReligionController.cs" company="NIC">
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
    /// ZmstReligionController.
    /// </summary>
    public class ZmstReligionController : ControllerBase
    {
        private readonly IZmstReligionDirector zmstreligionDirector;
        private readonly ILogger<ZmstReligionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstReligionController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstreligionDirector">zmstreligionDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstReligionController(IZmstReligionDirector zmstreligionDirector, ILogger<ZmstReligionController> logger)
        {
            this.zmstreligionDirector = zmstreligionDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstReligion List.
        /// </summary>
        /// <returns>Get All ZmstReligion List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstReligion), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstReligion), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstReligion>>> GetAllAsync()
        {
            return await zmstreligionDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstReligion List By Id.
        /// </summary>
        /// <param name="ReligionId">ReligionId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstReligion), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstReligion), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstReligion>> GetByIdAsync(string ReligionId)
        {
            var response = await zmstreligionDirector.GetByIdAsync(ReligionId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstReligion.
        /// </summary>
        /// <param name="zmstreligion">zmstreligion.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstReligion zmstreligion)
        {
            if (zmstreligion == null)
            {
                return BadRequest(zmstreligion);
            }

            try
            {
                var response = await zmstreligionDirector.InsertAsync(zmstreligion, default).ConfigureAwait(false);
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
        /// Update to ZmstReligion.
        /// </summary>
        /// <param name="zmstreligion">zmstreligion.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstReligion zmstreligion)
        {
            if (zmstreligion == null)
            {
                return BadRequest(nameof(zmstreligion));
            }

            if (zmstreligion.ReligionId == "0")
            {
                return BadRequest(nameof(zmstreligion.ReligionId));
            }

            try
            {
                var response = await zmstreligionDirector.UpdateAsync(zmstreligion, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstReligion.
        /// </summary>
        /// <param name="ReligionId">ReligionId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string ReligionId)
        {
            string status;
            var response = await zmstreligionDirector.DeleteAsync(ReligionId, default).ConfigureAwait(false);

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

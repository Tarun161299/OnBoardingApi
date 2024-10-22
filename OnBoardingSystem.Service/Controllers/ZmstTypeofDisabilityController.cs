
//-----------------------------------------------------------------------
// <copyright file="ZmstTypeofDisabilityController.cs" company="NIC">
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
    /// ZmstTypeofDisabilityController.
    /// </summary>
    public class ZmstTypeofDisabilityController : ControllerBase
    {
        private readonly IZmstTypeofDisabilityDirector zmsttypeofdisabilityDirector;
        private readonly ILogger<ZmstTypeofDisabilityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstTypeofDisabilityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmsttypeofdisabilityDirector">zmsttypeofdisabilityDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstTypeofDisabilityController(IZmstTypeofDisabilityDirector zmsttypeofdisabilityDirector, ILogger<ZmstTypeofDisabilityController> logger)
        {
            this.zmsttypeofdisabilityDirector = zmsttypeofdisabilityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstTypeofDisability List.
        /// </summary>
        /// <returns>Get All ZmstTypeofDisability List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstTypeofDisability), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstTypeofDisability), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstTypeofDisability>>> GetAllAsync()
        {
            return await zmsttypeofdisabilityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstTypeofDisability List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstTypeofDisability), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstTypeofDisability), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstTypeofDisability>> GetByIdAsync(string Id)
        {
            var response = await zmsttypeofdisabilityDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstTypeofDisability.
        /// </summary>
        /// <param name="zmsttypeofdisability">zmsttypeofdisability.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstTypeofDisability zmsttypeofdisability)
        {
            if (zmsttypeofdisability == null)
            {
                return BadRequest(zmsttypeofdisability);
            }

            try
            {
                var response = await zmsttypeofdisabilityDirector.InsertAsync(zmsttypeofdisability, default).ConfigureAwait(false);
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
        /// Update to ZmstTypeofDisability.
        /// </summary>
        /// <param name="zmsttypeofdisability">zmsttypeofdisability.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstTypeofDisability zmsttypeofdisability)
        {
            if (zmsttypeofdisability == null)
            {
                return BadRequest(nameof(zmsttypeofdisability));
            }

            if (zmsttypeofdisability.Id == "0")
            {
                return BadRequest(nameof(zmsttypeofdisability.Id));
            }

            try
            {
                var response = await zmsttypeofdisabilityDirector.UpdateAsync(zmsttypeofdisability, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstTypeofDisability.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await zmsttypeofdisabilityDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
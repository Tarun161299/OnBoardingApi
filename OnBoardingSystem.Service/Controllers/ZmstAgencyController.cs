
//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Exceptions;

    /// <summary>
    /// ZmstAgencyController.
    /// </summary>
    public class ZmstAgencyController : ControllerBase
    {
        private readonly IZmstAgencyDirector zmstagencyDirector;
        private readonly ILogger<ZmstAgencyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstagencyDirector">zmstagencyDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstAgencyController(IZmstAgencyDirector zmstagencyDirector, ILogger<ZmstAgencyController> logger)
        {
            this.zmstagencyDirector = zmstagencyDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstAgency List.
        /// </summary>
        /// <returns>Get All ZmstAgency List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstAgency>>> GetAllAsync()
        {
            return await zmstagencyDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstAgency List By Id.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstAgency>> GetByIdAsync(string AgencyId)
        {
            var response = await zmstagencyDirector.GetByIdAsync(AgencyId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstAgency.
        /// </summary>
        /// <param name="zmstagency">zmstagency.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstAgency zmstagency)
        {
            if (zmstagency == null)
            {
                return BadRequest(zmstagency);
            }

            try
            {
                var response = await zmstagencyDirector.InsertAsync(zmstagency, default).ConfigureAwait(false);
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
        /// Update to ZmstAgency.
        /// </summary>
        /// <param name="zmstagency">zmstagency.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstAgency zmstagency)
        {
            string message = "";
            if (zmstagency == null)
            {
                return BadRequest(nameof(zmstagency));
            }

            if (zmstagency.AgencyId == "0")
            {
                return BadRequest(nameof(zmstagency.AgencyId));
            }

            try
            {
                var response = await zmstagencyDirector.UpdateAsync(zmstagency, default).ConfigureAwait(false);
                if (response > 0)
                {
                    message = "\"Updated Successfully\"";
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
        /// Delete ZmstAgency.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string AgencyId)
        {
            string status;
            var response = await zmstagencyDirector.DeleteAsync(AgencyId, default).ConfigureAwait(false);

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

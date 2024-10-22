
//-----------------------------------------------------------------------
// <copyright file="MdStatusController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// MdStatusController.
    /// </summary>
    public class MdStatusController : ControllerBase
    {
        private readonly IMdStatusDirector mdstatusDirector;
        private readonly ILogger<MdStatusController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdStatusController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdstatusDirector">mdstatusDirector.</param>
        /// <param name="logger">logger.</param>
        public MdStatusController(IMdStatusDirector mdstatusDirector, ILogger<MdStatusController> logger)
        {
            this.mdstatusDirector = mdstatusDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdStatus List.
        /// </summary>
        /// <returns>Get All MdStatus List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdStatus>>> GetAllAsync()
        {
            return await mdstatusDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdStatus List By Id.
        /// </summary>
        /// <param name="StatusId">StatusId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdStatus>> GetByIdAsync(string StatusId)
        {
            var response = await mdstatusDirector.GetByIdAsync(StatusId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdStatus.
        /// </summary>
        /// <param name="mdstatus">mdstatus.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdStatus mdstatus)
        {
            if (mdstatus == null)
            {
                return BadRequest(mdstatus);
            }

            try
            {
                var response = await mdstatusDirector.InsertAsync(mdstatus, default).ConfigureAwait(false);
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
        /// Update to MdStatus.
        /// </summary>
        /// <param name="mdstatus">mdstatus.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdStatus mdstatus)
        {
            if (mdstatus == null)
            {
                return BadRequest(nameof(mdstatus));
            }

            if (mdstatus.StatusId == "0")
            {
                return BadRequest(nameof(mdstatus.StatusId));
            }

            try
            {
                var response = await mdstatusDirector.UpdateAsync(mdstatus, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdStatus.
        /// </summary>
        /// <param name="StatusId">StatusId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string StatusId)
        {
            string status;
            var response = await mdstatusDirector.DeleteAsync(StatusId, default).ConfigureAwait(false);

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

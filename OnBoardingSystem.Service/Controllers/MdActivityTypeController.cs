
//-----------------------------------------------------------------------
// <copyright file="MdActivityTypeController.cs" company="NIC">
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
    /// MdActivityTypeController.
    /// </summary>
    public class MdActivityTypeController : ControllerBase
    {
        private readonly IMdActivityTypeDirector mdactivitytypeDirector;
        private readonly ILogger<MdActivityTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActivityTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdactivitytypeDirector">mdactivitytypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MdActivityTypeController(IMdActivityTypeDirector mdactivitytypeDirector, ILogger<MdActivityTypeController> logger)
        {
            this.mdactivitytypeDirector = mdactivitytypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdActivityType List.
        /// </summary>
        /// <returns>Get All MdActivityType List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdActivityType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdActivityType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdActivityType>>> GetAllAsync()
        {
            return await mdactivitytypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdActivityType List By Id.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdActivityType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdActivityType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdActivityType>> GetByIdAsync(int ActivityId)
        {
            var response = await mdactivitytypeDirector.GetByIdAsync(ActivityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdActivityType.
        /// </summary>
        /// <param name="mdactivitytype">mdactivitytype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdActivityType mdactivitytype)
        {
            if (mdactivitytype == null)
            {
                return BadRequest(mdactivitytype);
            }

            try
            {
                var response = await mdactivitytypeDirector.InsertAsync(mdactivitytype, default).ConfigureAwait(false);
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
        /// Update to MdActivityType.
        /// </summary>
        /// <param name="mdactivitytype">mdactivitytype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdActivityType mdactivitytype)
        {
            if (mdactivitytype == null)
            {
                return BadRequest(nameof(mdactivitytype));
            }

            if (mdactivitytype.ActivityId ==0)
            {
                return BadRequest(nameof(mdactivitytype.ActivityId));
            }

            try
            {
                var response = await mdactivitytypeDirector.UpdateAsync(mdactivitytype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdActivityType.
        /// </summary>
        /// <param name="ActivityId">ActivityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int ActivityId)
        {
            string status;
            var response = await mdactivitytypeDirector.DeleteAsync(ActivityId, default).ConfigureAwait(false);

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

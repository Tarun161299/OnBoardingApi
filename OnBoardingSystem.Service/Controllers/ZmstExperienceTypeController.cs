
//-----------------------------------------------------------------------
// <copyright file="ZmstExperienceTypeController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Business.Behaviors;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// ZmstExperienceTypeController.
    /// </summary>
    public class ZmstExperienceTypeController : ControllerBase
    {
        private readonly IZmstExperienceTypeDirector zmstexperiencetypeDirector;
        private readonly ILogger<ZmstExperienceTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstExperienceTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstexperiencetypeDirector">zmstexperiencetypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstExperienceTypeController(IZmstExperienceTypeDirector zmstexperiencetypeDirector, ILogger<ZmstExperienceTypeController> logger)
        {
            this.zmstexperiencetypeDirector = zmstexperiencetypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstExperienceType List.
        /// </summary>
        /// <returns>Get All ZmstExperienceType List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstExperienceType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstExperienceType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstExperienceType>>> GetAllAsync()
        {
            return await zmstexperiencetypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstExperienceType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstExperienceType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstExperienceType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstExperienceType>> GetByIdAsync(string Id)
        {
            var response = await zmstexperiencetypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstExperienceType.
        /// </summary>
        /// <param name="zmstexperiencetype">zmstexperiencetype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstExperienceType zmstexperiencetype)
        {
            if (zmstexperiencetype == null)
            {
                return BadRequest(zmstexperiencetype);
            }

            try
            {
                var response = await zmstexperiencetypeDirector.InsertAsync(zmstexperiencetype, default).ConfigureAwait(false);
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
        /// Update to ZmstExperienceType.
        /// </summary>
        /// <param name="zmstexperiencetype">zmstexperiencetype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstExperienceType zmstexperiencetype)
        {
            string message = "";
            if (zmstexperiencetype == null)
            {
                return BadRequest(nameof(zmstexperiencetype));
            }

            if (zmstexperiencetype.Id == "0")
            {
                return BadRequest(nameof(zmstexperiencetype.Id));
            }

            try
            {
                var response = await zmstexperiencetypeDirector.UpdateAsync(zmstexperiencetype, default).ConfigureAwait(false);
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
        /// Delete ZmstExperienceType.
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
            var response = await zmstexperiencetypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

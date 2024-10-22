
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteTypeController.cs" company="NIC">
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
    /// ZmstInstituteTypeController.
    /// </summary>
    public class ZmstInstituteTypeController : ControllerBase
    {
        private readonly IZmstInstituteTypeDirector zmstinstitutetypeDirector;
        private readonly ILogger<ZmstInstituteTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstinstitutetypeDirector">zmstinstitutetypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstInstituteTypeController(IZmstInstituteTypeDirector zmstinstitutetypeDirector, ILogger<ZmstInstituteTypeController> logger)
        {
            this.zmstinstitutetypeDirector = zmstinstitutetypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstInstituteType List.
        /// </summary>
        /// <returns>Get All ZmstInstituteType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstInstituteType>>> GetAllAsync()
        {
            return await zmstinstitutetypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstInstituteType List By Id.
        /// </summary>
        /// <param name="InstituteTypeId">InstituteTypeId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstInstituteType>> GetByIdAsync(string InstituteTypeId)
        {
            var response = await zmstinstitutetypeDirector.GetByIdAsync(InstituteTypeId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstInstituteType.
        /// </summary>
        /// <param name="zmstinstitutetype">zmstinstitutetype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstInstituteType zmstinstitutetype)
        {
            if (zmstinstitutetype == null)
            {
                return BadRequest(zmstinstitutetype);
            }

            try
            {
                var response = await zmstinstitutetypeDirector.InsertAsync(zmstinstitutetype, default).ConfigureAwait(false);
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
        /// Update to ZmstInstituteType.
        /// </summary>
        /// <param name="zmstinstitutetype">zmstinstitutetype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstInstituteType zmstinstitutetype)
        {
            string message = "";
            if (zmstinstitutetype == null)
            {
                return BadRequest(nameof(zmstinstitutetype));
            }

            if (zmstinstitutetype.InstituteTypeId == "0")
            {
                return BadRequest(nameof(zmstinstitutetype.InstituteTypeId));
            }

            try
            {
                var response = await zmstinstitutetypeDirector.UpdateAsync(zmstinstitutetype, default).ConfigureAwait(false);
                if (response>0)
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
        /// Delete ZmstInstituteType.
        /// </summary>
        /// <param name="InstituteTypeId">InstituteTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string InstituteTypeId)
        {
            string status;
            var response = await zmstinstitutetypeDirector.DeleteAsync(InstituteTypeId, default).ConfigureAwait(false);

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

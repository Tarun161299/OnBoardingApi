//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
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
    /// MinistryController.
    /// </summary>
    public class MinistryController : ControllerBase
    {
        private readonly IMdMinistryDirector iministry;
        private readonly ILogger<MinistryController> logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public MinistryController(IMdMinistryDirector iMdMinistry, ILogger<MinistryController> logger)
        {
            this.iministry = iMdMinistry;
            this.logger = logger;
        }

        /// <summary>
        /// Get Ministry List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdMinistry>>> GetAllAsync()
        {
            logger.LogError("OSROOOOOOOOOOOOOOOOOOOOOOOOOO");
            return await iministry.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes Rule.
        /// </summary>
        /// <param name="ministrytId">ministrytId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAsync(int ministrytId)
        {
            string status;
            if (ministrytId == 0)
            {
                return BadRequest(nameof(ministrytId));
            }

            var response = await iministry.DeleteAsync(ministrytId, default).ConfigureAwait(false);
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

        /// <summary>
        /// GetList by id.
        /// </summary>
        /// <param name="ministriesId">ministriesId.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdMinistry>> GetByIdAsync(int ministriesId)
        {
            var response = await iministry.GetByIdAsync(ministriesId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="mdMinistry">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdMinistry mdMinistry)
        {
            if (mdMinistry == null)
            {
                return BadRequest(mdMinistry);
            }
            try
            {
                var response = await iministry.SaveAsync(mdMinistry, default).ConfigureAwait(false);
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
        /// Insert By Procedure.
        /// </summary>
        /// <param name="ministryId">ministryId.</param>
        /// <param name="ministryName">ministryName.</param>
        /// <returns>MdMinistry.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertUseProcedureAsync(int ministryId, string ministryName)
        {
            var response = await iministry.SaveAsync(ministryId, ministryName, default).ConfigureAwait(false);
            string status;
            switch (response)
            {
                case 1: status = "Save"; break;
                default: status = "Data Not Found"; break;
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }

        /// <summary>
        /// Update to Ministry.
        /// </summary>
        /// <param name="ministry">ministry.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody]  AbsModels.MdMinistry ministry)
        {
            if (ministry == null)
            {
                return BadRequest(nameof(ministry));
            }

            if (ministry.MinistryId == 0)
            {
                return BadRequest(nameof(ministry.MinistryId));
            }

            try
            {
                var response = await this.iministry.UpdateAsync(ministry.MinistryId, ministry.MinistryName, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
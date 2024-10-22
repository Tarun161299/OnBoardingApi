
//-----------------------------------------------------------------------
// <copyright file="MdMainModuleController.cs" company="NIC">
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
    /// MdMainModuleController.
    /// </summary>
    public class MdMainModuleController : ControllerBase
    {
        private readonly IMdMainModuleDirector mdmainmoduleDirector;
        private readonly ILogger<MdMainModuleController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMainModuleController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdmainmoduleDirector">mdmainmoduleDirector.</param>
        /// <param name="logger">logger.</param>
        public MdMainModuleController(IMdMainModuleDirector mdmainmoduleDirector, ILogger<MdMainModuleController> logger)
        {
            this.mdmainmoduleDirector = mdmainmoduleDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdMainModule List.
        /// </summary>
        /// <returns>Get All MdMainModule List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdMainModule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdMainModule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdMainModule>>> GetAllAsync()
        {
            return await mdmainmoduleDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdMainModule List By Id.
        /// </summary>
        /// <param name="MainModuleId">MainModuleId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdMainModule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdMainModule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdMainModule>>> GetByIdAsync(string MainModuleId)
        {
            var response = await mdmainmoduleDirector.GetByIdAsync(MainModuleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdMainModule.
        /// </summary>
        /// <param name="mdmainmodule">mdmainmodule.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdMainModule mdmainmodule)
        {
            if (mdmainmodule == null)
            {
                return BadRequest(mdmainmodule);
            }

            try
            {
                var response = await mdmainmoduleDirector.InsertAsync(mdmainmodule, default).ConfigureAwait(false);
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
        /// Update to MdMainModule.
        /// </summary>
        /// <param name="mdmainmodule">mdmainmodule.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdMainModule mdmainmodule)
        {
            if (mdmainmodule == null)
            {
                return BadRequest(nameof(mdmainmodule));
            }

            if (mdmainmodule.MainModuleId == "0")
            {
                return BadRequest(nameof(mdmainmodule.MainModuleId));
            }

            try
            {
                var response = await mdmainmoduleDirector.UpdateAsync(mdmainmodule, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdMainModule.
        /// </summary>
        /// <param name="MainModuleId">MainModuleId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string MainModuleId)
        {
            string status;
            var response = await mdmainmoduleDirector.DeleteAsync(MainModuleId, default).ConfigureAwait(false);

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

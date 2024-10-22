//-----------------------------------------------------------------------
// <copyright file="MdStateController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Business.Services;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// MdStateController.
    /// </summary>
    public class MdStateController : ControllerBase
    {
        private readonly IMdStateDirector mdstateDirector;
        private readonly ILogger<MdStateController> logger;
        private readonly MDApiKeyValidateService _mDApiKeyValidateService;
        private readonly HttpContext _httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdStateController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdstateDirector">mdstateDirector.</param>
        /// <param name="logger">logger.</param>
        public MdStateController(IMdStateDirector mdstateDirector, ILogger<MdStateController> logger, MDApiKeyValidateService mDApiKeyValidateService)
        {
            this.mdstateDirector = mdstateDirector;
            this.logger = logger;
            this._mDApiKeyValidateService = mDApiKeyValidateService;
        }

        /// <summary>
        /// Get MdState List.
        /// </summary>
        /// <returns>Get All MdState List.</returns> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdState), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdState), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdState>>> GetAllAsync()
        {
            return await mdstateDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdState List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdState), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdState), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdState>> GetByIdAsync(string Id)
        {
            var response = await mdstateDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdState.
        /// </summary>
        /// <param name="mdstate">mdstate.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdState mdstate)
        {
            if (mdstate == null)
            {
                return BadRequest(mdstate);
            }
            try
            {
                var response = await mdstateDirector.InsertAsync(mdstate, default).ConfigureAwait(false);
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
        /// Update to MdState.
        /// </summary>
        /// <param name="mdstate">mdstate.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.MdState mdstate, string Id)
        {
            string message = "";
            if (mdstate == null)
            {
                return BadRequest(nameof(mdstate));
            }

            if (mdstate.Id == "0")
            {
                return BadRequest(nameof(mdstate.Id));
            }

            try
            {
                var response = await mdstateDirector.UpdateAsync(mdstate, Id, default).ConfigureAwait(false);
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
            catch (EntityFoundException entityFoundEx)
            {
                Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete MdState.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            try
            {
                string status;
                var response = await mdstateDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
            catch (EntityFoundException entityFoundEx)
            {
                Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                Log.Information(ex.Message);
                throw ex;
            }
        }

    }
}

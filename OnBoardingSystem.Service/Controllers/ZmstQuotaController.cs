
//-----------------------------------------------------------------------
// <copyright file="ZmstQuotaController.cs" company="NIC">
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
    /// ZmstQuotaController.
    /// </summary>
    public class ZmstQuotaController : ControllerBase
    {
        private readonly IZmstQuotaDirector zmstquotaDirector;
        private readonly ILogger<ZmstQuotaController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuotaController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstquotaDirector">zmstquotaDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQuotaController(IZmstQuotaDirector zmstquotaDirector, ILogger<ZmstQuotaController> logger)
        {
            this.zmstquotaDirector = zmstquotaDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQuota List.
        /// </summary>
        /// <returns>Get All ZmstQuota List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuota), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuota), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQuota>>> GetAllAsync()
        {
            return await zmstquotaDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQuota List By Id.
        /// </summary>
        /// <param name="QuotaId">QuotaId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuota), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuota), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQuota>> GetByIdAsync(string QuotaId)
        {
            var response = await zmstquotaDirector.GetByIdAsync(QuotaId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQuota.
        /// </summary>
        /// <param name="zmstquota">zmstquota.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQuota zmstquota)
        {
            if (zmstquota == null)
            {
                return BadRequest(zmstquota);
            }

            try
            {
                var response = await zmstquotaDirector.InsertAsync(zmstquota, default).ConfigureAwait(false);
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
        /// Update to ZmstQuota.
        /// </summary>
        /// <param name="zmstquota">zmstquota.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQuota zmstquota)
        {
            if (zmstquota == null)
            {
                return BadRequest(nameof(zmstquota));
            }

            if (zmstquota.QuotaId == "0")
            {
                return BadRequest(nameof(zmstquota.QuotaId));
            }

            try
            {
                var response = await zmstquotaDirector.UpdateAsync(zmstquota, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQuota.
        /// </summary>
        /// <param name="QuotaId">QuotaId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QuotaId)
        {
            string status;
            var response = await zmstquotaDirector.DeleteAsync(QuotaId, default).ConfigureAwait(false);

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

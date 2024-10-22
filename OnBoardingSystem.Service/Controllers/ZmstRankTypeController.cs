
//-----------------------------------------------------------------------
// <copyright file="ZmstRankTypeController.cs" company="NIC">
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
    /// ZmstRankTypeController.
    /// </summary>
    public class ZmstRankTypeController : ControllerBase
    {
        private readonly IZmstRankTypeDirector zmstranktypeDirector;
        private readonly ILogger<ZmstRankTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstRankTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstranktypeDirector">zmstranktypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstRankTypeController(IZmstRankTypeDirector zmstranktypeDirector, ILogger<ZmstRankTypeController> logger)
        {
            this.zmstranktypeDirector = zmstranktypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstRankType List.
        /// </summary>
        /// <returns>Get All ZmstRankType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstRankType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstRankType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstRankType>>> GetAllAsync()
        {
            return await zmstranktypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstRankType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstRankType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstRankType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstRankType>> GetByIdAsync(string Id)
        {
            var response = await zmstranktypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstRankType.
        /// </summary>
        /// <param name="zmstranktype">zmstranktype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstRankType zmstranktype)
        {
            if (zmstranktype == null)
            {
                return BadRequest(zmstranktype);
            }

            try
            {
                var response = await zmstranktypeDirector.InsertAsync(zmstranktype, default).ConfigureAwait(false);
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
        /// Update to ZmstRankType.
        /// </summary>
        /// <param name="zmstranktype">zmstranktype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstRankType zmstranktype)
        {
            if (zmstranktype == null)
            {
                return BadRequest(nameof(zmstranktype));
            }

            if (zmstranktype.Id == "0")
            {
                return BadRequest(nameof(zmstranktype.Id));
            }

            try
            {
                var response = await zmstranktypeDirector.UpdateAsync(zmstranktype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstRankType.
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
            var response = await zmstranktypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

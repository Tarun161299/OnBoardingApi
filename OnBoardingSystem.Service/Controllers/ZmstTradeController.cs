
//-----------------------------------------------------------------------
// <copyright file="ZmstTradeController.cs" company="NIC">
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
    /// ZmstTradeController.
    /// </summary>
    public class ZmstTradeController : ControllerBase
    {
        private readonly IZmstTradeDirector zmsttradeDirector;
        private readonly ILogger<ZmstTradeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstTradeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmsttradeDirector">zmsttradeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstTradeController(IZmstTradeDirector zmsttradeDirector, ILogger<ZmstTradeController> logger)
        {
            this.zmsttradeDirector = zmsttradeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstTrade List.
        /// </summary>
        /// <returns>Get All ZmstTrade List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstTrade), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstTrade), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstTrade>>> GetAllAsync()
        {
            return await zmsttradeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstTrade List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstTrade), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstTrade), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstTrade>> GetByIdAsync(string Id)
        {
            var response = await zmsttradeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstTrade.
        /// </summary>
        /// <param name="zmsttrade">zmsttrade.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstTrade zmsttrade)
        {
            if (zmsttrade == null)
            {
                return BadRequest(zmsttrade);
            }

            try
            {
                var response = await zmsttradeDirector.InsertAsync(zmsttrade, default).ConfigureAwait(false);
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
        /// Update to ZmstTrade.
        /// </summary>
        /// <param name="zmsttrade">zmsttrade.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstTrade zmsttrade)
        {
            string message = "";
            if (zmsttrade == null)
            {
                return BadRequest(nameof(zmsttrade));
            }

            if (zmsttrade.Id == "0")
            {
                return BadRequest(nameof(zmsttrade.Id));
            }

            try
            {
                var response = await zmsttradeDirector.UpdateAsync(zmsttrade, default).ConfigureAwait(false);
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
        /// Delete ZmstTrade.
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
            var response = await zmsttradeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

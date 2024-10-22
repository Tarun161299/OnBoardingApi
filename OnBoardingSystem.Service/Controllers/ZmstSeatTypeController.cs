
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatTypeController.cs" company="NIC">
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
    /// ZmstSeatTypeController.
    /// </summary>
    public class ZmstSeatTypeController : ControllerBase
    {
        private readonly IZmstSeatTypeDirector zmstseattypeDirector;
        private readonly ILogger<ZmstSeatTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstseattypeDirector">zmstseattypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSeatTypeController(IZmstSeatTypeDirector zmstseattypeDirector, ILogger<ZmstSeatTypeController> logger)
        {
            this.zmstseattypeDirector = zmstseattypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSeatType List.
        /// </summary>
        /// <returns>Get All ZmstSeatType List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSeatType>>> GetAllAsync()
        {
            return await zmstseattypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSeatType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSeatType>> GetByIdAsync(string Id)
        {
            var response = await zmstseattypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSeatType.
        /// </summary>
        /// <param name="zmstseattype">zmstseattype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSeatType zmstseattype)
        {
            if (zmstseattype == null)
            {
                return BadRequest(zmstseattype);
            }

            try
            {
                var response = await zmstseattypeDirector.InsertAsync(zmstseattype, default).ConfigureAwait(false);
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
        /// Update to ZmstSeatType.
        /// </summary>
        /// <param name="zmstseattype">zmstseattype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstSeatType zmstseattype)
        {
            string message = "";
            if (zmstseattype == null)
            {
                return BadRequest(nameof(zmstseattype));
            }

            if (zmstseattype.Id == "0")
            {
                return BadRequest(nameof(zmstseattype.Id));
            }

            try
            {
                var response = await zmstseattypeDirector.UpdateAsync(zmstseattype, default).ConfigureAwait(false);
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
        /// Delete ZmstSeatType.
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
            var response = await zmstseattypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

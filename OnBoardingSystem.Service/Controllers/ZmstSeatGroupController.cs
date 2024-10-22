
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatGroupController.cs" company="NIC">
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
    /// ZmstSeatGroupController.
    /// </summary>
    public class ZmstSeatGroupController : ControllerBase
    {
        private readonly IZmstSeatGroupDirector zmstseatgroupDirector;
        private readonly ILogger<ZmstSeatGroupController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatGroupController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstseatgroupDirector">zmstseatgroupDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSeatGroupController(IZmstSeatGroupDirector zmstseatgroupDirector, ILogger<ZmstSeatGroupController> logger)
        {
            this.zmstseatgroupDirector = zmstseatgroupDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSeatGroup List.
        /// </summary>
        /// <returns>Get All ZmstSeatGroup List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGroup), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGroup), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSeatGroup>>> GetAllAsync()
        {
            return await zmstseatgroupDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSeatGroup List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGroup), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGroup), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSeatGroup>> GetByIdAsync(string Id)
        {
            var response = await zmstseatgroupDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSeatGroup.
        /// </summary>
        /// <param name="zmstseatgroup">zmstseatgroup.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSeatGroup zmstseatgroup)
        {
            if (zmstseatgroup == null)
            {
                return BadRequest(zmstseatgroup);
            }

            try
            {
                var response = await zmstseatgroupDirector.InsertAsync(zmstseatgroup, default).ConfigureAwait(false);
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
        /// Update to ZmstSeatGroup.
        /// </summary>
        /// <param name="zmstseatgroup">zmstseatgroup.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstSeatGroup zmstseatgroup)
        {
            string message = "";
            if (zmstseatgroup == null)
            {
                return BadRequest(nameof(zmstseatgroup));
            }

            if (zmstseatgroup.Id == "0")
            {
                return BadRequest(nameof(zmstseatgroup.Id));
            }

            try
            {
                var response = await zmstseatgroupDirector.UpdateAsync(zmstseatgroup, default).ConfigureAwait(false);
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
        /// Delete ZmstSeatGroup.
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
            var response = await zmstseatgroupDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

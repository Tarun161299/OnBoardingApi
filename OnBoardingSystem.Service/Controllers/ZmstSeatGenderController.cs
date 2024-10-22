
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatGenderController.cs" company="NIC">
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
    /// ZmstSeatGenderController.
    /// </summary>
    public class ZmstSeatGenderController : ControllerBase
    {
        private readonly IZmstSeatGenderDirector zmstseatgenderDirector;
        private readonly ILogger<ZmstSeatGenderController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatGenderController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstseatgenderDirector">zmstseatgenderDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSeatGenderController(IZmstSeatGenderDirector zmstseatgenderDirector, ILogger<ZmstSeatGenderController> logger)
        {
            this.zmstseatgenderDirector = zmstseatgenderDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSeatGender List.
        /// </summary>
        /// <returns>Get All ZmstSeatGender List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGender), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGender), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSeatGender>>> GetAllAsync()
        {
            return await zmstseatgenderDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSeatGender List By Id.
        /// </summary>
        /// <param name="SeatGenderId">SeatGenderId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGender), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatGender), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSeatGender>> GetByIdAsync(string SeatGenderId)
        {
            var response = await zmstseatgenderDirector.GetByIdAsync(SeatGenderId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSeatGender.
        /// </summary>
        /// <param name="zmstseatgender">zmstseatgender.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSeatGender zmstseatgender)
        {
            if (zmstseatgender == null)
            {
                return BadRequest(zmstseatgender);
            }

            try
            {
                var response = await zmstseatgenderDirector.InsertAsync(zmstseatgender, default).ConfigureAwait(false);
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
        /// Update to ZmstSeatGender.
        /// </summary>
        /// <param name="zmstseatgender">zmstseatgender.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSeatGender zmstseatgender)
        {
            if (zmstseatgender == null)
            {
                return BadRequest(nameof(zmstseatgender));
            }

            if (zmstseatgender.SeatGenderId == "0")
            {
                return BadRequest(nameof(zmstseatgender.SeatGenderId));
            }

            try
            {
                var response = await zmstseatgenderDirector.UpdateAsync(zmstseatgender, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstSeatGender.
        /// </summary>
        /// <param name="SeatGenderId">SeatGenderId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SeatGenderId)
        {
            string status;
            var response = await zmstseatgenderDirector.DeleteAsync(SeatGenderId, default).ConfigureAwait(false);

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
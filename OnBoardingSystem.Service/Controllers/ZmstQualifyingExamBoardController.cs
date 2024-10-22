
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamBoardController.cs" company="NIC">
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
    /// ZmstQualifyingExamBoardController.
    /// </summary>
    public class ZmstQualifyingExamBoardController : ControllerBase
    {
        private readonly IZmstQualifyingExamBoardDirector zmstqualifyingexamboardDirector;
        private readonly ILogger<ZmstQualifyingExamBoardController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamBoardController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamboardDirector">zmstqualifyingexamboardDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamBoardController(IZmstQualifyingExamBoardDirector zmstqualifyingexamboardDirector, ILogger<ZmstQualifyingExamBoardController> logger)
        {
            this.zmstqualifyingexamboardDirector = zmstqualifyingexamboardDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExamBoard List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamBoard List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamBoard), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamBoard), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExamBoard>>> GetAllAsync()
        {
            return await zmstqualifyingexamboardDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExamBoard List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamBoard), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamBoard), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExamBoard>> GetByIdAsync(string Id)
        {
            var response = await zmstqualifyingexamboardDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExamBoard.
        /// </summary>
        /// <param name="zmstqualifyingexamboard">zmstqualifyingexamboard.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExamBoard zmstqualifyingexamboard)
        {
            if (zmstqualifyingexamboard == null)
            {
                return BadRequest(zmstqualifyingexamboard);
            }

            try
            {
                var response = await zmstqualifyingexamboardDirector.InsertAsync(zmstqualifyingexamboard, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExamBoard.
        /// </summary>
        /// <param name="zmstqualifyingexamboard">zmstqualifyingexamboard.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQualifyingExamBoard zmstqualifyingexamboard)
        {
            if (zmstqualifyingexamboard == null)
            {
                return BadRequest(nameof(zmstqualifyingexamboard));
            }

            if (zmstqualifyingexamboard.Id == "0")
            {
                return BadRequest(nameof(zmstqualifyingexamboard.Id));
            }

            try
            {
                var response = await zmstqualifyingexamboardDirector.UpdateAsync(zmstqualifyingexamboard, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExamBoard.
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
            var response = await zmstqualifyingexamboardDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

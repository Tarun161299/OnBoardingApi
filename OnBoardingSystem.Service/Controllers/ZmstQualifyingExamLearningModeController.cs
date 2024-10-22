
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamLearningModeController.cs" company="NIC">
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
    /// ZmstQualifyingExamLearningModeController.
    /// </summary>
    public class ZmstQualifyingExamLearningModeController : ControllerBase
    {
        private readonly IZmstQualifyingExamLearningModeDirector zmstqualifyingexamlearningmodeDirector;
        private readonly ILogger<ZmstQualifyingExamLearningModeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamLearningModeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamlearningmodeDirector">zmstqualifyingexamlearningmodeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamLearningModeController(IZmstQualifyingExamLearningModeDirector zmstqualifyingexamlearningmodeDirector, ILogger<ZmstQualifyingExamLearningModeController> logger)
        {
            this.zmstqualifyingexamlearningmodeDirector = zmstqualifyingexamlearningmodeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExamLearningMode List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamLearningMode List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamLearningMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamLearningMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExamLearningMode>>> GetAllAsync()
        {
            return await zmstqualifyingexamlearningmodeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExamLearningMode List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamLearningMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamLearningMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExamLearningMode>> GetByIdAsync(string Id)
        {
            var response = await zmstqualifyingexamlearningmodeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExamLearningMode.
        /// </summary>
        /// <param name="zmstqualifyingexamlearningmode">zmstqualifyingexamlearningmode.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode)
        {
            if (zmstqualifyingexamlearningmode == null)
            {
                return BadRequest(zmstqualifyingexamlearningmode);
            }

            try
            {
                var response = await zmstqualifyingexamlearningmodeDirector.InsertAsync(zmstqualifyingexamlearningmode, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExamLearningMode.
        /// </summary>
        /// <param name="zmstqualifyingexamlearningmode">zmstqualifyingexamlearningmode.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody]  AbsModels.ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode)
        {
            if (zmstqualifyingexamlearningmode == null)
            {
                return BadRequest(nameof(zmstqualifyingexamlearningmode));
            }

            if (zmstqualifyingexamlearningmode.Id == "0")
            {
                return BadRequest(nameof(zmstqualifyingexamlearningmode.Id));
            }

            try
            {
                var response = await zmstqualifyingexamlearningmodeDirector.UpdateAsync(zmstqualifyingexamlearningmode, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExamLearningMode.
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
            var response = await zmstqualifyingexamlearningmodeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

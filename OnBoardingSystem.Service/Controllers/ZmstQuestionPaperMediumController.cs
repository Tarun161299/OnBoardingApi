
//-----------------------------------------------------------------------
// <copyright file="ZmstQuestionPaperMediumController.cs" company="NIC">
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
    /// ZmstQuestionPaperMediumController.
    /// </summary>
    public class ZmstQuestionPaperMediumController : ControllerBase
    {
        private readonly IZmstQuestionPaperMediumDirector zmstquestionpapermediumDirector;
        private readonly ILogger<ZmstQuestionPaperMediumController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuestionPaperMediumController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstquestionpapermediumDirector">zmstquestionpapermediumDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQuestionPaperMediumController(IZmstQuestionPaperMediumDirector zmstquestionpapermediumDirector, ILogger<ZmstQuestionPaperMediumController> logger)
        {
            this.zmstquestionpapermediumDirector = zmstquestionpapermediumDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQuestionPaperMedium List.
        /// </summary>
        /// <returns>Get All ZmstQuestionPaperMedium List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuestionPaperMedium), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuestionPaperMedium), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQuestionPaperMedium>>> GetAllAsync()
        {
            return await zmstquestionpapermediumDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQuestionPaperMedium List By Id.
        /// </summary>
        /// <param name="MediumId">MediumId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuestionPaperMedium), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuestionPaperMedium), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQuestionPaperMedium>> GetByIdAsync(string MediumId)
        {
            var response = await zmstquestionpapermediumDirector.GetByIdAsync(MediumId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="zmstquestionpapermedium">zmstquestionpapermedium.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQuestionPaperMedium zmstquestionpapermedium)
        {
            if (zmstquestionpapermedium == null)
            {
                return BadRequest(zmstquestionpapermedium);
            }

            try
            {
                var response = await zmstquestionpapermediumDirector.InsertAsync(zmstquestionpapermedium, default).ConfigureAwait(false);
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
        /// Update to ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="zmstquestionpapermedium">zmstquestionpapermedium.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQuestionPaperMedium zmstquestionpapermedium)
        {
            if (zmstquestionpapermedium == null)
            {
                return BadRequest(nameof(zmstquestionpapermedium));
            }

            if (zmstquestionpapermedium.MediumId == "0")
            {
                return BadRequest(nameof(zmstquestionpapermedium.MediumId));
            }

            try
            {
                var response = await zmstquestionpapermediumDirector.UpdateAsync(zmstquestionpapermedium, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="MediumId">MediumId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
         public async Task<ActionResult<string>> DeleteAsync(string MediumId)
        {
            string status;
            var response = await zmstquestionpapermediumDirector.DeleteAsync(MediumId, default).ConfigureAwait(false);

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

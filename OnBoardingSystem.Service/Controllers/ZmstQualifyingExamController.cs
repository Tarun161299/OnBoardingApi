
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamController.cs" company="NIC">
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
    /// ZmstQualifyingExamController.
    /// </summary>
    public class ZmstQualifyingExamController : ControllerBase
    {
        private readonly IZmstQualifyingExamDirector zmstqualifyingexamDirector;
        private readonly ILogger<ZmstQualifyingExamController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamDirector">zmstqualifyingexamDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamController(IZmstQualifyingExamDirector zmstqualifyingexamDirector, ILogger<ZmstQualifyingExamController> logger)
        {
            this.zmstqualifyingexamDirector = zmstqualifyingexamDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExam List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExam List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExam), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExam), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExam>>> GetAllAsync()
        {
            return await zmstqualifyingexamDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExam List By Id.
        /// </summary>
        /// <param name="QualifyingExamId">QualifyingExamId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExam), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExam), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExam>> GetByIdAsync(string QualifyingExamId)
        {
            var response = await zmstqualifyingexamDirector.GetByIdAsync(QualifyingExamId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExam.
        /// </summary>
        /// <param name="zmstqualifyingexam">zmstqualifyingexam.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExam zmstqualifyingexam)
        {
            if (zmstqualifyingexam == null)
            {
                return BadRequest(zmstqualifyingexam);
            }

            try
            {
                var response = await zmstqualifyingexamDirector.InsertAsync(zmstqualifyingexam, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExam.
        /// </summary>
        /// <param name="zmstqualifyingexam">zmstqualifyingexam.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQualifyingExam zmstqualifyingexam)
        {
            if (zmstqualifyingexam == null)
            {
                return BadRequest(nameof(zmstqualifyingexam));
            }

            if (zmstqualifyingexam.QualifyingExamId == "0")
            {
                return BadRequest(nameof(zmstqualifyingexam.QualifyingExamId));
            }

            try
            {
                var response = await zmstqualifyingexamDirector.UpdateAsync(zmstqualifyingexam, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExam.
        /// </summary>
        /// <param name="QualifyingExamId">QualifyingExamId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QualifyingExamId)
        {
            string status;
            var response = await zmstqualifyingexamDirector.DeleteAsync(QualifyingExamId, default).ConfigureAwait(false);

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

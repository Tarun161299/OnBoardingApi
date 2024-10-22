
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamStreamController.cs" company="NIC">
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
    /// ZmstQualifyingExamStreamController.
    /// </summary>
    public class ZmstQualifyingExamStreamController : ControllerBase
    {
        private readonly IZmstQualifyingExamStreamDirector zmstqualifyingexamstreamDirector;
        private readonly ILogger<ZmstQualifyingExamStreamController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamStreamController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamstreamDirector">zmstqualifyingexamstreamDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamStreamController(IZmstQualifyingExamStreamDirector zmstqualifyingexamstreamDirector, ILogger<ZmstQualifyingExamStreamController> logger)
        {
            this.zmstqualifyingexamstreamDirector = zmstqualifyingexamstreamDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExamStream List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamStream List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExamStream>>> GetAllAsync()
        {
            return await zmstqualifyingexamstreamDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExamStream List By Id.
        /// </summary>
        /// <param name="QualStreamId">QualStreamId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExamStream>> GetByIdAsync(string QualStreamId)
        {
            var response = await zmstqualifyingexamstreamDirector.GetByIdAsync(QualStreamId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="zmstqualifyingexamstream">zmstqualifyingexamstream.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExamStream zmstqualifyingexamstream)
        {
            if (zmstqualifyingexamstream == null)
            {
                return BadRequest(zmstqualifyingexamstream);
            }

            try
            {
                var response = await zmstqualifyingexamstreamDirector.InsertAsync(zmstqualifyingexamstream, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="zmstqualifyingexamstream">zmstqualifyingexamstream.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQualifyingExamStream zmstqualifyingexamstream)
        {
            if (zmstqualifyingexamstream == null)
            {
                return BadRequest(nameof(zmstqualifyingexamstream));
            }

            if (zmstqualifyingexamstream.QualStreamId == "0")
            {
                return BadRequest(nameof(zmstqualifyingexamstream.QualStreamId));
            }

            try
            {
                var response = await zmstqualifyingexamstreamDirector.UpdateAsync(zmstqualifyingexamstream, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="QualStreamId">QualStreamId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QualStreamId)
        {
            string status;
            var response = await zmstqualifyingexamstreamDirector.DeleteAsync(QualStreamId, default).ConfigureAwait(false);

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

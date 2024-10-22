
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamFromController.cs" company="NIC">
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
    /// ZmstQualifyingExamFromController.
    /// </summary>
    public class ZmstQualifyingExamFromController : ControllerBase
    {
        private readonly IZmstQualifyingExamFromDirector zmstqualifyingexamfromDirector;
        private readonly ILogger<ZmstQualifyingExamFromController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamFromController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamfromDirector">zmstqualifyingexamfromDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamFromController(IZmstQualifyingExamFromDirector zmstqualifyingexamfromDirector, ILogger<ZmstQualifyingExamFromController> logger)
        {
            this.zmstqualifyingexamfromDirector = zmstqualifyingexamfromDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExamFrom List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamFrom List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamFrom), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamFrom), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExamFrom>>> GetAllAsync()
        {
            return await zmstqualifyingexamfromDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExamFrom List By Id.
        /// </summary>
        /// <param name="QualExamFromId">QualExamFromId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamFrom), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamFrom), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExamFrom>> GetByIdAsync(string QualExamFromId)
        {
            var response = await zmstqualifyingexamfromDirector.GetByIdAsync(QualExamFromId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="zmstqualifyingexamfrom">zmstqualifyingexamfrom.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExamFrom zmstqualifyingexamfrom)
        {
            if (zmstqualifyingexamfrom == null)
            {
                return BadRequest(zmstqualifyingexamfrom);
            }

            try
            {
                var response = await zmstqualifyingexamfromDirector.InsertAsync(zmstqualifyingexamfrom, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="zmstqualifyingexamfrom">zmstqualifyingexamfrom.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQualifyingExamFrom zmstqualifyingexamfrom)
        {
            if (zmstqualifyingexamfrom == null)
            {
                return BadRequest(nameof(zmstqualifyingexamfrom));
            }

            if (zmstqualifyingexamfrom.QualExamFromId == "0")
            {
                return BadRequest(nameof(zmstqualifyingexamfrom.QualExamFromId));
            }

            try
            {
                var response = await zmstqualifyingexamfromDirector.UpdateAsync(zmstqualifyingexamfrom, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="QualExamFromId">QualExamFromId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QualExamFromId)
        {
            string status;
            var response = await zmstqualifyingexamfromDirector.DeleteAsync(QualExamFromId, default).ConfigureAwait(false);

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

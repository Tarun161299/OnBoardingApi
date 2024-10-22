
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamResultModeController.cs" company="NIC">
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
    /// ZmstQualifyingExamResultModeController.
    /// </summary>
    public class ZmstQualifyingExamResultModeController : ControllerBase
    {
        private readonly IZmstQualifyingExamResultModeDirector zmstqualifyingexamresultmodeDirector;
        private readonly ILogger<ZmstQualifyingExamResultModeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamResultModeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingexamresultmodeDirector">zmstqualifyingexamresultmodeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingExamResultModeController(IZmstQualifyingExamResultModeDirector zmstqualifyingexamresultmodeDirector, ILogger<ZmstQualifyingExamResultModeController> logger)
        {
            this.zmstqualifyingexamresultmodeDirector = zmstqualifyingexamresultmodeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingExamResultMode List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamResultMode List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamResultMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamResultMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualifyingExamResultMode>>> GetAllAsync()
        {
            return await zmstqualifyingexamresultmodeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingExamResultMode List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamResultMode), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingExamResultMode), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualifyingExamResultMode>> GetByIdAsync(string Id)
        {
            var response = await zmstqualifyingexamresultmodeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingExamResultMode.
        /// </summary>
        /// <param name="zmstqualifyingexamresultmode">zmstqualifyingexamresultmode.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingExamResultMode zmstqualifyingexamresultmode)
        {
            if (zmstqualifyingexamresultmode == null)
            {
                return BadRequest(zmstqualifyingexamresultmode);
            }

            try
            {
                var response = await zmstqualifyingexamresultmodeDirector.InsertAsync(zmstqualifyingexamresultmode, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingExamResultMode.
        /// </summary>
        /// <param name="zmstqualifyingexamresultmode">zmstqualifyingexamresultmode.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody]  AbsModels.ZmstQualifyingExamResultMode zmstqualifyingexamresultmode)
        {
            if (zmstqualifyingexamresultmode == null)
            {
                return BadRequest(nameof(zmstqualifyingexamresultmode));
            }

            if (zmstqualifyingexamresultmode.Id == "0")
            {
                return BadRequest(nameof(zmstqualifyingexamresultmode.Id));
            }

            try
            {
                var response = await zmstqualifyingexamresultmodeDirector.UpdateAsync(zmstqualifyingexamresultmode, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingExamResultMode.
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
            var response = await zmstqualifyingexamresultmodeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

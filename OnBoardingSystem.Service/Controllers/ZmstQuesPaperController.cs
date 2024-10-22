
//-----------------------------------------------------------------------
// <copyright file="ZmstQuesPaperController.cs" company="NIC">
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
    /// ZmstQuesPaperController.
    /// </summary>
    public class ZmstQuesPaperController : ControllerBase
    {
        private readonly IZmstQuesPaperDirector zmstquespaperDirector;
        private readonly ILogger<ZmstQuesPaperController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuesPaperController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstquespaperDirector">zmstquespaperDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQuesPaperController(IZmstQuesPaperDirector zmstquespaperDirector, ILogger<ZmstQuesPaperController> logger)
        {
            this.zmstquespaperDirector = zmstquespaperDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQuesPaper List.
        /// </summary>
        /// <returns>Get All ZmstQuesPaper List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuesPaper), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuesPaper), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQuesPaper>>> GetAllAsync()
        {
            return await zmstquespaperDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQuesPaper List By Id.
        /// </summary>
        /// <param name="PaperId">PaperId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuesPaper), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQuesPaper), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQuesPaper>> GetByIdAsync(string PaperId)
        {
            var response = await zmstquespaperDirector.GetByIdAsync(PaperId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQuesPaper.
        /// </summary>
        /// <param name="zmstquespaper">zmstquespaper.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQuesPaper zmstquespaper)
        {
            if (zmstquespaper == null)
            {
                return BadRequest(zmstquespaper);
            }

            try
            {
                var response = await zmstquespaperDirector.InsertAsync(zmstquespaper, default).ConfigureAwait(false);
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
        /// Update to ZmstQuesPaper.
        /// </summary>
        /// <param name="zmstquespaper">zmstquespaper.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQuesPaper zmstquespaper)
        {
            if (zmstquespaper == null)
            {
                return BadRequest(nameof(zmstquespaper));
            }

            if (zmstquespaper.PaperId == "0")
            {
                return BadRequest(nameof(zmstquespaper.PaperId));
            }

            try
            {
                var response = await zmstquespaperDirector.UpdateAsync(zmstquespaper, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQuesPaper.
        /// </summary>
        /// <param name="PaperId">PaperId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string PaperId)
        {
            string status;
            var response = await zmstquespaperDirector.DeleteAsync(PaperId, default).ConfigureAwait(false);

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

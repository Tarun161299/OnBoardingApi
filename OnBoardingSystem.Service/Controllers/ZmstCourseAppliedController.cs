
//-----------------------------------------------------------------------
// <copyright file="ZmstCourseAppliedController.cs" company="NIC">
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
    /// ZmstCourseAppliedController.
    /// </summary>
    public class ZmstCourseAppliedController : ControllerBase
    {
        private readonly IZmstCourseAppliedDirector zmstcourseappliedDirector;
        private readonly ILogger<ZmstCourseAppliedController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCourseAppliedController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstcourseappliedDirector">zmstcourseappliedDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstCourseAppliedController(IZmstCourseAppliedDirector zmstcourseappliedDirector, ILogger<ZmstCourseAppliedController> logger)
        {
            this.zmstcourseappliedDirector = zmstcourseappliedDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstCourseApplied List.
        /// </summary>
        /// <returns>Get All ZmstCourseApplied List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseApplied), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseApplied), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstCourseApplied>>> GetAllAsync()
        {
            return await zmstcourseappliedDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstCourseApplied List By Id.
        /// </summary>
        /// <param name="CourseId">CourseId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseApplied), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseApplied), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstCourseApplied>> GetByIdAsync(string CourseId)
        {
            var response = await zmstcourseappliedDirector.GetByIdAsync(CourseId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstCourseApplied.
        /// </summary>
        /// <param name="zmstcourseapplied">zmstcourseapplied.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstCourseApplied zmstcourseapplied)
        {
            if (zmstcourseapplied == null)
            {
                return BadRequest(zmstcourseapplied);
            }

            try
            {
                var response = await zmstcourseappliedDirector.InsertAsync(zmstcourseapplied, default).ConfigureAwait(false);
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
        /// Update to ZmstCourseApplied.
        /// </summary>
        /// <param name="zmstcourseapplied">zmstcourseapplied.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstCourseApplied zmstcourseapplied)
        {
            if (zmstcourseapplied == null)
            {
                return BadRequest(nameof(zmstcourseapplied));
            }

            if (zmstcourseapplied.CourseId == "0")
            {
                return BadRequest(nameof(zmstcourseapplied.CourseId));
            }

            try
            {
				var response = await zmstcourseappliedDirector.UpdateAsync(zmstcourseapplied, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstCourseApplied.
        /// </summary>
        /// <param name="CourseId">CourseId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string CourseId)
        {
            string status;
            var response = await zmstcourseappliedDirector.DeleteAsync(CourseId, default).ConfigureAwait(false);

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
	
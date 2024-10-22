
//-----------------------------------------------------------------------
// <copyright file="ZmstCourseAppliedLevelController.cs" company="NIC">
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
    /// ZmstCourseAppliedLevelController.
    /// </summary>
    public class ZmstCourseAppliedLevelController : ControllerBase
    {
        private readonly IZmstCourseAppliedLevelDirector zmstcourseappliedlevelDirector;
        private readonly ILogger<ZmstCourseAppliedLevelController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCourseAppliedLevelController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstcourseappliedlevelDirector">zmstcourseappliedlevelDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstCourseAppliedLevelController(IZmstCourseAppliedLevelDirector zmstcourseappliedlevelDirector, ILogger<ZmstCourseAppliedLevelController> logger)
        {
            this.zmstcourseappliedlevelDirector = zmstcourseappliedlevelDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstCourseAppliedLevel List.
        /// </summary>
        /// <returns>Get All ZmstCourseAppliedLevel List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseAppliedLevel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseAppliedLevel), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstCourseAppliedLevel>>> GetAllAsync()
        {
            return await zmstcourseappliedlevelDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstCourseAppliedLevel List By Id.
        /// </summary>
        /// <param name="CourseLevelId">CourseLevelId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseAppliedLevel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCourseAppliedLevel), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstCourseAppliedLevel>> GetByIdAsync(string CourseLevelId)
        {
            var response = await zmstcourseappliedlevelDirector.GetByIdAsync(CourseLevelId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="zmstcourseappliedlevel">zmstcourseappliedlevel.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstCourseAppliedLevel zmstcourseappliedlevel)
        {
            if (zmstcourseappliedlevel == null)
            {
                return BadRequest(zmstcourseappliedlevel);
            }

            try
            {
                var response = await zmstcourseappliedlevelDirector.InsertAsync(zmstcourseappliedlevel, default).ConfigureAwait(false);
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
        /// Update to ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="zmstcourseappliedlevel">zmstcourseappliedlevel.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstCourseAppliedLevel zmstcourseappliedlevel)
        {
            if (zmstcourseappliedlevel == null)
            {
                return BadRequest(nameof(zmstcourseappliedlevel));
            }

            if (zmstcourseappliedlevel.CourseLevelId == "0")
            {
                return BadRequest(nameof(zmstcourseappliedlevel.CourseLevelId));
            }

            try
            {
				var response = await zmstcourseappliedlevelDirector.UpdateAsync(zmstcourseappliedlevel, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="CourseLevelId">CourseLevelId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string CourseLevelId)
        {
            string status;
            var response = await zmstcourseappliedlevelDirector.DeleteAsync(CourseLevelId, default).ConfigureAwait(false);
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
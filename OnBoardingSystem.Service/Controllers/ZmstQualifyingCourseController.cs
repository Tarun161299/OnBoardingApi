
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingCourseController.cs" company="NIC">
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
    /// ZmstQualifyingCourseController.
    /// </summary>
    public class ZmstQualifyingCourseController : ControllerBase
    {
        private readonly IZmstQualifyingCourseDirector zmstqualifyingcourseDirector;
        private readonly ILogger<ZmstQualifyingCourseController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingCourseController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualifyingcourseDirector">zmstqualifyingcourseDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualifyingCourseController(IZmstQualifyingCourseDirector zmstqualifyingcourseDirector, ILogger<ZmstQualifyingCourseController> logger)
        {
            this.zmstqualifyingcourseDirector = zmstqualifyingcourseDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualifyingCourse List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingCourse List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingCourse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingCourse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<AbsModels.ZmstQualifyingCourse>>> GetAllAsync()
        {
            return await zmstqualifyingcourseDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualifyingCourse List By Id.
        /// </summary>
        /// <param name="QualificationCourseId">QualificationCourseId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingCourse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualifyingCourse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<AbsModels.ZmstQualifyingCourse>> GetByIdAsync(string QualificationCourseId)
        {
            var response = await zmstqualifyingcourseDirector.GetByIdAsync(QualificationCourseId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualifyingCourse.
        /// </summary>
        /// <param name="zmstqualifyingcourse">zmstqualifyingcourse.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualifyingCourse zmstqualifyingcourse)
        {
            if (zmstqualifyingcourse == null)
            {
                return BadRequest(zmstqualifyingcourse);
            }

            try
            {
                var response = await zmstqualifyingcourseDirector.InsertAsync(zmstqualifyingcourse, default).ConfigureAwait(false);
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
        /// Update to ZmstQualifyingCourse.
        /// </summary>
        /// <param name="zmstqualifyingcourse">zmstqualifyingcourse.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstQualifyingCourse zmstqualifyingcourse)
        {
            if (zmstqualifyingcourse == null)
            {
                return BadRequest(nameof(zmstqualifyingcourse));
            }

            if (zmstqualifyingcourse.QualificationCourseId == "0")
            {
                return BadRequest(nameof(zmstqualifyingcourse.QualificationCourseId));
            }

            try
            {
                var response = await zmstqualifyingcourseDirector.UpdateAsync(zmstqualifyingcourse, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualifyingCourse.
        /// </summary>
        /// <param name="QualificationCourseId">QualificationCourseId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QualificationCourseId)
        {
            string status;
            var response = await zmstqualifyingcourseDirector.DeleteAsync(QualificationCourseId, default).ConfigureAwait(false);

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

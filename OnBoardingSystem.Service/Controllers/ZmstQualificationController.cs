
//-----------------------------------------------------------------------
// <copyright file="ZmstQualificationController.cs" company="NIC">
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
    /// ZmstQualificationController.
    /// </summary>
    public class ZmstQualificationController : ControllerBase
    {
        private readonly IZmstQualificationDirector zmstqualificationDirector;
        private readonly ILogger<ZmstQualificationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualificationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstqualificationDirector">zmstqualificationDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstQualificationController(IZmstQualificationDirector zmstqualificationDirector, ILogger<ZmstQualificationController> logger)
        {
            this.zmstqualificationDirector = zmstqualificationDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstQualification List.
        /// </summary>
        /// <returns>Get All ZmstQualification List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualification), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualification), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstQualification>>> GetAllAsync()
        {
            return await zmstqualificationDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstQualification List By Id.
        /// </summary>
        /// <param name="QualificationId">QualificationId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualification), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstQualification), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstQualification>> GetByIdAsync(string QualificationId)
        {
            var response = await zmstqualificationDirector.GetByIdAsync(QualificationId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstQualification.
        /// </summary>
        /// <param name="zmstqualification">zmstqualification.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstQualification zmstqualification)
        {
            if (zmstqualification == null)
            {
                return BadRequest(zmstqualification);
            }

            try
            {
                var response = await zmstqualificationDirector.InsertAsync(zmstqualification, default).ConfigureAwait(false);
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
        /// Update to ZmstQualification.
        /// </summary>
        /// <param name="zmstqualification">zmstqualification.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody]  AbsModels.ZmstQualification zmstqualification)
        {
            string message = "";
            if (zmstqualification == null)
            {
                return BadRequest(nameof(zmstqualification));
            }

            if (zmstqualification.QualificationId == "0")
            {
                return BadRequest(nameof(zmstqualification.QualificationId));
            }

            try
            {
                var response = await zmstqualificationDirector.UpdateAsync(zmstqualification, default).ConfigureAwait(false);
                if (response > 0)
                {
                    message = "\"Updated Successfully\"";
                }
                else
                {
                    message = "\"Try Again\"";
                }
                return message;
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstQualification.
        /// </summary>
        /// <param name="QualificationId">QualificationId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string QualificationId)
        {
            string status;
            var response = await zmstqualificationDirector.DeleteAsync(QualificationId, default).ConfigureAwait(false);

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

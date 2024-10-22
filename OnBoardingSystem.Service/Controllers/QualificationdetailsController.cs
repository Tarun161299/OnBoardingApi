
//-----------------------------------------------------------------------
// <copyright file="qualificationDetailsController.cs" company="NIC">
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
    /// qualificationDetailsController.
    /// </summary>
    public class QualificationDetailsController : ControllerBase
    {
        private readonly IQualificationDetailsDirector qualificationdetailsDirector;
        private readonly ILogger<QualificationDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="qualificationDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="qualificationdetailsDirector">qualificationdetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public QualificationDetailsController(IQualificationDetailsDirector qualificationdetailsDirector, ILogger<QualificationDetailsController> logger)
        {
            this.qualificationdetailsDirector = qualificationdetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get qualificationDetails List.
        /// </summary>
        /// <returns>Get All qualificationDetails List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.QualificationDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.QualificationDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.QualificationDetails>>> GetAllAsync()
        {
            return await qualificationdetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get qualificationDetails List By Id.
        /// </summary>
        /// <param name="QualificationDetailsId">QualificationDetailsId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.QualificationDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.QualificationDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.QualificationDetails>> GetByIdAsync(string examCode)
        {
            var response = await qualificationdetailsDirector.GetByIdAsync(examCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert qualificationDetails.
        /// </summary>
        /// <param name="qualificationdetails">qualificationdetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.QualificationDetails qualificationdetails)
        {
            if (qualificationdetails == null)
            {
                return BadRequest(qualificationdetails);
            }

            try
            {
                var response = await qualificationdetailsDirector.InsertAsync(qualificationdetails, default).ConfigureAwait(false);
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
        /// Update to qualificationDetails.
        /// </summary>
        /// <param name="qualificationdetails">qualificationdetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody]AbsModels.QualificationDetails qualificationdetails)
        {
            if (qualificationdetails == null)
            {
                return BadRequest(nameof(qualificationdetails));
            }

            if (qualificationdetails.QualificationDetailsId == 0)
            {
                return BadRequest(nameof(qualificationdetails.QualificationDetailsId));
            }

            try
            {
                var response = await qualificationdetailsDirector.UpdateAsync(qualificationdetails, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete qualificationDetails.
        /// </summary>
        /// <param name="QualificationDetailsId">QualificationDetailsId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int QualificationDetailsId)
        {
            string status;
            var response = await qualificationdetailsDirector.DeleteAsync(QualificationDetailsId, default).ConfigureAwait(false);

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
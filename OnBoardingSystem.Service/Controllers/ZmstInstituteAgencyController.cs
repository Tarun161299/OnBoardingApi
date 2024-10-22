
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteAgencyController.cs" company="NIC">
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
    /// ZmstInstituteAgencyController.
    /// </summary>
    public class ZmstInstituteAgencyController : ControllerBase
    {
        private readonly IZmstInstituteAgencyDirector zmstinstituteagencyDirector;
        private readonly ILogger<ZmstInstituteAgencyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteAgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstinstituteagencyDirector">zmstinstituteagencyDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstInstituteAgencyController(IZmstInstituteAgencyDirector zmstinstituteagencyDirector, 
            ILogger<ZmstInstituteAgencyController> logger)
        {
            this.zmstinstituteagencyDirector = zmstinstituteagencyDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstInstituteAgency List.
        /// </summary>
        /// <returns>Get All ZmstInstituteAgency List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstInstituteAgency>>> GetAllAsync()
        {
            return await zmstinstituteagencyDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstInstituteAgency List By Id.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstInstituteAgency>> GetByIdAsync(string InstCd)
        {
            var response = await zmstinstituteagencyDirector.GetByIdAsync(InstCd, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert ZmstInstituteAgency.
        /// </summary>
        /// <param name="zmstinstituteagency">zmstinstituteagency.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstInstituteAgency zmstinstituteagency)
        {
            if (zmstinstituteagency == null)
            {
                return BadRequest(zmstinstituteagency);
            }

            try
            {
                var response = await zmstinstituteagencyDirector.InsertAsync(zmstinstituteagency, default).ConfigureAwait(false);
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
        /// Update to ZmstInstituteAgency.
        /// </summary>
        /// <param name="zmstinstituteagency">zmstinstituteagency.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstInstituteAgency zmstinstituteagency)
        {
            if (zmstinstituteagency == null)
            {
                return BadRequest(nameof(zmstinstituteagency));
            }

            if (zmstinstituteagency.InstCd == "0")
            {
                return BadRequest(nameof(zmstinstituteagency.InstCd));
            }

            try
            {
                var response = await zmstinstituteagencyDirector.UpdateAsync(zmstinstituteagency, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstInstituteAgency.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string InstCd)
        {
            string status;
            var response = await zmstinstituteagencyDirector.DeleteAsync(InstCd, default).ConfigureAwait(false);

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
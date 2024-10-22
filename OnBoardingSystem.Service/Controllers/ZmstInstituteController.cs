
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// ZmstInstituteController.
    /// </summary>
    public class ZmstInstituteController : ControllerBase
    {
        private readonly IZmstInstituteDirector zmstinstituteDirector;
        private readonly ILogger<ZmstInstituteController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstinstituteDirector">zmstinstituteDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstInstituteController(IZmstInstituteDirector zmstinstituteDirector, ILogger<ZmstInstituteController> logger)
        {
            this.zmstinstituteDirector = zmstinstituteDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstInstitute List.
        /// </summary>
        /// <returns>Get All ZmstInstitute List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstInstitute>>> GetAllAsync()
        {
            return await zmstinstituteDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstInstitute List.
        /// </summary>
        /// <returns>Get All ZmstInstitute List.</returns>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstInstitute>>> GetAllByIdsAsync([FromBody] FilterInstitute filterInstitute)
        {
            return await zmstinstituteDirector.GetAllByIdsAsync(filterInstitute, default).ConfigureAwait(false);
        }

        //GetAllByIdsAsync
        /// <summary>
        /// Get ZmstInstitute List By Id.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstInstitute>> GetByIdAsync(string InstCd)
        {
            var response = await zmstinstituteDirector.GetByIdAsync(InstCd, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstInstitute.
        /// </summary>
        /// <param name="zmstinstitute">zmstinstitute.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstInstitute zmstinstitute)
        {
            if (zmstinstitute == null)
            {
                return BadRequest(zmstinstitute);
            }

            try
            {
                var response = await zmstinstituteDirector.InsertAsync(zmstinstitute, default).ConfigureAwait(false);
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
        /// Update to ZmstInstitute.
        /// </summary>
        /// <param name="zmstinstitute">zmstinstitute.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstInstitute zmstinstitute)
        {
            if (zmstinstitute == null)
            {
                return BadRequest(nameof(zmstinstitute));
            }

            if (zmstinstitute.InstCd == "0")
            {
                return BadRequest(nameof(zmstinstitute.InstCd));
            }

            try
            {
                var response = await zmstinstituteDirector.UpdateAsync(zmstinstitute, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstInstitute.
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
            var response = await zmstinstituteDirector.DeleteAsync(InstCd, default).ConfigureAwait(false);

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
        
        /// <summary>
        /// Delete ZmstInstitute.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetMaxInstituteCode()
        {
            string status;
            var response = await zmstinstituteDirector.GetMaxIntitueCode(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        //GetAllByIdsAsync
        /// <summary>
        /// Get ZmstInstitute List By Id.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstInstitute>> GetInstituteStaticse(string Type)
        {
            var response = await zmstinstituteDirector.GetInstituteStatistics(Type, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        //GetAllByIdsAsync
        /// <summary>
        /// Get ZmstInstitute List By Id.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>Get by id.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstitute), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.FiterInstituteCount>> GetAllCountData([FromBody] FiterInstituteCount fiterInstituteCoun)
        {
            var response = await zmstinstituteDirector.GetAllCountData(fiterInstituteCoun, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
    }
}


//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteStreamController.cs" company="NIC">
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
    /// ZmstInstituteStreamController.
    /// </summary>
    public class ZmstInstituteStreamController : ControllerBase
    {
        private readonly IZmstInstituteStreamDirector zmstinstitutestreamDirector;
        private readonly ILogger<ZmstInstituteStreamController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteStreamController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstinstitutestreamDirector">zmstinstitutestreamDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstInstituteStreamController(IZmstInstituteStreamDirector zmstinstitutestreamDirector, ILogger<ZmstInstituteStreamController> logger)
        {
            this.zmstinstitutestreamDirector = zmstinstitutestreamDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstInstituteStream List.
        /// </summary>
        /// <returns>Get All ZmstInstituteStream List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstInstituteStream>>> GetAllAsync()
        {
            return await zmstinstitutestreamDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstInstituteStream List By Id.
        /// </summary>
        /// <param name="InstCd">InstCd.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteStream), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstInstituteStream), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstInstituteStream>> GetByIdAsync(string InstCd)
        {
            var response = await zmstinstitutestreamDirector.GetByIdAsync(InstCd, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstInstituteStream.
        /// </summary>
        /// <param name="zmstinstitutestream">zmstinstitutestream.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync(string instcode,[FromBody] List<AbsModels.ZmstInstituteStream> zmstinstitutestream)
        {
            if (zmstinstitutestream == null)
            {
                return BadRequest(zmstinstitutestream);
            }

            try
            {
                var response = await zmstinstitutestreamDirector.InsertAsync(instcode, zmstinstitutestream, default).ConfigureAwait(false);
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
        /// Update to ZmstInstituteStream.
        /// </summary>
        /// <param name="zmstinstitutestream">zmstinstitutestream.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(AbsModels.ZmstInstituteStream zmstinstitutestream)
        {
            if (zmstinstitutestream == null)
            {
                return BadRequest(nameof(zmstinstitutestream));
            }

            if (zmstinstitutestream.InstCd == "0")
            {
                return BadRequest(nameof(zmstinstitutestream.InstCd));
            }

            try
            {
                var response = await zmstinstitutestreamDirector.UpdateAsync(zmstinstitutestream, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstInstituteStream.
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
            var response = await zmstinstitutestreamDirector.DeleteAsync(InstCd, default).ConfigureAwait(false);

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

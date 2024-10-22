
//-----------------------------------------------------------------------
// <copyright file="ZmstNationalityController.cs" company="NIC">
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
    /// ZmstNationalityController.
    /// </summary>
    // [Route("api/[controller]")]
    //// [Route("api")]
    // [ApiController]
    public class ZmstNationalityController : ControllerBase
    {
        private readonly IZmstNationalityDirector zmstnationalityDirector;
        private readonly ILogger<ZmstNationalityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstNationalityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstnationalityDirector">zmstnationalityDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstNationalityController(IZmstNationalityDirector zmstnationalityDirector, ILogger<ZmstNationalityController> logger)
        {
            this.zmstnationalityDirector = zmstnationalityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstNationality List.
        /// </summary>
        /// <returns>Get All ZmstNationality List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstNationality), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstNationality), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
       // [Route("GetAll")]
        //[Route("list/Nationalitylist")]
        public async Task<ActionResult<List<AbsModels.ZmstNationality>>> GetAllAsync()
        {
            return await zmstnationalityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstNationality List By Id.
        /// </summary>
        /// <param name="NationalityId">NationalityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstNationality), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstNationality), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstNationality>> GetByIdAsync(string NationalityId)
        {
            var response = await zmstnationalityDirector.GetByIdAsync(NationalityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstNationality.
        /// </summary>
        /// <param name="zmstnationality">zmstnationality.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstNationality zmstnationality)
        {
            if (zmstnationality == null)
            {
                return BadRequest(zmstnationality);
            }

            try
            {
                var response = await zmstnationalityDirector.InsertAsync(zmstnationality, default).ConfigureAwait(false);
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
        /// Update to ZmstNationality.
        /// </summary>
        /// <param name="zmstnationality">zmstnationality.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
      //  [Route("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstNationality zmstnationality)
        {
            if (zmstnationality == null)
            {
                return BadRequest(nameof(zmstnationality));
            }

            if (zmstnationality.NationalityId == "0")
            {
                return BadRequest(nameof(zmstnationality.NationalityId));
            }

            try
            {
                var response = await zmstnationalityDirector.UpdateAsync(zmstnationality, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstNationality.
        /// </summary>
        /// <param name="NationalityId">NationalityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
       // [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string NationalityId)
        {
            string status;
            var response = await zmstnationalityDirector.DeleteAsync(NationalityId, default).ConfigureAwait(false);

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

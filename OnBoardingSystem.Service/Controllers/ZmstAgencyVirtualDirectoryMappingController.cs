
//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyVirtualDirectoryMappingController.cs" company="NIC">
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
    /// ZmstAgencyVirtualDirectoryMappingController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ZmstAgencyVirtualDirectoryMappingController : ControllerBase
    {
        private readonly IZmstAgencyVirtualDirectoryMappingDirector zmstagencyvirtualdirectorymappingDirector;
        private readonly ILogger<ZmstAgencyVirtualDirectoryMappingController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyVirtualDirectoryMappingController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstagencyvirtualdirectorymappingDirector">zmstagencyvirtualdirectorymappingDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstAgencyVirtualDirectoryMappingController(IZmstAgencyVirtualDirectoryMappingDirector zmstagencyvirtualdirectorymappingDirector, ILogger<ZmstAgencyVirtualDirectoryMappingController> logger)
        {
            this.zmstagencyvirtualdirectorymappingDirector = zmstagencyvirtualdirectorymappingDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstAgencyVirtualDirectoryMapping List.
        /// </summary>
        /// <returns>Get All ZmstAgencyVirtualDirectoryMapping List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyVirtualDirectoryMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyVirtualDirectoryMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstAgencyVirtualDirectoryMapping>>> GetAsync()
        {
            return await zmstagencyvirtualdirectorymappingDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstAgencyVirtualDirectoryMapping List By Id.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyVirtualDirectoryMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyVirtualDirectoryMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstAgencyVirtualDirectoryMapping>> GetAsync(string AgencyId)
        {
            var response = await zmstagencyvirtualdirectorymappingDirector.GetByIdAsync(AgencyId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="zmstagencyvirtualdirectorymapping">zmstagencyvirtualdirectorymapping.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping)
        {
            if (zmstagencyvirtualdirectorymapping == null)
            {
                return BadRequest(zmstagencyvirtualdirectorymapping);
            }

            try
            {
                var response = await zmstagencyvirtualdirectorymappingDirector.InsertAsync(zmstagencyvirtualdirectorymapping, default).ConfigureAwait(false);
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
        /// Update to ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="zmstagencyvirtualdirectorymapping">zmstagencyvirtualdirectorymapping.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
        public async Task<ActionResult> PutAsync(AbsModels.ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping)
        {
            if (zmstagencyvirtualdirectorymapping == null)
            {
                return BadRequest(nameof(zmstagencyvirtualdirectorymapping));
            }

            if (zmstagencyvirtualdirectorymapping.AgencyId == "0")
            {
                return BadRequest(nameof(zmstagencyvirtualdirectorymapping.AgencyId));
            }

            try
            {
                var response = await zmstagencyvirtualdirectorymappingDirector.UpdateAsync(zmstagencyvirtualdirectorymapping, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string AgencyId)
        {
            string status;
            var response = await zmstagencyvirtualdirectorymappingDirector.DeleteAsync(AgencyId, default).ConfigureAwait(false);

            if (response > 0)
            {
                status = "\"Delete Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return status;
        }
    }
}

    
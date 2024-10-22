//-----------------------------------------------------------------------
// <copyright file="MdOrganizationController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// MdOrganizationController.
    /// </summary>
    public class MdOrganizationController : ControllerBase
    {
        /// <summary>
        /// MdOrganizationController.
        /// </summary>
        private readonly IMdOrganizationDirector mdorganizationDirector;
        private readonly ILogger<MdOrganizationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdOrganizationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdorganizationDirector">mdorganizationDirector.</param>
        /// <param name="logger">logger.</param>
        public MdOrganizationController(IMdOrganizationDirector mdorganizationDirector, ILogger<MdOrganizationController> logger)
        {
            this.mdorganizationDirector = mdorganizationDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetAllAsync()
        {
            return await mdorganizationDirector.GetAllAsync(default).ConfigureAwait(false);

        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="id">id.</param>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetByIdAsync(string id)
        {
            return await mdorganizationDirector.GetByStateIdAsync(id, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="stateId">stateId.</param>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetByStateIdAsync(string stateId)
        {
            return await mdorganizationDirector.GetByStateIdAsync(stateId, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Insert MdOrganization.
        /// </summary>
        /// <param name="mdorganization">mdorganization.</param>
        /// <returns>Effected Row.</returns>

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdOrganization mdorganization)
        {
            if (mdorganization == null)
            {
                return BadRequest(mdorganization);
            }

            try
            {
                var response = await mdorganizationDirector.InsertAsync(mdorganization, default).ConfigureAwait(false);
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
        /// Update to MdOrganization.
        /// </summary>
        /// <param name="mdorganization">mdorganization.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdOrganization mdorganization)
        {
            if (mdorganization == null)
            {
                return BadRequest(nameof(mdorganization));
            }

            if (mdorganization.OrganizationId == "0")
            {
                return BadRequest(nameof(mdorganization.OrganizationId));
            }

            try
            {
                string status;
                var response = await mdorganizationDirector.UpdateAsync(mdorganization, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Updated Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdOrganization.
        /// </summary>
        /// <param name="OrganizationId">OrganizationId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string OrganizationId)
        {
            string status;
            var response = await mdorganizationDirector.DeleteAsync(OrganizationId, default).ConfigureAwait(false);

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
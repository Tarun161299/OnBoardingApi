
//-----------------------------------------------------------------------
// <copyright file="MdRoleController.cs" company="NIC">
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
    /// MdRoleController.
    /// </summary>
    public class MdRoleController : ControllerBase
    {
        private readonly IMdRoleDirector mdroleDirector;
        private readonly ILogger<MdRoleController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdRoleController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdroleDirector">mdroleDirector.</param>
        /// <param name="logger">logger.</param>
        public MdRoleController(IMdRoleDirector mdroleDirector, ILogger<MdRoleController> logger)
        {
            this.mdroleDirector = mdroleDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdRole List.
        /// </summary>
        /// <returns>Get All MdRole List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdRole>>> GetAllAsync()
        {
            return await mdroleDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdRole List By Id.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdRole>> GetByIdAsync(string RoleId)
        {
            var response = await mdroleDirector.GetByIdAsync(RoleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdRole mdrole)
        {
            if (mdrole == null)
            {
                return BadRequest(mdrole);
            }

            try
            {
                var response = await mdroleDirector.InsertAsync(mdrole, default).ConfigureAwait(false);
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
        /// Update to MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdRole mdrole)
        {
            if (mdrole == null)
            {
                return BadRequest(nameof(mdrole));
            }

            if (mdrole.RoleId == "0")
            {
                return BadRequest(nameof(mdrole.RoleId));
            }

            try
            {
				var response = await mdroleDirector.UpdateAsync(mdrole, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdRole.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string RoleId)
        {
            string status;
            var response = await mdroleDirector.DeleteAsync(RoleId, default).ConfigureAwait(false);

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
	

//-----------------------------------------------------------------------
// <copyright file="AppUserRoleMappingController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// AppUserRoleMappingController.
    /// </summary>
    public class AppUserRoleMappingController : ControllerBase
    {
        private readonly IAppUserRoleMappingDirector appuserrolemappingDirector;
        private readonly ILogger<AppUserRoleMappingController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRoleMappingController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appuserrolemappingDirector">appuserrolemappingDirector.</param>
        /// <param name="logger">logger.</param>
        public AppUserRoleMappingController(IAppUserRoleMappingDirector appuserrolemappingDirector, ILogger<AppUserRoleMappingController> logger)
        {
            this.appuserrolemappingDirector = appuserrolemappingDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppUserRoleMapping List.
        /// </summary>
        /// <returns>Get All AppUserRoleMapping List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppUserRoleMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppUserRoleMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppUserRoleMapping>>> GetAllAsync()
        {
            return await appuserrolemappingDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppUserRoleMapping List By Id.
        /// </summary>
        /// <param name="UserID">UserID.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppUserRoleMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppUserRoleMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppUserRoleMapping>> GetByIdAsync(string UserID)
        {
            var response = await appuserrolemappingDirector.GetByIdAsync(UserID, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppUserRoleMapping.
        /// </summary>
        /// <param name="appuserrolemapping">appuserrolemapping.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] List<AbsModels.AppUserRoleMapping> appuserrolemapping,string userroleid)
        {
            string status;

            try
            {
                var response = await appuserrolemappingDirector.InsertAsync(appuserrolemapping, userroleid, default).ConfigureAwait(false);

                if (response > 0)
                {
                    status = "\"Data Stored Successfully\"";
                }
                else
                {
                    status = "\"Try Again\""; 
                }

               return response > 0 ? Created(string.Empty, status) : Ok(status);

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
        /// Update to AppUserRoleMapping.
        /// </summary>
        /// <param name="appuserrolemapping">appuserrolemapping.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.AppUserRoleMapping appuserrolemapping)
        {
            if (appuserrolemapping == null)
            {
                return BadRequest(nameof(appuserrolemapping));
            }

            if (appuserrolemapping.UserId == "0")
            {
                return BadRequest(nameof(appuserrolemapping.UserId));
            }

            try
            {
				var response = await appuserrolemappingDirector.UpdateAsync(appuserrolemapping, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppUserRoleMapping.
        /// </summary>
        /// <param name="UserID">UserID.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string UserID)
        {
            string status;
            var response = await appuserrolemappingDirector.DeleteAsync(UserID, default).ConfigureAwait(false);

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
	
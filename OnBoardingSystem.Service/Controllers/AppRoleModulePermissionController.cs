
//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermissionController.cs" company="NIC">
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
    /// AppRoleModulePermissionController.
    /// </summary>
    public class AppRoleModulePermissionController : ControllerBase
    {
        private readonly IAppRoleModulePermissionDirector approlemodulepermissionDirector;
        private readonly ILogger<AppRoleModulePermissionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRoleModulePermissionController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="approlemodulepermissionDirector">approlemodulepermissionDirector.</param>
        /// <param name="logger">logger.</param>
        public AppRoleModulePermissionController(IAppRoleModulePermissionDirector approlemodulepermissionDirector, ILogger<AppRoleModulePermissionController> logger)
        {
            this.approlemodulepermissionDirector = approlemodulepermissionDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppRoleModulePermission List.
        /// </summary>
        /// <returns>Get All AppRoleModulePermission List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppRoleModulePermission>>> GetAllAsync()
        {
            return await approlemodulepermissionDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppRoleModulePermission List By Id.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppRoleModulePermission>> GetByIdAsync(string RoleId)
        {
            var response = await approlemodulepermissionDirector.GetByIdAsync(RoleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] List<AbsModels.AppRoleModulePermission> appRoleModulePermission,string roleid)
        {
            string status;

            try
            {
                var response = await approlemodulepermissionDirector.InsertAsync(appRoleModulePermission, roleid, default).ConfigureAwait(false);

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
        /// Update to AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.AppRoleModulePermission approlemodulepermission)
        {
            if (approlemodulepermission == null)
            {
                return BadRequest(nameof(approlemodulepermission));
            }

            if (approlemodulepermission.RoleId == "0")
            {
                return BadRequest(nameof(approlemodulepermission.RoleId));
            }

            try
            {
				var response = await approlemodulepermissionDirector.UpdateAsync(approlemodulepermission, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppRoleModulePermission.
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
            var response = await approlemodulepermissionDirector.DeleteAsync(RoleId, default).ConfigureAwait(false);

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
	
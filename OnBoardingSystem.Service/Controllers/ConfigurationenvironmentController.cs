
//-----------------------------------------------------------------------
// <copyright file="ConfigurationEnvironmentController.cs" company="NIC">
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
    /// ConfigurationEnvironmentController.
    /// </summary>
    public class ConfigurationEnvironmentController : ControllerBase
    {
        private readonly IConfigurationEnvironmentDirector configurationenvironmentDirector;
        private readonly ILogger<ConfigurationEnvironmentController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationEnvironmentController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="configurationenvironmentDirector">configurationenvironmentDirector.</param>
        /// <param name="logger">logger.</param>
        public ConfigurationEnvironmentController(IConfigurationEnvironmentDirector configurationenvironmentDirector, ILogger<ConfigurationEnvironmentController> logger)
        {
            this.configurationenvironmentDirector = configurationenvironmentDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ConfigurationEnvironment List.
        /// </summary>
        /// <returns>Get All ConfigurationEnvironment List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ConfigurationEnvironment), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ConfigurationEnvironment), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ConfigurationEnvironment>>> GetAllAsync()
        {
            return await configurationenvironmentDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ConfigurationEnvironment List By Id.
        /// </summary>
        /// <param name="ApplicationId">ApplicationId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ConfigurationEnvironment), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ConfigurationEnvironment), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ConfigurationEnvironment>> GetByIdAsync(int ApplicationId)
        {
            var response = await configurationenvironmentDirector.GetByIdAsync(ApplicationId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ConfigurationEnvironment.
        /// </summary>
        /// <param name="configurationenvironment">configurationenvironment.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ConfigurationEnvironment configurationenvironment)
        {
            if (configurationenvironment == null)
            {
                return BadRequest(configurationenvironment);
            }

            try
            {
                var response = await configurationenvironmentDirector.InsertAsync(configurationenvironment, default).ConfigureAwait(false);
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
        /// Update to ConfigurationEnvironment.
        /// </summary>
        /// <param name="configurationenvironment">configurationenvironment.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ConfigurationEnvironment configurationenvironment)
        {
            if (configurationenvironment == null)
            {
                return BadRequest(nameof(configurationenvironment));
            }

            if (configurationenvironment.ApplicationId == 0)
            {
                return BadRequest(nameof(configurationenvironment.ApplicationId));
            }

            try
            {
                var response = await configurationenvironmentDirector.UpdateAsync(configurationenvironment, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ConfigurationEnvironment.
        /// </summary>
        /// <param name="ApplicationId">ApplicationId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int ApplicationId)
        {
            string status;
            var response = await configurationenvironmentDirector.DeleteAsync(ApplicationId, default).ConfigureAwait(false);

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

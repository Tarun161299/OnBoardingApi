
//-----------------------------------------------------------------------
// <copyright file="MdSmsEmailTemplateController.cs" company="NIC">
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
    /// MdSmsEmailTemplateController.
    /// </summary>
    public class MdSmsEmailTemplateController : ControllerBase
    {
        private readonly IMdSmsEmailTemplateDirector mdsmsemailtemplateDirector;
        private readonly ILogger<MdSmsEmailTemplateController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdSmsEmailTemplateController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdsmsemailtemplateDirector">mdsmsemailtemplateDirector.</param>
        /// <param name="logger">logger.</param>
        public MdSmsEmailTemplateController(IMdSmsEmailTemplateDirector mdsmsemailtemplateDirector, ILogger<MdSmsEmailTemplateController> logger)
        {
            this.mdsmsemailtemplateDirector = mdsmsemailtemplateDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdSmsEmailTemplate List.
        /// </summary>
        /// <returns>Get All MdSmsEmailTemplate List.</returns>   

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdSmsEmailTemplate), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdSmsEmailTemplate), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdSmsEmailTemplate>>> GetAllAsync()
        {
            return await mdsmsemailtemplateDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdSmsEmailTemplate List By Id.
        /// </summary>
        /// <param name="TemplateId">TemplateId.</param>
        /// <returns>Get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdSmsEmailTemplate), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdSmsEmailTemplate), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdSmsEmailTemplate>> GetByIdAsync(string TemplateId)
        {
            var response = await mdsmsemailtemplateDirector.GetByIdAsync(TemplateId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdSmsEmailTemplate.
        /// </summary>
        /// <param name="mdsmsemailtemplate">mdsmsemailtemplate.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdSmsEmailTemplate mdsmsemailtemplate)
        {
            if (mdsmsemailtemplate == null)
            {
                return BadRequest(mdsmsemailtemplate);
            }

            try
            {
                var response = await mdsmsemailtemplateDirector.InsertAsync(mdsmsemailtemplate, default).ConfigureAwait(false);
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
        /// Update to MdSmsEmailTemplate.
        /// </summary>
        /// <param name="mdsmsemailtemplate">mdsmsemailtemplate.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdSmsEmailTemplate mdsmsemailtemplate)
        {
            if (mdsmsemailtemplate == null)
            {
                return BadRequest(nameof(mdsmsemailtemplate));
            }

            if (mdsmsemailtemplate.TemplateId == "0")
            {
                return BadRequest(nameof(mdsmsemailtemplate.TemplateId));
            }

            try
            {
                var response = await mdsmsemailtemplateDirector.UpdateAsync(mdsmsemailtemplate, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdSmsEmailTemplate.
        /// </summary>
        /// <param name="TemplateId">TemplateId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string TemplateId)
        {
            string status;
            var response = await mdsmsemailtemplateDirector.DeleteAsync(TemplateId, default).ConfigureAwait(false);

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
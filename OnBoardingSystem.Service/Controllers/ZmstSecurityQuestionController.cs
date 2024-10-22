
//-----------------------------------------------------------------------
// <copyright file="ZmstSecurityQuestionController.cs" company="NIC">
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
    /// ZmstSecurityQuestionController.
    /// </summary>
    public class ZmstSecurityQuestionController : ControllerBase
    {
        private readonly IZmstSecurityQuestionDirector zmstsecurityquestionDirector;
        private readonly ILogger<ZmstSecurityQuestionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSecurityQuestionController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstsecurityquestionDirector">zmstsecurityquestionDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSecurityQuestionController(IZmstSecurityQuestionDirector zmstsecurityquestionDirector, ILogger<ZmstSecurityQuestionController> logger)
        {
            this.zmstsecurityquestionDirector = zmstsecurityquestionDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSecurityQuestion List.
        /// </summary>
        /// <returns>Get All ZmstSecurityQuestion List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSecurityQuestion), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSecurityQuestion), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSecurityQuestion>>> GetAllAsync()
        {
            return await zmstsecurityquestionDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstSecurityQuestion List By Id.
        /// </summary>
        /// <param name="SecurityQuesId">SecurityQuesId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSecurityQuestion), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSecurityQuestion), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSecurityQuestion>> GetByIdAsync(string SecurityQuesId)
        {
            var response = await zmstsecurityquestionDirector.GetByIdAsync(SecurityQuesId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstSecurityQuestion.
        /// </summary>
        /// <param name="zmstsecurityquestion">zmstsecurityquestion.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSecurityQuestion zmstsecurityquestion)
        {
            if (zmstsecurityquestion == null)
            {
                return BadRequest(zmstsecurityquestion);
            }

            try
            {
                var response = await zmstsecurityquestionDirector.InsertAsync(zmstsecurityquestion, default).ConfigureAwait(false);
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
        /// Update to ZmstSecurityQuestion.
        /// </summary>
        /// <param name="zmstsecurityquestion">zmstsecurityquestion.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSecurityQuestion zmstsecurityquestion)
        {
            if (zmstsecurityquestion == null)
            {
                return BadRequest(nameof(zmstsecurityquestion));
            }

            if (zmstsecurityquestion.SecurityQuesId == "0")
            {
                return BadRequest(nameof(zmstsecurityquestion.SecurityQuesId));
            }

            try
            {
				var response = await zmstsecurityquestionDirector.UpdateAsync(zmstsecurityquestion, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstSecurityQuestion.
        /// </summary>
        /// <param name="SecurityQuesId">SecurityQuesId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SecurityQuesId)
        {
            string status;
            var response = await zmstsecurityquestionDirector.DeleteAsync(SecurityQuesId, default).ConfigureAwait(false);

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
	
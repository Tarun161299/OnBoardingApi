
//-----------------------------------------------------------------------
// <copyright file="AppLoginDetailsController.cs" company="NIC">
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
    /// AppLoginDetailsController.
    /// </summary>
    public class AppLoginDetailsController : ControllerBase
    {
        private readonly IAppLoginDetailsDirector applogindetailsDirector;
        private readonly ILogger<AppLoginDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLoginDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="applogindetailsDirector">applogindetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public AppLoginDetailsController(IAppLoginDetailsDirector applogindetailsDirector, ILogger<AppLoginDetailsController> logger)
        {
            this.applogindetailsDirector = applogindetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppLoginDetails List.
        /// </summary>
        /// <returns>Get All AppLoginDetails List.</returns>
        /// 

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<List<AbsModels.AppLoginDetails>>> GetAllAsync()
        //{
        //    return await applogindetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppLoginDetails>>> GetAllPmuUsersAsync()
        {
            return await applogindetailsDirector.GetAllPmuUsersAsync(default).ConfigureAwait(false);
        }
        /// <summary>
        /// Get AppLoginDetails List By Id.
        /// </summary>
        /// <param name="RequestNo">RequestNo.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppLoginDetails>> GetByIdAsync(string UserID)
        {
            var response = await applogindetailsDirector.GetByIdAsync(UserID, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.AppLoginDetails applogindetails)
        {
            if (applogindetails == null)
            {
                return BadRequest(applogindetails);
            }

            try
            {
                var response = await applogindetailsDirector.InsertAsync(applogindetails, default).ConfigureAwait(false);
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
        /// Update to AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.AppLoginDetails applogindetails)
        {
            if (applogindetails == null)
            {
                return BadRequest(nameof(applogindetails));
            }

            if (applogindetails.RequestNo == "0")
            {
                return BadRequest(nameof(applogindetails.RequestNo));
            }

            try
            {
				var response = await applogindetailsDirector.UpdateAsync(applogindetails, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppLoginDetails.
        /// </summary>
        /// <param name="RequestNo">RequestNo.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string RequestNo)
        {
            string status;
            var response = await applogindetailsDirector.DeleteAsync(RequestNo, default).ConfigureAwait(false);

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
	
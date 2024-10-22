
//-----------------------------------------------------------------------
// <copyright file="AppProjectPaymentDetailsController.cs" company="NIC">
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
    /// AppProjectPaymentDetailsController.
    /// </summary>
    public class AppProjectPaymentDetailsController : ControllerBase
    {
        private readonly IAppProjectPaymentDetailsDirector appprojectpaymentdetailsDirector;
        private readonly ILogger<AppProjectPaymentDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectPaymentDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appprojectpaymentdetailsDirector">appprojectpaymentdetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public AppProjectPaymentDetailsController(IAppProjectPaymentDetailsDirector appprojectpaymentdetailsDirector, ILogger<AppProjectPaymentDetailsController> logger)
        {
            this.appprojectpaymentdetailsDirector = appprojectpaymentdetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppProjectPaymentDetails List.
        /// </summary>
        /// <returns>Get All AppProjectPaymentDetails List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppProjectPaymentDetails>>> GetAllAsync()
        {
            return await appprojectpaymentdetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get AppProjectPaymentDetails List By Id.
        /// </summary>
        /// <param name="PaymentId">PaymentId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppProjectPaymentDetails>> GetByIdAsync(int PaymentId)
        {
            var response = await appprojectpaymentdetailsDirector.GetByIdAsync(PaymentId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get AppProjectPaymentDetails List By Id.
        /// </summary>
        /// <param name="PaymentParentRefId">PaymentId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppProjectPaymentDetails>> GetByPaymentIdAsync(string PaymentParentRefId)
        {
            var response = await appprojectpaymentdetailsDirector.GetByPaymentParentRefIdAsync(PaymentParentRefId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails),StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails),StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> InsertAsync([FromBody] AbsModels.AppProjectPaymentDetails appprojectpaymentdetails)
        {
            if (appprojectpaymentdetails == null)
            {
                return BadRequest(appprojectpaymentdetails);
            }

            try
            {
                var response = await appprojectpaymentdetailsDirector.InsertAsync(appprojectpaymentdetails, default).ConfigureAwait(false);
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
        /// Update to AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.AppProjectPaymentDetails appprojectpaymentdetails)
        {
            if (appprojectpaymentdetails == null)
            {
                return BadRequest(nameof(appprojectpaymentdetails));
            }

            if (appprojectpaymentdetails.PaymentId == 0)
            {
                return BadRequest(nameof(appprojectpaymentdetails.PaymentId));
            }

            try
            {
                var response = await appprojectpaymentdetailsDirector.UpdateAsync(appprojectpaymentdetails, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppProjectPaymentDetails.
        /// </summary>
        /// <param name="PaymentId">PaymentId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int PaymentId)
        {
            string status;
            var response = await appprojectpaymentdetailsDirector.DeleteAsync(PaymentId, default).ConfigureAwait(false);

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

        /// <summary>
        /// Insert AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(AbsModels.AppProjectPaymentDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateStatusAsync([FromBody] AbsModels.AppProjectPaymentDetails appprojectpaymentdetails)
        {
            if (appprojectpaymentdetails == null)
            {
                return BadRequest(appprojectpaymentdetails);
            }

            try
            {
                var response = await appprojectpaymentdetailsDirector.UpdateStatusAsync(appprojectpaymentdetails, default).ConfigureAwait(false);
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
    }
}

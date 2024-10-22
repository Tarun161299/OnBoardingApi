//-----------------------------------------------------------------------
// <copyright file="ApplicationSummaryController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure;
    using Azure.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    //using OnBoardingSystem.Data.EF.Models;
    using Serilog;

    public class ApplicationSummaryController : ControllerBase
    {
        private readonly IApplicationSummaryDirector iZmst_AppSummary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyExamCounsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="_iZmstAgencyExamCounsDirector">IMdMinistry.</param>
        public ApplicationSummaryController(IApplicationSummaryDirector _iZmst_AppSummary)
        {
            this.iZmst_AppSummary = _iZmst_AppSummary;

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
        public async Task<ActionResult<string>> InsertAsync([FromBody] ApplicationSummary mdsmsemailtemplate)
        {
            string message;
            if (mdsmsemailtemplate == null)
            {
                return BadRequest(mdsmsemailtemplate);
            }

            try
            {
                var response = await iZmst_AppSummary.InsertAsync(mdsmsemailtemplate, default).ConfigureAwait(false);
                if (response > 0)
                {
                    message = "\"Data Stored Successfully\"";
                }
                else
                {
                    message = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, message) : Ok(response);
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
        /// Get App_ProjectDetails List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ApplicationSummary>>> GetAllAsync()
        {
            return await iZmst_AppSummary.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes Rule.
        /// </summary>
        /// <param name="zmstApplicationSummary","projectCode">ministrytId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync([FromBody] ApplicationSummary zmstApplicationSummary)
        {
            string status;
            var response = await iZmst_AppSummary.DeleteAsync(zmstApplicationSummary, default).ConfigureAwait(false);

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
        /// Update to ZmstApplicationSummary.
        /// </summary>
        /// <param name="ZmstApplicationSummary">ZmstApplicationSummary.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] ApplicationSummary zmstApplicationSummary)
        {
            if (zmstApplicationSummary == null)
            {
                return BadRequest(nameof(zmstApplicationSummary));
            }

            if (zmstApplicationSummary.AppId == "")
            {
                return BadRequest(nameof(zmstApplicationSummary.AppId));
            }

            try
            {
                string status;
                var response = await iZmst_AppSummary.UpdateAsync(zmstApplicationSummary, default).ConfigureAwait(false);
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
        /// Update All to ZmstApplicationSummary.
        /// </summary>
        /// <param name="ZmstApplicationSummary">ZmstApplicationSummary.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAllAsync([FromBody] List<ApplicationSummary> zmstApplicationSummary)
        {
            try
            {
                string status;
                var response = 0;
                foreach (var zmstapp in zmstApplicationSummary)
                {
                    try
                    {
                        response = await iZmst_AppSummary.UpdateAsync(zmstapp, default).ConfigureAwait(false);
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }
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
        /// Get App_ProjectDetails List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ApplicationSummary>>> GetAllRegistrationList()
        {
            return await iZmst_AppSummary.GetAllRegistrationAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Update to ZmstApplicationSummary.
        /// </summary>
        /// <param name="ZmstApplicationSummary">ZmstApplicationSummary.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateExamManagementServiceAsync([FromBody] ApplicationSummary zmstApplicationSummary)
        {
            if (zmstApplicationSummary == null)
            {
                return BadRequest(nameof(zmstApplicationSummary));
            }

            if (zmstApplicationSummary.AppId == "")
            {
                return BadRequest(nameof(zmstApplicationSummary.AppId));
            }

            try
            {
                string status;
                var response = await iZmst_AppSummary.UpdateExamManagementServiceAsync(zmstApplicationSummary, default).ConfigureAwait(false);
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
        /// Update to ZmstApplicationSummary.
        /// </summary>
        /// <param name="ZmstApplicationSummary">ZmstApplicationSummary.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAllExamManagementServiceAsync([FromBody] List<ApplicationSummary> zmstApplicationSummary)
        {
            if (zmstApplicationSummary == null)
            {
                return BadRequest(nameof(zmstApplicationSummary));
            }
            try
            {
                string status;
                var response = 0;
                foreach (var zmstapplication in zmstApplicationSummary)
                {

                    response = await iZmst_AppSummary.UpdateExamManagementServiceAsync(zmstapplication, default).ConfigureAwait(false);
                }
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
        /// Get App_ProjectDetails List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CounsellingAndAdmissionSystemSummary>>> GetAppSummaryByCounsAsync()
        {
            return await iZmst_AppSummary.GetAppSummaryByCouns(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get App_ProjectDetails List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSummary), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CounsellingAndAdmissionSystemSummary>>> GetAppSummaryByRegstAsync()
        {
            return await iZmst_AppSummary.GetAppSummaryByRegst(default).ConfigureAwait(false);
        }

    }
}

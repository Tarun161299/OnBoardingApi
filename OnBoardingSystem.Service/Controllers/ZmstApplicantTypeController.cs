
//-----------------------------------------------------------------------
// <copyright file="ZmstApplicantTypeController.cs" company="NIC">
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
    /// ZmstApplicantTypeController.
    /// </summary>
    public class ZmstApplicantTypeController : ControllerBase
    {
        private readonly IZmstApplicantTypeDirector zmstapplicanttypeDirector;
        private readonly ILogger<ZmstApplicantTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstApplicantTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstapplicanttypeDirector">zmstapplicanttypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstApplicantTypeController(IZmstApplicantTypeDirector zmstapplicanttypeDirector, ILogger<ZmstApplicantTypeController> logger)
        {
            this.zmstapplicanttypeDirector = zmstapplicanttypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstApplicantType List.
        /// </summary>
        /// <returns>Get All ZmstApplicantType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstApplicantType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstApplicantType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstApplicantType>>> GetAllAsync()
        {
            return await zmstapplicanttypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstApplicantType List By Id.
        /// </summary>
        /// <param name="ID">ID.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstApplicantType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstApplicantType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstApplicantType>> GetByIdAsync(int ID)
        {
            var response = await zmstapplicanttypeDirector.GetByIdAsync(ID, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstApplicantType.
        /// </summary>
        /// <param name="zmstapplicanttype">zmstapplicanttype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstApplicantType zmstapplicanttype)
        {
            if (zmstapplicanttype == null)
            {
                return BadRequest(zmstapplicanttype);
            }

            try
            {
                var response = await zmstapplicanttypeDirector.InsertAsync(zmstapplicanttype, default).ConfigureAwait(false);
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
        /// Update to ZmstApplicantType.
        /// </summary>
        /// <param name="zmstapplicanttype">zmstapplicanttype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstApplicantType zmstapplicanttype)
        {
            if (zmstapplicanttype == null)
            {
                return BadRequest(nameof(zmstapplicanttype));
            }

            if (zmstapplicanttype.ID == 0)
            {
                return BadRequest(nameof(zmstapplicanttype.ID));
            }

            try
            {
                var response = await zmstapplicanttypeDirector.UpdateAsync(zmstapplicanttype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstApplicantType.
        /// </summary>
        /// <param name="ID">ID.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int ID)
        {
            string status;
            var response = await zmstapplicanttypeDirector.DeleteAsync(ID, default).ConfigureAwait(false);

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

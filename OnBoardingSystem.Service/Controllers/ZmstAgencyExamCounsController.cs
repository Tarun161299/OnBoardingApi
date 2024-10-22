//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyExamCouns.cs" company="NIC">
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
    using OnBoardingSystem.Data.Business.Behaviors;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    public class ZmstAgencyExamCounsController : ControllerBase
    {
        private readonly IZmstAgencyExamCounsDirector iZmstAgencyExamCounsDirector;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyExamCounsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="_iZmstAgencyExamCounsDirector">IMdMinistry.</param>
        public ZmstAgencyExamCounsController(IZmstAgencyExamCounsDirector _iZmstAgencyExamCounsDirector)
        {
            this.iZmstAgencyExamCounsDirector = _iZmstAgencyExamCounsDirector;

        }
        /// <summary>
        /// Get ZmstAgencyExamCouns List.
        /// </summary>
        /// <returns>Get All ZmstAgencyExamCouns List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyExamCouns), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyExamCouns), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstAgencyExamCouns>>> GetAllAsync()
        {
            return await iZmstAgencyExamCounsDirector.GetAllAsync(default).ConfigureAwait(false);
        }
        /// <summary>
        /// Get ZmstAgencyExamCouns List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyExamCouns), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstAgencyExamCouns), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstAgencyExamCouns>>> GetbyidAsync(int agencyid)
        {
            return await iZmstAgencyExamCounsDirector.GetByIdAsync(agencyid,default).ConfigureAwait(false);
        }
        /// <summary>
        /// Insert ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="zmstagencyexamcouns">zmstagencyexamcouns.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstAgencyExamCouns zmstagencyexamcouns)
        {
            if (zmstagencyexamcouns == null)
            {
                return BadRequest(zmstagencyexamcouns);
            }

            try
            {
                var response = await iZmstAgencyExamCounsDirector.InsertAsync(zmstagencyexamcouns, default).ConfigureAwait(false);
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
        /// Update to ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="zmstagencyexamcouns">zmstagencyexamcouns.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstAgencyExamCouns zmstagencyexamcouns)
        {
            string message = "";
            if (zmstagencyexamcouns == null)
            {
                return BadRequest(nameof(zmstagencyexamcouns));
            }

            if (zmstagencyexamcouns.AgencyId == 0)
            {
                return BadRequest(nameof(zmstagencyexamcouns.AgencyId));
            }

            try
            {
                var response = await iZmstAgencyExamCounsDirector.UpdateAsync(zmstagencyexamcouns, default).ConfigureAwait(false);
                if(response > 0)
                {
                    message = "\"Updated Successfully\"";
                }
                else
                {
                    message = "\"Try Again\"";
                }
                return message;
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int AgencyId)
        {
            string status;
            var response = await iZmstAgencyExamCounsDirector.DeleteAsync(AgencyId, default).ConfigureAwait(false);

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

//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
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
    public class ZmstWillingnessController : ControllerBase
    {
        private readonly IZmstWillingnessDirector iZmstWillingnessDirector;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public ZmstWillingnessController(IZmstWillingnessDirector IZmstWillingnessDirector)
        {
            this.iZmstWillingnessDirector = IZmstWillingnessDirector;
        }

        /// <summary>
        /// Get ZmstServiceType List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstWillingness), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstWillingness), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstWillingness>>> GetAllAsync()
        {
            return await iZmstWillingnessDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Save AppOnboarding Request Data.
        /// </summary>
        /// <param name="zmstWillingness">appOnboardingRequest.</param>
        /// <returns>AppOnboardingRequest.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.ZmstWillingness), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.ZmstWillingness), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] Data.Abstractions.Models.ZmstWillingness zmstWillingness)
        {  
            if (zmstWillingness == null)
            {
                return BadRequest(zmstWillingness);
            }

            try
            {
                var response = await iZmstWillingnessDirector.InsertAsync(zmstWillingness, default).ConfigureAwait(false);
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] Data.Abstractions.Models.ZmstWillingness zmstWillingness)
        {
            string status = "";
            if (zmstWillingness == null)
            {
                return BadRequest(nameof(zmstWillingness));
            }

            if (zmstWillingness.WillingnessId == "0")
            {
                return BadRequest(nameof(zmstWillingness.WillingnessId));
            }

            try
            {
                var response = await iZmstWillingnessDirector.UpdateAsync( zmstWillingness, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Updated Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? this.Ok(status) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
        /// <summary>
        /// Delete MdDistrict.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

       // [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await iZmstWillingnessDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
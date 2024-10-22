
//-----------------------------------------------------------------------
// <copyright file="ZmstCountryController.cs" company="NIC">
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
    /// ZmstCountryController.
    /// </summary>
    public class ZmstCountryController : ControllerBase
    {
        private readonly IZmstCountryDirector zmstcountryDirector;
        private readonly ILogger<ZmstCountryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCountryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstcountryDirector">zmstcountryDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstCountryController(IZmstCountryDirector zmstcountryDirector, ILogger<ZmstCountryController> logger)
        {
            this.zmstcountryDirector = zmstcountryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstCountry List.
        /// </summary>
        /// <returns>Get All ZmstCountry List.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCountry), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCountry), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("list/CountryList")]
        public async Task<ActionResult<List<AbsModels.ZmstCountry>>> GetAllAsync()
        {
            return await zmstcountryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstCountry List By Id.
        /// </summary>
        /// <param name="Code">Code.</param>
        /// <returns>Get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCountry), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCountry), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstCountry>> GetByIdAsync(string Code)
        {
            var response = await zmstcountryDirector.GetByIdAsync(Code, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstCountry.
        /// </summary>
        /// <param name="zmstcountry">zmstcountry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstCountry zmstcountry)
        {
            if (zmstcountry == null)
            {
                return BadRequest(zmstcountry);
            }

            try
            {
                var response = await zmstcountryDirector.InsertAsync(zmstcountry, default).ConfigureAwait(false);
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
        /// Update to ZmstCountry.
        /// </summary>
        /// <param name="zmstcountry">zmstcountry.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstCountry zmstcountry)
        {
            if (zmstcountry == null)
            {
                return BadRequest(nameof(zmstcountry));
            }

            if (zmstcountry.Code == "0")
            {
                return BadRequest(nameof(zmstcountry.Code));
            }

            try
            {
                string status = "";
                var response = await zmstcountryDirector.UpdateAsync(zmstcountry, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Updated Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
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
        /// Delete ZmstCountry.
        /// </summary>
        /// <param name="Code">Code.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Code)
        {
            string status;
            var response = await zmstcountryDirector.DeleteAsync(Code, default).ConfigureAwait(false);

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
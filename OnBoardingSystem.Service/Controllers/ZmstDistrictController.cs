
//-----------------------------------------------------------------------
// <copyright file="ZmstDistrictController.cs" company="NIC">
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
    /// ZmstDistrictController.
    /// </summary>
    public class ZmstDistrictController : ControllerBase
    {
        private readonly IZmstDistrictDirector zmstdistrictDirector;
        private readonly ILogger<ZmstDistrictController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstDistrictController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstdistrictDirector">zmstdistrictDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstDistrictController(IZmstDistrictDirector zmstdistrictDirector, ILogger<ZmstDistrictController> logger)
        {
            this.zmstdistrictDirector = zmstdistrictDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstDistrict List.
        /// </summary>
        /// <returns>Get All ZmstDistrict List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstDistrict), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstDistrict), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstDistrict>>> GetAllAsync()
        {
            return await zmstdistrictDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstDistrict List By Id.
        /// </summary>
        /// <param name="StateId">StateId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstDistrict), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstDistrict), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstDistrict>> GetByIdAsync(string StateId)
        {
            var response = await zmstdistrictDirector.GetByIdAsync(StateId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstDistrict.
        /// </summary>
        /// <param name="zmstdistrict">zmstdistrict.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstDistrict zmstdistrict)
        {
            if (zmstdistrict == null)
            {
                return BadRequest(zmstdistrict);
            }

            try
            {
                var response = await zmstdistrictDirector.InsertAsync(zmstdistrict, default).ConfigureAwait(false);
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
        /// Update to ZmstDistrict.
        /// </summary>
        /// <param name="zmstdistrict">zmstdistrict.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstDistrict zmstdistrict)
        {
            if (zmstdistrict == null)
            {
                return BadRequest(nameof(zmstdistrict));
            }

            if (zmstdistrict.StateId == "0")
            {
                return BadRequest(nameof(zmstdistrict.StateId));
            }

            try
            {
                var response = await zmstdistrictDirector.UpdateAsync(zmstdistrict, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstDistrict.
        /// </summary>
        /// <param name="StateId">StateId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string StateId)
        {
            string status;
            var response = await zmstdistrictDirector.DeleteAsync(StateId, default).ConfigureAwait(false);

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

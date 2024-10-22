//-----------------------------------------------------------------------
// <copyright file="MdDistrictController.cs" company="NIC">
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
    /// MdDistrictController.
    /// </summary>
    public class MdDistrictController : ControllerBase
    {
        private readonly IMdDistrictDirector mddistrictDirector;
        private readonly ILogger<MdDistrictController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDistrictController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mddistrictDirector">mddistrictDirector.</param>
        /// <param name="logger">logger.</param>
        public MdDistrictController(IMdDistrictDirector mddistrictDirector, ILogger<MdDistrictController> logger)
        {
            this.mddistrictDirector = mddistrictDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdDistrict List.
        /// </summary>
        /// <returns>Get All MdDistrict List.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdDistrict>>> GetAllAsync()
        {
            return await mddistrictDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdDistrict List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdDistrict>> GetByIdAsync(string Id)
        {
            var response = await mddistrictDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdDistrict.
        /// </summary>
        /// <param name="mddistrict">mddistrict.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdDistrict mddistrict)
        {
            if (mddistrict == null)
            {
                return BadRequest(mddistrict);
            }

            try
            {
                var response = await mddistrictDirector.InsertAsync(mddistrict, default).ConfigureAwait(false);
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
        /// Update to MdDistrict.
        /// </summary>
        /// <param name="mddistrict">mddistrict.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.MdDistrict mddistrict)
        {
            string message = "";
            if (mddistrict == null)
            {
                return BadRequest(nameof(mddistrict));
            }

            if (mddistrict.Id == "0")
            {
                return BadRequest(nameof(mddistrict.Id));
            }

            try
            {
                var response = await mddistrictDirector.UpdateAsync(mddistrict, default).ConfigureAwait(false);
                if (response > 0)
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
        /// Delete MdDistrict.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await mddistrictDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
        /// Get MdDistrict List By StateId.
        /// </summary>
        /// <param name="StateId">stateId.</param>
        /// <returns>Get by StateId.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdDistrict), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetListByStateId")]
        public async Task<ActionResult<List<AbsModels.MdDistrict>>> GetListByStateIdAsync(string StateId)
        {
            var response = await mddistrictDirector.GetListByStateIdAsync(StateId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
    }
}
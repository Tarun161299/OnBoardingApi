
//-----------------------------------------------------------------------
// <copyright file="ZmstIdentityTypeController.cs" company="NIC">
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
    /// ZmstIdentityTypeController.
    /// </summary>
    public class ZmstIdentityTypeController : ControllerBase
    {
        private readonly IZmstIdentityTypeDirector zmstidentitytypeDirector;
        private readonly ILogger<ZmstIdentityTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstIdentityTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstidentitytypeDirector">zmstidentitytypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstIdentityTypeController(IZmstIdentityTypeDirector zmstidentitytypeDirector, ILogger<ZmstIdentityTypeController> logger)
        {
            this.zmstidentitytypeDirector = zmstidentitytypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstIdentityType List.
        /// </summary>
        /// <returns>Get All ZmstIdentityType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstIdentityType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstIdentityType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstIdentityType>>> GetAllAsync()
        {
            return await zmstidentitytypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstIdentityType List By Id.
        /// </summary>
        /// <param name="IdentityTypeId">IdentityTypeId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstIdentityType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstIdentityType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstIdentityType>> GetByIdAsync(string IdentityTypeId)
        {
            var response = await zmstidentitytypeDirector.GetByIdAsync(IdentityTypeId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstIdentityType.
        /// </summary>
        /// <param name="zmstidentitytype">zmstidentitytype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstIdentityType zmstidentitytype)
        {
            if (zmstidentitytype == null)
            {
                return BadRequest(zmstidentitytype);
            }

            try
            {
                var response = await zmstidentitytypeDirector.InsertAsync(zmstidentitytype, default).ConfigureAwait(false);
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
        /// Update to ZmstIdentityType.
        /// </summary>
        /// <param name="zmstidentitytype">zmstidentitytype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstIdentityType zmstidentitytype)
        {
            if (zmstidentitytype == null)
            {
                return BadRequest(nameof(zmstidentitytype));
            }

            if (zmstidentitytype.IdentityTypeId == "0")
            {
                return BadRequest(nameof(zmstidentitytype.IdentityTypeId));
            }

            try
            {
                var response = await zmstidentitytypeDirector.UpdateAsync(zmstidentitytype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstIdentityType.
        /// </summary>
        /// <param name="IdentityTypeId">IdentityTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string IdentityTypeId)
        {
            string status;
            var response = await zmstidentitytypeDirector.DeleteAsync(IdentityTypeId, default).ConfigureAwait(false);

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

//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
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

    public class ZmstServiceTypeController : ControllerBase
    {
        private readonly IZmstServiceTypeDirector izmstservicetype;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public ZmstServiceTypeController(IZmstServiceTypeDirector iZmstServiceTypeDirector)
        {
            this.izmstservicetype = iZmstServiceTypeDirector;
        }

        /// <summary>
        /// Get ZmstServiceType List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstServiceType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstServiceType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstServiceType>>> GetAllAsync()
        {
            return await izmstservicetype.GetAllAsync(default).ConfigureAwait(false);
        }
        /// <summary>
        /// Get appOnboardingRequest List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstServiceType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstServiceType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ZmstServiceType>>> GetByRequestNo(string requestNo)
        {
            var response = await izmstservicetype.GetByRequestNo(requestNo, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert ZmstServiceType.
        /// </summary>
        /// <param name="zmstservicetype">zmstservicetype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstServiceType zmstservicetype)
        {
            if (zmstservicetype == null)
            {
                return BadRequest(zmstservicetype);
            }

            try
            {
                var response = await izmstservicetype.InsertAsync(zmstservicetype, default).ConfigureAwait(false);
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
        /// Update to ZmstServiceType.
        /// </summary>
        /// <param name="zmstservicetype">zmstservicetype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstServiceType zmstservicetype)
        {
            if (zmstservicetype == null)
            {
                return BadRequest(nameof(zmstservicetype));
            }

            if (zmstservicetype.ServiceTypeId == "0")
            {
                return BadRequest(nameof(zmstservicetype.ServiceTypeId));
            }

            try
            {
                var response = await izmstservicetype.UpdateAsync(zmstservicetype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstServiceType.
        /// </summary>
        /// <param name="ServiceTypeId">ServiceTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string ServiceTypeId)
        {
            string status;
            var response = await izmstservicetype.DeleteAsync(ServiceTypeId, default).ConfigureAwait(false);

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

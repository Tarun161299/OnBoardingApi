
//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategoryPriorityController.cs" company="NIC">
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
    /// ZmstSubCategoryPriorityController.
    /// </summary>
    public class ZmstSubCategoryPriorityController : ControllerBase
    {
        private readonly IZmstSubCategoryPriorityDirector zmstsubcategorypriorityDirector;
        private readonly ILogger<ZmstSubCategoryPriorityController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSubCategoryPriorityController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstsubcategorypriorityDirector">zmstsubcategorypriorityDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSubCategoryPriorityController(IZmstSubCategoryPriorityDirector zmstsubcategorypriorityDirector, ILogger<ZmstSubCategoryPriorityController> logger)
        {
            this.zmstsubcategorypriorityDirector = zmstsubcategorypriorityDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSubCategoryPriority List.
        /// </summary>
        /// <returns>Get All ZmstSubCategoryPriority List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategoryPriority), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategoryPriority), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSubCategoryPriority>>> GetAllAsync()
        {
            return await zmstsubcategorypriorityDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSubCategoryPriority List By Id.
        /// </summary>
        /// <param name="SubCategoryPriorityId">SubCategoryPriorityId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategoryPriority), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategoryPriority), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSubCategoryPriority>> GetByIdAsync(string SubCategoryPriorityId)
        {
            var response = await zmstsubcategorypriorityDirector.GetByIdAsync(SubCategoryPriorityId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="zmstsubcategorypriority">zmstsubcategorypriority.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSubCategoryPriority zmstsubcategorypriority)
        {
            if (zmstsubcategorypriority == null)
            {
                return BadRequest(zmstsubcategorypriority);
            }

            try
            {
                var response = await zmstsubcategorypriorityDirector.InsertAsync(zmstsubcategorypriority, default).ConfigureAwait(false);
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
        /// Update to ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="zmstsubcategorypriority">zmstsubcategorypriority.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSubCategoryPriority zmstsubcategorypriority)
        {
            if (zmstsubcategorypriority == null)
            {
                return BadRequest(nameof(zmstsubcategorypriority));
            }

            if (zmstsubcategorypriority.SubCategoryPriorityId == "0")
            {
                return BadRequest(nameof(zmstsubcategorypriority.SubCategoryPriorityId));
            }

            try
            {
                var response = await zmstsubcategorypriorityDirector.UpdateAsync(zmstsubcategorypriority, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="SubCategoryPriorityId">SubCategoryPriorityId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SubCategoryPriorityId)
        {
            string status;
            var response = await zmstsubcategorypriorityDirector.DeleteAsync(SubCategoryPriorityId, default).ConfigureAwait(false);

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

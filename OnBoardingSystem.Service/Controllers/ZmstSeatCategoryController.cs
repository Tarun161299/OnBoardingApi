
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatCategoryController.cs" company="NIC">
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
    /// ZmstSeatCategoryController.
    /// </summary>
    public class ZmstSeatCategoryController : ControllerBase
    {
        private readonly IZmstSeatCategoryDirector zmstseatcategoryDirector;
        private readonly ILogger<ZmstSeatCategoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatCategoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstseatcategoryDirector">zmstseatcategoryDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSeatCategoryController(IZmstSeatCategoryDirector zmstseatcategoryDirector, ILogger<ZmstSeatCategoryController> logger)
        {
            this.zmstseatcategoryDirector = zmstseatcategoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSeatCategory List.
        /// </summary>
        /// <returns>Get All ZmstSeatCategory List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSeatCategory>>> GetAllAsync()
        {
            return await zmstseatcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSeatCategory List By Id.
        /// </summary>
        /// <param name="SeatCategoryId">SeatCategoryId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSeatCategory>> GetByIdAsync(string SeatCategoryId)
        {
            var response = await zmstseatcategoryDirector.GetByIdAsync(SeatCategoryId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSeatCategory.
        /// </summary>
        /// <param name="zmstseatcategory">zmstseatcategory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSeatCategory zmstseatcategory)
        {
            if (zmstseatcategory == null)
            {
                return BadRequest(zmstseatcategory);
            }

            try
            {
                var response = await zmstseatcategoryDirector.InsertAsync(zmstseatcategory, default).ConfigureAwait(false);
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
        /// Update to ZmstSeatCategory.
        /// </summary>
        /// <param name="zmstseatcategory">zmstseatcategory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSeatCategory zmstseatcategory)
        {
            if (zmstseatcategory == null)
            {
                return BadRequest(nameof(zmstseatcategory));
            }

            if (zmstseatcategory.SeatCategoryId == "0")
            {
                return BadRequest(nameof(zmstseatcategory.SeatCategoryId));
            }

            try
            {
                var response = await zmstseatcategoryDirector.UpdateAsync(zmstseatcategory, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstSeatCategory.
        /// </summary>
        /// <param name="SeatCategoryId">SeatCategoryId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SeatCategoryId)
        {
            string status;
            var response = await zmstseatcategoryDirector.DeleteAsync(SeatCategoryId, default).ConfigureAwait(false);

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

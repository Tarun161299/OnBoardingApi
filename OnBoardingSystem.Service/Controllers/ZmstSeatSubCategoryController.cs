
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatSubCategoryController.cs" company="NIC">
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
    /// ZmstSeatSubCategoryController.
    /// </summary>
    public class ZmstSeatSubCategoryController : ControllerBase
    {
        private readonly IZmstSeatSubCategoryDirector zmstseatsubcategoryDirector;
        private readonly ILogger<ZmstSeatSubCategoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatSubCategoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstseatsubcategoryDirector">zmstseatsubcategoryDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSeatSubCategoryController(IZmstSeatSubCategoryDirector zmstseatsubcategoryDirector, ILogger<ZmstSeatSubCategoryController> logger)
        {
            this.zmstseatsubcategoryDirector = zmstseatsubcategoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSeatSubCategory List.
        /// </summary>
        /// <returns>Get All ZmstSeatSubCategory List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatSubCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatSubCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSeatSubCategory>>> GetAllAsync()
        {
            return await zmstseatsubcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstSeatSubCategory List By Id.
        /// </summary>
        /// <param name="SeatSubCategoryId">SeatSubCategoryId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatSubCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSeatSubCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSeatSubCategory>> GetByIdAsync(string SeatSubCategoryId)
        {
            var response = await zmstseatsubcategoryDirector.GetByIdAsync(SeatSubCategoryId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstSeatSubCategory.
        /// </summary>
        /// <param name="zmstseatsubcategory">zmstseatsubcategory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSeatSubCategory zmstseatsubcategory)
        {
            if (zmstseatsubcategory == null)
            {
                return BadRequest(zmstseatsubcategory);
            }

            try
            {
                var response = await zmstseatsubcategoryDirector.InsertAsync(zmstseatsubcategory, default).ConfigureAwait(false);
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
        /// Update to ZmstSeatSubCategory.
        /// </summary>
        /// <param name="zmstseatsubcategory">zmstseatsubcategory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstSeatSubCategory zmstseatsubcategory)
        {
            string message = "";
            if (zmstseatsubcategory == null)
            {
                return BadRequest(nameof(zmstseatsubcategory));
            }

            if (zmstseatsubcategory.SeatSubCategoryId == "0")
            {
                return BadRequest(nameof(zmstseatsubcategory.SeatSubCategoryId));
            }

            try
            {
                var response = await zmstseatsubcategoryDirector.UpdateAsync(zmstseatsubcategory, default).ConfigureAwait(false);
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
        /// Delete ZmstSeatSubCategory.
        /// </summary>
        /// <param name="SeatSubCategoryId">SeatSubCategoryId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SeatSubCategoryId)
        {
            string status;
            var response = await zmstseatsubcategoryDirector.DeleteAsync(SeatSubCategoryId, default).ConfigureAwait(false);

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

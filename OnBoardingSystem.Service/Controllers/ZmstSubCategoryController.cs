
//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategoryController.cs" company="NIC">
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
    /// ZmstSubCategoryController.
    /// </summary>
    public class ZmstSubCategoryController : ControllerBase
    {
        private readonly IZmstSubCategoryDirector zmstsubcategoryDirector;
        private readonly ILogger<ZmstSubCategoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSubCategoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstsubcategoryDirector">zmstsubcategoryDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSubCategoryController(IZmstSubCategoryDirector zmstsubcategoryDirector, ILogger<ZmstSubCategoryController> logger)
        {
            this.zmstsubcategoryDirector = zmstsubcategoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSubCategory List.
        /// </summary>
        /// <returns>Get All ZmstSubCategory List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSubCategory>>> GetAllAsync()
        {
            return await zmstsubcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstSubCategory List By Id.
        /// </summary>
        /// <param name="SubCategoryId">SubCategoryId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSubCategory>> GetByIdAsync(string SubCategoryId)
        {
            var response = await zmstsubcategoryDirector.GetByIdAsync(SubCategoryId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstSubCategory.
        /// </summary>
        /// <param name="zmstsubcategory">zmstsubcategory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSubCategory zmstsubcategory)
        {
            if (zmstsubcategory == null)
            {
                return BadRequest(zmstsubcategory);
            }

            try
            {
                var response = await zmstsubcategoryDirector.InsertAsync(zmstsubcategory, default).ConfigureAwait(false);
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
        /// Update to ZmstSubCategory.
        /// </summary>
        /// <param name="zmstsubcategory">zmstsubcategory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSubCategory zmstsubcategory)
        {
            if (zmstsubcategory == null)
            {
                return BadRequest(nameof(zmstsubcategory));
            }

            if (zmstsubcategory.SubCategoryId == "0")
            {
                return BadRequest(nameof(zmstsubcategory.SubCategoryId));
            }

            try
            {
				var response = await zmstsubcategoryDirector.UpdateAsync(zmstsubcategory, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstSubCategory.
        /// </summary>
        /// <param name="SubCategoryId">SubCategoryId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SubCategoryId)
        {
            string status;
            var response = await zmstsubcategoryDirector.DeleteAsync(SubCategoryId, default).ConfigureAwait(false);

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
	
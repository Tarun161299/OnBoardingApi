
//-----------------------------------------------------------------------
// <copyright file="ZmstCategoryController.cs" company="NIC">
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
    /// ZmstCategoryController.
    /// </summary>
    public class ZmstCategoryController : ControllerBase
    {
        private readonly IZmstCategoryDirector zmstcategoryDirector;
        private readonly ILogger<ZmstCategoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCategoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstcategoryDirector">zmstcategoryDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstCategoryController(IZmstCategoryDirector zmstcategoryDirector, ILogger<ZmstCategoryController> logger)
        {
            this.zmstcategoryDirector = zmstcategoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstCategory List.
        /// </summary>
        /// <returns>Get All ZmstCategory List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstCategory>>> GetAllAsync()
        {
            return await zmstcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstCategory List By Id.
        /// </summary>
        /// <param name="CategoryId">CategoryId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstCategory>> GetByIdAsync(string CategoryId)
        {
            var response = await zmstcategoryDirector.GetByIdAsync(CategoryId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstCategory.
        /// </summary>
        /// <param name="zmstcategory">zmstcategory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstCategory zmstcategory)
        {
            if (zmstcategory == null)
            {
                return BadRequest(zmstcategory);
            }

            try
            {
                var response = await zmstcategoryDirector.InsertAsync(zmstcategory, default).ConfigureAwait(false);
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
        /// Update to ZmstCategory.
        /// </summary>
        /// <param name="zmstcategory">zmstcategory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstCategory zmstcategory)
        {
            if (zmstcategory == null)
            {
                return BadRequest(nameof(zmstcategory));
            }

            if (zmstcategory.CategoryId == "0")
            {
                return BadRequest(nameof(zmstcategory.CategoryId));
            }

            try
            {
				var response = await zmstcategoryDirector.UpdateAsync(zmstcategory, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstCategory.
        /// </summary>
        /// <param name="CategoryId">CategoryId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string CategoryId)
        {
            string status;
            var response = await zmstcategoryDirector.DeleteAsync(CategoryId, default).ConfigureAwait(false);

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
	
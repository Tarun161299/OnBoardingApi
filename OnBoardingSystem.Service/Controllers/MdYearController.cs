
//-----------------------------------------------------------------------
// <copyright file="MdYearController.cs" company="NIC">
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
    /// MdYearController.
    /// </summary>
    public class MdYearController : ControllerBase
    {
        private readonly IMdYearDirector mdyearDirector;
        private readonly ILogger<MdYearController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdYearController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdyearDirector">mdyearDirector.</param>
        /// <param name="logger">logger.</param>
        public MdYearController(IMdYearDirector mdyearDirector, ILogger<MdYearController> logger)
        {
            this.mdyearDirector = mdyearDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdYear List.
        /// </summary>
        /// <returns>Get All MdYear List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdYear), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdYear), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.MdYear>>> GetAllAsync()
        {
            return await mdyearDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdYear List By Id.
        /// </summary>
        /// <param name="YearId">YearId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdYear), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdYear), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.MdYear>> GetByGroupIdAsync(string yearGroup)
        {
            var response = await mdyearDirector.GetByGroupIdAsync(yearGroup, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdYear.
        /// </summary>
        /// <param name="mdyear">mdyear.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.MdYear mdyear)
        {
            if (mdyear == null)
            {
                return BadRequest(mdyear);
            }

            try
            {
                var response = await mdyearDirector.InsertAsync(mdyear, default).ConfigureAwait(false);
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
        /// Update to MdYear.
        /// </summary>
        /// <param name="mdyear">mdyear.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.MdYear mdyear)
        {
            if (mdyear == null)
            {
                return BadRequest(nameof(mdyear));
            }

            if (mdyear.YearId == "0")
            {
                return BadRequest(nameof(mdyear.YearId));
            }

            try
            {
				var response = await mdyearDirector.UpdateAsync(mdyear, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdYear.
        /// </summary>
        /// <param name="YearId">YearId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string YearId)
        {
            string status;
            var response = await mdyearDirector.DeleteAsync(YearId, default).ConfigureAwait(false);

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
	
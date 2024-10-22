
//-----------------------------------------------------------------------
// <copyright file="ZmstBranchController.cs" company="NIC">
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
    /// ZmstBranchController.
    /// </summary>
    public class ZmstBranchController : ControllerBase
    {
        private readonly IZmstBranchDirector zmstbranchDirector;
        private readonly ILogger<ZmstBranchController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstBranchController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstbranchDirector">zmstbranchDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstBranchController(IZmstBranchDirector zmstbranchDirector, ILogger<ZmstBranchController> logger)
        {
            this.zmstbranchDirector = zmstbranchDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstBranch List.
        /// </summary>
        /// <returns>Get All ZmstBranch List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstBranch), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstBranch), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstBranch>>> GetAllAsync()
        {
            return await zmstbranchDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstBranch List By Id.
        /// </summary>
        /// <param name="BrCd">BrCd.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstBranch), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstBranch), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstBranch>> GetByIdAsync(string BrCd)
        {
            var response = await zmstbranchDirector.GetByIdAsync(BrCd, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstBranch.
        /// </summary>
        /// <param name="zmstbranch">zmstbranch.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstBranch zmstbranch)
        {
            if (zmstbranch == null)
            {
                return BadRequest(zmstbranch);
            }

            try
            {
                var response = await zmstbranchDirector.InsertAsync(zmstbranch, default).ConfigureAwait(false);
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
        /// Update to ZmstBranch.
        /// </summary>
        /// <param name="zmstbranch">zmstbranch.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstBranch zmstbranch)
        {
            if (zmstbranch == null)
            {
                return BadRequest(nameof(zmstbranch));
            }

            if (zmstbranch.BrCd == "0")
            {
                return BadRequest(nameof(zmstbranch.BrCd));
            }

            try
            {
				var response = await zmstbranchDirector.UpdateAsync(zmstbranch, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstBranch.
        /// </summary>
        /// <param name="BrCd">BrCd.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string BrCd)
        {
            string status;
            var response = await zmstbranchDirector.DeleteAsync(BrCd, default).ConfigureAwait(false);

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
	
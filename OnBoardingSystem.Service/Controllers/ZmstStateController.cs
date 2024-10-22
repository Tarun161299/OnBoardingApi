
//-----------------------------------------------------------------------
// <copyright file="ZmstStateController.cs" company="NIC">
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
    /// ZmstStateController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ZmstStateController : ControllerBase
    {
        private readonly IZmstStateDirector zmststateDirector;
        private readonly ILogger<ZmstStateController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstStateController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmststateDirector">zmststateDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstStateController(IZmstStateDirector zmststateDirector, ILogger<ZmstStateController> logger)
        {
            this.zmststateDirector = zmststateDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstState List.
        /// </summary>
        /// <returns>Get All ZmstState List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstState), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstState), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstState>>> GetAsync()
        {
            return await zmststateDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstState List By Id.
        /// </summary>
        /// <param name="StateId">StateId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstState), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstState), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstState>> GetAsync(string StateId)
        {
            var response = await zmststateDirector.GetByIdAsync(StateId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstState.
        /// </summary>
        /// <param name="zmststate">zmststate.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.ZmstState zmststate)
        {
            if (zmststate == null)
            {
                return BadRequest(zmststate);
            }

            try
            {
                var response = await zmststateDirector.InsertAsync(zmststate, default).ConfigureAwait(false);
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
        /// Update to ZmstState.
        /// </summary>
        /// <param name="zmststate">zmststate.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.ZmstState zmststate)
        {
            if (zmststate == null)
            {
                return BadRequest(nameof(zmststate));
            }

            if (zmststate.StateId == "0")
            {
                return BadRequest(nameof(zmststate.StateId));
            }

            try
            {
				var response = await zmststateDirector.UpdateAsync(zmststate, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstState.
        /// </summary>
        /// <param name="StateId">StateId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string StateId)
        {
            string status;
            var response = await zmststateDirector.DeleteAsync(StateId, default).ConfigureAwait(false);

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
	
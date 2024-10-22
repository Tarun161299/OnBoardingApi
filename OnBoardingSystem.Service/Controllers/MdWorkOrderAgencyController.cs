
//-----------------------------------------------------------------------
// <copyright file="MdWorkOrderAgencyController.cs" company="NIC">
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
    /// MdWorkOrderAgencyController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdWorkOrderAgencyController : ControllerBase
    {
        private readonly IMdWorkOrderAgencyDirector mdworkorderagencyDirector;
        private readonly ILogger<MdWorkOrderAgencyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdWorkOrderAgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdworkorderagencyDirector">mdworkorderagencyDirector.</param>
        /// <param name="logger">logger.</param>
        public MdWorkOrderAgencyController(IMdWorkOrderAgencyDirector mdworkorderagencyDirector, ILogger<MdWorkOrderAgencyController> logger)
        {
            this.mdworkorderagencyDirector = mdworkorderagencyDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdWorkOrderAgency List.
        /// </summary>
        /// <returns>Get All MdWorkOrderAgency List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdWorkOrderAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdWorkOrderAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdWorkOrderAgency>>> GetAsync()
        {
            return await mdworkorderagencyDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdWorkOrderAgency List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdWorkOrderAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdWorkOrderAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdWorkOrderAgency>> GetAsync(int Id)
        {
            var response = await mdworkorderagencyDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdWorkOrderAgency.
        /// </summary>
        /// <param name="mdworkorderagency">mdworkorderagency.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdWorkOrderAgency mdworkorderagency)
        {
            if (mdworkorderagency == null)
            {
                return BadRequest(mdworkorderagency);
            }

            try
            {
                var response = await mdworkorderagencyDirector.InsertAsync(mdworkorderagency, default).ConfigureAwait(false);
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
        /// Update to MdWorkOrderAgency.
        /// </summary>
        /// <param name="mdworkorderagency">mdworkorderagency.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdWorkOrderAgency mdworkorderagency)
        {
            if (mdworkorderagency == null)
            {
                return BadRequest(nameof(mdworkorderagency));
            }

            if (mdworkorderagency.Id == 0)
            {
                return BadRequest(nameof(mdworkorderagency.Id));
            }

            try
            {
				var response = await mdworkorderagencyDirector.UpdateAsync(mdworkorderagency, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdWorkOrderAgency.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int Id)
        {
            string status;
            var response = await mdworkorderagencyDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	
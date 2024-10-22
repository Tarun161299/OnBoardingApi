
//-----------------------------------------------------------------------
// <copyright file="MdEmpStatusController.cs" company="NIC">
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
    /// MdEmpStatusController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdEmpStatusController : ControllerBase
    {
        private readonly IMdEmpStatusDirector mdempstatusDirector;
        private readonly ILogger<MdEmpStatusController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdEmpStatusController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdempstatusDirector">mdempstatusDirector.</param>
        /// <param name="logger">logger.</param>
        public MdEmpStatusController(IMdEmpStatusDirector mdempstatusDirector, ILogger<MdEmpStatusController> logger)
        {
            this.mdempstatusDirector = mdempstatusDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdEmpStatus List.
        /// </summary>
        /// <returns>Get All MdEmpStatus List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdEmpStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdEmpStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdEmpStatus>>> GetAsync()
        {
            return await mdempstatusDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdEmpStatus List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdEmpStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdEmpStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdEmpStatus>> GetAsync(string Id)
        {
            var response = await mdempstatusDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdEmpStatus.
        /// </summary>
        /// <param name="mdempstatus">mdempstatus.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdEmpStatus mdempstatus)
        {
            if (mdempstatus == null)
            {
                return BadRequest(mdempstatus);
            }

            try
            {
                var response = await mdempstatusDirector.InsertAsync(mdempstatus, default).ConfigureAwait(false);
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
        /// Update to MdEmpStatus.
        /// </summary>
        /// <param name="mdempstatus">mdempstatus.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdEmpStatus mdempstatus)
        {
            if (mdempstatus == null)
            {
                return BadRequest(nameof(mdempstatus));
            }

            if (mdempstatus.Id == "0")
            {
                return BadRequest(nameof(mdempstatus.Id));
            }

            try
            {
				var response = await mdempstatusDirector.UpdateAsync(mdempstatus, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdEmpStatus.
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
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await mdempstatusDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	

//-----------------------------------------------------------------------
// <copyright file="ZmstPassingStatusController.cs" company="NIC">
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
    /// ZmstPassingStatusController.
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController]
    public class ZmstPassingStatusController : ControllerBase
    {
        private readonly IZmstPassingStatusDirector zmstpassingstatusDirector;
        private readonly ILogger<ZmstPassingStatusController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstPassingStatusController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstpassingstatusDirector">zmstpassingstatusDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstPassingStatusController(IZmstPassingStatusDirector zmstpassingstatusDirector, ILogger<ZmstPassingStatusController> logger)
        {
            this.zmstpassingstatusDirector = zmstpassingstatusDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstPassingStatus List.
        /// </summary>
        /// <returns>Get All ZmstPassingStatus List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstPassingStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstPassingStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
       // [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstPassingStatus>>> GetAllAsync()
        {
            return await zmstpassingstatusDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstPassingStatus List By Id.
        /// </summary>
        /// <param name="PassingStatusId">PassingStatusId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstPassingStatus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstPassingStatus), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstPassingStatus>> GetByIdAsync(string PassingStatusId)
        {
            var response = await zmstpassingstatusDirector.GetByIdAsync(PassingStatusId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstPassingStatus.
        /// </summary>
        /// <param name="zmstpassingstatus">zmstpassingstatus.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstPassingStatus zmstpassingstatus)
        {
            if (zmstpassingstatus == null)
            {
                return BadRequest(zmstpassingstatus);
            }

            try
            {
                var response = await zmstpassingstatusDirector.InsertAsync(zmstpassingstatus, default).ConfigureAwait(false);
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
        /// Update to ZmstPassingStatus.
        /// </summary>
        /// <param name="zmstpassingstatus">zmstpassingstatus.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Update")]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstPassingStatus zmstpassingstatus)
        {
            if (zmstpassingstatus == null)
            {
                return BadRequest(nameof(zmstpassingstatus));
            }

            if (zmstpassingstatus.PassingStatusId == "0")
            {
                return BadRequest(nameof(zmstpassingstatus.PassingStatusId));
            }

            try
            {
				var response = await zmstpassingstatusDirector.UpdateAsync(zmstpassingstatus, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstPassingStatus.
        /// </summary>
        /// <param name="PassingStatusId">PassingStatusId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string PassingStatusId)
        {
            string status;
            var response = await zmstpassingstatusDirector.DeleteAsync(PassingStatusId, default).ConfigureAwait(false);

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
	
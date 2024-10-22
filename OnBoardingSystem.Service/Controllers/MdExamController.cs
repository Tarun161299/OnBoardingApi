
//-----------------------------------------------------------------------
// <copyright file="MdExamController.cs" company="NIC">
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
    /// MdExamController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdExamController : ControllerBase
    {
        private readonly IMdExamDirector mdexamDirector;
        private readonly ILogger<MdExamController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdExamController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdexamDirector">mdexamDirector.</param>
        /// <param name="logger">logger.</param>
        public MdExamController(IMdExamDirector mdexamDirector, ILogger<MdExamController> logger)
        {
            this.mdexamDirector = mdexamDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdExam List.
        /// </summary>
        /// <returns>Get All MdExam List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdExam), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdExam), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdExam>>> GetAsync()
        {
            return await mdexamDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdExam List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdExam), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdExam), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdExam>> GetAsync(string Id)
        {
            var response = await mdexamDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdExam.
        /// </summary>
        /// <param name="mdexam">mdexam.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdExam mdexam)
        {
            if (mdexam == null)
            {
                return BadRequest(mdexam);
            }

            try
            {
                var response = await mdexamDirector.InsertAsync(mdexam, default).ConfigureAwait(false);
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
        /// Update to MdExam.
        /// </summary>
        /// <param name="mdexam">mdexam.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdExam mdexam)
        {
            if (mdexam == null)
            {
                return BadRequest(nameof(mdexam));
            }

            if (mdexam.Id == "0")
            {
                return BadRequest(nameof(mdexam.Id));
            }

            try
            {
				var response = await mdexamDirector.UpdateAsync(mdexam, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdExam.
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
            var response = await mdexamDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	
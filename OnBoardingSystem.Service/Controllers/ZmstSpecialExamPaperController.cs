
//-----------------------------------------------------------------------
// <copyright file="ZmstSpecialExamPaperController.cs" company="NIC">
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
    /// ZmstSpecialExamPaperController.
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController]
    public class ZmstSpecialExamPaperController : ControllerBase
    {
        private readonly IZmstSpecialExamPaperDirector zmstspecialexampaperDirector;
        private readonly ILogger<ZmstSpecialExamPaperController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSpecialExamPaperController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstspecialexampaperDirector">zmstspecialexampaperDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSpecialExamPaperController(IZmstSpecialExamPaperDirector zmstspecialexampaperDirector, ILogger<ZmstSpecialExamPaperController> logger)
        {
            this.zmstspecialexampaperDirector = zmstspecialexampaperDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSpecialExamPaper List.
        /// </summary>
        /// <returns>Get All ZmstSpecialExamPaper List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSpecialExamPaper), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSpecialExamPaper), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstSpecialExamPaper>>> GetAllAsync()
        {
            return await zmstspecialexampaperDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstSpecialExamPaper List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSpecialExamPaper), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSpecialExamPaper), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstSpecialExamPaper>> GetByIdAsync(string Id)
        {
            var response = await zmstspecialexampaperDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="zmstspecialexampaper">zmstspecialexampaper.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSpecialExamPaper zmstspecialexampaper)
        {
            if (zmstspecialexampaper == null)
            {
                return BadRequest(zmstspecialexampaper);
            }

            try
            {
                var response = await zmstspecialexampaperDirector.InsertAsync(zmstspecialexampaper, default).ConfigureAwait(false);
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
        /// Update to ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="zmstspecialexampaper">zmstspecialexampaper.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Update")]
		public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstSpecialExamPaper zmstspecialexampaper)
        {
            string message = "";
            if (zmstspecialexampaper == null)
            {
                return BadRequest(nameof(zmstspecialexampaper));
            }

            if (zmstspecialexampaper.Id == "0")
            {
                return BadRequest(nameof(zmstspecialexampaper.Id));
            }

            try
            {
				var response = await zmstspecialexampaperDirector.UpdateAsync(zmstspecialexampaper, default).ConfigureAwait(false);
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
        /// Delete ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await zmstspecialexampaperDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	

//-----------------------------------------------------------------------
// <copyright file="ZmstExamTypeController.cs" company="NIC">
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
    /// ZmstExamTypeController.
    /// </summary>    
    public class ZmstExamTypeController : ControllerBase
    {
        private readonly IZmstExamTypeDirector zmstexamtypeDirector;
        private readonly ILogger<ZmstExamTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstExamTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstexamtypeDirector">zmstexamtypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstExamTypeController(IZmstExamTypeDirector zmstexamtypeDirector, ILogger<ZmstExamTypeController> logger)
        {
            this.zmstexamtypeDirector = zmstexamtypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstExamType List.
        /// </summary>
        /// <returns>Get All ZmstExamType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstExamType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstExamType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstExamType>>> GetAllAsync()
        {
            return await zmstexamtypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstExamType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstExamType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstExamType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstExamType>> GetByIdAsync(string Id)
        {
            var response = await zmstexamtypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstExamType.
        /// </summary>
        /// <param name="zmstexamtype">zmstexamtype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        //[Route("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstExamType zmstexamtype)
        {
            if (zmstexamtype == null)
            {
                return BadRequest(zmstexamtype);
            }

            try
            {
                var response = await zmstexamtypeDirector.InsertAsync(zmstexamtype, default).ConfigureAwait(false);
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
        /// Update to ZmstExamType.
        /// </summary>
        /// <param name="zmstexamtype">zmstexamtype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("Update")]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] AbsModels.ZmstExamType zmstexamtype)
        {
            string message = "";
            if (zmstexamtype == null)
            {
                return BadRequest(nameof(zmstexamtype));
            }

            if (zmstexamtype.Id == "0")
            {
                return BadRequest(nameof(zmstexamtype.Id));
            }

            try
            {
                var response = await zmstexamtypeDirector.UpdateAsync(zmstexamtype, default).ConfigureAwait(false);
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
        /// Delete ZmstExamType.
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
            var response = await zmstexamtypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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

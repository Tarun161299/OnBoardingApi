//-----------------------------------------------------------------------
// <copyright file="ZmstProjectsController.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// MinistryController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ZmstProjectsController : ControllerBase
    {
        private readonly IZmstProjectsDirector iZmstProjectsDirector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iZmstProjectsDirector">IMdMinistry.</param>
        public ZmstProjectsController(IZmstProjectsDirector _iZmstProjectsDirector)
        {
            this.iZmstProjectsDirector = _iZmstProjectsDirector;
        }

        /// <summary>
        /// Get ZmstProjects List.
        /// </summary>
        /// <returns>Get All ZmstProjects List.</returns>            
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstProjects>>> GetAsync()
        {
            return await iZmstProjectsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstProjects List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdMinistry), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByid")]
        public async Task<ActionResult<List<AbsModels.ZmstProjects>>> GetAsync(int projectid)
        {
            return await iZmstProjectsDirector.GetByIdAsync(projectid, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes Rule.
        /// </summary>
        /// <param name="projectid">projectid.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult> DeleteAsync(int projectid)
        {
            string status;
            var response = await iZmstProjectsDirector.DeleteAsync(projectid, default).ConfigureAwait(false);
            if (response > 0)
            {
                status = "\"Project Cycle Deleted\"";
            }
            else
            {
                status = "\"Try Again\"";
            }
            return response > 0 ? Created(string.Empty, status) : Ok(status);

        }

        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="zmstProject">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.ZmstProjects zmstProject)
        {
            string status;
            if (zmstProject == null)
            {
                return BadRequest(zmstProject);
            }

            try
            {
                var response = await iZmstProjectsDirector.SaveAsync(zmstProject, default).ConfigureAwait(false);
                if (response > 0)
                {
                    if (response == 404)
                    {
                        status = "\"Project Cycle Already Exist\"";
                    }
                    else
                    {
                        status = "\"Project Cycle Stored Successfully\"";
                    }
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (EntityFoundException entityFoundEx)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Update to Ministry.
        /// </summary>
        /// <param name="ministry">ministry.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
        public async Task<ActionResult> PutAsync(AbsModels.ZmstProjects zmstProject)
        {
            string status;
            try
            {
                var response = await this.iZmstProjectsDirector.UpdateAsync(zmstProject, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Project Cycle Update Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}

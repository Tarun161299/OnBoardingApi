
//-----------------------------------------------------------------------
// <copyright file="MdIdTypeController.cs" company="NIC">
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
    /// MdIdTypeController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdIdTypeController : ControllerBase
    {
        private readonly IMdIdTypeDirector mdidtypeDirector;
        private readonly ILogger<MdIdTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdIdTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdidtypeDirector">mdidtypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MdIdTypeController(IMdIdTypeDirector mdidtypeDirector, ILogger<MdIdTypeController> logger)
        {
            this.mdidtypeDirector = mdidtypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdIdType List.
        /// </summary>
        /// <returns>Get All MdIdType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdIdType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdIdType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdIdType>>> GetAsync()
        {
            return await mdidtypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdIdType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdIdType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdIdType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdIdType>> GetAsync(string Id)
        {
            var response = await mdidtypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert MdIdType.
        /// </summary>
        /// <param name="mdidtype">mdidtype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdIdType mdidtype)
        {
            if (mdidtype == null)
            {
                return BadRequest(mdidtype);
            }

            try
            {
                var response = await mdidtypeDirector.InsertAsync(mdidtype, default).ConfigureAwait(false);
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
        /// Update to MdIdType.
        /// </summary>
        /// <param name="mdidtype">mdidtype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdIdType mdidtype)
        {
            if (mdidtype == null)
            {
                return BadRequest(nameof(mdidtype));
            }

            if (mdidtype.Id == "0")
            {
                return BadRequest(nameof(mdidtype.Id));
            }

            try
            {
				var response = await mdidtypeDirector.UpdateAsync(mdidtype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete MdIdType.
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
            var response = await mdidtypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	
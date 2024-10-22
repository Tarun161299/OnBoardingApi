
//-----------------------------------------------------------------------
// <copyright file="ZmstSymbolController.cs" company="NIC">
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
    /// ZmstSymbolController.
    /// </summary>
    public class ZmstSymbolController : ControllerBase
    {
        private readonly IZmstSymbolDirector zmstsymbolDirector;
        private readonly ILogger<ZmstSymbolController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSymbolController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstsymbolDirector">zmstsymbolDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstSymbolController(IZmstSymbolDirector zmstsymbolDirector, ILogger<ZmstSymbolController> logger)
        {
            this.zmstsymbolDirector = zmstsymbolDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstSymbol List.
        /// </summary>
        /// <returns>Get All ZmstSymbol List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSymbol), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSymbol), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSymbol>>> GetAllAsync()
        {
            return await zmstsymbolDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get ZmstSymbol List By Id.
        /// </summary>
        /// <param name="SymbolId">SymbolId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSymbol), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSymbol), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstSymbol>> GetByIdAsync(string SymbolId)
        {
            var response = await zmstsymbolDirector.GetByIdAsync(SymbolId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert ZmstSymbol.
        /// </summary>
        /// <param name="zmstsymbol">zmstsymbol.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstSymbol zmstsymbol)
        {
            if (zmstsymbol == null)
            {
                return BadRequest(zmstsymbol);
            }

            try
            {
                var response = await zmstsymbolDirector.InsertAsync(zmstsymbol, default).ConfigureAwait(false);
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
        /// Update to ZmstSymbol.
        /// </summary>
        /// <param name="zmstsymbol">zmstsymbol.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstSymbol zmstsymbol)
        {
            if (zmstsymbol == null)
            {
                return BadRequest(nameof(zmstsymbol));
            }

            if (zmstsymbol.SymbolId == "0")
            {
                return BadRequest(nameof(zmstsymbol.SymbolId));
            }

            try
            {
				var response = await zmstsymbolDirector.UpdateAsync(zmstsymbol, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete ZmstSymbol.
        /// </summary>
        /// <param name="SymbolId">SymbolId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string SymbolId)
        {
            string status;
            var response = await zmstsymbolDirector.DeleteAsync(SymbolId, default).ConfigureAwait(false);

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
	
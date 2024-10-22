//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// StateController.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateDirector iState;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iState">IStateDirector.</param>

        public StateController(IStateDirector _iState)
        {
            this.iState = _iState;
        }

        /// <summary>
        /// Get State List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.State), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.State), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.State>>> GetAsync()
        {
            //logger.LogError("OSROOOOOOOOOOOOOOOOOOOOOOOOOO");
            return await iState.GetAllAsync(default).ConfigureAwait(false);
        }

    }
}

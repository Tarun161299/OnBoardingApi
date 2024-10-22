//-----------------------------------------------------------------------
// <copyright file="RequestListInfoController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;

    /// <summary>
    /// RequestListInfoController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestListInfoController : ControllerBase
    {
        private readonly IRequestListInfoDirector IrequestList;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestListInfoController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="minis">IRequestListInfo.</param>
        public RequestListInfoController(IRequestListInfoDirector minis)
        {
            this.IrequestList = minis;
        }

        /// <summary>
        /// Get Request List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.RequestList), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.RequestList), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.RequestList>>> GetAsync()
        {
            return await IrequestList.GetRequestListAsync(default).ConfigureAwait(false);
        }

        ///// <summary>
        ///// GetList by id.
        ///// </summary>
        ///// <param name="requestId">ministriesId.</param>
        ///// <returns>get by id.</returns>
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(SuccessStatus), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(SuccessStatus), StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //[Route("GetById")]
        //public async Task<ActionResult<OnBoardingRequestDetailUpsert>> GetAsync(string requestId)
        //{
        //    var response = await IrequestList.GetRequestListByIdAsync(requestId, default).ConfigureAwait(false);
        //    return response == null ? Created(string.Empty, response) : Ok(response);
        //}
    }
}

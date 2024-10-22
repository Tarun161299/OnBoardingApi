//-----------------------------------------------------------------------
// <copyright file="OrganizationController.cs" company="NIC">
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
    using Serilog;

    /// <summary>
    /// OrganizationController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        /// <summary>
        /// OrganizationController.
        /// </summary>
        private readonly IMdOrganizationDirector iorganization;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdOrganization">IMdMinistry.</param>
        public OrganizationController(IMdOrganizationDirector iMdOrganization)
        {
            this.iorganization = iMdOrganization;
        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetAsync()
        {
            return await iorganization.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="id">id.</param>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetByIdAsync(string id)
        {
            return await iorganization.GetByStateIdAsync(id, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Organization List.
        /// </summary>
        /// <returns>GetAll.</returns>
        /// <param name="stateId">stateId.</param>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdOrganization), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByStateId")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdOrganization>>> GetByStateIdAsync(string? stateId)
        {
            return await iorganization.GetByStateIdAsync(stateId, default).ConfigureAwait(false);
        }
    }
}
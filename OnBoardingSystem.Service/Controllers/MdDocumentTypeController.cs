//-----------------------------------------------------------------------
// <copyright file="MdDocumentTypeController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;

    /// <summary>
    /// MdDocumentTypeController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdDocumentTypeController : ControllerBase
    {

        /// <summary>
        /// MdDocumentTypeController.
        /// </summary>
        private readonly IMdDocumentTypeDirector iMdDocumentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDocumentTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdDocumentType">IMdDocumentType.</param>
        public MdDocumentTypeController(IMdDocumentTypeDirector iMdDocumentType)
        {
            this.iMdDocumentType = iMdDocumentType;
        }

        /// <summary>
        /// Get MdDocumentType List.
        /// </summary>
        /// <returns>GetAll.</returns>
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdDocumentType>>> GetAsync()
        {
            return await iMdDocumentType.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdDocumentType List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MdDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByRole")]
        public async Task<ActionResult<List<OnBoardingSystem.Data.Abstractions.Models.MdDocumentType>>> GetByRoleAsync(string Role)
        {
            return await iMdDocumentType.GetByRoleAsync(Role, default).ConfigureAwait(false);
        }

    }
}

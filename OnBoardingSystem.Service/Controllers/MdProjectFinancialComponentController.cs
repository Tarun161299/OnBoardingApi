//-----------------------------------------------------------------------
// <copyright file="MinistryController.cs" company="NIC">
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
    /// MinistryController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdProjectFinancialComponentController : ControllerBase
    {
        private readonly IMdFinancialComponentDirector ImdFinancialComponentDirector;
        private readonly ILogger<MinistryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public MdProjectFinancialComponentController(IMdFinancialComponentDirector _ImdFinancialComponentDirector)
        {
            this.ImdFinancialComponentDirector = _ImdFinancialComponentDirector;
        }
        /// <summary>
        /// Get MdProjectfinancialComponent List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdProjectFinancialComponents), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdProjectFinancialComponents), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdProjectFinancialComponents>>> GetAsync()
        {
            return await ImdFinancialComponentDirector.GetAllAsync(default).ConfigureAwait(false);
        }
    }
}

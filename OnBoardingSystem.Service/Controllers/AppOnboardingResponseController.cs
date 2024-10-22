//-----------------------------------------------------------------------
// <copyright file="AppOnboardingResponseController.cs" company="NIC">
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
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// AppOnboardingResponseController.
    /// </summary>
    public class AppOnboardingResponseController : ControllerBase
    {
        private readonly IAppOnboardingResponseDirector iAppOnboardingResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingResponseController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingResponse">IAppOnboardingResponse.</param>
        public AppOnboardingResponseController(IAppOnboardingResponseDirector iAppOnboardingResponses)
        {
            this.iAppOnboardingResponse = iAppOnboardingResponses;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppOnboardingResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppOnboardingResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertUseProcedureAsync([FromBody] AppOnboardingResponse appOnboardingResponse)
        {
            var response = await iAppOnboardingResponse.SaveAppOnboardingResponseData(appOnboardingResponse, default).ConfigureAwait(false);
            string status;
            if (response==true)
            {   
                return status = "\"Success\"";
            }
            else 
            {
                return status = "\"Try Again\"";
            }
        }
    }
}

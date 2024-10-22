using Microsoft.AspNetCore.Mvc;
using OnBoardingSystem.Data.Abstractions.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;

namespace OnBoardingSystem.Service.Controllers
{
    public class DaywiseRegistrationController : ControllerBase
    {
        private readonly IDaywiseRegistrationDirector iDaywiseRegistrationDirector;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingRequestController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingRequest">iAppOnboardingRequest.</param>
        public DaywiseRegistrationController(IDaywiseRegistrationDirector iAppOnboardingRequest)
        {
            this.iDaywiseRegistrationDirector = iAppOnboardingRequest;
        }

        /// <summary>
        /// Get DaywiseRegistration List.
        /// </summary>
        /// <returns>GetAll.</returns>

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ApplicationDayWise>>> GetAllAsync([FromBody] DaywiseRegistration dates)
        {
            return await iDaywiseRegistrationDirector.GetAllAsync(dates, default).ConfigureAwait(false);
        }
    }
}

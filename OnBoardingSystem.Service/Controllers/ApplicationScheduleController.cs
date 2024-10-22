using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;
using System.Text.Json;

namespace OnBoardingSystem.Service.Controllers
{
    public class ApplicationScheduleController : ControllerBase
    {

        private readonly IApplicationScheduleDirector iApplicationSchedule;
        private readonly ILogger<MinistryController> logger;
        //public const string SessionKeyName = "_Name";
        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iApplicationSchedule">IMdMinistry.</param>
        public ApplicationScheduleController(IApplicationScheduleDirector iApplicationSchedule, ILogger<MinistryController> logger)
        {
            this.iApplicationSchedule = iApplicationSchedule;
            this.logger = logger;
        }

        /// <summary>
        /// Get ApplicationSchedule List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ApplicationSchedule>>> GetAllAsync([FromBody] calendarDate dates)
        {
            return await iApplicationSchedule.GetAllAsync(dates, default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ApplicationSchedule List by id.
        /// </summary>
        /// <param name="requestId">ApplicationSchedule List by id.</param>
        /// <returns>get by id.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppOnboardRequestAndDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApplicationSchedule>> GetByActivityId([FromBody] calendarDate dates)
        {
            var response = await iApplicationSchedule.GetAllAsync(dates, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get ApplicationSchedule List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppScheduleData>>> GetAllActivityAsync()
        {
            return await iApplicationSchedule.GetAllActivityAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ApplicationSchedule List.
        /// </summary>
        /// <param name="requestId">ApplicationSchedule List by id.</param>
        /// <returns>get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationSchedule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetByProjectId([FromBody] List<ZmstProjectSchedule> projectIds)
        {

            var response = await iApplicationSchedule.GetByProjectId(projectIds, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }
    }
}

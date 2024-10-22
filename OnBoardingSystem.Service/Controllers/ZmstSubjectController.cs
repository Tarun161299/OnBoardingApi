using Microsoft.AspNetCore.Mvc;

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Business.Behaviors;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    public class ZmstSubjectController : Controller
    {
        private readonly IZmstSubjectDirector iZmstSubjectDirector;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public ZmstSubjectController(IZmstSubjectDirector IZmstSubjectDirector)
        {
            this.iZmstSubjectDirector = IZmstSubjectDirector;
        }
        /// <summary>
        /// Get ZmstServiceType List.
        /// </summary>
        /// <returns>GetAll.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstSubject), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstSubject>>> GetAllAsync()
        {
            return await iZmstSubjectDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Save AppOnboarding Request Data.
        /// </summary>
        /// <param name="zmstSubject">appOnboardingRequest.</param>
        /// <returns>AppOnboardingRequest.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.ZmstSubject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.ZmstSubject), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] Data.Abstractions.Models.ZmstSubject zmstSubject)
        {
            if (zmstSubject == null)
            {
                return BadRequest(zmstSubject);
            }

            try
            {
                var response = await iZmstSubjectDirector.InsertAsync(zmstSubject, default).ConfigureAwait(false);
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateAsync([FromBody] Data.Abstractions.Models.ZmstSubject zmstSubject)
        {
            string message = "";
            if (zmstSubject == null)
            {
                return BadRequest(nameof(zmstSubject));
            }

            if (zmstSubject.subjectId == "0")
            {
                return BadRequest(nameof(zmstSubject.subjectId));
            }

            try
            {
                var response = await iZmstSubjectDirector.UpdateAsync(zmstSubject, default).ConfigureAwait(false);
                if (response > 0)
                {
                    message = "\"Updated Successfully\"";
                }
                else
                {
                    message = "\"Try Again\"";
                }
                return message;
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
        /// <summary>
        /// Delete MdDistrict.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await iZmstSubjectDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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



namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

      public class AdministratorController : ControllerBase
      {
      
        private readonly IAdministratorDirector iadministrator;

        public AdministratorController(IAdministratorDirector _iadministrator)
        {
            this.iadministrator = _iadministrator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> SaveAsync([FromBody] AbsModels.Administrator administrator)
        {
            string status;

            try
            {
                var response = await iadministrator.InsertAsync(administrator, default).ConfigureAwait(false);
                switch (response)
                {
                    case 1: status = "\"Data Stored Successfully\""; break;
                    case 2: status = "\"Data Stored Successfully\""; break;
                    default: status = "\"Try Again\""; break;
                }

                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (EntityFoundException entityFoundEx)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(string id, [FromBody] Data.Abstractions.Models.Administrator administrator)
        {
            string status;
            if (administrator == null)
            {
                return BadRequest(nameof(administrator));
            }

            try
            {
                var response = await this.iadministrator.UpdateAsync(id, administrator, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Update Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<string>> DeleteAsync(string id)
        {
            string status;
            var response = await this.iadministrator.DeleteAsync(id, default).ConfigureAwait(false);

            if (response > 0)
            {
                status = "\"Deleted Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);

       }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.Administrator), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.Administrator), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<AbsModels.Administratordetails>>> GetAllAsync()
        {
            return await iadministrator.GetAllAsync(default).ConfigureAwait(false);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.Administrator), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.Administrator), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.Administrator>> GetDocumentAsync(string id)
        {
            var response = await iadministrator.GetDocumentByIdAsync(id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

    }
}

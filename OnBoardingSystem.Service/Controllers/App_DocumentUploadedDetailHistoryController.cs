

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using System.Text.Json;
    using Microsoft.AspNetCore.Authorization;
    public class App_DocumentUploadedDetailHistoryController
    {
        private readonly IAppDocumentUploadedDetailHistoryDirector appDocumentUploadedDetailHistory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="_appDocumentUploadedDetail">iAppDocumentUploadedDetail.</param>

        public App_DocumentUploadedDetailHistoryController(IHttpContextAccessor httpContextAccessor, IAppDocumentUploadedDetailHistoryDirector _appDocumentUploadedDetailHistory)
        {
            this.appDocumentUploadedDetailHistory = _appDocumentUploadedDetailHistory;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="appDocumentUploadedDetail">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetailHistoty), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetailHistoty), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] AbsModels.AppDocumentUploadedDetail appDocumentUploadedDetail)
        {
            string status;
            //appDocumentUploadedDetail[0].IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //var jsonString = JsonSerializer.Serialize(appDocumentUploadedDetail);
            var response = await appDocumentUploadedDetailHistory.Save(appDocumentUploadedDetail, default).ConfigureAwait(false);
            if (response == true)
            {
                status = "\"Data Stored Successfully\"";

            }
            else
            {
                status = "\"Try Again\"";
            }

            return status;// == true ? Created(string.Empty, status) : Ok(status);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ManageProgramController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Azure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// ManageProgramController.
    /// </summary>
    public class ZmstProgramController
    {
        private readonly IZmstProgramDirector iManageProgramDirector;
        // private readonly ILogger<MdDistrictController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDistrictController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mddistrictDirector">mddistrictDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstProgramController(IZmstProgramDirector _iManageProgramDirector, ILogger<MdDistrictController> logger)
        {
            this.iManageProgramDirector = _iManageProgramDirector;

        }
        /// <summary>
        /// Get MdDistrict List.
        /// </summary>
        /// <returns>Get All MdDistrict List.</returns>

        // [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public List<ManageProgramModel> InsertAsync([FromBody] ExcelBase64 excelBase64)
        {
            return iManageProgramDirector.ProgramExcel(excelBase64);
        }
        //btnDownload_Click

        // [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public string Download()
        {
            return JsonSerializer.Serialize(iManageProgramDirector.DownloadExcel()); ;
        }

        // [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProgram), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstProgram>>> GetAll()
        {
            return await iManageProgramDirector.GetAll(default).ConfigureAwait(false);
        }
    }
}

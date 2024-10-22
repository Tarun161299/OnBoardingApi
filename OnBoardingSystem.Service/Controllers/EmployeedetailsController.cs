
//-----------------------------------------------------------------------
// <copyright file="EmployeeDetailsController.cs" company="NIC">
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
    /// EmployeeDetailsController.
    /// </summary>
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailsDirector employeedetailsDirector;
        private readonly ILogger<EmployeeDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="employeedetailsDirector">employeedetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public EmployeeDetailsController(IEmployeeDetailsDirector employeedetailsDirector, ILogger<EmployeeDetailsController> logger)
        {
            this.employeedetailsDirector = employeedetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get EmployeeDetails List.
        /// </summary>
        /// <returns>Get All EmployeeDetails List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.EmployeeDetails>>> GetAllAsync()
        {
            return await employeedetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get EmployeeDetails List By Id.
        /// </summary>
        /// <param name="EmpId">EmpId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.EmployeeDetails>> GetByIdAsync(int EmpId)
        {
            var response = await employeedetailsDirector.GetByIdAsync(EmpId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get All EmployeeDetails List.
        /// </summary>
        /// <returns>Get All EmployeeDetails List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.EmployeeDetails>>> GetAllEmpDetailsAsync()
        {
            return await employeedetailsDirector.GetAllEmpDetailsAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get EmployeeDetails List By Id.
        /// </summary>
        /// <param name="EmpId">EmpId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.EmployeeDetails>> GetByEmployeeCodeAsync(string EmpCode)
        {
            var response = await employeedetailsDirector.GetByEmployeeCodeAsync(EmpCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert EmployeeDetails.
        /// </summary>
        /// <param name="employeedetails">employeedetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] AbsModels.EmployeeDetails employeedetails)
        {
            if (employeedetails == null)
            {
                return BadRequest(employeedetails);
            }

            try
            {
                var response = await employeedetailsDirector.InsertAsync(employeedetails, default).ConfigureAwait(false);
                return response;
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

        /// <summary>
        /// Update to EmployeeDetails.
        /// </summary>
        /// <param name="employeedetails">employeedetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.EmployeeDetails employeedetails)
        {
            string status;
            if (employeedetails == null)
            {
                return BadRequest(nameof(employeedetails));
            }

            if (employeedetails.EmpCode == "")
            {
                return BadRequest(nameof(employeedetails.EmpCode));
            }

            try
            {
                var response = await employeedetailsDirector.UpdateAsync(employeedetails, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Updated Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete EmployeeDetails.
        /// </summary>
        /// <param name="EmpId">EmpId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(int EmpId)
        {
            string status;
            var response = await employeedetailsDirector.DeleteAsync(EmpId, default).ConfigureAwait(false);

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

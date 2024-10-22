
//-----------------------------------------------------------------------
// <copyright file="EmployeeWorkOrderController.cs" company="NIC">
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
    /// EmployeeWorkOrderController.
    /// </summary>
    public class EmployeeWorkOrderController : ControllerBase
    {
        private readonly IEmployeeWorkOrderDirector employeeworkorderDirector;
        private readonly ILogger<EmployeeWorkOrderController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeWorkOrderController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="employeeworkorderDirector">employeeworkorderDirector.</param>
        /// <param name="logger">logger.</param>
        public EmployeeWorkOrderController(IEmployeeWorkOrderDirector employeeworkorderDirector, ILogger<EmployeeWorkOrderController> logger)
        {
            this.employeeworkorderDirector = employeeworkorderDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get employeeWorkOrder List.
        /// </summary>
        /// <returns>Get All employeeWorkOrder List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.EmployeeWorkOrder>>> GetAllAsync()
        
        {
            return await employeeworkorderDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get employeeWorkOrder List By Id.
        /// </summary>
        /// <param name="EmpCode">EmpCode.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.EmployeeWorkOrder>> GetByIdAsync(string EmpCode)
        {
            var response = await employeeworkorderDirector.GetByIdAsync(EmpCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get employeeWorkOrder List By Id.
        /// </summary>
        /// <param name="EmpCode">EmpCode.</param>
        /// <returns>Get by id.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.EmployeeWorkOrder), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.EmployeeWorkOrder>>> GetByEmpCodeAsync(string EmpCode)
        {
            var response = await employeeworkorderDirector.GetByEmpCodeAsync(EmpCode, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Insert employeeWorkOrder.
        /// </summary>
        /// <param name="employeeworkorder">employeeworkorder.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.EmployeeWorkOrder employeeworkorder)
        {
            if (employeeworkorder == null)
            {
                return BadRequest(employeeworkorder);
            }

            try
            {
                var response = await employeeworkorderDirector.InsertAsync(employeeworkorder, default).ConfigureAwait(false);
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

        /// <summary>
        /// Update to employeeWorkOrder.
        /// </summary>
        /// <param name="employeeworkorder">employeeworkorder.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.EmployeeWorkOrder employeeworkorder)
        {
            if (employeeworkorder == null)
            {
                return BadRequest(nameof(employeeworkorder));
            }

            if (employeeworkorder.EmpCode == "0")
            {
                return BadRequest(nameof(employeeworkorder.EmpCode));
            }

            try
            {
                var response = await employeeworkorderDirector.UpdateAsync(employeeworkorder, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete employeeWorkOrder.
        /// </summary>
        /// <param name="EmpCode">EmpCode.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync([FromBody] AbsModels.EmployeeWorkOrder employeeworkorder)
        {
            string status;
            var response = await employeeworkorderDirector.DeleteAsync(employeeworkorder, default).ConfigureAwait(false);

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
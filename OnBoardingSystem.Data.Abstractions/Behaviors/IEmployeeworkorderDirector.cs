
//-----------------------------------------------------------------------
// <copyright file="IemployeeWorkOrderDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of EmployeeWorkOrder behavior.
    /// </summary>
    public interface IEmployeeWorkOrderDirector
    {
        /// <summary>
        ///  Get All EmployeeWorkOrder List.
        /// </summary>
        /// <returns>EmployeeWorkOrder List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<EmployeeWorkOrder>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get EmployeeWorkOrder Entity.
        /// </summary>
        /// <returns>EmployeeWorkOrder Entity.</returns>
        /// <param name="EmpCode">EmpCode Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeWorkOrder> GetByIdAsync(string EmpCode, CancellationToken cancellationToken);

        /// <summary>
        ///  Get EmployeeWorkOrder Entity.
        /// </summary>
        /// <returns>EmployeeWorkOrder Entity.</returns>
        /// <param name="EmpCode">EmpCode Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<EmployeeWorkOrder>> GetByEmpCodeAsync(string EmpCode, CancellationToken cancellationToken);

        /// <summary>
        /// Delete EmployeeWorkOrder.
        /// </summary>
        /// <param name="EmpCode">EmpCode Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken);

        /// <summary>
        /// Save EmployeeWorkOrder.
        /// </summary>
        /// <param name="employeeworkorder">employeeworkorder Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken);

        /// <summary>
        /// Update EmployeeWorkOrder.
        /// </summary>
        /// <param name="employeeworkorder">employeeworkorder Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken);		
    }
}

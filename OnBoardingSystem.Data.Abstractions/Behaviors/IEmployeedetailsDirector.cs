
//-----------------------------------------------------------------------
// <copyright file="IEmployeeDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of EmployeeDetails behavior.
    /// </summary>
    public interface IEmployeeDetailsDirector
    {
        /// <summary>
        ///  Get All EmployeeDetails List.
        /// </summary>
        /// <returns>EmployeeDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<EmployeeDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get EmployeeDetails Entity.
        /// </summary>
        /// <returns>EmployeeDetails Entity.</returns>
        /// <param name="EmpId">EmpId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeDetails> GetByIdAsync(int EmpId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get All EmployeeDetails List.
        /// </summary>
        /// <returns>EmployeeDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<EmployeeDetails>> GetAllEmpDetailsAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get EmployeeDetails Entity.
        /// </summary>
        /// <returns>EmployeeDetails Entity.</returns>
        /// <param name="EmpCode">EmpCode Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeDetails> GetByEmployeeCodeAsync(string EmpCode, CancellationToken cancellationToken);

        /// <summary>
        /// Delete EmployeeDetails.
        /// </summary>
        /// <param name="EmpId">EmpId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int EmpId, CancellationToken cancellationToken);

        /// <summary>
        /// Save EmployeeDetails.
        /// </summary>
        /// <param name="employeedetails">employeedetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<string> InsertAsync(EmployeeDetails employeedetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update EmployeeDetails.
        /// </summary>
        /// <param name="employeedetails">employeedetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(EmployeeDetails employeedetails, CancellationToken cancellationToken);
		
    }
}

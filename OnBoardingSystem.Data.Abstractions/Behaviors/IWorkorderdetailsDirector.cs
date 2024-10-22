
//-----------------------------------------------------------------------
// <copyright file="IWorkOrderDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of WorkOrderDetails behavior.
    /// </summary>
    public interface IWorkOrderDetailsDirector
    {
        /// <summary>
        ///  Get All WorkOrderDetails List.
        /// </summary>
        /// <returns>WorkOrderDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<WorkOrderDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get WorkOrderDetails Entity.
        /// </summary>
        /// <returns>WorkOrderDetails Entity.</returns>
        /// <param name="WorkorderId">WorkorderId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<WorkOrderDetails> GetByIdAsync(int WorkorderId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete WorkOrderDetails.
        /// </summary>
        /// <param name="WorkorderId">WorkorderId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int WorkorderId, CancellationToken cancellationToken);

        /// <summary>
        /// Save WorkOrderDetails.
        /// </summary>
        /// <param name="workorderdetails">workorderdetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(WorkOrderDetails workorderdetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update WorkOrderDetails.
        /// </summary>
        /// <param name="workorderdetails">workorderdetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(WorkOrderDetails workorderdetails, CancellationToken cancellationToken);

        /// <summary>
        ///  Get WorkOrderDetails Entity.
        /// </summary>
        /// <returns>WorkOrderDetails Entity.</returns>
        /// <param name="WorkorderId">WorkorderId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<WorkOrderDetails>> GetByProjectCodeAsync(string projectCode, CancellationToken cancellationToken);
        
    }
}

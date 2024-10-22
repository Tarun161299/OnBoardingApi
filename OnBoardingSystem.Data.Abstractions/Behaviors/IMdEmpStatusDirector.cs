
//-----------------------------------------------------------------------
// <copyright file="IMdEmpStatusDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdEmpStatus behavior.
    /// </summary>
    public interface IMdEmpStatusDirector
    {
        /// <summary>
        ///  Get All MdEmpStatus List.
        /// </summary>
        /// <returns>MdEmpStatus List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdEmpStatus>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdEmpStatus Entity.
        /// </summary>
        /// <returns>MdEmpStatus Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdEmpStatus> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdEmpStatus.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdEmpStatus.
        /// </summary>
        /// <param name="mdempstatus">mdempstatus Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdEmpStatus mdempstatus, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdEmpStatus.
        /// </summary>
        /// <param name="mdempstatus">mdempstatus Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdEmpStatus mdempstatus, CancellationToken cancellationToken);
		
    }
}

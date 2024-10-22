
//-----------------------------------------------------------------------
// <copyright file="IMdStatusDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using OnBoardingSystem.Data.Abstractions.Models;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    //using OnBoardingSystem.Common.Models;
    /// <summary>
    /// Director of MdStatus behavior.
    /// </summary>
    public interface IMdStatusDirector
    {
        /// <summary>
        ///  Get All MdStatus List.
        /// </summary>
        /// <returns>MdStatus List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdStatus>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdStatus Entity.
        /// </summary>
        /// <returns>MdStatus Entity.</returns>
        /// <param name="StatusId">StatusId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdStatus> GetByIdAsync(string StatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdStatus.
        /// </summary>
        /// <param name="StatusId">StatusId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string StatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdStatus.
        /// </summary>
        /// <param name="mdstatus">mdstatus Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdStatus mdstatus, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdStatus.
        /// </summary>
        /// <param name="mdstatus">mdstatus Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdStatus mdstatus, CancellationToken cancellationToken);

    }
}

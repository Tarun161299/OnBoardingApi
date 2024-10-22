
//-----------------------------------------------------------------------
// <copyright file="IZmstFeeTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstFeeType behavior.
    /// </summary>
    public interface IZmstFeeTypeDirector
    {
        /// <summary>
        ///  Get All ZmstFeeType List.
        /// </summary>
        /// <returns>ZmstFeeType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstFeeType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstFeeType Entity.
        /// </summary>
        /// <returns>ZmstFeeType Entity.</returns>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstFeeType> GetByIdAsync(int ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstFeeType.
        /// </summary>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstFeeType.
        /// </summary>
        /// <param name="zmstfeetype">zmstfeetype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstFeeType zmstfeetype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstFeeType.
        /// </summary>
        /// <param name="zmstfeetype">zmstfeetype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstFeeType zmstfeetype, CancellationToken cancellationToken);
		
    }
}

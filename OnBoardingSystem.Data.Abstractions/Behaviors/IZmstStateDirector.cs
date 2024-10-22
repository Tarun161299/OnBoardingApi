
//-----------------------------------------------------------------------
// <copyright file="IZmstStateDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstState behavior.
    /// </summary>
    public interface IZmstStateDirector
    {
        /// <summary>
        ///  Get All ZmstState List.
        /// </summary>
        /// <returns>ZmstState List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstState>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstState Entity.
        /// </summary>
        /// <returns>ZmstState Entity.</returns>
        /// <param name="StateId">StateId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstState> GetByIdAsync(string StateId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstState.
        /// </summary>
        /// <param name="StateId">StateId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string StateId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstState.
        /// </summary>
        /// <param name="zmststate">zmststate Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstState zmststate, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstState.
        /// </summary>
        /// <param name="zmststate">zmststate Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstState zmststate, CancellationToken cancellationToken);
		
    }
}

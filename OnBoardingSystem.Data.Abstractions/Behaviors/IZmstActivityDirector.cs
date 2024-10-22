
//-----------------------------------------------------------------------
// <copyright file="IZmstActivityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    //using OnBoardingSystem.Data.EF.Models;

    /// <summary>
    /// Director of ZmstActivity behavior.
    /// </summary>
    public interface IZmstActivityDirector
    {
        /// <summary>
        ///  Get All ZmstActivity List.
        /// </summary>
        /// <returns>ZmstActivity List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstActivity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstActivity Entity.
        /// </summary>
        /// <returns>ZmstActivity Entity.</returns>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstActivity> GetByIdAsync(string ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstActivity.
        /// </summary>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstActivity.
        /// </summary>
        /// <param name="zmstactivity">zmstactivity Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstActivity zmstactivity, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstActivity.
        /// </summary>
        /// <param name="zmstactivity">zmstactivity Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstActivity zmstactivity, CancellationToken cancellationToken);

    }
}

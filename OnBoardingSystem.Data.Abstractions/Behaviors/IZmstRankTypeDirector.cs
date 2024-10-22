
//-----------------------------------------------------------------------
// <copyright file="IZmstRankTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstRankType behavior.
    /// </summary>
    public interface IZmstRankTypeDirector
    {
        /// <summary>
        ///  Get All ZmstRankType List.
        /// </summary>
        /// <returns>ZmstRankType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstRankType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstRankType Entity.
        /// </summary>
        /// <returns>ZmstRankType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstRankType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstRankType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstRankType.
        /// </summary>
        /// <param name="zmstranktype">zmstranktype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstRankType zmstranktype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstRankType.
        /// </summary>
        /// <param name="zmstranktype">zmstranktype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstRankType zmstranktype, CancellationToken cancellationToken);
		
    }
}

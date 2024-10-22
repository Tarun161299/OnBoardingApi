
//-----------------------------------------------------------------------
// <copyright file="IZmstQuesPaperDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQuesPaper behavior.
    /// </summary>
    public interface IZmstQuesPaperDirector
    {
        /// <summary>
        ///  Get All ZmstQuesPaper List.
        /// </summary>
        /// <returns>ZmstQuesPaper List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQuesPaper>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQuesPaper Entity.
        /// </summary>
        /// <returns>ZmstQuesPaper Entity.</returns>
        /// <param name="PaperId">PaperId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQuesPaper> GetByIdAsync(string PaperId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQuesPaper.
        /// </summary>
        /// <param name="PaperId">PaperId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string PaperId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQuesPaper.
        /// </summary>
        /// <param name="zmstquespaper">zmstquespaper Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQuesPaper zmstquespaper, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQuesPaper.
        /// </summary>
        /// <param name="zmstquespaper">zmstquespaper Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQuesPaper zmstquespaper, CancellationToken cancellationToken);
		
    }
}

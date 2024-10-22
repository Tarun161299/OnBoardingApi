
//-----------------------------------------------------------------------
// <copyright file="IZmstExamTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstExamType behavior.
    /// </summary>
    public interface IZmstExamTypeDirector
    {
        /// <summary>
        ///  Get All ZmstExamType List.
        /// </summary>
        /// <returns>ZmstExamType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstExamType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstExamType Entity.
        /// </summary>
        /// <returns>ZmstExamType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstExamType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstExamType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstExamType.
        /// </summary>
        /// <param name="zmstexamtype">zmstexamtype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstExamType zmstexamtype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstExamType.
        /// </summary>
        /// <param name="zmstexamtype">zmstexamtype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstExamType zmstexamtype, CancellationToken cancellationToken);
		
    }
}

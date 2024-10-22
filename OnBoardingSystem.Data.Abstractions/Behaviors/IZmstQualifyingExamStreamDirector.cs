
//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamStreamDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExamStream behavior.
    /// </summary>
    public interface IZmstQualifyingExamStreamDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExamStream List.
        /// </summary>
        /// <returns>ZmstQualifyingExamStream List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExamStream>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExamStream Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExamStream Entity.</returns>
        /// <param name="QualStreamId">QualStreamId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExamStream> GetByIdAsync(string QualStreamId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="QualStreamId">QualStreamId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QualStreamId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="zmstqualifyingexamstream">zmstqualifyingexamstream Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExamStream zmstqualifyingexamstream, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExamStream.
        /// </summary>
        /// <param name="zmstqualifyingexamstream">zmstqualifyingexamstream Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExamStream zmstqualifyingexamstream, CancellationToken cancellationToken);
		
    }
}

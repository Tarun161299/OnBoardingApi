
//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExam behavior.
    /// </summary>
    public interface IZmstQualifyingExamDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExam List.
        /// </summary>
        /// <returns>ZmstQualifyingExam List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExam>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExam Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExam Entity.</returns>
        /// <param name="QualifyingExamId">QualifyingExamId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExam> GetByIdAsync(string QualifyingExamId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExam.
        /// </summary>
        /// <param name="QualifyingExamId">QualifyingExamId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QualifyingExamId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExam.
        /// </summary>
        /// <param name="zmstqualifyingexam">zmstqualifyingexam Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExam zmstqualifyingexam, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExam.
        /// </summary>
        /// <param name="zmstqualifyingexam">zmstqualifyingexam Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExam zmstqualifyingexam, CancellationToken cancellationToken);
		
    }
}

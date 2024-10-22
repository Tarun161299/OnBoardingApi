
//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamFromDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExamFrom behavior.
    /// </summary>
    public interface IZmstQualifyingExamFromDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExamFrom List.
        /// </summary>
        /// <returns>ZmstQualifyingExamFrom List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExamFrom>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExamFrom Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExamFrom Entity.</returns>
        /// <param name="QualExamFromId">QualExamFromId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExamFrom> GetByIdAsync(string QualExamFromId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="QualExamFromId">QualExamFromId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QualExamFromId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="zmstqualifyingexamfrom">zmstqualifyingexamfrom Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExamFrom zmstqualifyingexamfrom, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExamFrom.
        /// </summary>
        /// <param name="zmstqualifyingexamfrom">zmstqualifyingexamfrom Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExamFrom zmstqualifyingexamfrom, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamBoardDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExamBoard behavior.
    /// </summary>
    public interface IZmstQualifyingExamBoardDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExamBoard List.
        /// </summary>
        /// <returns>ZmstQualifyingExamBoard List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExamBoard>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExamBoard Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExamBoard Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExamBoard> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExamBoard.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExamBoard.
        /// </summary>
        /// <param name="zmstqualifyingexamboard">zmstqualifyingexamboard Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExamBoard zmstqualifyingexamboard, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExamBoard.
        /// </summary>
        /// <param name="zmstqualifyingexamboard">zmstqualifyingexamboard Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExamBoard zmstqualifyingexamboard, CancellationToken cancellationToken);
		
    }
}

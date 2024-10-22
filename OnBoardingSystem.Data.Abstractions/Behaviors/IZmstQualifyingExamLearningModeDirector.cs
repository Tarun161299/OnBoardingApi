
//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamLearningModeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExamLearningMode behavior.
    /// </summary>
    public interface IZmstQualifyingExamLearningModeDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExamLearningMode List.
        /// </summary>
        /// <returns>ZmstQualifyingExamLearningMode List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExamLearningMode>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExamLearningMode Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExamLearningMode Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExamLearningMode> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExamLearningMode.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExamLearningMode.
        /// </summary>
        /// <param name="zmstqualifyingexamlearningmode">zmstqualifyingexamlearningmode Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExamLearningMode.
        /// </summary>
        /// <param name="zmstqualifyingexamlearningmode">zmstqualifyingexamlearningmode Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode, CancellationToken cancellationToken);
		
    }
}

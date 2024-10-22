
//-----------------------------------------------------------------------
// <copyright file="IZmstQuestionPaperMediumDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQuestionPaperMedium behavior.
    /// </summary>
    public interface IZmstQuestionPaperMediumDirector
    {
        /// <summary>
        ///  Get All ZmstQuestionPaperMedium List.
        /// </summary>
        /// <returns>ZmstQuestionPaperMedium List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQuestionPaperMedium>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQuestionPaperMedium Entity.
        /// </summary>
        /// <returns>ZmstQuestionPaperMedium Entity.</returns>
        /// <param name="MediumId">MediumId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQuestionPaperMedium> GetByIdAsync(string MediumId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="MediumId">MediumId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string MediumId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="zmstquestionpapermedium">zmstquestionpapermedium Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQuestionPaperMedium zmstquestionpapermedium, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQuestionPaperMedium.
        /// </summary>
        /// <param name="zmstquestionpapermedium">zmstquestionpapermedium Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQuestionPaperMedium zmstquestionpapermedium, CancellationToken cancellationToken);
		
    }
}

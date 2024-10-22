
//-----------------------------------------------------------------------
// <copyright file="IZmstSpecialExamPaperDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSpecialExamPaper behavior.
    /// </summary>
    public interface IZmstSpecialExamPaperDirector
    {
        /// <summary>
        ///  Get All ZmstSpecialExamPaper List.
        /// </summary>
        /// <returns>ZmstSpecialExamPaper List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSpecialExamPaper>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSpecialExamPaper Entity.
        /// </summary>
        /// <returns>ZmstSpecialExamPaper Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSpecialExamPaper> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="zmstspecialexampaper">zmstspecialexampaper Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSpecialExamPaper zmstspecialexampaper, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSpecialExamPaper.
        /// </summary>
        /// <param name="zmstspecialexampaper">zmstspecialexampaper Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSpecialExamPaper zmstspecialexampaper, CancellationToken cancellationToken);
		
    }
}

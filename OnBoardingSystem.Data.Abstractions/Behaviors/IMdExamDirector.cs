
//-----------------------------------------------------------------------
// <copyright file="IMdExamDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdExam behavior.
    /// </summary>
    public interface IMdExamDirector
    {
        /// <summary>
        ///  Get All MdExam List.
        /// </summary>
        /// <returns>MdExam List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdExam>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdExam Entity.
        /// </summary>
        /// <returns>MdExam Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdExam> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdExam.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdExam.
        /// </summary>
        /// <param name="mdexam">mdexam Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdExam mdexam, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdExam.
        /// </summary>
        /// <param name="mdexam">mdexam Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdExam mdexam, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingExamResultModeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingExamResultMode behavior.
    /// </summary>
    public interface IZmstQualifyingExamResultModeDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingExamResultMode List.
        /// </summary>
        /// <returns>ZmstQualifyingExamResultMode List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingExamResultMode>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingExamResultMode Entity.
        /// </summary>
        /// <returns>ZmstQualifyingExamResultMode Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingExamResultMode> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingExamResultMode.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingExamResultMode.
        /// </summary>
        /// <param name="zmstqualifyingexamresultmode">zmstqualifyingexamresultmode Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingExamResultMode zmstqualifyingexamresultmode, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingExamResultMode.
        /// </summary>
        /// <param name="zmstqualifyingexamresultmode">zmstqualifyingexamresultmode Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingExamResultMode zmstqualifyingexamresultmode, CancellationToken cancellationToken);
		
    }
}

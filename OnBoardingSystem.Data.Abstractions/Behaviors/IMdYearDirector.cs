
//-----------------------------------------------------------------------
// <copyright file="IMdYearDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdYear behavior.
    /// </summary>
    public interface IMdYearDirector
    {
        /// <summary>
        ///  Get All MdYear List.
        /// </summary>
        /// <returns>MdYear List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdYear>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdYear Entity.
        /// </summary>
        /// <returns>MdYear Entity.</returns>
        /// <param name="YearId">YearId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdYear>> GetByGroupIdAsync(string YearId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdYear.
        /// </summary>
        /// <param name="YearId">YearId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string YearId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdYear.
        /// </summary>
        /// <param name="mdyear">mdyear Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdYear mdyear, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdYear.
        /// </summary>
        /// <param name="mdyear">mdyear Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdYear mdyear, CancellationToken cancellationToken);
		
    }
}

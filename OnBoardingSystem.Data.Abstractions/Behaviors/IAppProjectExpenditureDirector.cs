
//-----------------------------------------------------------------------
// <copyright file="IAppProjectExpenditureDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppProjectExpenditure behavior.
    /// </summary>
    public interface IAppProjectExpenditureDirector
    {
        /// <summary>
        ///  Get All AppProjectExpenditure List.
        /// </summary>
        /// <returns>AppProjectExpenditure List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectExpenditure>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppProjectExpenditure Entity.
        /// </summary>
        /// <returns>AppProjectExpenditure Entity.</returns>
        /// <param name="ProjectId">ProjectId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectExpenditure>> GetByIdAsync(int ProjectId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppProjectExpenditure.
        /// </summary>
        /// <param name="ProjectId">ProjectId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ProjectId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppProjectExpenditure.
        /// </summary>
        /// <param name="appprojectexpenditure">appprojectexpenditure Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppProjectExpenditure.
        /// </summary>
        /// <param name="appprojectexpenditure">appprojectexpenditure Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken);
		
    }
}

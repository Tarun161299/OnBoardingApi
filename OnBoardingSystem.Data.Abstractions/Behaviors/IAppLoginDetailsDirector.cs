
//-----------------------------------------------------------------------
// <copyright file="IAppLoginDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppLoginDetails behavior.
    /// </summary>
    public interface IAppLoginDetailsDirector
    {
        /// <summary>
        ///  Get All AppLoginDetails List.
        /// </summary>
        /// <returns>AppLoginDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
       // Task<List<AppLoginDetails>> GetAllAsync(CancellationToken cancellationToken);

        Task<List<AppLoginDetails>> GetAllPmuUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppLoginDetails Entity.
        /// </summary>
        /// <returns>AppLoginDetails Entity.</returns>
        /// <param name="RequestNo">RequestNo Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppLoginDetails>> GetByIdAsync(string UserID, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppLoginDetails.
        /// </summary>
        /// <param name="RequestNo">RequestNo Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string RequestNo, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppLoginDetails applogindetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppLoginDetails applogindetails, CancellationToken cancellationToken);
		
    }
}

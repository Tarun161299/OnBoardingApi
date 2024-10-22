//-----------------------------------------------------------------------
// <copyright file="IMdMinistryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdMinistry Director behavior.
    /// </summary>
    public interface IAppProjectCostDiector
    {
        /// <summary>
        /// Save Ministry.
        /// </summary>
        /// <param name="mdMinistry">saveMinistry.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> SaveAsync(AppProjectCost appProjectCost, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectCostList>> GetByIdAsync(int projectid, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Ministry.
        /// </summary>
        /// <param name="ministryId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int financialComponentId,int projectid, CancellationToken cancellationToken);

        /// <summary>
        /// Update ministry.
        /// </summary>
        /// <param name="ministryId">ministryId.</param>
        /// <param name="ministryName">ministryName.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(Abstractions.Models.AppProjectCost appProjectCost, CancellationToken cancellationToken);
    }
}

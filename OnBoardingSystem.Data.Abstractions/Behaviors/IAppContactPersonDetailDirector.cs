//-----------------------------------------------------------------------
// <copyright file="IAppContactPersonDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAppContactPersonDetailDirector Director behavior.
    /// </summary>
    public interface IAppContactPersonDetailDirector
    {
        /// <summary>
        ///  Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>AppOnboardingRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppContactPersonDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Insert AppOnboardingRequest Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="appOnboardingRequestData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveAsync(AppContactPersonDetails appOnboardingRequestData, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="requestId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppContactPersonDetails> GetListByIdAsync(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppContactPersonDetails List.
        /// </summary>
        /// <returns>Onboarding AppContactPersonDetails data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppContactPersonDetails>> GetByRequestIdAsync(string requestId, CancellationToken cancellationToken);
    }
}
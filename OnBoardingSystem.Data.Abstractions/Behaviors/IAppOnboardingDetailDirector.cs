//-----------------------------------------------------------------------
// <copyright file="IAppOnboardingDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAppOnboardingRequestDirector Director behavior.
    /// </summary>
    public interface IAppOnboardingDetailDirector
    {
        /// <summary>
        ///  Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>AppOnboardingRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppOnboardingDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Insert AppOnboardingRequest Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="appOnboardingRequestData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveAsync(AppOnboardingDetails appOnboardingRequestData, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="requestId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppOnboardingDetails> GetListByIdAsync(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert UserInfo Data.
        /// </summary>
        /// <returns>UserInfo.</returns>
        /// <param name="appOnboardingDetail">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> Save(string appOnboardingDetail, CancellationToken cancellationToken);

        Task<int> Updatestatus(AppOnboardingDetailStatus appststus, CancellationToken cancellationToken);
    }
}

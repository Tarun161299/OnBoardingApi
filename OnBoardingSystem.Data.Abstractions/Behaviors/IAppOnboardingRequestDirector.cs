//-----------------------------------------------------------------------
// <copyright file="IAppOnboardingRequestDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAppOnboardingRequestDirector Director behavior.
    /// </summary>
    public interface IAppOnboardingRequestDirector
    {
        /// <summary>
        ///  Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>AppOnboardingRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<OnBoardingRequestDetailUpsert>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        ///  Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>AppOnboardingRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<OnBoardingRequestDetailUpsert>> GetAllByStatusAsync(string Status,CancellationToken cancellationToken);
        /// <summary>
        ///  Insert AppOnboardingRequest Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="appOnboardingRequestData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> SaveAppOnboardingRequestDataAsync(AppOnboardingRequest appOnboardingRequestData, CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="OTP">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> SendOTP(OTPModal otpModal, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        //Task<AppOnboardRequestAndDetail> GetRequestListByIdAsync(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppOnboardRequestAndDetail> GetByIdAsync(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Status> GetStatusByIdAsync(string requestNo, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest Details.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> GetOnBoardingRequestLink(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Dsashboard Status Count.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DashboardCount> GetStatusCountAsync(CancellationToken cancellationToken);
    }
}

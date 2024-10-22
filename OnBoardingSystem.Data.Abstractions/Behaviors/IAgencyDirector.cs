//-----------------------------------------------------------------------
// <copyright file="IAppContactPersonDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAgencyDirector Director behavior.
    /// </summary>
    public interface IAgencyDirector
    {
        /// <summary>
        ///  Get AppOnBoardingRequest List.
        /// </summary>
        /// <returns>AppOnboardingRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdAgency>> GetAllAsync(CancellationToken cancellationToken);
    }
}

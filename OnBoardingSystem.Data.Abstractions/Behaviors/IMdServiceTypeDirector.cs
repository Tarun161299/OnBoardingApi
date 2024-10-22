//-----------------------------------------------------------------------
// <copyright file="IMdServiceTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdServiceType Director behavior.
    /// </summary>
    public interface IMdServiceTypeDirector
    {
        /// <summary>
        ///  Get Service List.
        /// </summary>
        /// <returns>Onboarding Service List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdServiceType>> GetAllAsync(CancellationToken cancellationToken);
    }
}

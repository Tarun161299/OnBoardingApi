//-----------------------------------------------------------------------
// <copyright file="IMDModuleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MDModuleDirector Director behavior.
    /// </summary>
    public interface IMDModuleDirector
    {
        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MDModule> GetByUserIdAsync(string UserId,CancellationToken cancellationToken);

    }
}

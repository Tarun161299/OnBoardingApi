//-----------------------------------------------------------------------
// <copyright file="IRequestListInfoDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    
    /// <summary>
    /// Director of RequestList Director behavior.
    /// </summary>
    public interface IRequestListInfoDirector
    {
        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>OnboardingRequestList Info .</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<RequestList>> GetRequestListAsync(CancellationToken cancellationToken);
    }
}

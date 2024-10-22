//-----------------------------------------------------------------------
// <copyright file="IMdAgencyTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdAgencyType Director behavior.
    /// </summary>
    public interface IMdAgencyTypeDirector
    {
        /// <summary>
        ///  Get Md_AgencyType List.
        /// </summary>
        /// <returns>Onboarding Agency List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdAgencyType>> GetAllAsync(CancellationToken cancellationToken);
    }
}

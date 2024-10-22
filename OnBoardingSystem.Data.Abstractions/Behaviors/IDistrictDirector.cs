//-----------------------------------------------------------------------
// <copyright file="IDistrictDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IDistrictDirector
    {
        /// <summary>
        ///  Get State List.
        /// </summary>
        /// <returns>State.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDistrict>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppOnboardingRequestId .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDistrict>> GetDistrictListByStateIdAsync(string stateID, CancellationToken cancellationToken);
    }
}

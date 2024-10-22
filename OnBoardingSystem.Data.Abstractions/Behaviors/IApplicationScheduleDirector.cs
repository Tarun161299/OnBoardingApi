//-----------------------------------------------------------------------
// <copyright file="IApplicationScheduleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ApplicationSchedule Director behavior.
    /// </summary>
    public interface IApplicationScheduleDirector
    {
        /// <summary>
        ///  Get ApplicationSchedule List.
        /// </summary>
        /// <returns>Onboarding ApplicationSchedule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ApplicationSchedule>> GetAllAsync(calendarDate startEndDate,CancellationToken cancellationToken);
       // GetAllActivityAsync
        /// <summary>
        ///  Get ApplicationSchedule List.
        /// </summary>
        /// <returns>Onboarding ApplicationSchedule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppScheduleData>> GetAllActivityAsync( CancellationToken cancellationToken);

        /// <summary>
        ///  Get ApplicationSchedule List.
        /// </summary>
        /// <returns>Onboarding ApplicationSchedule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetByProjectId(List<ZmstProjectSchedule> startEndDate, CancellationToken cancellationToken);

    }
}

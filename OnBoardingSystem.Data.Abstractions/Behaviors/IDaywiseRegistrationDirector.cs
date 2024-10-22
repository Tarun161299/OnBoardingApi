using OnBoardingSystem.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    public interface IDaywiseRegistrationDirector
    {
        /// <summary>
        ///  Get ApplicationSchedule List.
        /// </summary>
        /// <returns>Onboarding ApplicationSchedule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ApplicationDayWise>> GetAllAsync(DaywiseRegistration startEndDate, CancellationToken cancellationToken);
        
        ///// <summary>
        /////  Get ApplicationSchedule List.
        ///// </summary>
        ///// <returns>Onboarding ApplicationSchedule List.</returns>
        ///// <param name="cancellationToken">cancellation Token.</param>
        ///// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        //Task<List<AppScheduleData>> GetAllActivityAsync(CancellationToken cancellationToken);

    }
}

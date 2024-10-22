using OnBoardingSystem.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    public interface IAppOnboardingResponseDirector
    {

        /// <summary>
        ///  Insert AppOnboardingResponse Data By Procedure.
        /// </summary>
        /// <returns>AppOnboardingResponse.</returns>
        /// <param name="ministryId">entityID Token.</param>
        /// <param name="ministryName">NDC Tok.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> SaveAppOnboardingResponseData(AppOnboardingResponse appOnboardingResponse, CancellationToken cancellationToken);

    }
}

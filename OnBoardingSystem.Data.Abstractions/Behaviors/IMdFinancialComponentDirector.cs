using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdMinistry Director behavior.
    /// </summary>
    public interface IMdFinancialComponentDirector
    {
        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdProjectFinancialComponents>> GetAllAsync(CancellationToken cancellationToken);
    }
}

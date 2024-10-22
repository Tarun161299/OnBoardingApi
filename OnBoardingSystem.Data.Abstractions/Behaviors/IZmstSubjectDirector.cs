
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBoardingSystem.Data.Abstractions.Models;
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    /// <summary>
    /// Director of MdMinistry Director behavior.
    /// </summary>
    public interface IZmstSubjectDirector
    {
        /// <summary>
        ///  Get ServiceType List.
        /// </summary>
        /// <returns>Onboarding ServiceType.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSubject>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Insert AppOnboardingRequest Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="zmstSubjectData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSubject zmstSubjectData, CancellationToken cancellationToken);
        /// <summary>
        /// Update MdDistrict.
        /// </summary>
        /// <param name="zmstSubjectId">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(ZmstSubject zmstSubjectId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete MdDistrict.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

    }

}

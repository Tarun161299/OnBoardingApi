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
    public interface IZmstAgencyExamCounsDirector
    {
        /// <summary>
        ///  Get All ZmstAgencyExamCouns List.
        /// </summary>
        /// <returns>ZmstAgencyExamCouns List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstAgencyExamCouns>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        ///  Get ServiceType List.
        /// </summary>
        /// <returns>Onboarding ServiceType.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstAgencyExamCouns>> GetByIdAsync(int agecnyid,CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="zmstagencyexamcouns">zmstagencyexamcouns Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstAgencyExamCouns zmstagencyexamcouns, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstAgencyExamCouns.
        /// </summary>
        /// <param name="zmstagencyexamcouns">zmstagencyexamcouns Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstAgencyExamCouns zmstagencyexamcouns, CancellationToken cancellationToken);

    }
}

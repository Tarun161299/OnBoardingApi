
//-----------------------------------------------------------------------
// <copyright file="IZmstResidentialEligibilityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstResidentialEligibility behavior.
    /// </summary>
    public interface IZmstResidentialEligibilityDirector
    {
        /// <summary>
        ///  Get All ZmstResidentialEligibility List.
        /// </summary>
        /// <returns>ZmstResidentialEligibility List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstResidentialEligibility>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstResidentialEligibility Entity.
        /// </summary>
        /// <returns>ZmstResidentialEligibility Entity.</returns>
        /// <param name="ResidentialEligibilityId">ResidentialEligibilityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstResidentialEligibility> GetByIdAsync(string ResidentialEligibilityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstResidentialEligibility.
        /// </summary>
        /// <param name="ResidentialEligibilityId">ResidentialEligibilityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string ResidentialEligibilityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstResidentialEligibility.
        /// </summary>
        /// <param name="zmstresidentialeligibility">zmstresidentialeligibility Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstResidentialEligibility zmstresidentialeligibility, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstResidentialEligibility.
        /// </summary>
        /// <param name="zmstresidentialeligibility">zmstresidentialeligibility Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstResidentialEligibility zmstresidentialeligibility, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstAgencyDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstAgency behavior.
    /// </summary>
    public interface IZmstAgencyDirector
    {
        /// <summary>
        ///  Get All ZmstAgency List.
        /// </summary>
        /// <returns>ZmstAgency List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstAgency>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstAgency Entity.
        /// </summary>
        /// <returns>ZmstAgency Entity.</returns>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstAgency> GetByIdAsync(string AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstAgency.
        /// </summary>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstAgency.
        /// </summary>
        /// <param name="zmstagency">zmstagency Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstAgency zmstagency, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstAgency.
        /// </summary>
        /// <param name="zmstagency">zmstagency Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstAgency zmstagency, CancellationToken cancellationToken);
		
    }
}

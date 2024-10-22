
//-----------------------------------------------------------------------
// <copyright file="IZmstQuotaDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQuota behavior.
    /// </summary>
    public interface IZmstQuotaDirector
    {
        /// <summary>
        ///  Get All ZmstQuota List.
        /// </summary>
        /// <returns>ZmstQuota List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQuota>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQuota Entity.
        /// </summary>
        /// <returns>ZmstQuota Entity.</returns>
        /// <param name="QuotaId">QuotaId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQuota> GetByIdAsync(string QuotaId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQuota.
        /// </summary>
        /// <param name="QuotaId">QuotaId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QuotaId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQuota.
        /// </summary>
        /// <param name="zmstquota">zmstquota Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQuota zmstquota, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQuota.
        /// </summary>
        /// <param name="zmstquota">zmstquota Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQuota zmstquota, CancellationToken cancellationToken);
		
    }
}

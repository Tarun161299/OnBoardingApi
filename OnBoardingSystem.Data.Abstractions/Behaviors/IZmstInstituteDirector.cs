
//-----------------------------------------------------------------------
// <copyright file="IZmstInstituteDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstInstitute behavior.
    /// </summary>
    public interface IZmstInstituteDirector
    {
        /// <summary>
        ///  Get All ZmstInstitute List.
        /// </summary>
        /// <returns>ZmstInstitute List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstInstitute>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstInstitute Entity.
        /// </summary>
        /// <returns>ZmstInstitute Entity.</returns>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstInstitute> GetByIdAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstitute.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstitute.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<ZmstInstitute>> GetAllByIdsAsync(FilterInstitute filterInstitute, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstitute.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<string> GetMaxIntitueCode(CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstInstitute.
        /// </summary>
        /// <param name="zmstinstitute">zmstinstitute Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstInstitute zmstinstitute, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstInstitute.
        /// </summary>
        /// <param name="zmstinstitute">zmstinstitute Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstInstitute zmstinstitute, CancellationToken cancellationToken);

        /// <summary>
        ///  Get All ZmstInstitute List.
        /// </summary>
        /// <returns>ZmstInstitute List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<InstituteStatistics>> GetInstituteStatistics(string Type, CancellationToken cancellationToken);

        /// <summary>
        ///  Get All ZmstInstitute List.
        /// </summary>
        /// <returns>ZmstInstitute List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstInstitute>> GetAllCountData(FiterInstituteCount fiterInstituteCount, CancellationToken cancellationToken);
    }
}

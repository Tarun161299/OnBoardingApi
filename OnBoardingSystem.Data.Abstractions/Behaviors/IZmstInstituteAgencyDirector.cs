
//-----------------------------------------------------------------------
// <copyright file="IZmstInstituteAgencyDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstInstituteAgency behavior.
    /// </summary>
    public interface IZmstInstituteAgencyDirector
    {
        /// <summary>
        ///  Get All ZmstInstituteAgency List.
        /// </summary>
        /// <returns>ZmstInstituteAgency List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstInstituteAgency>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstInstituteAgency Entity.
        /// </summary>
        /// <returns>ZmstInstituteAgency Entity.</returns>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstInstituteAgency> GetByIdAsync(string InstCd, CancellationToken cancellationToken);
        /// <summary>
        /// Delete ZmstInstituteAgency.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
       // Task<List<ZmstInstituteAgency>> GetByAgencyIdAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstituteAgency.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstInstituteAgency.
        /// </summary>
        /// <param name="zmstinstituteagency">zmstinstituteagency Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstInstituteAgency zmstinstituteagency, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstInstituteAgency.
        /// </summary>
        /// <param name="zmstinstituteagency">zmstinstituteagency Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstInstituteAgency zmstinstituteagency, CancellationToken cancellationToken);
		
    }
}

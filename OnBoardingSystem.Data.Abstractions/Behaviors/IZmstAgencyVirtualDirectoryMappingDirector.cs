
//-----------------------------------------------------------------------
// <copyright file="IZmstAgencyVirtualDirectoryMappingDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstAgencyVirtualDirectoryMapping behavior.
    /// </summary>
    public interface IZmstAgencyVirtualDirectoryMappingDirector
    {
        /// <summary>
        ///  Get All ZmstAgencyVirtualDirectoryMapping List.
        /// </summary>
        /// <returns>ZmstAgencyVirtualDirectoryMapping List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstAgencyVirtualDirectoryMapping>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstAgencyVirtualDirectoryMapping Entity.
        /// </summary>
        /// <returns>ZmstAgencyVirtualDirectoryMapping Entity.</returns>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstAgencyVirtualDirectoryMapping> GetByIdAsync(string AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="zmstagencyvirtualdirectorymapping">zmstagencyvirtualdirectorymapping Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstAgencyVirtualDirectoryMapping.
        /// </summary>
        /// <param name="zmstagencyvirtualdirectorymapping">zmstagencyvirtualdirectorymapping Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping, CancellationToken cancellationToken);
		
    }
}

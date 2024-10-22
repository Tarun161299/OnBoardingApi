//-----------------------------------------------------------------------
// <copyright file="IMdMinistryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdMinistry Director behavior.
    /// </summary>
    public interface IZmstServiceTypeDirector
    {
        /// <summary>
        ///  Get ServiceType List.
        /// </summary>
        /// <returns>Onboarding ServiceType.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstServiceType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ServiceType List.
        /// </summary>
        /// <returns>Onboarding ServiceType.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstServiceType>> GetByRequestNo(string RequestNo,CancellationToken cancellationToken);
        /// <summary>
        /// Delete ZmstServiceType.
        /// </summary>
        /// <param name="ServiceTypeId">ServiceTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string ServiceTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstServiceType.
        /// </summary>
        /// <param name="zmstservicetype">zmstservicetype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstServiceType zmstservicetype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstServiceType.
        /// </summary>
        /// <param name="zmstservicetype">zmstservicetype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstServiceType zmstservicetype, CancellationToken cancellationToken);
    }
}
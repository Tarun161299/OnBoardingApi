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
    public interface IZmstProjectsDirector
    {
        /// <summary>
        ///  Get All ZmstProject List.
        /// </summary>
        /// <returns>ZmstProject List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstProjects>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstProject List.
        /// </summary>
        /// <returns>ZmstProject List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstProjects>> GetByIdAsync(int projectid, CancellationToken cancellationToken);
        /// <summary>
        /// SaveZmstProject List.
        /// </summary>
        /// <param name="zmstProject">saveMinistry.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> SaveAsync(ZmstProjects zmstProject, CancellationToken cancellationToken);
        /// <summary>
        /// Update ZmstProject List.
        /// </summary>
        /// <param name="ministryId">ministryId.</param>
        /// <param name="ministryName">ministryName.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(ZmstProjects zmstProject, CancellationToken cancellationToken);
        /// <summary>
        /// Delete ZmstProject List.
        /// </summary>
        /// <param name="ministryId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int projectid, CancellationToken cancellationToken);

    }
}

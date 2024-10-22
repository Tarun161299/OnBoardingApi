
//-----------------------------------------------------------------------
// <copyright file="IMdRoleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdRole behavior.
    /// </summary>
    public interface IMdRoleDirector
    {
        /// <summary>
        ///  Get All MdRole List.
        /// </summary>
        /// <returns>MdRole List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdRole>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdRole Entity.
        /// </summary>
        /// <returns>MdRole Entity.</returns>
        /// <param name="RoleId">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdRole> GetByIdAsync(string RoleId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdRole.
        /// </summary>
        /// <param name="RoleId">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string RoleId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdRole mdrole, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdRole mdrole, CancellationToken cancellationToken);
		
    }
}

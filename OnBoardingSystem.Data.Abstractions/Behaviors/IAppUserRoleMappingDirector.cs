
//-----------------------------------------------------------------------
// <copyright file="IAppUserRoleMappingDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppUserRoleMapping behavior.
    /// </summary>
    public interface IAppUserRoleMappingDirector
    {
        /// <summary>
        ///  Get All AppUserRoleMapping List.
        /// </summary>
        /// <returns>AppUserRoleMapping List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppUserRoleMapping>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppUserRoleMapping Entity.
        /// </summary>
        /// <returns>AppUserRoleMapping Entity.</returns>
        /// <param name="UserID">UserID Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppUserRoleMapping>> GetByIdAsync(string UserID, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppUserRoleMapping.
        /// </summary>
        /// <param name="UserID">UserID Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string UserID, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppUserRoleMapping.
        /// </summary>
        /// <param name="appuserrolemapping">appuserrolemapping Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(List<AppUserRoleMapping> appuserrolemapping,string roleid, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppUserRoleMapping.
        /// </summary>
        /// <param name="appuserrolemapping">appuserrolemapping Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppUserRoleMapping appuserrolemapping, CancellationToken cancellationToken);
		
    }
}

//-----------------------------------------------------------------------
// <copyright file="IMdOrganizationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdOrganization Director behavior.
    /// </summary>
    public interface IMdOrganizationDirector
    {
        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding Organization List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdOrganization>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding Organization List.</returns>
        /// <param name="id">id Token.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdOrganization>> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding Organization List.</returns>
        /// <param name="stateid">stateid Token.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        
        Task<List<MdOrganization>> GetByStateIdAsync(string stateid, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdOrganization.
        /// </summary>
        /// <param name="OrganizationId">OrganizationId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string OrganizationId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdOrganization.
        /// </summary>
        /// <param name="mdorganization">mdorganization Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdOrganization mdorganization, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdOrganization.
        /// </summary>
        /// <param name="mdorganization">mdorganization Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(MdOrganization mdorganization, CancellationToken cancellationToken);
    }
}
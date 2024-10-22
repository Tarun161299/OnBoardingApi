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
    public interface IMdMinistryDirector
    {
        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdMinistry>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get Ministry List.
        /// </summary>
        /// <returns>Onboarding Ministry.</returns>
        /// <param name="ministriesId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdMinistry> GetByIdAsync(int ministriesId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete Ministry.
        /// </summary>
        /// <param name="ministryId">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ministryId, CancellationToken cancellationToken);

        /// <summary>
        /// Save Ministry.
        /// </summary>
        /// <param name="mdMinistry">saveMinistry.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> SaveAsync(MdMinistry mdMinistry, CancellationToken cancellationToken);

        /// <summary>
        /// Update ministry.
        /// </summary>
        /// <param name="ministryId">ministryId.</param>
        /// <param name="ministryName">ministryName.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(int ministryId, string ministryName, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert Minisrty Data By Procedure.
        /// </summary>
        /// <returns>Minisrty.</returns>
        /// <param name="ministryId">entityID Token.</param>
        /// <param name="ministryName">NDC Tok.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveAsync(int ministryId, string ministryName, CancellationToken cancellationToken);

        ///// <summary>
        /////  Get Multipletable List.
        ///// </summary>
        ///// <returns>Onboarding Ministry.</returns>
        ///// <param name="ministriesId">ministriesId.</param>
        ///// /// <param name="requestid">requestid.</param>
        ///// <param name="cancellationToken">cancellation Token.</param>
        ///// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        //Task<MdMinistryRequestInfoList> GetMultipleByIdAsync(int ministriesId, string requestid, CancellationToken cancellationToken);
    }
}
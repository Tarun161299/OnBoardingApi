
//-----------------------------------------------------------------------
// <copyright file="IZmstExperienceTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstExperienceType behavior.
    /// </summary>
    public interface IZmstExperienceTypeDirector
    {
        /// <summary>
        ///  Get All ZmstExperienceType List.
        /// </summary>
        /// <returns>ZmstExperienceType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstExperienceType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstExperienceType Entity.
        /// </summary>
        /// <returns>ZmstExperienceType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstExperienceType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstExperienceType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstExperienceType.
        /// </summary>
        /// <param name="zmstexperiencetype">zmstexperiencetype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstExperienceType zmstexperiencetype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstExperienceType.
        /// </summary>
        /// <param name="zmstexperiencetype">zmstexperiencetype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstExperienceType zmstexperiencetype, CancellationToken cancellationToken);

    }
}

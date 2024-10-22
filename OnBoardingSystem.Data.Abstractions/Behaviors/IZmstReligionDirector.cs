
//-----------------------------------------------------------------------
// <copyright file="IZmstReligionDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstReligion behavior.
    /// </summary>
    public interface IZmstReligionDirector
    {
        /// <summary>
        ///  Get All ZmstReligion List.
        /// </summary>
        /// <returns>ZmstReligion List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstReligion>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstReligion Entity.
        /// </summary>
        /// <returns>ZmstReligion Entity.</returns>
        /// <param name="ReligionId">ReligionId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstReligion> GetByIdAsync(string ReligionId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstReligion.
        /// </summary>
        /// <param name="ReligionId">ReligionId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string ReligionId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstReligion.
        /// </summary>
        /// <param name="zmstreligion">zmstreligion Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstReligion zmstreligion, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstReligion.
        /// </summary>
        /// <param name="zmstreligion">zmstreligion Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstReligion zmstreligion, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstInstituteTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstInstituteType behavior.
    /// </summary>
    public interface IZmstInstituteTypeDirector
    {
        /// <summary>
        ///  Get All ZmstInstituteType List.
        /// </summary>
        /// <returns>ZmstInstituteType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstInstituteType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstInstituteType Entity.
        /// </summary>
        /// <returns>ZmstInstituteType Entity.</returns>
        /// <param name="InstituteTypeId">InstituteTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstInstituteType> GetByIdAsync(string InstituteTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstituteType.
        /// </summary>
        /// <param name="InstituteTypeId">InstituteTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string InstituteTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstInstituteType.
        /// </summary>
        /// <param name="zmstinstitutetype">zmstinstitutetype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstInstituteType zmstinstitutetype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstInstituteType.
        /// </summary>
        /// <param name="zmstinstitutetype">zmstinstitutetype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstInstituteType zmstinstitutetype, CancellationToken cancellationToken);
		
    }
}

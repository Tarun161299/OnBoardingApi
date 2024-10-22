
//-----------------------------------------------------------------------
// <copyright file="IZmstMinimumQualificationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstMinimumQualification behavior.
    /// </summary>
    public interface IZmstMinimumQualificationDirector
    {
        /// <summary>
        ///  Get All ZmstMinimumQualification List.
        /// </summary>
        /// <returns>ZmstMinimumQualification List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstMinimumQualification>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstMinimumQualification Entity.
        /// </summary>
        /// <returns>ZmstMinimumQualification Entity.</returns>
        /// <param name="MinimumQualId">MinimumQualId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstMinimumQualification> GetByIdAsync(string MinimumQualId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstMinimumQualification.
        /// </summary>
        /// <param name="MinimumQualId">MinimumQualId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string MinimumQualId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstMinimumQualification.
        /// </summary>
        /// <param name="zmstminimumqualification">zmstminimumqualification Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstMinimumQualification zmstminimumqualification, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstMinimumQualification.
        /// </summary>
        /// <param name="zmstminimumqualification">zmstminimumqualification Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstMinimumQualification zmstminimumqualification, CancellationToken cancellationToken);
		
    }
}

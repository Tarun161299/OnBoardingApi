
//-----------------------------------------------------------------------
// <copyright file="IZmstQualificationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualification behavior.
    /// </summary>
    public interface IZmstQualificationDirector
    {
        /// <summary>
        ///  Get All ZmstQualification List.
        /// </summary>
        /// <returns>ZmstQualification List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualification>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualification Entity.
        /// </summary>
        /// <returns>ZmstQualification Entity.</returns>
        /// <param name="QualificationId">QualificationId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualification> GetByIdAsync(string QualificationId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualification.
        /// </summary>
        /// <param name="QualificationId">QualificationId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QualificationId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualification.
        /// </summary>
        /// <param name="zmstqualification">zmstqualification Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualification zmstqualification, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualification.
        /// </summary>
        /// <param name="zmstqualification">zmstqualification Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualification zmstqualification, CancellationToken cancellationToken);
		
    }
}

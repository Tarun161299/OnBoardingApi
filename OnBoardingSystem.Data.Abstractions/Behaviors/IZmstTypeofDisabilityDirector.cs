
//-----------------------------------------------------------------------
// <copyright file="IZmstTypeofDisabilityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstTypeofDisability behavior.
    /// </summary>
    public interface IZmstTypeofDisabilityDirector
    {
        /// <summary>
        ///  Get All ZmstTypeofDisability List.
        /// </summary>
        /// <returns>ZmstTypeofDisability List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstTypeofDisability>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstTypeofDisability Entity.
        /// </summary>
        /// <returns>ZmstTypeofDisability Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstTypeofDisability> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstTypeofDisability.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstTypeofDisability.
        /// </summary>
        /// <param name="zmsttypeofdisability">zmsttypeofdisability Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstTypeofDisability zmsttypeofdisability, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstTypeofDisability.
        /// </summary>
        /// <param name="zmsttypeofdisability">zmsttypeofdisability Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstTypeofDisability zmsttypeofdisability, CancellationToken cancellationToken);
		
    }
}

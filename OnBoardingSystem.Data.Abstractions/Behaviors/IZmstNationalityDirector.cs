
//-----------------------------------------------------------------------
// <copyright file="IZmstNationalityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstNationality behavior.
    /// </summary>
    public interface IZmstNationalityDirector
    {
        /// <summary>
        ///  Get All ZmstNationality List.
        /// </summary>
        /// <returns>ZmstNationality List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstNationality>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstNationality Entity.
        /// </summary>
        /// <returns>ZmstNationality Entity.</returns>
        /// <param name="NationalityId">NationalityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstNationality> GetByIdAsync(string NationalityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstNationality.
        /// </summary>
        /// <param name="NationalityId">NationalityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string NationalityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstNationality.
        /// </summary>
        /// <param name="zmstnationality">zmstnationality Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstNationality zmstnationality, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstNationality.
        /// </summary>
        /// <param name="zmstnationality">zmstnationality Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstNationality zmstnationality, CancellationToken cancellationToken);
		
    }
}

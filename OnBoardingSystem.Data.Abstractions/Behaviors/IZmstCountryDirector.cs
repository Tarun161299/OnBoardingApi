
//-----------------------------------------------------------------------
// <copyright file="IZmstCountryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstCountry behavior.
    /// </summary>
    public interface IZmstCountryDirector
    {
        /// <summary>
        ///  Get All ZmstCountry List.
        /// </summary>
        /// <returns>ZmstCountry List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstCountry>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstCountry Entity.
        /// </summary>
        /// <returns>ZmstCountry Entity.</returns>
        /// <param name="Code">Code Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstCountry> GetByIdAsync(string Code, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstCountry.
        /// </summary>
        /// <param name="Code">Code Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Code, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstCountry.
        /// </summary>
        /// <param name="zmstcountry">zmstcountry Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstCountry zmstcountry, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstCountry.
        /// </summary>
        /// <param name="zmstcountry">zmstcountry Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(ZmstCountry zmstcountry, CancellationToken cancellationToken);

    }
}
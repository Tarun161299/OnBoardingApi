
//-----------------------------------------------------------------------
// <copyright file="IZmstGenderDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstGender behavior.
    /// </summary>
    public interface IZmstGenderDirector
    {
        /// <summary>
        ///  Get All ZmstGender List.
        /// </summary>
        /// <returns>ZmstGender List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstGender>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstGender Entity.
        /// </summary>
        /// <returns>ZmstGender Entity.</returns>
        /// <param name="GenderId">GenderId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstGender> GetByIdAsync(string GenderId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstGender.
        /// </summary>
        /// <param name="GenderId">GenderId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string GenderId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstGender.
        /// </summary>
        /// <param name="zmstgender">zmstgender Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstGender zmstgender, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstGender.
        /// </summary>
        /// <param name="zmstgender">zmstgender Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstGender zmstgender, CancellationToken cancellationToken);
		
    }
}

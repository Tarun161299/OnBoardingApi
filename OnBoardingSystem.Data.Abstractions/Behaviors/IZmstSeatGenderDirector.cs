
//-----------------------------------------------------------------------
// <copyright file="IZmstSeatGenderDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSeatGender behavior.
    /// </summary>
    public interface IZmstSeatGenderDirector
    {
        /// <summary>
        ///  Get All ZmstSeatGender List.
        /// </summary>
        /// <returns>ZmstSeatGender List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSeatGender>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSeatGender Entity.
        /// </summary>
        /// <returns>ZmstSeatGender Entity.</returns>
        /// <param name="SeatGenderId">SeatGenderId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSeatGender> GetByIdAsync(string SeatGenderId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSeatGender.
        /// </summary>
        /// <param name="SeatGenderId">SeatGenderId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SeatGenderId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSeatGender.
        /// </summary>
        /// <param name="zmstseatgender">zmstseatgender Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSeatGender zmstseatgender, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSeatGender.
        /// </summary>
        /// <param name="zmstseatgender">zmstseatgender Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSeatGender zmstseatgender, CancellationToken cancellationToken);
		
    }
}

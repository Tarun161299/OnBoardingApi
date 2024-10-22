
//-----------------------------------------------------------------------
// <copyright file="IZmstSeatTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSeatType behavior.
    /// </summary>
    public interface IZmstSeatTypeDirector
    {
        /// <summary>
        ///  Get All ZmstSeatType List.
        /// </summary>
        /// <returns>ZmstSeatType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSeatType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSeatType Entity.
        /// </summary>
        /// <returns>ZmstSeatType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSeatType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSeatType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSeatType.
        /// </summary>
        /// <param name="zmstseattype">zmstseattype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSeatType zmstseattype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSeatType.
        /// </summary>
        /// <param name="zmstseattype">zmstseattype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSeatType zmstseattype, CancellationToken cancellationToken);

    }
}

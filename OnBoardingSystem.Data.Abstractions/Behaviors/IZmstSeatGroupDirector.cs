
//-----------------------------------------------------------------------
// <copyright file="IZmstSeatGroupDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSeatGroup behavior.
    /// </summary>
    public interface IZmstSeatGroupDirector
    {
        /// <summary>
        ///  Get All ZmstSeatGroup List.
        /// </summary>
        /// <returns>ZmstSeatGroup List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSeatGroup>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSeatGroup Entity.
        /// </summary>
        /// <returns>ZmstSeatGroup Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSeatGroup> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSeatGroup.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSeatGroup.
        /// </summary>
        /// <param name="zmstseatgroup">zmstseatgroup Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSeatGroup zmstseatgroup, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSeatGroup.
        /// </summary>
        /// <param name="zmstseatgroup">zmstseatgroup Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSeatGroup zmstseatgroup, CancellationToken cancellationToken);

    }
}

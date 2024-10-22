
//-----------------------------------------------------------------------
// <copyright file="IZmstSeatCategoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSeatCategory behavior.
    /// </summary>
    public interface IZmstSeatCategoryDirector
    {
        /// <summary>
        ///  Get All ZmstSeatCategory List.
        /// </summary>
        /// <returns>ZmstSeatCategory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSeatCategory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSeatCategory Entity.
        /// </summary>
        /// <returns>ZmstSeatCategory Entity.</returns>
        /// <param name="SeatCategoryId">SeatCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSeatCategory> GetByIdAsync(string SeatCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSeatCategory.
        /// </summary>
        /// <param name="SeatCategoryId">SeatCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SeatCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSeatCategory.
        /// </summary>
        /// <param name="zmstseatcategory">zmstseatcategory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSeatCategory zmstseatcategory, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSeatCategory.
        /// </summary>
        /// <param name="zmstseatcategory">zmstseatcategory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSeatCategory zmstseatcategory, CancellationToken cancellationToken);
		
    }
}

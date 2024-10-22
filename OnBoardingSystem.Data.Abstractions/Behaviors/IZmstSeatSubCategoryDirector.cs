
//-----------------------------------------------------------------------
// <copyright file="IZmstSeatSubCategoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSeatSubCategory behavior.
    /// </summary>
    public interface IZmstSeatSubCategoryDirector
    {
        /// <summary>
        ///  Get All ZmstSeatSubCategory List.
        /// </summary>
        /// <returns>ZmstSeatSubCategory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSeatSubCategory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSeatSubCategory Entity.
        /// </summary>
        /// <returns>ZmstSeatSubCategory Entity.</returns>
        /// <param name="SeatSubCategoryId">SeatSubCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSeatSubCategory> GetByIdAsync(string SeatSubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSeatSubCategory.
        /// </summary>
        /// <param name="SeatSubCategoryId">SeatSubCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SeatSubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSeatSubCategory.
        /// </summary>
        /// <param name="zmstseatsubcategory">zmstseatsubcategory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSeatSubCategory zmstseatsubcategory, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSeatSubCategory.
        /// </summary>
        /// <param name="zmstseatsubcategory">zmstseatsubcategory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSeatSubCategory zmstseatsubcategory, CancellationToken cancellationToken);

    }
}

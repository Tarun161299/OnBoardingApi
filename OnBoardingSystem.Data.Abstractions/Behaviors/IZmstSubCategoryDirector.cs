
//-----------------------------------------------------------------------
// <copyright file="IZmstSubCategoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSubCategory behavior.
    /// </summary>
    public interface IZmstSubCategoryDirector
    {
        /// <summary>
        ///  Get All ZmstSubCategory List.
        /// </summary>
        /// <returns>ZmstSubCategory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSubCategory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSubCategory Entity.
        /// </summary>
        /// <returns>ZmstSubCategory Entity.</returns>
        /// <param name="SubCategoryId">SubCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSubCategory> GetByIdAsync(string SubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSubCategory.
        /// </summary>
        /// <param name="SubCategoryId">SubCategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSubCategory.
        /// </summary>
        /// <param name="zmstsubcategory">zmstsubcategory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSubCategory zmstsubcategory, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSubCategory.
        /// </summary>
        /// <param name="zmstsubcategory">zmstsubcategory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSubCategory zmstsubcategory, CancellationToken cancellationToken);
		
    }
}

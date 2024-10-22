
//-----------------------------------------------------------------------
// <copyright file="IZmstCategoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstCategory behavior.
    /// </summary>
    public interface IZmstCategoryDirector
    {
        /// <summary>
        ///  Get All ZmstCategory List.
        /// </summary>
        /// <returns>ZmstCategory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstCategory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstCategory Entity.
        /// </summary>
        /// <returns>ZmstCategory Entity.</returns>
        /// <param name="CategoryId">CategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstCategory> GetByIdAsync(string CategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstCategory.
        /// </summary>
        /// <param name="CategoryId">CategoryId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string CategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstCategory.
        /// </summary>
        /// <param name="zmstcategory">zmstcategory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstCategory zmstcategory, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstCategory.
        /// </summary>
        /// <param name="zmstcategory">zmstcategory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstCategory zmstcategory, CancellationToken cancellationToken);
		
    }
}

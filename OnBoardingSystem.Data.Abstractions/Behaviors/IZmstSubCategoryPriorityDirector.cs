
//-----------------------------------------------------------------------
// <copyright file="IZmstSubCategoryPriorityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSubCategoryPriority behavior.
    /// </summary>
    public interface IZmstSubCategoryPriorityDirector
    {
        /// <summary>
        ///  Get All ZmstSubCategoryPriority List.
        /// </summary>
        /// <returns>ZmstSubCategoryPriority List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSubCategoryPriority>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSubCategoryPriority Entity.
        /// </summary>
        /// <returns>ZmstSubCategoryPriority Entity.</returns>
        /// <param name="SubCategoryPriorityId">SubCategoryPriorityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSubCategoryPriority> GetByIdAsync(string SubCategoryPriorityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="SubCategoryPriorityId">SubCategoryPriorityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SubCategoryPriorityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="zmstsubcategorypriority">zmstsubcategorypriority Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSubCategoryPriority zmstsubcategorypriority, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSubCategoryPriority.
        /// </summary>
        /// <param name="zmstsubcategorypriority">zmstsubcategorypriority Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSubCategoryPriority zmstsubcategorypriority, CancellationToken cancellationToken);
		
    }
}

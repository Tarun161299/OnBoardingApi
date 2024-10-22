
//-----------------------------------------------------------------------
// <copyright file="IZmstBranchDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstBranch behavior.
    /// </summary>
    public interface IZmstBranchDirector
    {
        /// <summary>
        ///  Get All ZmstBranch List.
        /// </summary>
        /// <returns>ZmstBranch List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstBranch>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstBranch Entity.
        /// </summary>
        /// <returns>ZmstBranch Entity.</returns>
        /// <param name="BrCd">BrCd Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstBranch> GetByIdAsync(string BrCd, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstBranch.
        /// </summary>
        /// <param name="BrCd">BrCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string BrCd, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstBranch.
        /// </summary>
        /// <param name="zmstbranch">zmstbranch Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstBranch zmstbranch, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstBranch.
        /// </summary>
        /// <param name="zmstbranch">zmstbranch Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstBranch zmstbranch, CancellationToken cancellationToken);
		
    }
}

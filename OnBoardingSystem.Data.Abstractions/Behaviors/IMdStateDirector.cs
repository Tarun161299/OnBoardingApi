
//-----------------------------------------------------------------------
// <copyright file="IMdStateDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using Microsoft.AspNetCore.Http;
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdState behavior.
    /// </summary>
    public interface IMdStateDirector
    {
        /// <summary>
        ///  Get All MdState List.
        /// </summary>
        /// <returns>MdState List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdState>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdState Entity.
        /// </summary>
        /// <returns>MdState Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdState> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdState.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdState.
        /// </summary>
        /// <param name="mdstate">mdstate Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdState mdstate, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdState.
        /// </summary>
        /// <param name="mdstate">mdstate Parameter.</param> 
		/// <param name="Id">Id Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdState mdstate, string Id, CancellationToken cancellationToken);

    }
}

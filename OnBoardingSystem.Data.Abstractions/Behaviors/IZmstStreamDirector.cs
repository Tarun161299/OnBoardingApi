
//-----------------------------------------------------------------------
// <copyright file="IZmstStreamDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstStream behavior.
    /// </summary>
    public interface IZmstStreamDirector
    {
        /// <summary>
        ///  Get All ZmstStream List.
        /// </summary>
        /// <returns>ZmstStream List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstStream>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstStream Entity.
        /// </summary>
        /// <returns>ZmstStream Entity.</returns>
        /// <param name="StreamId">StreamId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstStream> GetByIdAsync(string StreamId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstStream.
        /// </summary>
        /// <param name="StreamId">StreamId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string StreamId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstStream.
        /// </summary>
        /// <param name="zmststream">zmststream Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstStream zmststream, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstStream.
        /// </summary>
        /// <param name="zmststream">zmststream Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstStream zmststream, CancellationToken cancellationToken);

        /// <summary>
        ///  Get All ZmstStream List.
        /// </summary>
        /// <returns>ZmstStream List.</returns>
        /// <param name="instcd">instcd</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstStream>> GetListByInstcdAsync(string instcd,CancellationToken cancellationToken);

    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstTradeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    //using OnBoardingSystem.Data.EF.Models;

    /// <summary>
    /// Director of ZmstTrade behavior.
    /// </summary>
    public interface IZmstTradeDirector
    {
        /// <summary>
        ///  Get All ZmstTrade List.
        /// </summary>
        /// <returns>ZmstTrade List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstTrade>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstTrade Entity.
        /// </summary>
        /// <returns>ZmstTrade Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstTrade> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstTrade.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstTrade.
        /// </summary>
        /// <param name="zmsttrade">zmsttrade Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstTrade zmsttrade, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstTrade.
        /// </summary>
        /// <param name="zmsttrade">zmsttrade Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstTrade zmsttrade, CancellationToken cancellationToken);

    }
}

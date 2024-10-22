
//-----------------------------------------------------------------------
// <copyright file="IZmstSymbolDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSymbol behavior.
    /// </summary>
    public interface IZmstSymbolDirector
    {
        /// <summary>
        ///  Get All ZmstSymbol List.
        /// </summary>
        /// <returns>ZmstSymbol List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSymbol>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSymbol Entity.
        /// </summary>
        /// <returns>ZmstSymbol Entity.</returns>
        /// <param name="SymbolId">SymbolId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSymbol> GetByIdAsync(string SymbolId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSymbol.
        /// </summary>
        /// <param name="SymbolId">SymbolId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SymbolId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSymbol.
        /// </summary>
        /// <param name="zmstsymbol">zmstsymbol Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSymbol zmstsymbol, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSymbol.
        /// </summary>
        /// <param name="zmstsymbol">zmstsymbol Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSymbol zmstsymbol, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IMdMainModuleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdMainModule behavior.
    /// </summary>
    public interface IMdMainModuleDirector
    {
        /// <summary>
        ///  Get All MdMainModule List.
        /// </summary>
        /// <returns>MdMainModule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdMainModule>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdMainModule Entity.
        /// </summary>
        /// <returns>MdMainModule Entity.</returns>
        /// <param name="MainModuleId">MainModuleId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdMainModule>> GetByIdAsync(string MainModuleId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdMainModule.
        /// </summary>
        /// <param name="MainModuleId">MainModuleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string MainModuleId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdMainModule.
        /// </summary>
        /// <param name="mdmainmodule">mdmainmodule Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdMainModule mdmainmodule, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdMainModule.
        /// </summary>
        /// <param name="mdmainmodule">mdmainmodule Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdMainModule mdmainmodule, CancellationToken cancellationToken);

    }
}

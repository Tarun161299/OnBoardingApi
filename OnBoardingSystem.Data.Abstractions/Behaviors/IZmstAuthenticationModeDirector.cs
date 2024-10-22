
//-----------------------------------------------------------------------
// <copyright file="IZmstAuthenticationModeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstAuthenticationMode behavior.
    /// </summary>
    public interface IZmstAuthenticationModeDirector
    {
        /// <summary>
        ///  Get All ZmstAuthenticationMode List.
        /// </summary>
        /// <returns>ZmstAuthenticationMode List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstAuthenticationMode>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstAuthenticationMode Entity.
        /// </summary>
        /// <returns>ZmstAuthenticationMode Entity.</returns>
        /// <param name="AuthCode">AuthCode Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstAuthenticationMode> GetByIdAsync(string AuthCode, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstAuthenticationMode.
        /// </summary>
        /// <param name="AuthCode">AuthCode Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string AuthCode, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstAuthenticationMode.
        /// </summary>
        /// <param name="zmstauthenticationmode">zmstauthenticationmode Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstAuthenticationMode zmstauthenticationmode, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstAuthenticationMode.
        /// </summary>
        /// <param name="zmstauthenticationmode">zmstauthenticationmode Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstAuthenticationMode zmstauthenticationmode, CancellationToken cancellationToken);
		
    }
}

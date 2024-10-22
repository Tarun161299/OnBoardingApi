
//-----------------------------------------------------------------------
// <copyright file="IMdIdTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdIdType behavior.
    /// </summary>
    public interface IMdIdTypeDirector
    {
        /// <summary>
        ///  Get All MdIdType List.
        /// </summary>
        /// <returns>MdIdType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdIdType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdIdType Entity.
        /// </summary>
        /// <returns>MdIdType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdIdType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdIdType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdIdType.
        /// </summary>
        /// <param name="mdidtype">mdidtype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdIdType mdidtype, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdIdType.
        /// </summary>
        /// <param name="mdidtype">mdidtype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdIdType mdidtype, CancellationToken cancellationToken);
		
    }
}

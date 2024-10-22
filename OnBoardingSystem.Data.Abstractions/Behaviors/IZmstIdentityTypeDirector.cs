
//-----------------------------------------------------------------------
// <copyright file="IZmstIdentityTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstIdentityType behavior.
    /// </summary>
    public interface IZmstIdentityTypeDirector
    {
        /// <summary>
        ///  Get All ZmstIdentityType List.
        /// </summary>
        /// <returns>ZmstIdentityType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstIdentityType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstIdentityType Entity.
        /// </summary>
        /// <returns>ZmstIdentityType Entity.</returns>
        /// <param name="IdentityTypeId">IdentityTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstIdentityType> GetByIdAsync(string IdentityTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstIdentityType.
        /// </summary>
        /// <param name="IdentityTypeId">IdentityTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string IdentityTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstIdentityType.
        /// </summary>
        /// <param name="zmstidentitytype">zmstidentitytype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstIdentityType zmstidentitytype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstIdentityType.
        /// </summary>
        /// <param name="zmstidentitytype">zmstidentitytype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstIdentityType zmstidentitytype, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstInstituteStreamDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Director of ZmstInstituteStream behavior.
    /// </summary>
    public interface IZmstInstituteStreamDirector
    {
        /// <summary>
        ///  Get All ZmstInstituteStream List.
        /// </summary>
        /// <returns>ZmstInstituteStream List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstInstituteStream>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstInstituteStream Entity.
        /// </summary>
        /// <returns>ZmstInstituteStream Entity.</returns>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstInstituteStream> GetByIdAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstInstituteStream.
        /// </summary>
        /// <param name="InstCd">InstCd Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstInstituteStream.
        /// </summary>
        /// <param name="zmstinstitutestream">zmstinstitutestream Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(string instCd,List <ZmstInstituteStream> zmstinstitutestream, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstInstituteStream.
        /// </summary>
        /// <param name="zmstinstitutestream">zmstinstitutestream Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstInstituteStream zmstinstitutestream, CancellationToken cancellationToken);
		
    }
}

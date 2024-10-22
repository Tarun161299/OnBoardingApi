
//-----------------------------------------------------------------------
// <copyright file="IZmstDistrictDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstDistrict behavior.
    /// </summary>
    public interface IZmstDistrictDirector
    {
        /// <summary>
        ///  Get All ZmstDistrict List.
        /// </summary>
        /// <returns>ZmstDistrict List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstDistrict>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstDistrict Entity.
        /// </summary>
        /// <returns>ZmstDistrict Entity.</returns>
        /// <param name="StateId">StateId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstDistrict> GetByIdAsync(string StateId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstDistrict.
        /// </summary>
        /// <param name="StateId">StateId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string StateId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstDistrict.
        /// </summary>
        /// <param name="zmstdistrict">zmstdistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstDistrict zmstdistrict, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstDistrict.
        /// </summary>
        /// <param name="zmstdistrict">zmstdistrict Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstDistrict zmstdistrict, CancellationToken cancellationToken);
		
    }
}

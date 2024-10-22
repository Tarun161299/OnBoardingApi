
//-----------------------------------------------------------------------
// <copyright file="IMdDistrictDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdDistrict behavior.
    /// </summary>
    public interface IMdDistrictDirector
    {
        /// <summary>
        ///  Get All MdDistrict List.
        /// </summary>
        /// <returns>MdDistrict List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDistrict>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdDistrict Entity.
        /// </summary>
        /// <returns>MdDistrict Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdDistrict> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdDistrict.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdDistrict.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdDistrict mddistrict, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdDistrict.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(MdDistrict mddistrict, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AllMdDistrict List By StateId.
        /// </summary>
        /// <returns>MdDistrict List By StateId.</returns>
        /// <param name="StateId">StateId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDistrict>> GetListByStateIdAsync(string StateId, CancellationToken cancellationToken);
    }
}

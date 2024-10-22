
//-----------------------------------------------------------------------
// <copyright file="IZmstApplicantTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstApplicantType behavior.
    /// </summary>
    public interface IZmstApplicantTypeDirector
    {
        /// <summary>
        ///  Get All ZmstApplicantType List.
        /// </summary>
        /// <returns>ZmstApplicantType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstApplicantType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstApplicantType Entity.
        /// </summary>
        /// <returns>ZmstApplicantType Entity.</returns>
        /// <param name="ID">ID Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstApplicantType> GetByIdAsync(int ID, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstApplicantType.
        /// </summary>
        /// <param name="ID">ID Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ID, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstApplicantType.
        /// </summary>
        /// <param name="zmstapplicanttype">zmstapplicanttype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstApplicantType zmstapplicanttype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstApplicantType.
        /// </summary>
        /// <param name="zmstapplicanttype">zmstapplicanttype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstApplicantType zmstapplicanttype, CancellationToken cancellationToken);

    }
}

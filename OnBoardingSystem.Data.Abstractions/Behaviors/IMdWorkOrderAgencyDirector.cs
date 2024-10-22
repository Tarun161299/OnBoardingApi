
//-----------------------------------------------------------------------
// <copyright file="IMdWorkOrderAgencyDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdWorkOrderAgency behavior.
    /// </summary>
    public interface IMdWorkOrderAgencyDirector
    {
        /// <summary>
        ///  Get All MdWorkOrderAgency List.
        /// </summary>
        /// <returns>MdWorkOrderAgency List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdWorkOrderAgency>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdWorkOrderAgency Entity.
        /// </summary>
        /// <returns>MdWorkOrderAgency Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdWorkOrderAgency> GetByIdAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdWorkOrderAgency.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdWorkOrderAgency.
        /// </summary>
        /// <param name="mdworkorderagency">mdworkorderagency Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdWorkOrderAgency mdworkorderagency, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdWorkOrderAgency.
        /// </summary>
        /// <param name="mdworkorderagency">mdworkorderagency Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdWorkOrderAgency mdworkorderagency, CancellationToken cancellationToken);
		
    }
}

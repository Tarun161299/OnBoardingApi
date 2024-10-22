
//-----------------------------------------------------------------------
// <copyright file="IMdActivityTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    //using OnBoardingSystem.Data.EF.Models;

    /// <summary>
    /// Director of MdActivityType behavior.
    /// </summary>
    public interface IMdActivityTypeDirector
    {
        /// <summary>
        ///  Get All MdActivityType List.
        /// </summary>
        /// <returns>MdActivityType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdActivityType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdActivityType Entity.
        /// </summary>
        /// <returns>MdActivityType Entity.</returns>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdActivityType> GetByIdAsync(int ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdActivityType.
        /// </summary>
        /// <param name="ActivityId">ActivityId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ActivityId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdActivityType.
        /// </summary>
        /// <param name="mdactivitytype">mdactivitytype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdActivityType mdactivitytype, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdActivityType.
        /// </summary>
        /// <param name="mdactivitytype">mdactivitytype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdActivityType mdactivitytype, CancellationToken cancellationToken);

    }
}

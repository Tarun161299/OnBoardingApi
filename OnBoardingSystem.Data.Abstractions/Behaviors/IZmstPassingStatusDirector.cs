
//-----------------------------------------------------------------------
// <copyright file="IZmstPassingStatusDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstPassingStatus behavior.
    /// </summary>
    public interface IZmstPassingStatusDirector
    {
        /// <summary>
        ///  Get All ZmstPassingStatus List.
        /// </summary>
        /// <returns>ZmstPassingStatus List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstPassingStatus>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstPassingStatus Entity.
        /// </summary>
        /// <returns>ZmstPassingStatus Entity.</returns>
        /// <param name="PassingStatusId">PassingStatusId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstPassingStatus> GetByIdAsync(string PassingStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstPassingStatus.
        /// </summary>
        /// <param name="PassingStatusId">PassingStatusId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string PassingStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstPassingStatus.
        /// </summary>
        /// <param name="zmstpassingstatus">zmstpassingstatus Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstPassingStatus zmstpassingstatus, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstPassingStatus.
        /// </summary>
        /// <param name="zmstpassingstatus">zmstpassingstatus Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstPassingStatus zmstpassingstatus, CancellationToken cancellationToken);
		
    }
}

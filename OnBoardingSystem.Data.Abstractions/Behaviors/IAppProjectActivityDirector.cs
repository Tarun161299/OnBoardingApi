
//-----------------------------------------------------------------------
// <copyright file="IAppProjectActivityDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppProjectActivity behavior.
    /// </summary>
    public interface IAppProjectActivityDirector
    {
        /// <summary>
        ///  Get All AppProjectActivity List.
        /// </summary>
        /// <returns>AppProjectActivity List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectActivity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppProjectActivity Entity.
        /// </summary>
        /// <returns>AppProjectActivity Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppProjectActivity> GetByIdAsync(AppDocumentFilter getByRefIdNActivity, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppProjectActivity Entity.
        /// </summary>
        /// <returns>AppProjectActivity Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectActivity>> GetByParentRefIdAsync(string ParentRefId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppProjectActivity.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppProjectActivity.
        /// </summary>
        /// <param name="appprojectactivity">appprojectactivity Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppProjectActivity appprojectactivity, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppProjectActivity.
        /// </summary>
        /// <param name="appprojectactivity">appprojectactivity Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppProjectActivity appprojectactivity, CancellationToken cancellationToken);

    }
}

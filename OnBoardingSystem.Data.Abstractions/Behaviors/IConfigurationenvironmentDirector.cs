
//-----------------------------------------------------------------------
// <copyright file="IConfigurationEnvironmentDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ConfigurationEnvironment behavior.
    /// </summary>
    public interface IConfigurationEnvironmentDirector
    {
        /// <summary>
        ///  Get All ConfigurationEnvironment List.
        /// </summary>
        /// <returns>ConfigurationEnvironment List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ConfigurationEnvironment>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ConfigurationEnvironment Entity.
        /// </summary>
        /// <returns>ConfigurationEnvironment Entity.</returns>
        /// <param name="ApplicationId">ApplicationId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ConfigurationEnvironment> GetByIdAsync(int ApplicationId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ConfigurationEnvironment.
        /// </summary>
        /// <param name="ApplicationId">ApplicationId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ApplicationId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ConfigurationEnvironment.
        /// </summary>
        /// <param name="configurationenvironment">configurationenvironment Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ConfigurationEnvironment configurationenvironment, CancellationToken cancellationToken);

        /// <summary>
        /// Update ConfigurationEnvironment.
        /// </summary>
        /// <param name="configurationenvironment">configurationenvironment Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ConfigurationEnvironment configurationenvironment, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IConfigurationApisecureKey.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    public interface IConfigurationAPISecureKeyDirector
    {
        /// <summary>
        ///  Get All ConfigurationAPISecureKey List.
        /// </summary>
        /// <returns>ConfigurationAPISecureKey List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ConfigurationApisecureKey>> GetAllAsync(CancellationToken cancellationToken);
    }
}

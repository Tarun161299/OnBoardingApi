//-----------------------------------------------------------------------
// <copyright file="IMdDocumentTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdDocumentType Director behavior.
    /// </summary>
    public interface IMdDocumentTypeDirector
    {
        /// <summary>
        ///  Get MdDocumentType List.
        /// </summary>
        /// <returns>Onboarding MdDocumentType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDocumentType>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        ///  Get MdDocumentType List.
        /// </summary>
        /// <returns>Onboarding MdDocumentType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDocumentType>> GetByRoleAsync(string Role,CancellationToken cancellationToken);
    }
}

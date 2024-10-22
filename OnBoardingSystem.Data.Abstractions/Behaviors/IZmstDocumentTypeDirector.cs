
//-----------------------------------------------------------------------
// <copyright file="IZmstDocumentTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstDocumentType behavior.
    /// </summary>
    public interface IZmstDocumentTypeDirector
    {
        /// <summary>
        ///  Get All ZmstDocumentType List.
        /// </summary>
        /// <returns>ZmstDocumentType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstDocumentType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstDocumentType Entity.
        /// </summary>
        /// <returns>ZmstDocumentType Entity.</returns>
        /// <param name="DocumentTypeId">DocumentTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstDocumentType> GetByIdAsync(string DocumentTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstDocumentType.
        /// </summary>
        /// <param name="DocumentTypeId">DocumentTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string DocumentTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstDocumentType.
        /// </summary>
        /// <param name="zmstdocumenttype">zmstdocumenttype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstDocumentType zmstdocumenttype, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstDocumentType.
        /// </summary>
        /// <param name="zmstdocumenttype">zmstdocumenttype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstDocumentType zmstdocumenttype, CancellationToken cancellationToken);
		
    }
}

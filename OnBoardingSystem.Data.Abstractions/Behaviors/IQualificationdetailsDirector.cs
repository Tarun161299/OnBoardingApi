
//-----------------------------------------------------------------------
// <copyright file="IQualificationDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of QualificationDetails behavior.
    /// </summary>
    public interface IQualificationDetailsDirector
    {
        /// <summary>
        ///  Get All QualificationDetails List.
        /// </summary>
        /// <returns>QualificationDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<QualificationDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get QualificationDetails Entity.
        /// </summary>
        /// <returns>QualificationDetails Entity.</returns>
        /// <param name="QualificationDetailsId">QualificationDetailsId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<QualificationDetails>> GetByIdAsync(string ExamCode, CancellationToken cancellationToken);

        /// <summary>
        /// Delete QualificationDetails.
        /// </summary>
        /// <param name="QualificationDetailsId">QualificationDetailsId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int QualificationDetailsId, CancellationToken cancellationToken);

        /// <summary>
        /// Save QualificationDetails.
        /// </summary>
        /// <param name="QualificationDetails">QualificationDetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(QualificationDetails qualificationdetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update QualificationDetails.
        /// </summary>
        /// <param name="QualificationDetails">QualificationDetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(QualificationDetails qualificationdetails, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstQualifyingCourseDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstQualifyingCourse behavior.
    /// </summary>
    public interface IZmstQualifyingCourseDirector
    {
        /// <summary>
        ///  Get All ZmstQualifyingCourse List.
        /// </summary>
        /// <returns>ZmstQualifyingCourse List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstQualifyingCourse>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstQualifyingCourse Entity.
        /// </summary>
        /// <returns>ZmstQualifyingCourse Entity.</returns>
        /// <param name="QualificationCourseId">QualificationCourseId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstQualifyingCourse> GetByIdAsync(string QualificationCourseId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstQualifyingCourse.
        /// </summary>
        /// <param name="QualificationCourseId">QualificationCourseId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string QualificationCourseId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstQualifyingCourse.
        /// </summary>
        /// <param name="zmstqualifyingcourse">zmstqualifyingcourse Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstQualifyingCourse zmstqualifyingcourse, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstQualifyingCourse.
        /// </summary>
        /// <param name="zmstqualifyingcourse">zmstqualifyingcourse Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstQualifyingCourse zmstqualifyingcourse, CancellationToken cancellationToken);
		
    }
}

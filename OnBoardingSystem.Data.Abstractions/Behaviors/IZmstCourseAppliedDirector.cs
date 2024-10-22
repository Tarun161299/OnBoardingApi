
//-----------------------------------------------------------------------
// <copyright file="IZmstCourseAppliedDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstCourseApplied behavior.
    /// </summary>
    public interface IZmstCourseAppliedDirector
    {
        /// <summary>
        ///  Get All ZmstCourseApplied List.
        /// </summary>
        /// <returns>ZmstCourseApplied List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstCourseApplied>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstCourseApplied Entity.
        /// </summary>
        /// <returns>ZmstCourseApplied Entity.</returns>
        /// <param name="CourseId">CourseId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstCourseApplied> GetByIdAsync(string CourseId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstCourseApplied.
        /// </summary>
        /// <param name="CourseId">CourseId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string CourseId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstCourseApplied.
        /// </summary>
        /// <param name="zmstcourseapplied">zmstcourseapplied Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstCourseApplied zmstcourseapplied, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstCourseApplied.
        /// </summary>
        /// <param name="zmstcourseapplied">zmstcourseapplied Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstCourseApplied zmstcourseapplied, CancellationToken cancellationToken);
		
    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstCourseAppliedLevelDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstCourseAppliedLevel behavior.
    /// </summary>
    public interface IZmstCourseAppliedLevelDirector
    {
        /// <summary>
        ///  Get All ZmstCourseAppliedLevel List.
        /// </summary>
        /// <returns>ZmstCourseAppliedLevel List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstCourseAppliedLevel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstCourseAppliedLevel Entity.
        /// </summary>
        /// <returns>ZmstCourseAppliedLevel Entity.</returns>
        /// <param name="CourseLevelId">CourseLevelId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstCourseAppliedLevel> GetByIdAsync(string CourseLevelId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="CourseLevelId">CourseLevelId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string CourseLevelId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="zmstcourseappliedlevel">zmstcourseappliedlevel Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstCourseAppliedLevel zmstcourseappliedlevel, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstCourseAppliedLevel.
        /// </summary>
        /// <param name="zmstcourseappliedlevel">zmstcourseappliedlevel Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstCourseAppliedLevel zmstcourseappliedlevel, CancellationToken cancellationToken);
		
    }
}

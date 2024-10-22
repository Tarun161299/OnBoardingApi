//-----------------------------------------------------------------------
// <copyright file="IProjectCreationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppProjectDetails Director behavior.
    /// </summary>
    public interface IAppProjectDetailsDirector
    {
        /// <summary>
        ///  Get AppProjectDetails List.
        /// </summary>
        /// <returns>Onboarding Project Details.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectDetails>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        ///  Insert AppProjectDetails Data.
        /// </summary>
        /// <returns>AppProjectDetails.</returns>
        /// <param name="appProjectDetails">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> Save(AppProjectDetails appProjectDetails, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert AppProjectDetails Data.
        /// </summary>
        /// <returns>AppProjectDetails.</returns>
        /// <param name="appProjectDetails">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> Update(AppProjectDetails appProjectDetails, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding AppProjectDetails List.</returns>
        /// <param name="id">requestId.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectDetails>> GetById(string id, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding AppProjectDetails List.</returns>
        /// <param name="id">requestId.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        /// //CounsellingDocs>> GetByRequestNoAsync
        Task<List<CounsellingDocs>> GetByRequestNoAsync(string id, CancellationToken cancellationToken);
        /// <summary>
        ///  Get Organization List.
        /// </summary>
        /// <returns>Onboarding AppProjectDetails List.</returns>
        /// <param name="id">requestId.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppProjectDetails> GetByProjectId(int id, CancellationToken cancellationToken);
        //GetByRequestNoAsync
        /// <summary>
        ///  Insert AppprojectDetails Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="appProjectDetails">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveProjectDetails(AppProjectDetails appProjectDetails, CancellationToken cancellationToken);
        
        /// <summary>
        ///  Insert AppprojectDetails Data.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="appProjectDetails">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SavePIDetails(PIDetails piDetails, CancellationToken cancellationToken);
    }
}

//-----------------------------------------------------------------------
// <copyright file="IAppDocumentUploadedDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    /// <summary>
    /// Director of IAppDocumentUploadedDetail Director behavior.
    /// </summary>
    public interface IAppDocumentUploadedDetailDirector
    {
        /// <summary>
        /// Save AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="appDocumentUploadedDetail">saveAppDocumentUploadDetail.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        //Task<int> SaveAsync(AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert UserInfo Data.
        /// </summary>
        /// <returns>UserInfo.</returns>
        /// <param name="appDocumentUploadedDetail">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> Save(List<AppDocumentUploadedDetail> appDocumentUploadedDetail, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppDocumentUploadAndDocumentType>> GetByRequestId(AppDocumentFilter appDocFilter, CancellationToken cancellationToken);

		/// <summary>
		///  Get AppOnboardingRequest List.
		/// </summary>
		/// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
		/// <param name="projectId">AppDocumentUploadedDetail .</param>
		/// <param name="cancellationToken">cancellation Token.</param>
		/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
		Task<List<AppDocumentUploadAndDocumentType>> GetByProjectDetailId(int id, CancellationToken cancellationToken);

		/// <summary>
		///  Get AppOnboardingRequest List.
		/// </summary>
		/// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
		/// <param name="documentByrequestId">AppDocumentUploadedDetail .</param>
		/// <param name="cancellationToken">cancellation Token.</param>
		/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
		Task<AppDocumentUploadedDetail> GetDocumentByRequestId(AppDocActivity AppDoc, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppDocumentUploadAndDocumentType>> UserMenuByRequestId(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppDocumentUploadAndDocumentType>> ModuleRefId(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> Saveprofilephoto(AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="requestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppDocumentUploadedDetail> GetById(int id, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppOnboardingRequest List.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="documentByrequestId">AppDocumentUploadedDetail .</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppDocumentUploadedDetail> GetDocumentByDocType(AppDocumentFilter employeeDocDetails, CancellationToken cancellationToken);
        /// <summary>
        ///  Insert UserInfo Data.
        /// </summary>
        /// <returns>UserInfo.</returns>
        /// <param name="appDocumentUploadedDetail">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> InsertAndUpdateActivityStatus(AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken);
    }
}

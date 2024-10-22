//-----------------------------------------------------------------------
// <copyright file="IApplicationSummaryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Director of ApplicationSummary Director behavior.
    /// </summary>
    public interface IApplicationSummaryDirector
    {
        /// <summary>
        ///  Get ZmstApplicationSummary List.
        /// </summary>
        /// <returns>ZmstApplicationSummary.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ApplicationSummary>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZMSTApplicationSummary.
        /// </summary>
        /// <param name="zmstApplicationSummary">Onboarding Ministry Token.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(ApplicationSummary zmstApplicationSummary, CancellationToken cancellationToken);
        /// <summary>
        /// Save zmstApplicationSummary.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ApplicationSummary zmstApplicationSummary, CancellationToken cancellationToken);

        /// <summary>
        /// Update zmstApplicationSummary.
        /// </summary>
        /// <param name="zmstApplicationSummary">mdstate Parameter.</param> 
        /// <param name="Id">Id Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(ApplicationSummary zmstApplicationSummary, CancellationToken cancellationToken);
        
        /// <summary>
        ///  Get ZmstApplicationSummary Registration List.
        /// </summary>
        /// <returns>ZmstApplicationSummary.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ApplicationSummary>> GetAllRegistrationAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Save zmstApplicationSummary.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateExamManagementServiceAsync(ApplicationSummary zmstApplicationSummary, CancellationToken cancellationToken);

        //GetApplicationSummary
        /// <summary>
        /// Save zmstApplicationSummary.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<GetApplicationSummary>> GetAppSummaryData( CancellationToken cancellationToken);

        //GetApplicationSummary
        /// <summary>
        /// Save zmstApplicationSummary.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<CounsellingAndAdmissionSystemSummary>> GetAppSummaryByCouns(CancellationToken cancellationToken);

        //GetApplicationSummary
        /// <summary>
        /// Save zmstApplicationSummary.
        /// </summary>
        /// <param name="mddistrict">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<CounsellingAndAdmissionSystemSummary>> GetAppSummaryByRegst(CancellationToken cancellationToken);
    }
}

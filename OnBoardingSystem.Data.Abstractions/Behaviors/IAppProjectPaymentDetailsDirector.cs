
//-----------------------------------------------------------------------
// <copyright file="IAppProjectPaymentDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppProjectPaymentDetails behavior.
    /// </summary>
    public interface IAppProjectPaymentDetailsDirector
    {
        /// <summary>
        ///  Get All AppProjectPaymentDetails List.
        /// </summary>
        /// <returns>AppProjectPaymentDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppProjectPaymentDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppProjectPaymentDetails Entity.
        /// </summary>
        /// <returns>AppProjectPaymentDetails Entity.</returns>
        /// <param name="PaymentId">PaymentId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppProjectPaymentDetails> GetByIdAsync(int PaymentId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppProjectPaymentDetails Entity.
        /// </summary>
        /// <returns>AppProjectPaymentDetails Entity.</returns>
        /// <param name="PaymentId">PaymentId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppProjectPaymentDetails> GetByPaymentParentRefIdAsync(string PaymentParentRefId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete AppProjectPaymentDetails.
        /// </summary>
        /// <param name="PaymentId">PaymentId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int PaymentId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken);
        
        /// <summary>
        /// Update Status AppProjectPaymentDetails.
        /// </summary>
        /// <param name="appprojectpaymentdetails">appprojectpaymentdetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateStatusAsync(AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken);

    }
}

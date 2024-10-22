
//-----------------------------------------------------------------------
// <copyright file="IMdSmsEmailTemplateDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdSmsEmailTemplate behavior.
    /// </summary>
    public interface IMdSmsEmailTemplateDirector
    {
        /// <summary>
        ///  Get All MdSmsEmailTemplate List.
        /// </summary>
        /// <returns>MdSmsEmailTemplate List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdSmsEmailTemplate>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdSmsEmailTemplate Entity.
        /// </summary>
        /// <returns>MdSmsEmailTemplate Entity.</returns>
        /// <param name="TemplateId">TemplateId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdSmsEmailTemplate> GetByIdAsync(string TemplateId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdSmsEmailTemplate.
        /// </summary>
        /// <param name="TemplateId">TemplateId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string TemplateId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdSmsEmailTemplate.
        /// </summary>
        /// <param name="mdsmsemailtemplate">mdsmsemailtemplate Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdSmsEmailTemplate mdsmsemailtemplate, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdSmsEmailTemplate.
        /// </summary>
        /// <param name="mdsmsemailtemplate">mdsmsemailtemplate Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(MdSmsEmailTemplate mdsmsemailtemplate, CancellationToken cancellationToken);

    }
}


//-----------------------------------------------------------------------
// <copyright file="IZmstSecurityQuestionDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstSecurityQuestion behavior.
    /// </summary>
    public interface IZmstSecurityQuestionDirector
    {
        /// <summary>
        ///  Get All ZmstSecurityQuestion List.
        /// </summary>
        /// <returns>ZmstSecurityQuestion List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstSecurityQuestion>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstSecurityQuestion Entity.
        /// </summary>
        /// <returns>ZmstSecurityQuestion Entity.</returns>
        /// <param name="SecurityQuesId">SecurityQuesId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstSecurityQuestion> GetByIdAsync(string SecurityQuesId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ZmstSecurityQuestion.
        /// </summary>
        /// <param name="SecurityQuesId">SecurityQuesId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string SecurityQuesId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstSecurityQuestion.
        /// </summary>
        /// <param name="zmstsecurityquestion">zmstsecurityquestion Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstSecurityQuestion zmstsecurityquestion, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstSecurityQuestion.
        /// </summary>
        /// <param name="zmstsecurityquestion">zmstsecurityquestion Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstSecurityQuestion zmstsecurityquestion, CancellationToken cancellationToken);
		
    }
}

//-----------------------------------------------------------------------
// <copyright file="ICaptchaDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
using OnBoardingSystem.Data.Abstractions.Models;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    public interface ICaptchaDirector
    {
        /// <summary>
		/// Save ESSOData.
		/// </summary>
		/// <param name="ICaptchaDirector">ICaptchaDirector Parameter.</param>
		/// <param name="cancellationToken">cancellationToken.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<AppCaptcha> InsertAsync(string key, string base64String, string hashvalue, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ESSOData.
        /// </summary>
        /// <param name="id">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        //Task<int> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<int> checkCaptcha(Check_captcha captcha, CancellationToken cancellationToken);
    }
}

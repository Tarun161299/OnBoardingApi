//-----------------------------------------------------------------------
// <copyright file="IAppOnboardingAdminloginDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using Microsoft.AspNetCore.Http;
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAppOnboardingAdminloginDirector Director behavior.
    /// </summary>
    public interface IAppOnboardingAdminloginDirector
    {
        /// <summary>
        ///  Insert UserInfo Data.
        /// </summary>
        /// <returns>UserInfo.</returns>
        /// <param name="adminCredentials">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Session> CheckAdminLoginAsync(UserInfo adminCredentials, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert SignUp Data.
        /// </summary>
        /// <returns>SignUp.</returns>
        /// <param name="signUpData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveSignUpDetailsAsync(SignUp signUpData, CancellationToken cancellationToken);

        /// <summary>
        ///  Check userID Availibilty.
        /// </summary>
        /// <returns>userID.</returns>
        /// <param name="userID">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> CheckUserIdAvailibity(string userID, CancellationToken cancellationToken);

        /// <summary>
        ///  Get GetRoleById List.
        /// </summary>
        /// <returns>GetRoleById.</returns>
        /// <param name="userID">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<UserRole>> GetRoleByIdAsync(string userID, CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> SendForgotPasswordMail(string requestId, CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> ForgotPasswordMail(ResendPassword resendPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdDistrict.
        /// </summary>
        /// <param name="confirmPassword">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(ResendPassword confirmPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdDistrict.
        /// </summary>
        /// <param name="confirmPassword">mddistrict Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateByUseridAsync(ResendPassword confirmPassword, CancellationToken cancellationToken);
        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetSalt(CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetCaptcha(CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> Logout(logout logout,CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> Token(string token, string username, CancellationToken cancellationToken);
        
        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetIPAddress(CancellationToken cancellationToken);

        Task<int> SaveUserDetailsAsync(SignUp signUpData, CancellationToken cancellationToken);

        Task<List<SignUp>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<SignUp>> BoardUserGetAllAsync(CancellationToken cancellationToken);
        Task<List<SignUp>> GetDocumentByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<SignUp>> GetBoardUserdetailByIdAsync(string id, CancellationToken cancellationToken);
        Task<int> UpdateUserDetailsAsync(SignUp signUp, CancellationToken cancellationToken);

        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);
        Task<List<AppUserRoleMapping>> CheckExistUserIdAsync(string userID, CancellationToken cancellationToken);

    }
}

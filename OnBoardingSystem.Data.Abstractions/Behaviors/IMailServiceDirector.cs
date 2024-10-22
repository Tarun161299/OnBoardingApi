//-----------------------------------------------------------------------
// <copyright file="IMailServiceDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// Director of IAppSettingDirector Director behavior.
    /// </summary>
    public interface IMailServiceDirector
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task<bool> sendRequestStatusEmailAsync(string Email, string requestNo, string statusReq, string statusDetail, string Cordmail, string cordName, CancellationToken cancellationToken);

    }
}

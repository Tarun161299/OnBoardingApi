using OnBoardingSystem.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    public interface IAppDocumentUploadedDetailHistoryDirector
    {

        /// <summary>
        ///  Insert UserInfo Data.
        /// </summary>
        /// <returns>UserInfo.</returns>
        /// <param name="appDocumentUploadedDetail">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> Save(AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken);
    }
}

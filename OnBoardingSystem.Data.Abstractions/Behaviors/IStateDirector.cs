//-----------------------------------------------------------------------
// <copyright file="IStateDirector.cs" company="NIC">
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
    public interface IStateDirector
    { /// <summary>
      ///  Get State List.
      /// </summary>
      /// <returns>State.</returns>
      /// <param name="cancellationToken">cancellation Token.</param>
      /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<State>> GetAllAsync(CancellationToken cancellationToken);

    }
}

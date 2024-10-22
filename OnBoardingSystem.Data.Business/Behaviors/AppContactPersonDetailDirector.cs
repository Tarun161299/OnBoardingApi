//-----------------------------------------------------------------------
// <copyright file="AppContactPersonDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class AppContactPersonDetailDirector : IAppContactPersonDetailDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppContactPersonDetailDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppContactPersonDetailDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppContactPersonDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await this.unitOfWork.AppContactPersonDetailRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AbsModels.AppContactPersonDetails>>(list);

            return result;
        }

        /// <inheritdoc />
        public virtual async Task<List<AppContactPersonDetails>> GetByRequestIdAsync(string requestId, CancellationToken cancellationToken)
        {
            var contactDetail = await this.unitOfWork.AppContactPersonDetailRepository.FindAllByAsync(x => x.RequestNo == requestId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AbsModels.AppContactPersonDetails>>(contactDetail);
            return result;
        }

        /// <inheritdoc />
        public Task<AbsModels.AppContactPersonDetails> GetListByIdAsync(string requestId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<int> SaveAsync(AbsModels.AppContactPersonDetails appOnboardingRequestData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
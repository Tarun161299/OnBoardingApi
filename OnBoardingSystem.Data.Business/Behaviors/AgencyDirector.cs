//-----------------------------------------------------------------------
// <copyright file="AgencyDirector.cs" company="NIC">
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
    public class AgencyDirector: IAgencyDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AgencyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdAgency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await this.unitOfWork.MDAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AbsModels.MdAgency>>(list);

            return result;
        }
    }
}

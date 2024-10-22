//-----------------------------------------------------------------------
// <copyright file="MdAgencyTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;

    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class MdServiceTypeDirector : IMdServiceTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdServiceTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdServiceType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdagencyTypelist = await this.unitOfWork.MdServiceTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AbsModels.MdServiceType>>(mdagencyTypelist);

            return result;
        }
    }
}
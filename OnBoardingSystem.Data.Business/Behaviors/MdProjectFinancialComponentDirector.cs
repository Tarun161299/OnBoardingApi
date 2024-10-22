//-----------------------------------------------------------------------
// <copyright file="MdMinistryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class MdProjectFinancialComponentDirector:IMdFinancialComponentDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMinistryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdProjectFinancialComponentDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.MdProjectFinancialComponents>> GetAllAsync(CancellationToken cancellationToken)
        {
            var ministrylist = await this.unitOfWork.MdProjectFinancialComponentRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.MdProjectFinancialComponents>>(ministrylist);
        }
    }
}

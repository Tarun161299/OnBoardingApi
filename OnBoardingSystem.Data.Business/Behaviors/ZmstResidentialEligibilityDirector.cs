
//-----------------------------------------------------------------------
// <copyright file="ZmstResidentialEligibilityDirector.cs" company="NIC">
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
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class ZmstResidentialEligibilityDirector : IZmstResidentialEligibilityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstResidentialEligibilityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstResidentialEligibilityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstResidentialEligibility>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstresidentialeligibilitylist = await this.unitOfWork.ZmstResidentialEligibilityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstResidentialEligibility>>(zmstresidentialeligibilitylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstResidentialEligibility> GetByIdAsync(string ResidentialEligibilityId, CancellationToken cancellationToken)
        {
            var zmstresidentialeligibilitylist = await this.unitOfWork.ZmstResidentialEligibilityRepository.FindByAsync(x => x.ResidentialEligibilityId == ResidentialEligibilityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstResidentialEligibility>(zmstresidentialeligibilitylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstResidentialEligibility zmstresidentialeligibility, CancellationToken cancellationToken)
        {
            if (zmstresidentialeligibility == null)
            {
                throw new ArgumentNullException(nameof(zmstresidentialeligibility));
            }

            var chkefzmstresidentialeligibility = await this.unitOfWork.ZmstResidentialEligibilityRepository.FindByAsync(r => r.ResidentialEligibilityId == zmstresidentialeligibility.ResidentialEligibilityId, default);
            if (chkefzmstresidentialeligibility != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstresidentialeligibility} already exists");
            }

            var efzmstresidentialeligibility = this.mapper.Map<Data.EF.Models.ZmstResidentialEligibility>(zmstresidentialeligibility);

            await this.unitOfWork.ZmstResidentialEligibilityRepository.InsertAsync(efzmstresidentialeligibility, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstResidentialEligibility zmstresidentialeligibility, CancellationToken cancellationToken)
        
		{
            if (zmstresidentialeligibility.ResidentialEligibilityId == "0")
            {
                throw new ArgumentException(nameof(zmstresidentialeligibility.ResidentialEligibilityId));
            }
			
			Data.EF.Models.ZmstResidentialEligibility entityUpd = await unitOfWork.ZmstResidentialEligibilityRepository.FindByAsync(e => e.ResidentialEligibilityId == zmstresidentialeligibility.ResidentialEligibilityId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.ResidentialEligibilityId= zmstresidentialeligibility.ResidentialEligibilityId;
					entityUpd.ResidentialEligibilityName= zmstresidentialeligibility.ResidentialEligibilityName;
					
				await unitOfWork.ZmstResidentialEligibilityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string ResidentialEligibilityId, CancellationToken cancellationToken)
        {
            if (ResidentialEligibilityId == "0")
            {
                throw new ArgumentNullException(nameof(ResidentialEligibilityId));
            }

            var entity = await this.unitOfWork.ZmstResidentialEligibilityRepository.FindByAsync(x => x.ResidentialEligibilityId == ResidentialEligibilityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ResidentialEligibilityId {ResidentialEligibilityId} was not found.");
            }

            await this.unitOfWork.ZmstResidentialEligibilityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstResidentialEligibilityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
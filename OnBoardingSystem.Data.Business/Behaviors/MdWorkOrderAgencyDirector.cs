
//-----------------------------------------------------------------------
// <copyright file="MdWorkOrderAgencyDirector.cs" company="NIC">
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
    public class MdWorkOrderAgencyDirector : IMdWorkOrderAgencyDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdWorkOrderAgencyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdWorkOrderAgencyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdWorkOrderAgency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdworkorderagencylist = await this.unitOfWork.MdWorkOrderAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdWorkOrderAgency>>(mdworkorderagencylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdWorkOrderAgency> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var mdworkorderagencylist = await this.unitOfWork.MdWorkOrderAgencyRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdWorkOrderAgency>(mdworkorderagencylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdWorkOrderAgency mdworkorderagency, CancellationToken cancellationToken)
        {
            if (mdworkorderagency == null)
            {
                throw new ArgumentNullException(nameof(mdworkorderagency));
            }

            var chkefmdworkorderagency = await this.unitOfWork.MdWorkOrderAgencyRepository.FindByAsync(r => r.Id == mdworkorderagency.Id, default);
            if (chkefmdworkorderagency != null)
            {
                throw new EntityFoundException($"This Records {chkefmdworkorderagency} already exists");
            }

            var efmdworkorderagency = this.mapper.Map<Data.EF.Models.MdWorkOrderAgency>(mdworkorderagency);

            await this.unitOfWork.MdWorkOrderAgencyRepository.InsertAsync(efmdworkorderagency, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdWorkOrderAgency mdworkorderagency, CancellationToken cancellationToken)
        
		{
            if (mdworkorderagency.Id == 0)
            {
                throw new ArgumentException(nameof(mdworkorderagency.Id));
            }
			
			Data.EF.Models.MdWorkOrderAgency entityUpd = await unitOfWork.MdWorkOrderAgencyRepository.FindByAsync(e => e.Id == mdworkorderagency.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= mdworkorderagency.Id;
					entityUpd.AgencyName= mdworkorderagency.AgencyName;
					
				await unitOfWork.MdWorkOrderAgencyRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            if (Id == 0)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.MdWorkOrderAgencyRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdWorkOrderAgencyRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdWorkOrderAgencyRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
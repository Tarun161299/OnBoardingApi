
//-----------------------------------------------------------------------
// <copyright file="ZmstTypeofDisabilityDirector.cs" company="NIC">
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
    public class ZmstTypeofDisabilityDirector : IZmstTypeofDisabilityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstTypeofDisabilityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstTypeofDisabilityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstTypeofDisability>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmsttypeofdisabilitylist = await this.unitOfWork.ZmstTypeofDisabilityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstTypeofDisability>>(zmsttypeofdisabilitylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstTypeofDisability> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmsttypeofdisabilitylist = await this.unitOfWork.ZmstTypeofDisabilityRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstTypeofDisability>(zmsttypeofdisabilitylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstTypeofDisability zmsttypeofdisability, CancellationToken cancellationToken)
        {
            if (zmsttypeofdisability == null)
            {
                throw new ArgumentNullException(nameof(zmsttypeofdisability));
            }

            var chkefzmsttypeofdisability = await this.unitOfWork.ZmstTypeofDisabilityRepository.FindByAsync(r => r.Id == zmsttypeofdisability.Id, default);
            if (chkefzmsttypeofdisability != null)
            {
                throw new EntityFoundException($"This Records {chkefzmsttypeofdisability} already exists");
            }

            var efzmsttypeofdisability = this.mapper.Map<Data.EF.Models.ZmstTypeofDisability>(zmsttypeofdisability);

            await this.unitOfWork.ZmstTypeofDisabilityRepository.InsertAsync(efzmsttypeofdisability, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstTypeofDisability zmsttypeofdisability, CancellationToken cancellationToken)
        
		{
            if (zmsttypeofdisability.Id == "0")
            {
                throw new ArgumentException(nameof(zmsttypeofdisability.Id));
            }
			
			Data.EF.Models.ZmstTypeofDisability entityUpd = await unitOfWork.ZmstTypeofDisabilityRepository.FindByAsync(e => e.Id == zmsttypeofdisability.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmsttypeofdisability.Id;
					entityUpd.Description= zmsttypeofdisability.Description;
					
				await unitOfWork.ZmstTypeofDisabilityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstTypeofDisabilityRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstTypeofDisabilityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstTypeofDisabilityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
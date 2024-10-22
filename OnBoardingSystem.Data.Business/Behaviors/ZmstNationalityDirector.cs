
//-----------------------------------------------------------------------
// <copyright file="ZmstNationalityDirector.cs" company="NIC">
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
    public class ZmstNationalityDirector : IZmstNationalityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstNationalityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstNationalityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstNationality>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstnationalitylist = await this.unitOfWork.ZmstNationalityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstNationality>>(zmstnationalitylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstNationality> GetByIdAsync(string NationalityId, CancellationToken cancellationToken)
        {
            var zmstnationalitylist = await this.unitOfWork.ZmstNationalityRepository.FindByAsync(x => x.NationalityId == NationalityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstNationality>(zmstnationalitylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstNationality zmstnationality, CancellationToken cancellationToken)
        {
            if (zmstnationality == null)
            {
                throw new ArgumentNullException(nameof(zmstnationality));
            }

            var chkefzmstnationality = await this.unitOfWork.ZmstNationalityRepository.FindByAsync(r => r.NationalityId == zmstnationality.NationalityId, default);
            if (chkefzmstnationality != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstnationality} already exists");
            }

            var efzmstnationality = this.mapper.Map<Data.EF.Models.ZmstNationality>(zmstnationality);

            await this.unitOfWork.ZmstNationalityRepository.InsertAsync(efzmstnationality, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstNationality zmstnationality, CancellationToken cancellationToken)
        
		{
            if (zmstnationality.NationalityId == "0")
            {
                throw new ArgumentException(nameof(zmstnationality.NationalityId));
            }
			
			Data.EF.Models.ZmstNationality entityUpd = await unitOfWork.ZmstNationalityRepository.FindByAsync(e => e.NationalityId == zmstnationality.NationalityId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.NationalityId= zmstnationality.NationalityId;
					entityUpd.Description= zmstnationality.Description;
					
				await unitOfWork.ZmstNationalityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string NationalityId, CancellationToken cancellationToken)
        {
            if (NationalityId == "0")
            {
                throw new ArgumentNullException(nameof(NationalityId));
            }

            var entity = await this.unitOfWork.ZmstNationalityRepository.FindByAsync(x => x.NationalityId == NationalityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an NationalityId {NationalityId} was not found.");
            }

            await this.unitOfWork.ZmstNationalityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstNationalityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
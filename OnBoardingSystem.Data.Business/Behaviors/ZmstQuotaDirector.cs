
//-----------------------------------------------------------------------
// <copyright file="ZmstQuotaDirector.cs" company="NIC">
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
    public class ZmstQuotaDirector : IZmstQuotaDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuotaDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQuotaDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQuota>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstquotalist = await this.unitOfWork.ZmstQuotaRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQuota>>(zmstquotalist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQuota> GetByIdAsync(string QuotaId, CancellationToken cancellationToken)
        {
            var zmstquotalist = await this.unitOfWork.ZmstQuotaRepository.FindByAsync(x => x.QuotaId == QuotaId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQuota>(zmstquotalist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQuota zmstquota, CancellationToken cancellationToken)
        {
            if (zmstquota == null)
            {
                throw new ArgumentNullException(nameof(zmstquota));
            }

            var chkefzmstquota = await this.unitOfWork.ZmstQuotaRepository.FindByAsync(r => r.QuotaId == zmstquota.QuotaId, default);
            if (chkefzmstquota != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstquota} already exists");
            }

            var efzmstquota = this.mapper.Map<Data.EF.Models.ZmstQuota>(zmstquota);

            await this.unitOfWork.ZmstQuotaRepository.InsertAsync(efzmstquota, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQuota zmstquota, CancellationToken cancellationToken)
        
		{
            if (zmstquota.QuotaId == "0")
            {
                throw new ArgumentException(nameof(zmstquota.QuotaId));
            }
			
			Data.EF.Models.ZmstQuota entityUpd = await unitOfWork.ZmstQuotaRepository.FindByAsync(e => e.QuotaId == zmstquota.QuotaId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.QuotaId= zmstquota.QuotaId;
					entityUpd.Name= zmstquota.Name;
					entityUpd.AlternateNames= zmstquota.AlternateNames;
					
				await unitOfWork.ZmstQuotaRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string QuotaId, CancellationToken cancellationToken)
        {
            if (QuotaId == "0")
            {
                throw new ArgumentNullException(nameof(QuotaId));
            }

            var entity = await this.unitOfWork.ZmstQuotaRepository.FindByAsync(x => x.QuotaId == QuotaId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QuotaId {QuotaId} was not found.");
            }

            await this.unitOfWork.ZmstQuotaRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQuotaRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
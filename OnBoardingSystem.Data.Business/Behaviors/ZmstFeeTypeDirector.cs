
//-----------------------------------------------------------------------
// <copyright file="ZmstFeeTypeDirector.cs" company="NIC">
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
    public class ZmstFeeTypeDirector : IZmstFeeTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstFeeTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstFeeTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstFeeType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstfeetypelist = await this.unitOfWork.ZmstFeeTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstFeeType>>(zmstfeetypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstFeeType> GetByIdAsync(int ActivityId, CancellationToken cancellationToken)
        {
            var zmstfeetypelist = await this.unitOfWork.ZmstFeeTypeRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstFeeType>(zmstfeetypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstFeeType zmstfeetype, CancellationToken cancellationToken)
        {
            if (zmstfeetype == null)
            {
                throw new ArgumentNullException(nameof(zmstfeetype));
            }

            var chkefzmstfeetype = await this.unitOfWork.ZmstFeeTypeRepository.FindByAsync(r => r.ActivityId == zmstfeetype.ActivityId, default);
            if (chkefzmstfeetype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstfeetype} already exists");
            }

            var efzmstfeetype = this.mapper.Map<Data.EF.Models.ZmstFeeType>(zmstfeetype);

            await this.unitOfWork.ZmstFeeTypeRepository.InsertAsync(efzmstfeetype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstFeeType zmstfeetype, CancellationToken cancellationToken)
        
		{
            if (zmstfeetype.ActivityId == 0)
            {
                throw new ArgumentException(nameof(zmstfeetype.ActivityId));
            }
			
			Data.EF.Models.ZmstFeeType entityUpd = await unitOfWork.ZmstFeeTypeRepository.FindByAsync(e => e.ActivityId == zmstfeetype.ActivityId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.ActivityId= zmstfeetype.ActivityId;
					entityUpd.Description= zmstfeetype.Description;
					
				await unitOfWork.ZmstFeeTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int ActivityId, CancellationToken cancellationToken)
        {
            if (ActivityId == 0)
            {
                throw new ArgumentNullException(nameof(ActivityId));
            }

            var entity = await this.unitOfWork.ZmstFeeTypeRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ActivityId {ActivityId} was not found.");
            }

            await this.unitOfWork.ZmstFeeTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstFeeTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
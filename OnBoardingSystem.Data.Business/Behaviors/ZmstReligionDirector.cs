
//-----------------------------------------------------------------------
// <copyright file="ZmstReligionDirector.cs" company="NIC">
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
    public class ZmstReligionDirector : IZmstReligionDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstReligionDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstReligionDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstReligion>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstreligionlist = await this.unitOfWork.ZmstReligionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstReligion>>(zmstreligionlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstReligion> GetByIdAsync(string ReligionId, CancellationToken cancellationToken)
        {
            var zmstreligionlist = await this.unitOfWork.ZmstReligionRepository.FindByAsync(x => x.ReligionId == ReligionId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstReligion>(zmstreligionlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstReligion zmstreligion, CancellationToken cancellationToken)
        {
            if (zmstreligion == null)
            {
                throw new ArgumentNullException(nameof(zmstreligion));
            }

            var chkefzmstreligion = await this.unitOfWork.ZmstReligionRepository.FindByAsync(r => r.ReligionId == zmstreligion.ReligionId, default);
            if (chkefzmstreligion != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstreligion} already exists");
            }

            var efzmstreligion = this.mapper.Map<Data.EF.Models.ZmstReligion>(zmstreligion);

            await this.unitOfWork.ZmstReligionRepository.InsertAsync(efzmstreligion, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstReligion zmstreligion, CancellationToken cancellationToken)
        
		{
            if (zmstreligion.ReligionId == "0")
            {
                throw new ArgumentException(nameof(zmstreligion.ReligionId));
            }
			
			Data.EF.Models.ZmstReligion entityUpd = await unitOfWork.ZmstReligionRepository.FindByAsync(e => e.ReligionId == zmstreligion.ReligionId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.ReligionId= zmstreligion.ReligionId;
					entityUpd.Description= zmstreligion.Description;
					entityUpd.AlternateNames= zmstreligion.AlternateNames;
					
				await unitOfWork.ZmstReligionRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string ReligionId, CancellationToken cancellationToken)
        {
            if (ReligionId == "0")
            {
                throw new ArgumentNullException(nameof(ReligionId));
            }

            var entity = await this.unitOfWork.ZmstReligionRepository.FindByAsync(x => x.ReligionId == ReligionId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ReligionId {ReligionId} was not found.");
            }

            await this.unitOfWork.ZmstReligionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstReligionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
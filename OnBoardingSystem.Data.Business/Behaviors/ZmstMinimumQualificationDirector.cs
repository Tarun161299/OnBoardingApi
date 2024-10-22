
//-----------------------------------------------------------------------
// <copyright file="ZmstMinimumQualificationDirector.cs" company="NIC">
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
    public class ZmstMinimumQualificationDirector : IZmstMinimumQualificationDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstMinimumQualificationDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstMinimumQualificationDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstMinimumQualification>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstminimumqualificationlist = await this.unitOfWork.ZmstMinimumQualificationRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstMinimumQualification>>(zmstminimumqualificationlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstMinimumQualification> GetByIdAsync(string MinimumQualId, CancellationToken cancellationToken)
        {
            var zmstminimumqualificationlist = await this.unitOfWork.ZmstMinimumQualificationRepository.FindByAsync(x => x.MinimumQualId == MinimumQualId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstMinimumQualification>(zmstminimumqualificationlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstMinimumQualification zmstminimumqualification, CancellationToken cancellationToken)
        {
            if (zmstminimumqualification == null)
            {
                throw new ArgumentNullException(nameof(zmstminimumqualification));
            }

            var chkefzmstminimumqualification = await this.unitOfWork.ZmstMinimumQualificationRepository.FindByAsync(r => r.MinimumQualId == zmstminimumqualification.MinimumQualId, default);
            if (chkefzmstminimumqualification != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstminimumqualification} already exists");
            }

            var efzmstminimumqualification = this.mapper.Map<Data.EF.Models.ZmstMinimumQualification>(zmstminimumqualification);

            await this.unitOfWork.ZmstMinimumQualificationRepository.InsertAsync(efzmstminimumqualification, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstMinimumQualification zmstminimumqualification, CancellationToken cancellationToken)
        
		{
            if (zmstminimumqualification.MinimumQualId == "0")
            {
                throw new ArgumentException(nameof(zmstminimumqualification.MinimumQualId));
            }
			
			Data.EF.Models.ZmstMinimumQualification entityUpd = await unitOfWork.ZmstMinimumQualificationRepository.FindByAsync(e => e.MinimumQualId == zmstminimumqualification.MinimumQualId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.MinimumQualId= zmstminimumqualification.MinimumQualId;
					entityUpd.Description= zmstminimumqualification.Description;
					entityUpd.AlternateNames= zmstminimumqualification.AlternateNames;
					
				await unitOfWork.ZmstMinimumQualificationRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string MinimumQualId, CancellationToken cancellationToken)
        {
            if (MinimumQualId == "0")
            {
                throw new ArgumentNullException(nameof(MinimumQualId));
            }

            var entity = await this.unitOfWork.ZmstMinimumQualificationRepository.FindByAsync(x => x.MinimumQualId == MinimumQualId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an MinimumQualId {MinimumQualId} was not found.");
            }

            await this.unitOfWork.ZmstMinimumQualificationRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstMinimumQualificationRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
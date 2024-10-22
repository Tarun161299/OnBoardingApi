
//-----------------------------------------------------------------------
// <copyright file="ZmstQualificationDirector.cs" company="NIC">
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
    public class ZmstQualificationDirector : IZmstQualificationDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualificationDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualificationDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualification>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualificationlist = await this.unitOfWork.ZmstQualificationRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualification>>(zmstqualificationlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualification> GetByIdAsync(string QualificationId, CancellationToken cancellationToken)
        {
            var zmstqualificationlist = await this.unitOfWork.ZmstQualificationRepository.FindByAsync(x => x.QualificationId == QualificationId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualification>(zmstqualificationlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualification zmstqualification, CancellationToken cancellationToken)
        {
            if (zmstqualification == null)
            {
                throw new ArgumentNullException(nameof(zmstqualification));
            }

            var chkefzmstqualification = await this.unitOfWork.ZmstQualificationRepository.FindByAsync(r => r.QualificationId == zmstqualification.QualificationId, default);
            if (chkefzmstqualification != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualification} already exists");
            }

            var efzmstqualification = this.mapper.Map<Data.EF.Models.ZmstQualification>(zmstqualification);

            await this.unitOfWork.ZmstQualificationRepository.InsertAsync(efzmstqualification, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualification zmstqualification, CancellationToken cancellationToken)
        
		{
            if (zmstqualification.QualificationId == "0")
            {
                throw new ArgumentException(nameof(zmstqualification.QualificationId));
            }
			
			Data.EF.Models.ZmstQualification entityUpd = await unitOfWork.ZmstQualificationRepository.FindByAsync(e => e.QualificationId == zmstqualification.QualificationId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.QualificationId= zmstqualification.QualificationId;
					entityUpd.Description= zmstqualification.Description;
					entityUpd.Name= zmstqualification.Name;
					entityUpd.AlternateNames= zmstqualification.AlternateNames;
					
				await unitOfWork.ZmstQualificationRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string QualificationId, CancellationToken cancellationToken)
        {
            if (QualificationId == "0")
            {
                throw new ArgumentNullException(nameof(QualificationId));
            }

            var entity = await this.unitOfWork.ZmstQualificationRepository.FindByAsync(x => x.QualificationId == QualificationId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualificationId {QualificationId} was not found.");
            }

            await this.unitOfWork.ZmstQualificationRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualificationRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
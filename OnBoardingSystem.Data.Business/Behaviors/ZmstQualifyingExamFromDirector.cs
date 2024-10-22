
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamFromDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamFromDirector : IZmstQualifyingExamFromDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamFromDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamFromDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExamFrom>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamfromlist = await this.unitOfWork.ZmstQualifyingExamFromRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExamFrom>>(zmstqualifyingexamfromlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExamFrom> GetByIdAsync(string QualExamFromId, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamfromlist = await this.unitOfWork.ZmstQualifyingExamFromRepository.FindByAsync(x => x.QualExamFromId == QualExamFromId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExamFrom>(zmstqualifyingexamfromlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExamFrom zmstqualifyingexamfrom, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexamfrom == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexamfrom));
            }

            var chkefzmstqualifyingexamfrom = await this.unitOfWork.ZmstQualifyingExamFromRepository.FindByAsync(r => r.QualExamFromId == zmstqualifyingexamfrom.QualExamFromId, default);
            if (chkefzmstqualifyingexamfrom != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexamfrom} already exists");
            }

            var efzmstqualifyingexamfrom = this.mapper.Map<Data.EF.Models.ZmstQualifyingExamFrom>(zmstqualifyingexamfrom);

            await this.unitOfWork.ZmstQualifyingExamFromRepository.InsertAsync(efzmstqualifyingexamfrom, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExamFrom zmstqualifyingexamfrom, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexamfrom.QualExamFromId == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexamfrom.QualExamFromId));
            }
			
			Data.EF.Models.ZmstQualifyingExamFrom entityUpd = await unitOfWork.ZmstQualifyingExamFromRepository.FindByAsync(e => e.QualExamFromId == zmstqualifyingexamfrom.QualExamFromId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.QualExamFromId= zmstqualifyingexamfrom.QualExamFromId;
					entityUpd.QualExamFromName= zmstqualifyingexamfrom.QualExamFromName;
					
				await unitOfWork.ZmstQualifyingExamFromRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string QualExamFromId, CancellationToken cancellationToken)
        {
            if (QualExamFromId == "0")
            {
                throw new ArgumentNullException(nameof(QualExamFromId));
            }

            var entity = await this.unitOfWork.ZmstQualifyingExamFromRepository.FindByAsync(x => x.QualExamFromId == QualExamFromId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualExamFromId {QualExamFromId} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamFromRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamFromRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
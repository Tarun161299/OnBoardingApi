
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamDirector : IZmstQualifyingExamDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExam>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamlist = await this.unitOfWork.ZmstQualifyingExamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExam>>(zmstqualifyingexamlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExam> GetByIdAsync(string QualifyingExamId, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamlist = await this.unitOfWork.ZmstQualifyingExamRepository.FindByAsync(x => x.QualifyingExamId == QualifyingExamId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExam>(zmstqualifyingexamlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExam zmstqualifyingexam, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexam == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexam));
            }

            var chkefzmstqualifyingexam = await this.unitOfWork.ZmstQualifyingExamRepository.FindByAsync(r => r.QualifyingExamId == zmstqualifyingexam.QualifyingExamId, default);
            if (chkefzmstqualifyingexam != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexam} already exists");
            }

            var efzmstqualifyingexam = this.mapper.Map<Data.EF.Models.ZmstQualifyingExam>(zmstqualifyingexam);

            await this.unitOfWork.ZmstQualifyingExamRepository.InsertAsync(efzmstqualifyingexam, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExam zmstqualifyingexam, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexam.QualifyingExamId == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexam.QualifyingExamId));
            }
			
			Data.EF.Models.ZmstQualifyingExam entityUpd = await unitOfWork.ZmstQualifyingExamRepository.FindByAsync(e => e.QualifyingExamId == zmstqualifyingexam.QualifyingExamId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.QualifyingExamId= zmstqualifyingexam.QualifyingExamId;
					entityUpd.QualifyingExamName= zmstqualifyingexam.QualifyingExamName;
					
				await unitOfWork.ZmstQualifyingExamRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string QualifyingExamId, CancellationToken cancellationToken)
        {
            if (QualifyingExamId == "0")
            {
                throw new ArgumentNullException(nameof(QualifyingExamId));
            }

            var entity = await this.unitOfWork.ZmstQualifyingExamRepository.FindByAsync(x => x.QualifyingExamId == QualifyingExamId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualifyingExamId {QualifyingExamId} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
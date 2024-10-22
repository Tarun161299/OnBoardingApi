
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamStreamDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamStreamDirector : IZmstQualifyingExamStreamDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamStreamDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamStreamDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExamStream>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamstreamlist = await this.unitOfWork.ZmstQualifyingExamStreamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExamStream>>(zmstqualifyingexamstreamlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExamStream> GetByIdAsync(string QualStreamId, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamstreamlist = await this.unitOfWork.ZmstQualifyingExamStreamRepository.FindByAsync(x => x.QualStreamId == QualStreamId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExamStream>(zmstqualifyingexamstreamlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExamStream zmstqualifyingexamstream, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexamstream == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexamstream));
            }

            var chkefzmstqualifyingexamstream = await this.unitOfWork.ZmstQualifyingExamStreamRepository.FindByAsync(r => r.QualStreamId == zmstqualifyingexamstream.QualStreamId, default);
            if (chkefzmstqualifyingexamstream != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexamstream} already exists");
            }

            var efzmstqualifyingexamstream = this.mapper.Map<Data.EF.Models.ZmstQualifyingExamStream>(zmstqualifyingexamstream);

            await this.unitOfWork.ZmstQualifyingExamStreamRepository.InsertAsync(efzmstqualifyingexamstream, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExamStream zmstqualifyingexamstream, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexamstream.QualStreamId == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexamstream.QualStreamId));
            }
			
			Data.EF.Models.ZmstQualifyingExamStream entityUpd = await unitOfWork.ZmstQualifyingExamStreamRepository.FindByAsync(e => e.QualStreamId == zmstqualifyingexamstream.QualStreamId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.QualStreamId= zmstqualifyingexamstream.QualStreamId;
					entityUpd.QualStreamName= zmstqualifyingexamstream.QualStreamName;
					
				await unitOfWork.ZmstQualifyingExamStreamRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string QualStreamId, CancellationToken cancellationToken)
        {
            if (QualStreamId == "0")
            {
                throw new ArgumentNullException(nameof(QualStreamId));
            }

            var entity = await this.unitOfWork.ZmstQualifyingExamStreamRepository.FindByAsync(x => x.QualStreamId == QualStreamId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualStreamId {QualStreamId} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamStreamRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamStreamRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
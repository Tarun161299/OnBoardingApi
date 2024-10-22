
//-----------------------------------------------------------------------
// <copyright file="ZmstQuestionPaperMediumDirector.cs" company="NIC">
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
    public class ZmstQuestionPaperMediumDirector : IZmstQuestionPaperMediumDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuestionPaperMediumDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQuestionPaperMediumDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQuestionPaperMedium>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstquestionpapermediumlist = await this.unitOfWork.ZmstQuestionPaperMediumRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQuestionPaperMedium>>(zmstquestionpapermediumlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQuestionPaperMedium> GetByIdAsync(string MediumId, CancellationToken cancellationToken)
        {
            var zmstquestionpapermediumlist = await this.unitOfWork.ZmstQuestionPaperMediumRepository.FindByAsync(x => x.MediumId == MediumId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQuestionPaperMedium>(zmstquestionpapermediumlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQuestionPaperMedium zmstquestionpapermedium, CancellationToken cancellationToken)
        {
            if (zmstquestionpapermedium == null)
            {
                throw new ArgumentNullException(nameof(zmstquestionpapermedium));
            }

            var chkefzmstquestionpapermedium = await this.unitOfWork.ZmstQuestionPaperMediumRepository.FindByAsync(r => r.MediumId == zmstquestionpapermedium.MediumId, default);
            if (chkefzmstquestionpapermedium != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstquestionpapermedium} already exists");
            }

            var efzmstquestionpapermedium = this.mapper.Map<Data.EF.Models.ZmstQuestionPaperMedium>(zmstquestionpapermedium);

            await this.unitOfWork.ZmstQuestionPaperMediumRepository.InsertAsync(efzmstquestionpapermedium, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQuestionPaperMedium zmstquestionpapermedium, CancellationToken cancellationToken)
        
		{
            if (zmstquestionpapermedium.MediumId == "0")
            {
                throw new ArgumentException(nameof(zmstquestionpapermedium.MediumId));
            }
			
			Data.EF.Models.ZmstQuestionPaperMedium entityUpd = await unitOfWork.ZmstQuestionPaperMediumRepository.FindByAsync(e => e.MediumId == zmstquestionpapermedium.MediumId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.MediumId= zmstquestionpapermedium.MediumId;
					entityUpd.MediumName= zmstquestionpapermedium.MediumName;
					
				await unitOfWork.ZmstQuestionPaperMediumRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string MediumId, CancellationToken cancellationToken)
        {
            if (MediumId == "0")
            {
                throw new ArgumentNullException(nameof(MediumId));
            }

            var entity = await this.unitOfWork.ZmstQuestionPaperMediumRepository.FindByAsync(x => x.MediumId == MediumId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an MediumId {MediumId} was not found.");
            }

            await this.unitOfWork.ZmstQuestionPaperMediumRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQuestionPaperMediumRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
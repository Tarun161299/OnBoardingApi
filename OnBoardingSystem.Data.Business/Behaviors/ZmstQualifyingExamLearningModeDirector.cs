
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamLearningModeDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamLearningModeDirector : IZmstQualifyingExamLearningModeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamLearningModeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamLearningModeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExamLearningMode>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamlearningmodelist = await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExamLearningMode>>(zmstqualifyingexamlearningmodelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExamLearningMode> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamlearningmodelist = await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExamLearningMode>(zmstqualifyingexamlearningmodelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexamlearningmode == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexamlearningmode));
            }

            var chkefzmstqualifyingexamlearningmode = await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.FindByAsync(r => r.Id == zmstqualifyingexamlearningmode.Id, default);
            if (chkefzmstqualifyingexamlearningmode != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexamlearningmode} already exists");
            }

            var efzmstqualifyingexamlearningmode = this.mapper.Map<Data.EF.Models.ZmstQualifyingExamLearningMode>(zmstqualifyingexamlearningmode);

            await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.InsertAsync(efzmstqualifyingexamlearningmode, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExamLearningMode zmstqualifyingexamlearningmode, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexamlearningmode.Id == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexamlearningmode.Id));
            }
			
			Data.EF.Models.ZmstQualifyingExamLearningMode entityUpd = await unitOfWork.ZmstQualifyingExamLearningModeRepository.FindByAsync(e => e.Id == zmstqualifyingexamlearningmode.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmstqualifyingexamlearningmode.Id;
					entityUpd.Description= zmstqualifyingexamlearningmode.Description;
					entityUpd.AlternateNames= zmstqualifyingexamlearningmode.AlternateNames;
					
				await unitOfWork.ZmstQualifyingExamLearningModeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamLearningModeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	

//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamBoardDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamBoardDirector : IZmstQualifyingExamBoardDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamBoardDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamBoardDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExamBoard>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamboardlist = await this.unitOfWork.ZmstQualifyingExamBoardRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExamBoard>>(zmstqualifyingexamboardlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExamBoard> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamboardlist = await this.unitOfWork.ZmstQualifyingExamBoardRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExamBoard>(zmstqualifyingexamboardlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExamBoard zmstqualifyingexamboard, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexamboard == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexamboard));
            }

            var chkefzmstqualifyingexamboard = await this.unitOfWork.ZmstQualifyingExamBoardRepository.FindByAsync(r => r.Id == zmstqualifyingexamboard.Id, default);
            if (chkefzmstqualifyingexamboard != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexamboard} already exists");
            }

            var efzmstqualifyingexamboard = this.mapper.Map<Data.EF.Models.ZmstQualifyingExamBoard>(zmstqualifyingexamboard);

            await this.unitOfWork.ZmstQualifyingExamBoardRepository.InsertAsync(efzmstqualifyingexamboard, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExamBoard zmstqualifyingexamboard, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexamboard.Id == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexamboard.Id));
            }
			
			Data.EF.Models.ZmstQualifyingExamBoard entityUpd = await unitOfWork.ZmstQualifyingExamBoardRepository.FindByAsync(e => e.Id == zmstqualifyingexamboard.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmstqualifyingexamboard.Id;
					entityUpd.Description= zmstqualifyingexamboard.Description;
					entityUpd.QualificationId= zmstqualifyingexamboard.QualificationId;
					entityUpd.AlternateNames= zmstqualifyingexamboard.AlternateNames;
					
				await unitOfWork.ZmstQualifyingExamBoardRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.ZmstQualifyingExamBoardRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamBoardRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamBoardRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
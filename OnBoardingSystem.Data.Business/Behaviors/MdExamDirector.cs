
//-----------------------------------------------------------------------
// <copyright file="MdExamDirector.cs" company="NIC">
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
    public class MdExamDirector : IMdExamDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdExamDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdExamDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdExam>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdexamlist = await this.unitOfWork.MdExamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdExam>>(mdexamlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdExam> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mdexamlist = await this.unitOfWork.MdExamRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdExam>(mdexamlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdExam mdexam, CancellationToken cancellationToken)
        {
            if (mdexam == null)
            {
                throw new ArgumentNullException(nameof(mdexam));
            }

            var chkefmdexam = await this.unitOfWork.MdExamRepository.FindByAsync(r => r.Id == mdexam.Id, default);
            if (chkefmdexam != null)
            {
                throw new EntityFoundException($"This Records {chkefmdexam} already exists");
            }

            var efmdexam = this.mapper.Map<Data.EF.Models.MdExam>(mdexam);

            await this.unitOfWork.MdExamRepository.InsertAsync(efmdexam, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdExam mdexam, CancellationToken cancellationToken)
        
		{
            if (mdexam.Id == "0")
            {
                throw new ArgumentException(nameof(mdexam.Id));
            }
			
			Data.EF.Models.MdExam entityUpd = await unitOfWork.MdExamRepository.FindByAsync(e => e.Id == mdexam.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= mdexam.Id;
					entityUpd.Exam= mdexam.Exam;
					
				await unitOfWork.MdExamRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.MdExamRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdExamRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdExamRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	

//-----------------------------------------------------------------------
// <copyright file="ZmstExamTypeDirector.cs" company="NIC">
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
    public class ZmstExamTypeDirector : IZmstExamTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstExamTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstExamTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstExamType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstexamtypelist = await this.unitOfWork.ZmstExamTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstExamType>>(zmstexamtypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstExamType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstexamtypelist = await this.unitOfWork.ZmstExamTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstExamType>(zmstexamtypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstExamType zmstexamtype, CancellationToken cancellationToken)
        {
            if (zmstexamtype == null)
            {
                throw new ArgumentNullException(nameof(zmstexamtype));
            }

            var chkefzmstexamtype = await this.unitOfWork.ZmstExamTypeRepository.FindByAsync(r => r.Id == zmstexamtype.Id, default);
            if (chkefzmstexamtype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstexamtype} already exists");
            }

            var efzmstexamtype = this.mapper.Map<Data.EF.Models.ZmstExamType>(zmstexamtype);

            await this.unitOfWork.ZmstExamTypeRepository.InsertAsync(efzmstexamtype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstExamType zmstexamtype, CancellationToken cancellationToken)
        
		{
            if (zmstexamtype.Id == "0")
            {
                throw new ArgumentException(nameof(zmstexamtype.Id));
            }
			
			Data.EF.Models.ZmstExamType entityUpd = await unitOfWork.ZmstExamTypeRepository.FindByAsync(e => e.Id == zmstexamtype.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmstexamtype.Id;
					entityUpd.Description= zmstexamtype.Description;
					
				await unitOfWork.ZmstExamTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.ZmstExamTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstExamTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstExamTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
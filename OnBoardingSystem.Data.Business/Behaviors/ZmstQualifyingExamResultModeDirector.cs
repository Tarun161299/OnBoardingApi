
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingExamResultModeDirector.cs" company="NIC">
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
    public class ZmstQualifyingExamResultModeDirector : IZmstQualifyingExamResultModeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingExamResultModeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingExamResultModeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingExamResultMode>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingexamresultmodelist = await this.unitOfWork.ZmstQualifyingExamResultModeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQualifyingExamResultMode>>(zmstqualifyingexamresultmodelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingExamResultMode> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstqualifyingexamresultmodelist = await this.unitOfWork.ZmstQualifyingExamResultModeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingExamResultMode>(zmstqualifyingexamresultmodelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingExamResultMode zmstqualifyingexamresultmode, CancellationToken cancellationToken)
        {
            if (zmstqualifyingexamresultmode == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingexamresultmode));
            }

            var chkefzmstqualifyingexamresultmode = await this.unitOfWork.ZmstQualifyingExamResultModeRepository.FindByAsync(r => r.Id == zmstqualifyingexamresultmode.Id, default);
            if (chkefzmstqualifyingexamresultmode != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingexamresultmode} already exists");
            }

            var efzmstqualifyingexamresultmode = this.mapper.Map<Data.EF.Models.ZmstQualifyingExamResultMode>(zmstqualifyingexamresultmode);

            await this.unitOfWork.ZmstQualifyingExamResultModeRepository.InsertAsync(efzmstqualifyingexamresultmode, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingExamResultMode zmstqualifyingexamresultmode, CancellationToken cancellationToken)
        
		{
            if (zmstqualifyingexamresultmode.Id == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingexamresultmode.Id));
            }
			
			Data.EF.Models.ZmstQualifyingExamResultMode entityUpd = await unitOfWork.ZmstQualifyingExamResultModeRepository.FindByAsync(e => e.Id == zmstqualifyingexamresultmode.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmstqualifyingexamresultmode.Id;
					entityUpd.Description= zmstqualifyingexamresultmode.Description;
					entityUpd.Alternatenames= zmstqualifyingexamresultmode.Alternatenames;
					
				await unitOfWork.ZmstQualifyingExamResultModeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.ZmstQualifyingExamResultModeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingExamResultModeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingExamResultModeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
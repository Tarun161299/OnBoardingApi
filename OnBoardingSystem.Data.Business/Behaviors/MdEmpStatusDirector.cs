
//-----------------------------------------------------------------------
// <copyright file="MdEmpStatusDirector.cs" company="NIC">
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
    public class MdEmpStatusDirector : IMdEmpStatusDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdEmpStatusDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdEmpStatusDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdEmpStatus>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdempstatuslist = await this.unitOfWork.MdEmpStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdEmpStatus>>(mdempstatuslist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdEmpStatus> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mdempstatuslist = await this.unitOfWork.MdEmpStatusRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdEmpStatus>(mdempstatuslist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdEmpStatus mdempstatus, CancellationToken cancellationToken)
        {
            if (mdempstatus == null)
            {
                throw new ArgumentNullException(nameof(mdempstatus));
            }

            var chkefmdempstatus = await this.unitOfWork.MdEmpStatusRepository.FindByAsync(r => r.Id == mdempstatus.Id, default);
            if (chkefmdempstatus != null)
            {
                throw new EntityFoundException($"This Records {chkefmdempstatus} already exists");
            }

            var efmdempstatus = this.mapper.Map<Data.EF.Models.MdEmpStatus>(mdempstatus);

            await this.unitOfWork.MdEmpStatusRepository.InsertAsync(efmdempstatus, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdEmpStatus mdempstatus, CancellationToken cancellationToken)
        
		{
            if (mdempstatus.Id == "0")
            {
                throw new ArgumentException(nameof(mdempstatus.Id));
            }
			
			Data.EF.Models.MdEmpStatus entityUpd = await unitOfWork.MdEmpStatusRepository.FindByAsync(e => e.Id == mdempstatus.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= mdempstatus.Id;
					entityUpd.Status= mdempstatus.Status;
					
				await unitOfWork.MdEmpStatusRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.MdEmpStatusRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdEmpStatusRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdEmpStatusRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
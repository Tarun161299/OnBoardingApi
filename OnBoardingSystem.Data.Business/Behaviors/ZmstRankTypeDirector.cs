
//-----------------------------------------------------------------------
// <copyright file="ZmstRankTypeDirector.cs" company="NIC">
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
    public class ZmstRankTypeDirector : IZmstRankTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstRankTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstRankTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstRankType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstranktypelist = await this.unitOfWork.ZmstRankTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstRankType>>(zmstranktypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstRankType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstranktypelist = await this.unitOfWork.ZmstRankTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstRankType>(zmstranktypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstRankType zmstranktype, CancellationToken cancellationToken)
        {
            if (zmstranktype == null)
            {
                throw new ArgumentNullException(nameof(zmstranktype));
            }

            var chkefzmstranktype = await this.unitOfWork.ZmstRankTypeRepository.FindByAsync(r => r.Id == zmstranktype.Id, default);
            if (chkefzmstranktype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstranktype} already exists");
            }

            var efzmstranktype = this.mapper.Map<Data.EF.Models.ZmstRankType>(zmstranktype);

            await this.unitOfWork.ZmstRankTypeRepository.InsertAsync(efzmstranktype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstRankType zmstranktype, CancellationToken cancellationToken)
        
		{
            if (zmstranktype.Id == "0")
            {
                throw new ArgumentException(nameof(zmstranktype.Id));
            }
			
			Data.EF.Models.ZmstRankType entityUpd = await unitOfWork.ZmstRankTypeRepository.FindByAsync(e => e.Id == zmstranktype.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= zmstranktype.Id;
					entityUpd.Description= zmstranktype.Description;
					
				await unitOfWork.ZmstRankTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.ZmstRankTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstRankTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstRankTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
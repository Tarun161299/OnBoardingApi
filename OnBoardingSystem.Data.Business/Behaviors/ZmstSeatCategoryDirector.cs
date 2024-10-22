
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatCategoryDirector.cs" company="NIC">
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
    public class ZmstSeatCategoryDirector : IZmstSeatCategoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatCategoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSeatCategoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSeatCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstseatcategorylist = await this.unitOfWork.ZmstSeatCategoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSeatCategory>>(zmstseatcategorylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSeatCategory> GetByIdAsync(string SeatCategoryId, CancellationToken cancellationToken)
        {
            var zmstseatcategorylist = await this.unitOfWork.ZmstSeatCategoryRepository.FindByAsync(x => x.SeatCategoryId == SeatCategoryId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSeatCategory>(zmstseatcategorylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSeatCategory zmstseatcategory, CancellationToken cancellationToken)
        {
            if (zmstseatcategory == null)
            {
                throw new ArgumentNullException(nameof(zmstseatcategory));
            }

            var chkefzmstseatcategory = await this.unitOfWork.ZmstSeatCategoryRepository.FindByAsync(r => r.SeatCategoryId == zmstseatcategory.SeatCategoryId, default);
            if (chkefzmstseatcategory != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstseatcategory} already exists");
            }

            var efzmstseatcategory = this.mapper.Map<Data.EF.Models.ZmstSeatCategory>(zmstseatcategory);

            await this.unitOfWork.ZmstSeatCategoryRepository.InsertAsync(efzmstseatcategory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSeatCategory zmstseatcategory, CancellationToken cancellationToken)
        
		{
            if (zmstseatcategory.SeatCategoryId == "0")
            {
                throw new ArgumentException(nameof(zmstseatcategory.SeatCategoryId));
            }
			
			Data.EF.Models.ZmstSeatCategory entityUpd = await unitOfWork.ZmstSeatCategoryRepository.FindByAsync(e => e.SeatCategoryId == zmstseatcategory.SeatCategoryId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SeatCategoryId= zmstseatcategory.SeatCategoryId;
					entityUpd.Description= zmstseatcategory.Description;
					entityUpd.AlternateNames= zmstseatcategory.AlternateNames;
					
				await unitOfWork.ZmstSeatCategoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SeatCategoryId, CancellationToken cancellationToken)
        {
            if (SeatCategoryId == "0")
            {
                throw new ArgumentNullException(nameof(SeatCategoryId));
            }

            var entity = await this.unitOfWork.ZmstSeatCategoryRepository.FindByAsync(x => x.SeatCategoryId == SeatCategoryId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SeatCategoryId {SeatCategoryId} was not found.");
            }

            await this.unitOfWork.ZmstSeatCategoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSeatCategoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
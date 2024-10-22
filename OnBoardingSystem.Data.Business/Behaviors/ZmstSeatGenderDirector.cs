
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatGenderDirector.cs" company="NIC">
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
    public class ZmstSeatGenderDirector : IZmstSeatGenderDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatGenderDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSeatGenderDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSeatGender>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstseatgenderlist = await this.unitOfWork.ZmstSeatGenderRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSeatGender>>(zmstseatgenderlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSeatGender> GetByIdAsync(string SeatGenderId, CancellationToken cancellationToken)
        {
            var zmstseatgenderlist = await this.unitOfWork.ZmstSeatGenderRepository.FindByAsync(x => x.SeatGenderId == SeatGenderId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSeatGender>(zmstseatgenderlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSeatGender zmstseatgender, CancellationToken cancellationToken)
        {
            if (zmstseatgender == null)
            {
                throw new ArgumentNullException(nameof(zmstseatgender));
            }

            var chkefzmstseatgender = await this.unitOfWork.ZmstSeatGenderRepository.FindByAsync(r => r.SeatGenderId == zmstseatgender.SeatGenderId, default);
            if (chkefzmstseatgender != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstseatgender} already exists");
            }

            var efzmstseatgender = this.mapper.Map<Data.EF.Models.ZmstSeatGender>(zmstseatgender);

            await this.unitOfWork.ZmstSeatGenderRepository.InsertAsync(efzmstseatgender, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSeatGender zmstseatgender, CancellationToken cancellationToken)
        
		{
            if (zmstseatgender.SeatGenderId == "0")
            {
                throw new ArgumentException(nameof(zmstseatgender.SeatGenderId));
            }
			
			Data.EF.Models.ZmstSeatGender entityUpd = await unitOfWork.ZmstSeatGenderRepository.FindByAsync(e => e.SeatGenderId == zmstseatgender.SeatGenderId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SeatGenderId= zmstseatgender.SeatGenderId;
					entityUpd.Description= zmstseatgender.Description;
					entityUpd.AlternateNames= zmstseatgender.AlternateNames;
					
				await unitOfWork.ZmstSeatGenderRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SeatGenderId, CancellationToken cancellationToken)
        {
            if (SeatGenderId == "0")
            {
                throw new ArgumentNullException(nameof(SeatGenderId));
            }

            var entity = await this.unitOfWork.ZmstSeatGenderRepository.FindByAsync(x => x.SeatGenderId == SeatGenderId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SeatGenderId {SeatGenderId} was not found.");
            }

            await this.unitOfWork.ZmstSeatGenderRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSeatGenderRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
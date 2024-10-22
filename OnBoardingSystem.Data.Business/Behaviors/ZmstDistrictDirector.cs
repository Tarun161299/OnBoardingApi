
//-----------------------------------------------------------------------
// <copyright file="ZmstDistrictDirector.cs" company="NIC">
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
    public class ZmstDistrictDirector : IZmstDistrictDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstDistrictDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstDistrictDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstdistrictlist = await this.unitOfWork.ZmstDistrictRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstDistrict>>(zmstdistrictlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstDistrict> GetByIdAsync(string StateId, CancellationToken cancellationToken)
        {
            var zmstdistrictlist = await this.unitOfWork.ZmstDistrictRepository.FindByAsync(x => x.StateId == StateId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstDistrict>(zmstdistrictlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstDistrict zmstdistrict, CancellationToken cancellationToken)
        {
            if (zmstdistrict == null)
            {
                throw new ArgumentNullException(nameof(zmstdistrict));
            }

            var chkefzmstdistrict = await this.unitOfWork.ZmstDistrictRepository.FindByAsync(r => r.StateId == zmstdistrict.StateId, default);
            if (chkefzmstdistrict != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstdistrict} already exists");
            }

            var efzmstdistrict = this.mapper.Map<Data.EF.Models.ZmstDistrict>(zmstdistrict);

            await this.unitOfWork.ZmstDistrictRepository.InsertAsync(efzmstdistrict, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstDistrict zmstdistrict, CancellationToken cancellationToken)
        
		{
            if (zmstdistrict.StateId == "0")
            {
                throw new ArgumentException(nameof(zmstdistrict.StateId));
            }
			
			Data.EF.Models.ZmstDistrict entityUpd = await unitOfWork.ZmstDistrictRepository.FindByAsync(e => e.StateId == zmstdistrict.StateId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.StateId= zmstdistrict.StateId;
					entityUpd.StateName= zmstdistrict.StateName;
					entityUpd.DistrictId= zmstdistrict.DistrictId;
					entityUpd.DistrictName= zmstdistrict.DistrictName;
					entityUpd.AlternateNames= zmstdistrict.AlternateNames;
					
				await unitOfWork.ZmstDistrictRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string StateId, CancellationToken cancellationToken)
        {
            if (StateId == "0")
            {
                throw new ArgumentNullException(nameof(StateId));
            }

            var entity = await this.unitOfWork.ZmstDistrictRepository.FindByAsync(x => x.StateId == StateId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an StateId {StateId} was not found.");
            }

            await this.unitOfWork.ZmstDistrictRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstDistrictRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
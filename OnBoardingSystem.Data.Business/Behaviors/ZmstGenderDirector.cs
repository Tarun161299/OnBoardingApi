
//-----------------------------------------------------------------------
// <copyright file="ZmstGenderDirector.cs" company="NIC">
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
    public class ZmstGenderDirector : IZmstGenderDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstGenderDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstGenderDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstGender>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstgenderlist = await this.unitOfWork.ZmstGenderRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstGender>>(zmstgenderlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstGender> GetByIdAsync(string GenderId, CancellationToken cancellationToken)
        {
            var zmstgenderlist = await this.unitOfWork.ZmstGenderRepository.FindByAsync(x => x.GenderId == GenderId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstGender>(zmstgenderlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstGender zmstgender, CancellationToken cancellationToken)
        {
            if (zmstgender == null)
            {
                throw new ArgumentNullException(nameof(zmstgender));
            }

            var chkefzmstgender = await this.unitOfWork.ZmstGenderRepository.FindByAsync(r => r.GenderId == zmstgender.GenderId, default);
            if (chkefzmstgender != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstgender} already exists");
            }

            var efzmstgender = this.mapper.Map<Data.EF.Models.ZmstGender>(zmstgender);

            await this.unitOfWork.ZmstGenderRepository.InsertAsync(efzmstgender, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstGender zmstgender, CancellationToken cancellationToken)
        
		{
            if (zmstgender.GenderId == "0")
            {
                throw new ArgumentException(nameof(zmstgender.GenderId));
            }
			
			Data.EF.Models.ZmstGender entityUpd = await unitOfWork.ZmstGenderRepository.FindByAsync(e => e.GenderId == zmstgender.GenderId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.GenderId= zmstgender.GenderId;
					entityUpd.GenderName= zmstgender.GenderName;
					entityUpd.AlternateNames= zmstgender.AlternateNames;
					
				await unitOfWork.ZmstGenderRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string GenderId, CancellationToken cancellationToken)
        {
            if (GenderId == "0")
            {
                throw new ArgumentNullException(nameof(GenderId));
            }

            var entity = await this.unitOfWork.ZmstGenderRepository.FindByAsync(x => x.GenderId == GenderId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an GenderId {GenderId} was not found.");
            }

            await this.unitOfWork.ZmstGenderRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstGenderRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
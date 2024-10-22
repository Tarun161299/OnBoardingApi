
//-----------------------------------------------------------------------
// <copyright file="MdYearDirector.cs" company="NIC">
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
    public class MdYearDirector : IMdYearDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdYearDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdYearDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdYear>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdyearlist = await this.unitOfWork.MdYearRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdYear>>(mdyearlist);
        }

		/// <inheritdoc/>
        public virtual async Task<List<AbsModels.MdYear>> GetByGroupIdAsync(string yearGroup, CancellationToken cancellationToken)
        {
            var mdyearlist = await this.unitOfWork.MdYearRepository.FindAllByAsync(x => x.YearGroup == yearGroup, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.MdYear>>(mdyearlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdYear mdyear, CancellationToken cancellationToken)
        {
            if (mdyear == null)
            {
                throw new ArgumentNullException(nameof(mdyear));
            }

            var chkefmdyear = await this.unitOfWork.MdYearRepository.FindByAsync(r => r.YearId == mdyear.YearId, default);
            if (chkefmdyear != null)
            {
                throw new EntityFoundException($"This Records {chkefmdyear} already exists");
            }

            var efmdyear = this.mapper.Map<Data.EF.Models.MdYear>(mdyear);

            await this.unitOfWork.MdYearRepository.InsertAsync(efmdyear, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdYear mdyear, CancellationToken cancellationToken)
        
		{
            if (mdyear.YearId == "0")
            {
                throw new ArgumentException(nameof(mdyear.YearId));
            }
			
			Data.EF.Models.MdYear entityUpd = await unitOfWork.MdYearRepository.FindByAsync(e => e.YearId == mdyear.YearId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.MdYearRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string YearId, CancellationToken cancellationToken)
        {
            if (YearId == "0")
            {
                throw new ArgumentNullException(nameof(YearId));
            }

            var entity = await this.unitOfWork.MdYearRepository.FindByAsync(x => x.YearId == YearId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an YearId {YearId} was not found.");
            }

            await this.unitOfWork.MdYearRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdYearRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
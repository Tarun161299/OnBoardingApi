
//-----------------------------------------------------------------------
// <copyright file="ZmstBranchDirector.cs" company="NIC">
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
    public class ZmstBranchDirector : IZmstBranchDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstBranchDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstBranchDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstBranch>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstbranchlist = await this.unitOfWork.ZmstBranchRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstBranch>>(zmstbranchlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstBranch> GetByIdAsync(string BrCd, CancellationToken cancellationToken)
        {
            var zmstbranchlist = await this.unitOfWork.ZmstBranchRepository.FindByAsync(x => x.BrCd == BrCd, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstBranch>(zmstbranchlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstBranch zmstbranch, CancellationToken cancellationToken)
        {
            if (zmstbranch == null)
            {
                throw new ArgumentNullException(nameof(zmstbranch));
            }

            var chkefzmstbranch = await this.unitOfWork.ZmstBranchRepository.FindByAsync(r => r.BrCd == zmstbranch.BrCd, default);
            if (chkefzmstbranch != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstbranch} already exists");
            }

            var efzmstbranch = this.mapper.Map<Data.EF.Models.ZmstBranch>(zmstbranch);

            await this.unitOfWork.ZmstBranchRepository.InsertAsync(efzmstbranch, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstBranch zmstbranch, CancellationToken cancellationToken)
        
		{
            if (zmstbranch.BrCd == "0")
            {
                throw new ArgumentException(nameof(zmstbranch.BrCd));
            }
			
			Data.EF.Models.ZmstBranch entityUpd = await unitOfWork.ZmstBranchRepository.FindByAsync(e => e.BrCd == zmstbranch.BrCd, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.BrCd= zmstbranch.BrCd;
					entityUpd.BrNm= zmstbranch.BrNm;
					entityUpd.Stream= zmstbranch.Stream;
					
				await unitOfWork.ZmstBranchRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string BrCd, CancellationToken cancellationToken)
        {
            if (BrCd == "0")
            {
                throw new ArgumentNullException(nameof(BrCd));
            }

            var entity = await this.unitOfWork.ZmstBranchRepository.FindByAsync(x => x.BrCd == BrCd, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an BrCd {BrCd} was not found.");
            }

            await this.unitOfWork.ZmstBranchRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstBranchRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
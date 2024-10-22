
//-----------------------------------------------------------------------
// <copyright file="ZmstIdentityTypeDirector.cs" company="NIC">
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
    public class ZmstIdentityTypeDirector : IZmstIdentityTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstIdentityTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstIdentityTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstIdentityType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstidentitytypelist = await this.unitOfWork.ZmstIdentityTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstIdentityType>>(zmstidentitytypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstIdentityType> GetByIdAsync(string IdentityTypeId, CancellationToken cancellationToken)
        {
            var zmstidentitytypelist = await this.unitOfWork.ZmstIdentityTypeRepository.FindByAsync(x => x.IdentityTypeId == IdentityTypeId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstIdentityType>(zmstidentitytypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstIdentityType zmstidentitytype, CancellationToken cancellationToken)
        {
            if (zmstidentitytype == null)
            {
                throw new ArgumentNullException(nameof(zmstidentitytype));
            }

            var chkefzmstidentitytype = await this.unitOfWork.ZmstIdentityTypeRepository.FindByAsync(r => r.IdentityTypeId == zmstidentitytype.IdentityTypeId, default);
            if (chkefzmstidentitytype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstidentitytype} already exists");
            }

            var efzmstidentitytype = this.mapper.Map<Data.EF.Models.ZmstIdentityType>(zmstidentitytype);

            await this.unitOfWork.ZmstIdentityTypeRepository.InsertAsync(efzmstidentitytype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstIdentityType zmstidentitytype, CancellationToken cancellationToken)
        
		{
            if (zmstidentitytype.IdentityTypeId == "0")
            {
                throw new ArgumentException(nameof(zmstidentitytype.IdentityTypeId));
            }
			
			Data.EF.Models.ZmstIdentityType entityUpd = await unitOfWork.ZmstIdentityTypeRepository.FindByAsync(e => e.IdentityTypeId == zmstidentitytype.IdentityTypeId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.IdentityTypeId= zmstidentitytype.IdentityTypeId;
					entityUpd.Description= zmstidentitytype.Description;
					entityUpd.AlternateNames= zmstidentitytype.AlternateNames;
					
				await unitOfWork.ZmstIdentityTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string IdentityTypeId, CancellationToken cancellationToken)
        {
            if (IdentityTypeId == "0")
            {
                throw new ArgumentNullException(nameof(IdentityTypeId));
            }

            var entity = await this.unitOfWork.ZmstIdentityTypeRepository.FindByAsync(x => x.IdentityTypeId == IdentityTypeId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an IdentityTypeId {IdentityTypeId} was not found.");
            }

            await this.unitOfWork.ZmstIdentityTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstIdentityTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
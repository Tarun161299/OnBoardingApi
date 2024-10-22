
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteTypeDirector.cs" company="NIC">
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
    public class ZmstInstituteTypeDirector : IZmstInstituteTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstInstituteTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstInstituteType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstinstitutetypelist = await this.unitOfWork.ZmstInstituteTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstInstituteType>>(zmstinstitutetypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstInstituteType> GetByIdAsync(string InstituteTypeId, CancellationToken cancellationToken)
        {
            var zmstinstitutetypelist = await this.unitOfWork.ZmstInstituteTypeRepository.FindByAsync(x => x.InstituteTypeId == InstituteTypeId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstInstituteType>(zmstinstitutetypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstInstituteType zmstinstitutetype, CancellationToken cancellationToken)
        {
            if (zmstinstitutetype == null)
            {
                throw new ArgumentNullException(nameof(zmstinstitutetype));
            }

            var chkefzmstinstitutetype = await this.unitOfWork.ZmstInstituteTypeRepository.FindByAsync(r => r.InstituteTypeId == zmstinstitutetype.InstituteTypeId, default);
            if (chkefzmstinstitutetype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstinstitutetype} already exists");
            }

            var efzmstinstitutetype = this.mapper.Map<Data.EF.Models.ZmstInstituteType>(zmstinstitutetype);

            await this.unitOfWork.ZmstInstituteTypeRepository.InsertAsync(efzmstinstitutetype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstInstituteType zmstinstitutetype, CancellationToken cancellationToken)
        
		{
            if (zmstinstitutetype.InstituteTypeId == "0")
            {
                throw new ArgumentException(nameof(zmstinstitutetype.InstituteTypeId));
            }
			
			Data.EF.Models.ZmstInstituteType entityUpd = await unitOfWork.ZmstInstituteTypeRepository.FindByAsync(e => e.InstituteTypeId == zmstinstitutetype.InstituteTypeId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.InstituteTypeId= zmstinstitutetype.InstituteTypeId;
					entityUpd.InstituteType= zmstinstitutetype.InstituteType;
					entityUpd.Priority= zmstinstitutetype.Priority;
					
				await unitOfWork.ZmstInstituteTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string InstituteTypeId, CancellationToken cancellationToken)
        {
            if (InstituteTypeId == "0")
            {
                throw new ArgumentNullException(nameof(InstituteTypeId));
            }

            var entity = await this.unitOfWork.ZmstInstituteTypeRepository.FindByAsync(x => x.InstituteTypeId == InstituteTypeId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an InstituteTypeId {InstituteTypeId} was not found.");
            }

            await this.unitOfWork.ZmstInstituteTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstInstituteTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
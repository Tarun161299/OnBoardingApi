
//-----------------------------------------------------------------------
// <copyright file="MdIdTypeDirector.cs" company="NIC">
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
    public class MdIdTypeDirector : IMdIdTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdIdTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdIdTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdIdType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdidtypelist = await this.unitOfWork.MdIdTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdIdType>>(mdidtypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdIdType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mdidtypelist = await this.unitOfWork.MdIdTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdIdType>(mdidtypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdIdType mdidtype, CancellationToken cancellationToken)
        {
            if (mdidtype == null)
            {
                throw new ArgumentNullException(nameof(mdidtype));
            }

            var chkefmdidtype = await this.unitOfWork.MdIdTypeRepository.FindByAsync(r => r.Id == mdidtype.Id, default);
            if (chkefmdidtype != null)
            {
                throw new EntityFoundException($"This Records {chkefmdidtype} already exists");
            }

            var efmdidtype = this.mapper.Map<Data.EF.Models.MdIdType>(mdidtype);

            await this.unitOfWork.MdIdTypeRepository.InsertAsync(efmdidtype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdIdType mdidtype, CancellationToken cancellationToken)
        
		{
            if (mdidtype.Id == "0")
            {
                throw new ArgumentException(nameof(mdidtype.Id));
            }
			
			Data.EF.Models.MdIdType entityUpd = await unitOfWork.MdIdTypeRepository.FindByAsync(e => e.Id == mdidtype.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= mdidtype.Id;
					entityUpd.IdType= mdidtype.IdType;
					
				await unitOfWork.MdIdTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.MdIdTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdIdTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdIdTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
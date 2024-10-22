
//-----------------------------------------------------------------------
// <copyright file="MdRoleDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class MdRoleDirector : IMdRoleDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdRoleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdRoleDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdRole>> GetAllAsync(CancellationToken cancellationToken)
        {
            
            var mdrolelist = this.unitOfWork.MdRoleRepository.GetAll();
            var query = from mrole in mdrolelist
                        where mrole.RoleName.ToUpper() != "USER" && mrole.RoleName.ToUpper() != "SUPERADMIN"
                        select new AbsModels.MdRole
                        {
                            RoleName = mrole.RoleName,
                        };

               return query.ToList();
               //var mdrolelist = await this.unitOfWork.MdRoleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
               //return this.mapper.Map<List<AbsModels.MdRole>>(mdrolelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdRole> GetByIdAsync(string RoleId, CancellationToken cancellationToken)
        {
            var mdrolelist = await this.unitOfWork.MdRoleRepository.FindByAsync(x => x.RoleId == RoleId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdRole>(mdrolelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdRole mdrole, CancellationToken cancellationToken)
        {
            if (mdrole == null)
            {
                throw new ArgumentNullException(nameof(mdrole));
            }

            var chkefmdrole = await this.unitOfWork.MdRoleRepository.FindByAsync(r => r.RoleId == mdrole.RoleId, default);
            if (chkefmdrole != null)
            {
                throw new EntityFoundException($"This Records {chkefmdrole} already exists");
            }

            var efmdrole = this.mapper.Map<Data.EF.Models.MdRole>(mdrole);

            await this.unitOfWork.MdRoleRepository.InsertAsync(efmdrole, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdRole mdrole, CancellationToken cancellationToken)
        
		{
            if (mdrole.RoleId == "0")
            {
                throw new ArgumentException(nameof(mdrole.RoleId));
            }
			
			Data.EF.Models.MdRole entityUpd = await unitOfWork.MdRoleRepository.FindByAsync(e => e.RoleId == mdrole.RoleId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.MdRoleRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string RoleId, CancellationToken cancellationToken)
        {
            if (RoleId == "0")
            {
                throw new ArgumentNullException(nameof(RoleId));
            }

            var entity = await this.unitOfWork.MdRoleRepository.FindByAsync(x => x.RoleId == RoleId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an RoleId {RoleId} was not found.");
            }

            await this.unitOfWork.MdRoleRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdRoleRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
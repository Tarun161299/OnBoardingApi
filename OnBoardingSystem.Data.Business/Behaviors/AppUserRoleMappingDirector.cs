
//-----------------------------------------------------------------------
// <copyright file="AppUserRoleMappingDirector.cs" company="NIC">
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
    public class AppUserRoleMappingDirector : IAppUserRoleMappingDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRoleMappingDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppUserRoleMappingDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppUserRoleMapping>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appuserrolemappinglist = await this.unitOfWork.AppUserRoleMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppUserRoleMapping>>(appuserrolemappinglist);
        }

		/// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppUserRoleMapping>> GetByIdAsync(string UserID, CancellationToken cancellationToken)
        {
            try
            {

                var mdrolelist = this.unitOfWork.MdRoleRepository.GetAll();
                var appuserlist = this.unitOfWork.AppUserRoleMappingRepository.FindAllBy(x => x.UserId == UserID);

                var query = from mrole in mdrolelist
                            join approle in appuserlist on mrole.RoleName equals approle.RoleId into temp
                            from fl in temp.DefaultIfEmpty()
                            where mrole.RoleName.ToUpper() != "USER" && mrole.RoleName.ToUpper() != "SUPERADMIN"
                            select new AbsModels.AppUserRoleMapping
                            {
                                Role = Convert.ToString(mrole.RoleName).Trim(),
                                RoleId = Convert.ToString(mrole.RoleName).Trim(),
                                UserId = UserID,
                                IsReadOnly = string.IsNullOrEmpty(fl.IsReadOnly) ? "N" : Convert.ToString(fl.IsReadOnly).Trim(),
                                IsActive = string.IsNullOrEmpty(fl.IsActive) ? "N" : Convert.ToString(fl.IsActive).Trim(),
                                Assign = Convert.ToString(fl.RoleId).Trim(),
                           };
                    
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
       }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(List<AbsModels.AppUserRoleMapping> appuserrolemapping,string roleid, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                int del = 0;
                var entity = await this.unitOfWork.AppUserRoleMappingRepository.FindAllByAsync(x => x.UserId == roleid, cancellationToken).ConfigureAwait(false);
                await this.unitOfWork.AppUserRoleMappingRepository.DeleteAsync(x => x.UserId == roleid, cancellationToken).ConfigureAwait(false);
                del = await unitOfWork.CommitAsync(cancellationToken);
                var appmod = this.mapper.Map<List<Data.EF.Models.AppUserRoleMapping>>(appuserrolemapping);
                await this.unitOfWork.AppUserRoleMappingRepository.InsertAsync(appmod, cancellationToken).ConfigureAwait(false);
                result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                return result + del;
            }
            catch (Exception ex)
            {

                throw ex;
            }
         }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppUserRoleMapping appuserrolemapping, CancellationToken cancellationToken)
        
		{
            if (appuserrolemapping.UserId == "0")
            {
                throw new ArgumentException(nameof(appuserrolemapping.UserId));
            }
			
			Data.EF.Models.AppUserRoleMapping entityUpd = await unitOfWork.AppUserRoleMappingRepository.FindByAsync(e => e.UserId == appuserrolemapping.UserId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.AppUserRoleMappingRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string UserID, CancellationToken cancellationToken)
        {
            if (UserID == "0")
            {
                throw new ArgumentNullException(nameof(UserID));
            }

            var entity = await this.unitOfWork.AppUserRoleMappingRepository.FindByAsync(x => x.UserId == UserID, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an UserID {UserID} was not found.");
            }

            await this.unitOfWork.AppUserRoleMappingRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppUserRoleMappingRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
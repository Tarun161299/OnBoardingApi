
//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermissionDirector.cs" company="NIC">
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
    using Abp.Modules;
    using Abp.Domain.Entities;

    /// <inheritdoc />
    public class AppRoleModulePermissionDirector : IAppRoleModulePermissionDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRoleModulePermissionDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppRoleModulePermissionDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppRoleModulePermission>> GetAllAsync(CancellationToken cancellationToken)
        {
            var approlemodulepermissionlist = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppRoleModulePermission>>(approlemodulepermissionlist);
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppRoleModulePermission>> GetByIdAsync(string RoleId, CancellationToken cancellationToken)
        {
            try
            {

                var mdmodulelist = this.unitOfWork.MDModuleRepository.GetAll();
                var approlelist = this.unitOfWork.AppRoleModulePermissionRepository.FindAllBy(x => x.RoleId == RoleId);

                var query = from mrole in mdmodulelist
                            join approle in approlelist on mrole.ModuleId equals approle.ModuleId into temp
                            from fl in temp.DefaultIfEmpty()
                            select new AbsModels.AppRoleModulePermission
                            {
                                ModuleName = Convert.ToString(mrole.Heading).Trim(),
                                ModuleId = Convert.ToString(mrole.ModuleId).Trim(),
                                IsReadOnly = string.IsNullOrEmpty(fl.IsReadOnly) ? "N" : Convert.ToString(fl.IsReadOnly).Trim(),
                                IsActive = string.IsNullOrEmpty(fl.IsActive) ? "N" : Convert.ToString(fl.IsActive).Trim(),
                                Assign = Convert.ToString(fl.ModuleId).Trim(),
                                RoleId = RoleId,
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

		/// <inheritdoc/>
        public virtual async Task<int> InsertAsync(List<AbsModels.AppRoleModulePermission> approlemodulepermission,string roleid, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                int del = 0;
                var entity = await this.unitOfWork.AppRoleModulePermissionRepository.FindAllByAsync(x => x.RoleId == roleid, cancellationToken).ConfigureAwait(false);
                //await this.unitOfWork.AppRoleModulePermissionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
                //await this.unitOfWork.AppRoleModulePermissionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
                await this.unitOfWork.AppRoleModulePermissionRepository.DeleteAsync(x => x.RoleId == roleid, cancellationToken).ConfigureAwait(false);
                del = await unitOfWork.CommitAsync(cancellationToken);
                var appmod = this.mapper.Map<List<Data.EF.Models.AppRoleModulePermission>>(approlemodulepermission);
                await this.unitOfWork.AppRoleModulePermissionRepository.InsertAsync(appmod, cancellationToken).ConfigureAwait(false);
                result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                return result + del;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<int> InsertAsync(string instcode, List<AbsModels.ZmstInstituteStream> zmstinstitutestream, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        //string instCd = zmstinstitutestream[0].InstCd;
        //        string instCd = instcode;
        //        await this.unitOfWork.ZmstInstituteStreamRepository.DeleteAsync(x => x.InstCd == instCd, cancellationToken).ConfigureAwait(false);
        //        await unitOfWork.CommitAsync(cancellationToken);
        //        var efzmstinstitutestream = this.mapper.Map<List<Data.EF.Models.ZmstInstituteStream>>(zmstinstitutestream);
        //        await this.unitOfWork.ZmstInstituteStreamRepository.InsertAsync(efzmstinstitutestream, cancellationToken).ConfigureAwait(false);
        //        return await unitOfWork.CommitAsync(cancellationToken);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.AppRoleModulePermission approlemodulepermission, CancellationToken cancellationToken)
        
		{
            if (approlemodulepermission.RoleId == "0")
            {
                throw new ArgumentException(nameof(approlemodulepermission.RoleId));
            }
			
			Data.EF.Models.AppRoleModulePermission entityUpd = await unitOfWork.AppRoleModulePermissionRepository.FindByAsync(e => e.RoleId == approlemodulepermission.RoleId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.AppRoleModulePermissionRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.AppRoleModulePermissionRepository.FindByAsync(x => x.RoleId == RoleId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new Abp.Domain.Entities.EntityNotFoundException($"The Data with an RoleId {RoleId} was not found.");
            }

            await this.unitOfWork.AppRoleModulePermissionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppRoleModulePermissionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
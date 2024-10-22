
//-----------------------------------------------------------------------
// <copyright file="MdMainModuleDirector.cs" company="NIC">
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
    using DocumentFormat.OpenXml.Wordprocessing;

    /// <inheritdoc />
    public class MdMainModuleDirector : IMdMainModuleDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMainModuleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdMainModuleDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdMainModule>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdmainmodulelist = await this.unitOfWork.MdMainModuleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdMainModule>>(mdmainmodulelist);
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.MdMainModule>> GetByIdAsync(string MainModuleId, CancellationToken cancellationToken)
        {
            var userRoleMApping = await this.unitOfWork.AppUserRoleMappingRepository.FindAllByAsync(x => x.UserId == MainModuleId, cancellationToken).ConfigureAwait(false);
            var modulePermission = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdModule = await this.unitOfWork.MDModuleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            //var mdmainmodulelist = await this.unitOfWork.MdMainModuleRepository.FindByAsync(x => x.MainModuleId == MainModuleId, cancellationToken).ConfigureAwait(false);
            var mdmainmodulelist = await this.unitOfWork.MdMainModuleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var assignedMainModule = from userRole in userRoleMApping
                                     join permission in modulePermission on userRole.RoleId equals permission.RoleId
                                     join mdMainModule in mdModule on permission.ModuleId equals mdMainModule.ModuleId
                                     select new Abstractions.Models.MDModuleData
                                     {
                                         ModuleId = mdMainModule.ModuleId,
                                         Heading = mdMainModule.Heading,
                                         SubHeading = mdMainModule.SubHeading,
                                         Url = mdMainModule.Url,
                                         Path = mdMainModule.Path,
                                         Parent = mdMainModule.Parent,
                                         IsActive = mdMainModule.IsActive,
                                         MainModule= mdMainModule.MainModule
                                     };

            var result = from mainModule in mdmainmodulelist
                         join module in assignedMainModule on mainModule.MainModuleId equals module.MainModule
                         select new Abstractions.Models.MdMainModule
                         {
                             MainModuleId = mainModule.MainModuleId,
                             Heading = mainModule.Heading,
                             SubHeading = mainModule.SubHeading,
                             Path = mainModule.Path,
                             IsActive = mainModule.IsActive,
                             DisplayPriority = mainModule.DisplayPriority,
                             Remarks = mainModule.Remarks,
                             Icon = mainModule.Icon,
                             CssClass = mainModule.CssClass,
                         };

           // var result = this.mapper.Map<List<Abstractions.Models.MdMainModule>>(assignedMainModule);
            return result.DistinctBy(x=>x.MainModuleId).ToList();
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdMainModule mdmainmodule, CancellationToken cancellationToken)
        {
            if (mdmainmodule == null)
            {
                throw new ArgumentNullException(nameof(mdmainmodule));
            }

            var chkefmdmainmodule = await this.unitOfWork.MdMainModuleRepository.FindByAsync(r => r.MainModuleId == mdmainmodule.MainModuleId, default);
            if (chkefmdmainmodule != null)
            {
                throw new EntityFoundException($"This Records {chkefmdmainmodule} already exists");
            }

            var efmdmainmodule = this.mapper.Map<Data.EF.Models.MdMainModule>(mdmainmodule);

            await this.unitOfWork.MdMainModuleRepository.InsertAsync(efmdmainmodule, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.MdMainModule mdmainmodule, CancellationToken cancellationToken)

        {
            if (mdmainmodule.MainModuleId == "0")
            {
                throw new ArgumentException(nameof(mdmainmodule.MainModuleId));
            }

            Data.EF.Models.MdMainModule entityUpd = await unitOfWork.MdMainModuleRepository.FindByAsync(e => e.MainModuleId == mdmainmodule.MainModuleId, cancellationToken);
            if (entityUpd != null)
            {

                await unitOfWork.MdMainModuleRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string MainModuleId, CancellationToken cancellationToken)
        {
            if (MainModuleId == "0")
            {
                throw new ArgumentNullException(nameof(MainModuleId));
            }

            var entity = await this.unitOfWork.MdMainModuleRepository.FindByAsync(x => x.MainModuleId == MainModuleId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an MainModuleId {MainModuleId} was not found.");
            }

            await this.unitOfWork.MdMainModuleRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdMainModuleRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

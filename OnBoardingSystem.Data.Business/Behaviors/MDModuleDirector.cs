//-----------------------------------------------------------------------
// <copyright file="MDModuleDirector.cs" company="NIC">
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
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    public class MDModuleDirector:IMDModuleDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModuleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MDModuleDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<Abstractions.Models.MDModule> GetByUserIdAsync(string userId,CancellationToken cancellationToken)
        {
            try
            {
                var obj = new MDModule();
                var appUserRoleMapping = await this.unitOfWork.AppUserRoleMappingRepository.FindAllByAsync(x => x.UserId == userId, cancellationToken).ConfigureAwait(false);
                var appRoleModulePermission = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var distAppRoleModulePermission= appRoleModulePermission;
                var mdmodule = await this.unitOfWork.MDModuleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var entity = from appRoleMapping in appUserRoleMapping
                             join roleModulePermission in distAppRoleModulePermission on appRoleMapping.RoleId equals roleModulePermission.RoleId
                             join modules in mdmodule on roleModulePermission.ModuleId equals modules.ModuleId
                             select new MDModuleData
                             {
                                 ModuleId = modules.ModuleId,
                                 Heading = modules.Heading,
                                 SubHeading = modules.SubHeading,
                                 Url = modules.Url,
                                 Parent = modules.Parent,
                                 Path = modules.Path,
                                 IsActive = modules.IsActive,
                                 MainModule= modules.MainModule,
                             };
                List<Abstractions.Models.MDModuleData> data = new List<Abstractions.Models.MDModuleData>();
                var result = this.mapper.Map<List<MDModuleData>>(entity.DistinctBy(x => x.ModuleId).ToList());
                var child = result.Where(a => a.Parent != null).DistinctBy(x => x.ModuleId);
                var parent = from module in mdmodule
                             join children in child on module.ModuleId equals children.Parent
                             select new MDModuleData
                             {
                                 ModuleId = module.ModuleId,
                                 Heading = module.Heading,
                                 SubHeading = module.SubHeading,
                                 Url = module.Url,
                                 Parent = module.Parent,
                                 Path = module.Path,
                                 IsActive = module.IsActive,
                                 MainModule= children.MainModule
                             };
                obj.SubModuleList = child.ToList();
                obj.ParentModuleList = parent.DistinctBy(w => w.ModuleId).ToList();

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}

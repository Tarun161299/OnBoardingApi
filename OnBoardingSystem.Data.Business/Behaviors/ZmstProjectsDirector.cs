//-----------------------------------------------------------------------
// <copyright file="MdMinistryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Castle.Core.Internal;
    using DocumentFormat.OpenXml.InkML;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using Serilog;
    using static System.Net.WebRequestMethods;

    public class ZmstProjectsDirector : IZmstProjectsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMinistryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstProjectsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstProjects>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstProjectList = await this.unitOfWork.ZmstProjectRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var agencylist = await this.unitOfWork.MDAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = from projectList in zmstProjectList
                         join agency in agencylist on projectList.AgencyId equals agency.AgencyId
                         select new Abstractions.Models.ZmstProjects
                         {
                                    AgencyId = projectList.AgencyId,
                                    AgencyName = agency.AgencyName,
                                    ExamCounsid = projectList.ExamCounsid,
                                    AcademicYear = projectList.AcademicYear,
                                    ServiceType = projectList.ServiceType,
                                    Attempt = projectList.Attempt,
                                    ProjectId = projectList.ProjectId,
                                    ProjectName = projectList.ProjectName,
                                    Description = projectList.Description,
                                    RequestLetter = projectList.RequestLetter,
                                    CreatedDate = projectList.CreatedDate,
                                    CreatedBy = projectList.CreatedBy,
                                    ModifiedDate = projectList.ModifiedDate,
                                    ModifiedBy = projectList.ModifiedBy,
                                    IsLive = projectList.IsLive,
                                    Pinitiated = projectList.Pinitiated,
                         };

            return result.ToList();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int projectid, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.ZmstProjectRepository.FindByAsync(
            x => x.ProjectId == projectid, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                // throw new EntityNotFoundException($"The Ministries with an MinistryId {ministryId} was not found.");
            }

            await this.unitOfWork.ZmstProjectRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstProjectRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstProjects>> GetByIdAsync(int projectid, CancellationToken cancellationToken)
        {
            try
            {
                var projectcyclelist = new Abstractions.Models.ZmstProjects();
                var ZmstProjectList = await this.unitOfWork.ZmstProjectRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var AppProjectDetails = await this.unitOfWork.AppProjectDetailsRepository.FindAllByAsync(x => x.Id == projectid, cancellationToken).ConfigureAwait(false);
                var cyclelist = from zmstprojectlist in ZmstProjectList
                                join appprojectdetails in AppProjectDetails on zmstprojectlist.AgencyId equals appprojectdetails.AgencyId
                                where (appprojectdetails.ProjectYear == zmstprojectlist.AcademicYear) && appprojectdetails.Id == projectid
                                select new Abstractions.Models.ZmstProjects
                                {
                                    AgencyId = zmstprojectlist.AgencyId,
                                    ExamCounsid = zmstprojectlist.ExamCounsid,
                                    AcademicYear = zmstprojectlist.AcademicYear,
                                    ServiceType = zmstprojectlist.ServiceType,
                                    Attempt = zmstprojectlist.Attempt,
                                    ProjectId = zmstprojectlist.ProjectId,
                                    ProjectName = zmstprojectlist.ProjectName,
                                    Description = zmstprojectlist.Description,
                                    RequestLetter = zmstprojectlist.RequestLetter,
                                    CreatedDate = zmstprojectlist.CreatedDate,
                                    CreatedBy = zmstprojectlist.CreatedBy,
                                    ModifiedDate = zmstprojectlist.ModifiedDate,
                                    ModifiedBy = zmstprojectlist.ModifiedBy,
                                    IsLive = zmstprojectlist.IsLive,
                                    Pinitiated = zmstprojectlist.Pinitiated,
                                };

                return cyclelist.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <inheritdoc/>
        public async Task<int> SaveAsync(Abstractions.Models.ZmstProjects zmstProject, CancellationToken cancellationToken)
        {
            int result = 0;
            if (zmstProject == null)
            {
                throw new ArgumentNullException(nameof(zmstProject));
            }
            if (zmstProject != null)
            {
                return await this.CommitZmstProjectsChangesAsync(zmstProject, cancellationToken);
            }

            return result;
        }

        private async Task<int> CommitZmstProjectsChangesAsync(Abstractions.Models.ZmstProjects zmstProject, CancellationToken cancellationToken)
        {
            if (zmstProject == null)
            {
                throw new ArgumentNullException(nameof(zmstProject));
            }
            var agencyVirtualMaqpping = await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.FindByAsync(r => r.AgencyId == (zmstProject.AgencyId).ToString() && r.VirtualDirectoryType == "Admin", default);
            if (agencyVirtualMaqpping != null)
            {
                var absAgencyVirtualMaqpping = this.mapper.Map<Abstractions.Models.ZmstAgencyVirtualDirectoryMapping>(agencyVirtualMaqpping);
                Abstractions.Models.ApplicationSummary absApplicationSummary = new Abstractions.Models.ApplicationSummary();
                absApplicationSummary.AgencyId = zmstProject.AgencyId.ToString();
                absApplicationSummary.AppYear = zmstProject.AcademicYear.ToString();
                absApplicationSummary.AppId = zmstProject.ProjectId.ToString();
                absApplicationSummary.AppTitle = zmstProject.ProjectName;
                absApplicationSummary.AppType = (zmstProject.ServiceType == 1) ? "REGST" : (zmstProject.ServiceType == 1) ? "COUNS" : string.Empty;//To be Changed
                absApplicationSummary.AppUrl = "https://admissions.nic.in/" + agencyVirtualMaqpping.BaseDirectory;
                absApplicationSummary.ApiLink = "https://admissions.nic.in/" + agencyVirtualMaqpping.BaseDirectory + "/" + agencyVirtualMaqpping.VirtualDirectory + "/AppSummary/API";
                absApplicationSummary.AdminUrl = "https://admissions.nic.in/" + agencyVirtualMaqpping.BaseDirectory + "/" + agencyVirtualMaqpping.VirtualDirectory;
                var efApplicationSummary = this.mapper.Map<Data.EF.Models.ApplicationSummary>(absApplicationSummary);
                await this.unitOfWork.ApplicationSummaryRepository.InsertAsync(efApplicationSummary, cancellationToken).ConfigureAwait(false);
            }
            var Insert_ZmstProject = this.mapper.Map<Data.EF.Models.ZmstProjects>(zmstProject);
            var Avail_zmstProject = await this.unitOfWork.ZmstProjectRepository.FindByAsync(r => r.ProjectId == zmstProject.ProjectId, default);
            if (Avail_zmstProject != null)
            {
                return 404;
            }
            await this.unitOfWork.ZmstProjectRepository.InsertAsync(Insert_ZmstProject, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(Abstractions.Models.ZmstProjects zmstProject, CancellationToken cancellationToken)
        {
            var events = unitOfWork.ZmstProjectRepository.FindAllBy(e => e.ProjectId == zmstProject.ProjectId);
            foreach (var evt in events)
            {
                evt.ProjectName = zmstProject.ProjectName;
                evt.IsLive = zmstProject.IsLive;
                evt.Pinitiated = zmstProject.Pinitiated;
                evt.ModifiedBy = zmstProject.ModifiedBy;
                evt.ModifiedDate = (DateTime.Now).ToString();
                await unitOfWork.ZmstProjectRepository.UpdateAsync(evt, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
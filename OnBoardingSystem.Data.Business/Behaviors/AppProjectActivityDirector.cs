
//-----------------------------------------------------------------------
// <copyright file="AppProjectActivityDirector.cs" company="NIC">
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
    using DocumentFormat.OpenXml.Office2010.Excel;
    using OnBoardingSystem.Data.Abstractions.Models;
    using System.Net;
    using DocumentFormat.OpenXml.InkML;
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class AppProjectActivityDirector : IAppProjectActivityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectActivityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppProjectActivityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppProjectActivity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appprojectactivitylist = await this.unitOfWork.AppProjectActivityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppProjectActivity>>(appprojectactivitylist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.AppProjectActivity> GetByIdAsync(AppDocumentFilter getByRefIdNActivity, CancellationToken cancellationToken)
        {
            var appprojectactivitylist = await this.unitOfWork.AppProjectActivityRepository.FindByAsync(x => x.ActivityId.ToString()== getByRefIdNActivity.ActivityId && x.ActivityParentRefId == getByRefIdNActivity.ModuleRefId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppProjectActivity>(appprojectactivitylist);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppProjectActivity>> GetByParentRefIdAsync(String ParentRefId, CancellationToken cancellationToken)
        {
            try
            {
                var projectactivitylist = await this.unitOfWork.AppProjectActivityRepository.FindAllByAsync(x => x.ActivityParentRefId == ParentRefId, cancellationToken).ConfigureAwait(false);
                var activitylist = await this.unitOfWork.MdActivityTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var mdstatuslist = await this.unitOfWork.mdstatusDirectorRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var query = from PA in projectactivitylist
                            join AT in activitylist on PA.ActivityId equals AT.ActivityId into tempappuploaddetails
                            from AT in tempappuploaddetails.DefaultIfEmpty()
                            join MS in mdstatuslist on PA.Status equals MS.StatusId into tempdocUserMapping
                            from MS in tempdocUserMapping.Where(ab=>ab.ActivityId == PA.ActivityId.ToString()) 
                select new Abstractions.Models.AppProjectActivity
                            {   
                                //Id = activitylist.ac,
                                //ActivityParentRefId = activitylist.ActivityParentRefId,
                                //ActivityId = activitylistList.a,
                                ActivityName = AT.Activity,
                                Status = MS.Status,
                                //SubmitTime = activitylist.SubmitTime,
                                //IpAddress = activitylist.IpAddress,
                            };
                var result = this.mapper.Map<List<Abstractions.Models.AppProjectActivity>>(query);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppProjectActivity appprojectactivity, CancellationToken cancellationToken)
        {
            if (appprojectactivity == null)
            {
                throw new ArgumentNullException(nameof(appprojectactivity));
            }

            var chkefappprojectactivity = await this.unitOfWork.AppProjectActivityRepository.FindByAsync(r => r.Id == appprojectactivity.Id, default);
            if (chkefappprojectactivity != null)
            {
                throw new EntityFoundException($"This Records {chkefappprojectactivity} already exists");
            }

            var efappprojectactivity = this.mapper.Map<Data.EF.Models.AppProjectActivity>(appprojectactivity);

            await this.unitOfWork.AppProjectActivityRepository.InsertAsync(efappprojectactivity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.AppProjectActivity appprojectactivity, CancellationToken cancellationToken)

        {
            //if (appprojectactivity.Id == 0)
            //{
            //    throw new ArgumentException(nameof(appprojectactivity.Id));
            //}

            Data.EF.Models.AppProjectActivity entityUpd = await unitOfWork.AppProjectActivityRepository.FindByAsync(e => e.ActivityId == appprojectactivity.ActivityId && e.ActivityParentRefId== appprojectactivity.ActivityParentRefId, cancellationToken);

            if (entityUpd != null)
            {
                entityUpd.Status = appprojectactivity.Status;

                await unitOfWork.AppProjectActivityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            if (Id == 0)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.AppProjectActivityRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.AppProjectActivityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppProjectActivityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

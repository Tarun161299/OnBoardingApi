
//-----------------------------------------------------------------------
// <copyright file="WorkOrderDetailsDirector.cs" company="NIC">
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
    using System.Text.Json;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Common.enums;

    /// <inheritdoc />
    public class WorkOrderDetailsDirector : IWorkOrderDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkOrderDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public WorkOrderDetailsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.WorkOrderDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var workorderdetailslist = await this.unitOfWork.WorkOrderDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.WorkOrderDetails>>(workorderdetailslist.OrderBy(x => x.AgencyName));
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.WorkOrderDetails> GetByIdAsync(int WorkorderId, CancellationToken cancellationToken)
        {
            var workorderdetailslist = await this.unitOfWork.WorkOrderDetailsRepository.FindByAsync(x => x.WorkorderId == WorkorderId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.WorkOrderDetails>(workorderdetailslist);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.WorkOrderDetails>> GetByProjectCodeAsync(string projectCode, CancellationToken cancellationToken)
        {
            var workorderdetailslist = await this.unitOfWork.WorkOrderDetailsRepository.FindAllByAsync(x => x.WorkorderNo == projectCode, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.WorkOrderDetails>>(workorderdetailslist);
            return result;
        }
        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.WorkOrderDetails workorderdetails, CancellationToken cancellationToken)
        {
            try
            {
                var result = 0;
                if (workorderdetails == null)
                {
                    throw new ArgumentNullException(nameof(workorderdetails));
                }

                var chkefworkorderdetails = await this.unitOfWork.WorkOrderDetailsRepository.FindByAsync(r => r.WorkorderId == workorderdetails.WorkorderId, default);
                if (chkefworkorderdetails != null)
                {
                    throw new EntityFoundException($"This Records {chkefworkorderdetails} already exists");
                }

                using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        var efWorkOderDetails = this.mapper.Map<WorkOrderDetails>(workorderdetails);
                        await this.unitOfWork.WorkOrderDetailsRepository.InsertAsync(efWorkOderDetails, default);
                        await this.unitOfWork.WorkOrderDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
                        AbsModels.AppDocumentUploadedDetail appDocumentUploadedDetail = new AbsModels.AppDocumentUploadedDetail();
                        appDocumentUploadedDetail.DocContent = workorderdetails.Document;
                        appDocumentUploadedDetail.ModuleRefId = efWorkOderDetails.WorkorderId.ToString();
                        appDocumentUploadedDetail.Activityid = Enumactivity.WorkOrderDetails.ToString();
                        appDocumentUploadedDetail.DocType = "";
                        var efAppDocumentDetails = this.mapper.Map<AppDocumentUploadedDetail>(appDocumentUploadedDetail);
                        await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(efAppDocumentDetails, default);
                        result = await this.unitOfWork.AppDocumentUploadedDetailRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>	
        public virtual async Task<int> UpdateAsync(AbsModels.WorkOrderDetails workorderdetails, CancellationToken cancellationToken)
        {
            if (workorderdetails.WorkorderId == 0)
            {
                throw new ArgumentException(nameof(workorderdetails.WorkorderId));
            }
            Data.EF.Models.WorkOrderDetails entityUpd = await unitOfWork.WorkOrderDetailsRepository.FindByAsync(e => e.WorkorderId == workorderdetails.WorkorderId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.WorkorderId = workorderdetails.WorkorderId;
                entityUpd.WorkorderNo = workorderdetails.WorkorderNo;
                entityUpd.ProjectCode = workorderdetails.ProjectCode;
                entityUpd.IssueDate = workorderdetails.IssueDate;
                entityUpd.AgencyName = workorderdetails.AgencyName;
                entityUpd.ResourceCategory = workorderdetails.ResourceCategory;
                entityUpd.ResourceNo = workorderdetails.ResourceNo;
                entityUpd.NoofMonths = workorderdetails.NoofMonths;
                entityUpd.WorkorderFrom = workorderdetails.WorkorderFrom;
                entityUpd.WorkorderTo = workorderdetails.WorkorderTo;
                entityUpd.DocName = workorderdetails.DocName;
                //entityUpd.Document= workorderdetails.Document;
                await unitOfWork.WorkOrderDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
                Data.EF.Models.AppDocumentUploadedDetail DocUpd = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.ModuleRefId == (workorderdetails.WorkorderId).ToString() && e.Activityid == Enumactivity.WorkOrderDetails.ToString(), cancellationToken);
                if (DocUpd != null)
                {
                    DocUpd.DocContent = workorderdetails.Document;
                }
                await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(DocUpd, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int WorkorderId, CancellationToken cancellationToken)
        {
            if (WorkorderId == 0)
            {
                throw new ArgumentNullException(nameof(WorkorderId));
            }

            var entity = await this.unitOfWork.WorkOrderDetailsRepository.FindByAsync(x => x.WorkorderId == WorkorderId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an WorkorderId {WorkorderId} was not found.");
            }

            await this.unitOfWork.WorkOrderDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.WorkOrderDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

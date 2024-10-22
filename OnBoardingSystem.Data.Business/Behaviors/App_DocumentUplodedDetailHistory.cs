//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailHistoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using Abp.Extensions;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Abs = Abstractions.Models;
    public class App_DocumentUplodedDetailHistoryDirector:IAppDocumentUploadedDetailHistoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly EncryptionDecryptionService decryptionService;
        private readonly UtilityService utilityService;
        private readonly SMSService sMSService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public App_DocumentUplodedDetailHistoryDirector(IMapper mapper, SMSService _sMSService, IUnitOfWork unitOfWork, EncryptionDecryptionService _encryptionDecryptionService, UtilityService _utilityService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.decryptionService = _encryptionDecryptionService;
            this.sMSService = _sMSService;
            this.utilityService = _utilityService;
        }

        public virtual async Task<bool> Save(Abs.AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken)
        {
            //var projectActivity = this.mapper.Map<List<Data.EF.Models.AppProjectActivity>>(activityList);
            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    var EFdocidOfappDocUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == appDocumentUploadedDetail.ModuleRefId && x.DocType == appDocumentUploadedDetail.DocType && x.Activityid == appDocumentUploadedDetail.Activityid, cancellationToken).ConfigureAwait(false);
                    var appDocUploadedDetails = this.mapper.Map<Abs.AppDocumentUploadedDetailHistoty>(appDocumentUploadedDetail);
                    var appDocUploadedDetailsHistory=this.mapper.Map<EF.Models.AppDocumentUploadedDetailHistoty>(EFdocidOfappDocUploadedDetails);
                    
                    var AbsdocidOfappDocUploadedDetails= this.mapper.Map< OnBoardingSystem.Data.Abstractions.Models.AppDocumentUploadedDetail >(EFdocidOfappDocUploadedDetails);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.DeleteAsync(x => x.DocumentId == AbsdocidOfappDocUploadedDetails.DocumentId, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.AppDocumentUploadedDetailHistotyRepository.InsertAsync(appDocUploadedDetailsHistory, cancellationToken).ConfigureAwait(false);
                    var effectedRows = await unitOfWork.CommitAsync(cancellationToken);
                    transaction.Commit();
                    if (effectedRows > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}


//-----------------------------------------------------------------------
// <copyright file="QualificationDetailsDirector.cs" company="NIC">
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
    using OnBoardingSystem.Common.enums;

    /// <inheritdoc />
    public class QualificationDetailsDirector : IQualificationDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="QualificationDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public QualificationDetailsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.QualificationDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var QualificationDetailslist = await this.unitOfWork.QualificationDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var empdetailslist = await this.unitOfWork.EmployeeDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var qualificationlist = from Quallist in QualificationDetailslist
                                    join empdetailslistObj in empdetailslist on Quallist.EmpCode equals empdetailslistObj.EmpCode
                                    select new Abstractions.Models.QualificationDetails
                                    {
                                        QualificationDetailsId = Quallist.QualificationDetailsId,
                                        EmpCode = Quallist.EmpCode,
                                        ExamPassed = Quallist.ExamPassed,
                                        BoardUniv = Quallist.BoardUniv,
                                        PassYear = Quallist.PassYear,
                                        Division = Quallist.Division,
                                        Documents = "",
                                        EmpName = empdetailslistObj.EmpName
                                    };
            return this.mapper.Map<List<AbsModels.QualificationDetails>>(qualificationlist);
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.QualificationDetails>> GetByIdAsync(string examCode, CancellationToken cancellationToken)
        {
            var QualificationDetailslist = await this.unitOfWork.QualificationDetailsRepository.FindAllByAsync(x => x.EmpCode == examCode, cancellationToken).ConfigureAwait(false);
            var Examlist = await this.unitOfWork.MdExamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            var qualificationlist = from Quallist in QualificationDetailslist
                                    join exmlist in Examlist on Quallist.ExamPassed equals exmlist.Id
                                    select new Abstractions.Models.QualificationDetails
                                    {
                                        QualificationDetailsId = Quallist.QualificationDetailsId,
                                        EmpCode = Quallist.EmpCode,
                                        ExamPassed = Quallist.ExamPassed,
                                        BoardUniv = Quallist.BoardUniv,
                                        PassYear = Quallist.PassYear,
                                        Division = Quallist.Division,
                                        Documents = "",
                                        ExamName = exmlist.Exam
                                    };

            var result = this.mapper.Map<List<Abstractions.Models.QualificationDetails>>(qualificationlist);
            return result.ToList();
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.QualificationDetails QualificationDetails, CancellationToken cancellationToken)
        {
            int result = 0;
            if (QualificationDetails == null)
            {
                throw new ArgumentNullException(nameof(QualificationDetails));
            }

            var chkefQualificationDetails = await this.unitOfWork.QualificationDetailsRepository.FindByAsync(r => r.EmpCode == QualificationDetails.EmpCode && r.ExamPassed == QualificationDetails.ExamPassed, default);
            if (chkefQualificationDetails != null)
            {
                throw new EntityFoundException($"This Records {chkefQualificationDetails} already exists");
            }

            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    var efQualificationDetails = this.mapper.Map<Data.EF.Models.QualificationDetails>(QualificationDetails);
                    await this.unitOfWork.QualificationDetailsRepository.InsertAsync(efQualificationDetails, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                    AppDocumentUploadedDetail appDocs = new AppDocumentUploadedDetail();
                    appDocs.Activityid = ((int)Enumactivity.EmployeeQualificationDetails).ToString();
                    appDocs.ModuleRefId = efQualificationDetails.QualificationDetailsId.ToString();
                    appDocs.DocContent = QualificationDetails.Documents;
                    appDocs.DocType = "";

                    var AppDocuments = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(appDocs);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(AppDocuments, cancellationToken).ConfigureAwait(false);
                    result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
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

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.QualificationDetails QualificationDetails, CancellationToken cancellationToken)

        {
            if (QualificationDetails.QualificationDetailsId == 0)
            {
                throw new ArgumentException(nameof(QualificationDetails.QualificationDetailsId));
            }

            Data.EF.Models.QualificationDetails entityUpd = await unitOfWork.QualificationDetailsRepository.FindByAsync(e => e.QualificationDetailsId == QualificationDetails.QualificationDetailsId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.QualificationDetailsId = QualificationDetails.QualificationDetailsId;
                entityUpd.EmpCode = QualificationDetails.EmpCode;
                entityUpd.ExamPassed = QualificationDetails.ExamPassed;
                entityUpd.BoardUniv = QualificationDetails.BoardUniv;
                entityUpd.PassYear = QualificationDetails.PassYear;
                entityUpd.Division = QualificationDetails.Division;
                await unitOfWork.QualificationDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }
            if (QualificationDetails.Documents != "")
            {
                Data.EF.Models.AppDocumentUploadedDetail appDoc = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.Activityid == Enumactivity.EmployeeQualificationDetails.ToString() && e.ModuleRefId == QualificationDetails.QualificationDetailsId.ToString(), cancellationToken);
                if (appDoc != null)
                {
                    appDoc.DocContent = QualificationDetails.Documents;
                    await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(appDoc, cancellationToken).ConfigureAwait(false);
                }
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int QualificationDetailsId, CancellationToken cancellationToken)
        {
            if (QualificationDetailsId == 0)
            {
                throw new ArgumentNullException(nameof(QualificationDetailsId));
            }

            var entity = await this.unitOfWork.QualificationDetailsRepository.FindByAsync(x => x.QualificationDetailsId == QualificationDetailsId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualificationDetailsId {QualificationDetailsId} was not found.");
            }

            await this.unitOfWork.QualificationDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.QualificationDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

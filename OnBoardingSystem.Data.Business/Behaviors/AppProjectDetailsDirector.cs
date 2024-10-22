//-----------------------------------------------------------------------
// <copyright file="ProjectCreationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.Diagnostics;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using EFModel = OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using OnBoardingSystem.Common.enums;
    using System;
    using DocumentFormat.OpenXml.Bibliography;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.Net;
    using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

    /// <inheritdoc />
    public class AppProjectDetailsDirector : IAppProjectDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Enum _activityenum;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>

        public AppProjectDetailsDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.utilityService = _utilityService;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>

        /// <inheritdoc />
        public virtual async Task<List<AppProjectDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var projectlist = await this.unitOfWork.AppProjectDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var requestList = await this.unitOfWork.AppOnboardingRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var YearList = await this.unitOfWork.MdYearRepository.FindAllByAsync(x=>x.YearGroup== "PROJECTYEAR", cancellationToken).ConfigureAwait(false);
                var result = from project in projectlist
                             join request in requestList on project.RequestNo equals request.RequestNo
                             into temp
                             from request in temp.DefaultIfEmpty()//into temp
                             join Years in YearList on project.ProjectYear.ToString() equals Years.YearId 
                             
                             select new AppProjectDetails
                             {
                                 Id = project.Id,
                                 RequestNo = project.RequestNo,
                                 RequestLetterNo = project.RequestLetterNo,
                                 RequestLetterDate = project.RequestLetterDate,
                                 IsWorkOrderRequired = project.IsWorkOrderRequired,
                                 ProjectCode = project.ProjectCode,
                                 ProjectName = project.ProjectName,
                                 ProjectYear = int.Parse(Years.Description),
                                 AgencyId = project.AgencyId,
                                 AgencyName = project.AgencyName,
                                 EfileNo = project.EfileNo,
                                 PrizmId = project.PrizmId,
                                 Status = project.Status,
                                 Remarks = project.Remarks,
                                 NicsiprojectCode = project.ProjectCode,
                                 Nicsipino = project.Nicsipino,
                                 Pidate = project.Pidate,
                                 Piamount = project.Piamount,
                                 SubmitTime = project.SubmitTime,
                                 Ipaddress = project.Ipaddress,
                                 SubmitBy = project.SubmitBy,
                                 ModifyBy = project.ModifyBy,
                                 ModifyOn = project.ModifyOn,
                                 IsActive = project.IsActive,
                                 IsRequestAvailable = (request == null) ? "N" : "Y"
                             };
                //return this.mapper.Map<List<AppProjectDetails>>(projectlist.OrderByDescending(rs => rs.SubmitTime).ToList());
                return result.OrderByDescending(rs => rs.SubmitTime).ToList();
            }
            catch (Exception ex)
            {
                return new List<AppProjectDetails>();
            }
        }

        /// <inheritdoc />
        public virtual async Task<int> Save(AppProjectDetails appProjectDetails, CancellationToken cancellationToken)
        {
            try
            {
                var param = new SqlParameter[]
                    {
                new SqlParameter()
                {
                    ParameterName = "@RequestNo",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.RequestNo,
                },
                new SqlParameter()
                {
                    ParameterName = "@RequestLetterNo",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.RequestLetterNo,
                },
                new SqlParameter()
                {
                    ParameterName = "@DateOfrqstletter",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = appProjectDetails.RequestLetterDate,
                },
                new SqlParameter()
                {
                    ParameterName = "@Agency",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appProjectDetails.AgencyId,
                },
                new SqlParameter()
                {
                    ParameterName = "@ProjectYear",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.ProjectYear,
                },

                new SqlParameter()
                {
                    ParameterName = "@ProjectName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.ProjectName,
                },
                new SqlParameter()
                {
                    ParameterName = "@AgencyName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.ProjectName,
                },
                new SqlParameter()
                {
                    ParameterName = "@IPAddress",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value =  _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                },
                new SqlParameter()
                {
                    ParameterName = "@SubmitBy",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appProjectDetails.SubmitBy,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsWorkOrderReq",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Value = appProjectDetails.IsWorkOrderRequired,
                },
                    };
                var storedProcedureName = $"{"USP_InsertProjectDetail"}  @RequestNo,@RequestLetterNo,@DateOfrqstletter,@ProjectName,@ProjectYear,@Agency,@AgencyName,@IPAddress,@SubmitBy,@IsWorkOrderReq";
                int result = await this.unitOfWork.AppProjectDetailsRepository.ExecuteSqlRawAsync(storedProcedureName, param, cancellationToken).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<List<AppProjectDetails>> GetById(string id, CancellationToken cancellationToken)
        {
            var ProjectDetailsList = await this.unitOfWork.AppProjectDetailsRepository.FindAllByAsync(x => x.RequestNo == id, cancellationToken).ConfigureAwait(false);
            var ProjectYearList = await this.unitOfWork.MdYearRepository.FindAllByAsync(x => x.YearGroup == "PROJECTYEAR", cancellationToken).ConfigureAwait(false);

            var appProjectDetailsList = from ProjectDetails in ProjectDetailsList
                                        join ProjectYears in ProjectYearList on ProjectDetails.ProjectYear.ToString() equals ProjectYears.YearId
                                        select new Abstractions.Models.AppProjectDetails
                                        {
                                            Id= ProjectDetails.Id,
                                            RequestNo= ProjectDetails.RequestNo,
                                            RequestLetterNo= ProjectDetails.RequestLetterNo,
                                            RequestLetterDate= ProjectDetails.RequestLetterDate,
                                            IsWorkOrderRequired= ProjectDetails.IsWorkOrderRequired,
                                            ProjectCode= ProjectDetails.ProjectCode,
                                            ProjectName= ProjectDetails.ProjectName,    
                                            ProjectYear= int.Parse(ProjectYears.Description),
                                            AgencyId= ProjectDetails.AgencyId,
                                            AgencyName= ProjectDetails.AgencyName,
                                            EfileNo= ProjectDetails.EfileNo,
                                            PrizmId= ProjectDetails.PrizmId,
                                            Status= ProjectDetails.Status,
                                            Remarks= ProjectDetails.Remarks,
                                            NicsiprojectCode= ProjectDetails.ProjectCode,
                                            Nicsipino= ProjectDetails.Nicsipino,
                                            Pidate= ProjectDetails.Pidate,
                                            Piamount= ProjectDetails.Piamount,
                                            SubmitTime= ProjectDetails.SubmitTime,
                                            Ipaddress= ProjectDetails.Ipaddress,
                                            SubmitBy= ProjectDetails.SubmitBy,
                                            ModifyBy= ProjectDetails.ModifyBy,
                                            ModifyOn= ProjectDetails.ModifyOn,
                                            IsActive= ProjectDetails.IsActive,
                                            //IsRequestAvailable= ProjectDetails.is

                                        };
            var result = this.mapper.Map<List<Abstractions.Models.AppProjectDetails>>(appProjectDetailsList);
            return result.ToList();
        }

        public virtual async Task<List<CounsellingDocs>> GetByRequestNoAsync(string Requestno, CancellationToken cancellationToken)
        {
            var appProjectDetailsList = await this.unitOfWork.AppProjectDetailsRepository.FindAllByAsync(x => x.RequestNo == Requestno, cancellationToken).ConfigureAwait(false);
            // var ProjectDetailsListData = await this.unitOfWork.ZmstProjectRepository.FindAllByAsync(x => x.AgencyId == appProjectDetailsList.AgencyId, cancellationToken).ConfigureAwait(false);
            var prjectdocData = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(x => x.Activityid == "601" && x.ModuleRefId == Requestno, cancellationToken).ConfigureAwait(false);
            var mdActivity = await this.unitOfWork.MdActivityTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var doctype = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var docList = from ProjectDetailsList in appProjectDetailsList
                          join prjectdoclist in prjectdocData on ProjectDetailsList.RequestNo.ToString() equals prjectdoclist.ModuleRefId
                          join activity in mdActivity on prjectdoclist.Activityid equals activity.ActivityId.ToString()
                          join doc in doctype on prjectdoclist.DocType equals doc.Id
                          select new Abstractions.Models.CounsellingDocs
                          {
                              Documentid = prjectdoclist.DocumentId.ToString(),
                              Docname = doc.Title,
                              statusId = "",
                              Status = "Completed",
                              Activity = activity.Activity,
                              ActivityId = activity.ActivityId.ToString(),
                              projectname = ProjectDetailsList.ProjectName,
                              projectId = ProjectDetailsList.Id.ToString()
                          };

            //var result = docList;
            return docList.ToList();
        }

        public virtual async Task<AppProjectDetails> GetByProjectId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var appProjectDetails = await this.unitOfWork.AppProjectDetailsRepository.FindAllByAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
                var mdyear = await this.unitOfWork.MdYearRepository.FindAllByAsync(x => x.YearGroup == "PROJECTYEAR", cancellationToken).ConfigureAwait(false);
                //var result = this.mapper.Map<Abstractions.Models.AppProjectDetails>(appProjectDetailsList);
                var result = from project in appProjectDetails
                             join year in mdyear on project.ProjectYear.ToString() equals year.YearId
                             select new AppProjectDetails
                             {
                                 Id = project.Id,
                                 RequestNo = project.RequestNo,
                                 RequestLetterNo = project.RequestLetterNo,
                                 RequestLetterDate = project.RequestLetterDate,
                                 IsWorkOrderRequired = project.IsWorkOrderRequired,
                                 ProjectCode = project.ProjectCode,
                                 ProjectName = project.ProjectName,
                                 ProjectYear = int.Parse(year.Description),
                                 AgencyId = project.AgencyId,
                                 AgencyName = project.AgencyName,
                                 EfileNo = project.EfileNo,
                                 PrizmId = project.PrizmId,
                                 Status = project.Status,
                                 Remarks = project.Remarks,
                                 NicsiprojectCode = project.ProjectCode,
                                 Nicsipino = project.Nicsipino,
                                 Pidate = project.Pidate,
                                 Piamount = project.Piamount,
                                 SubmitTime = project.SubmitTime,
                                 Ipaddress = project.Ipaddress,
                                 SubmitBy = project.SubmitBy,
                                 ModifyBy = project.ModifyBy,
                                 ModifyOn = project.ModifyOn,
                                 IsActive = project.IsActive 
                             };
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc />
        public virtual async Task<int> SaveProjectDetails(AppProjectDetails appProjectDetails, CancellationToken cancellationToken)
        {
            if (appProjectDetails.RequestNo == null)
            {
                throw new ArgumentException(nameof(appProjectDetails.RequestNo));
            }
            EFModel.AppProjectDetails data = await unitOfWork.AppProjectDetailsRepository.FindByAsync(e => e.RequestNo == appProjectDetails.RequestNo, cancellationToken);

            data.Status = appProjectDetails.Status;
            data.Remarks = appProjectDetails.Remarks;
            data.PrizmId = appProjectDetails.PrizmId;
            data.EfileNo = appProjectDetails.EfileNo;
            data.ModifyBy = appProjectDetails.ModifyBy;
            data.ModifyOn = DateTime.Now;
            await unitOfWork.AppProjectDetailsRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);
            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<int> SavePIDetails(PIDetails piDetails, CancellationToken cancellationToken)
        {
            //int result = 0;
            try
            {
                if (piDetails.RequestNo == null)
                {
                    throw new ArgumentException(nameof(piDetails.RequestNo));
                }

                EFModel.AppProjectDetails data = await unitOfWork.AppProjectDetailsRepository.FindByAsync(e => e.RequestNo == piDetails.RequestNo, cancellationToken);

                data.ProjectCode = piDetails.ProjectCode;
                data.Nicsipino = piDetails.Nicsipino;
                data.Pidate = piDetails.Pidate;
                data.Piamount = piDetails.Piamount;
                data.ModifyBy = piDetails.ModifyBy;
                data.ModifyOn = DateTime.Now;

                //await unitOfWork.CommitAsync(cancellationToken);
                var saveDocument = new OnBoardingSystem.Data.Abstractions.Models.AppDocumentUploadedDetail();
                List<AppDocumentUploadedDetail> doc1 = new List<AppDocumentUploadedDetail>();
                AppDocumentUploadedDetail docCover;
                AppDocumentUploadedDetail proposal = new AppDocumentUploadedDetail();
                AppDocumentUploadedDetail Pi = new AppDocumentUploadedDetail();
                string str = "";
                if (piDetails.FileContent != null)
                {
                    str = str + "," + piDetails.DocType;
                }
                if (piDetails.fileContentProposal != null)
                {
                    str = str + "," + piDetails.docTypeProposal;
                }
                if (piDetails.fileContentCover != null)
                {
                    str = str + "," + piDetails.docTypeCover;
                }

                str = str.Substring(1, str.Length - 1);
                string[] strAtt = str.Split(',');
                var EFdocidOfappDocUploadedDetails = await unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(e => e.ModuleRefId == piDetails.RequestNo
                && e.Activityid == ((int)Enumactivity.ProposalAndPI).ToString()
                && (strAtt.Contains(e.DocType)), cancellationToken);

                if (piDetails.FileContent != null)
                {
                    var temp = EFdocidOfappDocUploadedDetails.Where(e => e.DocType == piDetails.DocType).FirstOrDefault();

                    docCover = new AppDocumentUploadedDetail();
                    docCover.DocType = piDetails.DocType;
                    docCover.DocContent = piDetails.FileContent;
                    docCover.Activityid = ((int)Enumactivity.ProposalAndPI).ToString();
                    docCover.SubTime = DateTime.Now;
                    docCover.ModuleRefId = piDetails.RequestNo;
                    docCover.CreatedBy = piDetails.ModifyBy;
                    docCover.VersionNo = temp != null ? temp.VersionNo + 1 : 1;
                    doc1.Add(docCover);
                }
                if (piDetails.fileContentProposal != null)
                {
                    var temp = EFdocidOfappDocUploadedDetails.Where(e => e.DocType == piDetails.docTypeProposal).FirstOrDefault();

                    docCover = new AppDocumentUploadedDetail();
                    docCover.DocType = piDetails.docTypeProposal;
                    docCover.DocContent = piDetails.fileContentProposal;
                    docCover.Activityid = ((int)Enumactivity.ProposalAndPI).ToString();
                    docCover.SubTime = DateTime.Now;
                    docCover.ModuleRefId = piDetails.RequestNo;
                    docCover.CreatedBy = piDetails.ModifyBy;
                    docCover.VersionNo = temp != null ? temp.VersionNo + 1 : 1;
                    doc1.Add(docCover);
                }
                if (piDetails.fileContentCover != null)
                {
                    var temp = EFdocidOfappDocUploadedDetails.Where(e => e.DocType == piDetails.docTypeCover).FirstOrDefault();

                    docCover = new AppDocumentUploadedDetail();
                    docCover.DocType = piDetails.docTypeCover;
                    docCover.DocContent = piDetails.fileContentCover;
                    docCover.Activityid = ((int)Enumactivity.ProposalAndPI).ToString();
                    docCover.SubTime = DateTime.Now;
                    docCover.ModuleRefId = piDetails.RequestNo;
                    docCover.CreatedBy = piDetails.ModifyBy;
                    docCover.VersionNo = temp != null ? temp.VersionNo + 1 : 1;
                    doc1.Add(docCover);
                }

                var SaveDocument = this.mapper.Map<List<EFModel.AppDocumentUploadedDetail>>(doc1);

                using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (EFdocidOfappDocUploadedDetails.Count() != 0)
                        {
                            var appDocUploadedDetailsHistory = this.mapper.Map<List<EF.Models.AppDocumentUploadedDetailHistoty>>(EFdocidOfappDocUploadedDetails);
                            await this.unitOfWork.AppDocumentUploadedDetailHistotyRepository.InsertAsync(appDocUploadedDetailsHistory, cancellationToken).ConfigureAwait(false);
                        }

                        if (strAtt.Length > 0)
                        {
                            await unitOfWork.AppDocumentUploadedDetailRepository.DeleteAsync(e => e.ModuleRefId == piDetails.RequestNo && e.Activityid == ((int)Enumactivity.ProposalAndPI).ToString() && (strAtt.Contains(e.DocType)), cancellationToken);
                        }

                        await unitOfWork.AppProjectDetailsRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);
                        await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(SaveDocument, cancellationToken).ConfigureAwait(false);
                        var pIandProjectProposal = await this.unitOfWork.AppProjectActivityRepository.FindByAsync(x => x.ActivityParentRefId == piDetails.RequestNo && x.ActivityId == (int)Enumactivity.ProposalAndPI, cancellationToken).ConfigureAwait(false);
                        if (pIandProjectProposal != null)
                        {
                            pIandProjectProposal.Status = MdStatusEnum.Completed.Value.ToString();
                            await this.unitOfWork.AppProjectActivityRepository.UpdateAsync(pIandProjectProposal, cancellationToken).ConfigureAwait(false);
                        }

                        var result = await unitOfWork.CommitAsync(cancellationToken);
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

        public virtual async Task<int> Update(AppProjectDetails appProjectDetails, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.AppProjectDetailsRepository.FindByAsync(x => x.RequestNo == appProjectDetails.RequestNo, cancellationToken).ConfigureAwait(false);
            if (entity != null)
            {
                entity.RequestLetterDate = appProjectDetails.RequestLetterDate;
                entity.RequestLetterNo = appProjectDetails.RequestLetterNo;
                entity.ProjectName = appProjectDetails.ProjectName;
                entity.ProjectYear = appProjectDetails.ProjectYear;
                entity.AgencyId = appProjectDetails.AgencyId;
                entity.AgencyName = appProjectDetails.AgencyName;
                entity.IsWorkOrderRequired = appProjectDetails.IsWorkOrderRequired;

            }
            await this.unitOfWork.AppProjectDetailsRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            return await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
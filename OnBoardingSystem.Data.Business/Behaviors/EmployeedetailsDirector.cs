
//-----------------------------------------------------------------------
// <copyright file="EmployeeDetailsDirector.cs" company="NIC">
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
    using Azure.Core;
    using Microsoft.AspNetCore.Http;
    using System.Reflection.Emit;
    using DocumentFormat.OpenXml.InkML;
    using System.Data.SqlClient;
    using Newtonsoft.Json;
    using OnBoardingSystem.Common.enums;

    /// <inheritdoc />
    public class EmployeeDetailsDirector : IEmployeeDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public EmployeeDetailsDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.EmployeeDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                EmployeeDetails empDetail = new EmployeeDetails();
                var empdetailslist = await this.unitOfWork.EmployeeDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var empworkodrlist = await this.unitOfWork.EmployeeWorkOrderRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var workodrdetailslist = await this.unitOfWork.WorkOrderDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var query = from employee in empdetailslist
                            join empWorkOrder in empworkodrlist on employee.EmpCode equals empWorkOrder.EmpCode into empWorkOrders
                            from empWorkOrder in empWorkOrders.DefaultIfEmpty()
                            join workOrder in workodrdetailslist on empWorkOrder?.WorkorderNo equals workOrder.WorkorderNo into workOrders
                            from workOrder in workOrders.DefaultIfEmpty()
                            group new { employee, workOrder } by new
                            {
                                employee.EmpId,
                                employee.EmpCode,
                                employee.EmpName,
                                employee.Designation,
                                employee.FName,
                                employee.Division,
                                employee.Dob,
                                employee.MobileNumber,
                                employee.JoinDate,
                                employee.EmailId,
                                employee.EmpStatus
                            } into grouped
                            select new EmployeeDetails
                            {
                                EmpId = grouped.Key.EmpId,
                                EmpCode = grouped.Key.EmpCode,
                                EmpName = grouped.Key.EmpName,
                                Division = grouped.Key.Division,
                                Dob = grouped.Key.Dob,
                                MobileNumber = grouped.Key.MobileNumber,
                                JoinDate = grouped.Key.JoinDate,
                                EmailId = grouped.Key.EmailId,
                                Designation = grouped.Key.Designation,
                                FName = grouped.Key.FName,
                                EmpStatus = grouped.Key.EmpStatus,
                                workorderTo = grouped.Max(x => x.workOrder?.WorkorderTo)
                            };
                return this.mapper.Map<List<AbsModels.EmployeeDetails>>(query).OrderByDescending(x=>x.workorderTo).ToList();
            }   
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.EmployeeDetails> GetByIdAsync(int EmpId, CancellationToken cancellationToken)
        {
            var employeedetailslist = await this.unitOfWork.EmployeeDetailsRepository.FindByAsync(x => x.EmpId == EmpId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.EmployeeDetails>(employeedetailslist);
            return result;
        }

        public virtual async Task<List<AbsModels.EmployeeDetails>> GetAllEmpDetailsAsync(CancellationToken cancellationToken)
        {
            var employeedetailslist = await this.unitOfWork.EmployeeDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.EmployeeDetails>>(employeedetailslist).OrderBy(x => x.EmpName).ToList();
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.EmployeeDetails> GetByEmployeeCodeAsync(string EmpCode, CancellationToken cancellationToken)
        {
            try
            {
                var employeedetailslist = await this.unitOfWork.EmployeeDetailsRepository.FindByAsync(x => x.EmpCode == EmpCode, cancellationToken).ConfigureAwait(false);
                var appDocumentUploadedDetail = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(x => x.ModuleRefId == EmpCode && x.Activityid == ((int)Enumactivity.EmployeeDetails).ToString() && (x.DocType ==((int)EnumDocType.Photo).ToString() || x.DocType == ((int)EnumDocType.AddressProof).ToString()|| x.DocType == ((int)EnumDocType.IdProof).ToString()), cancellationToken).ConfigureAwait(false);
                EmployeeDetails empdetails = new EmployeeDetails();
                var employedetail = this.mapper.Map<Abstractions.Models.EmployeeDetails>(employeedetailslist);
                var appdoclist = this.mapper.Map<List<Abstractions.Models.AppDocumentUploadedDetail>>(appDocumentUploadedDetail);
                if (appdoclist.Count > 0)
                {
                    employedetail.UploadFile = (appdoclist.Find(x => x.DocType == ((int)EnumDocType.Photo).ToString()) == null) ? "" : appdoclist.Find(x => x.DocType == ((int)EnumDocType.Photo).ToString()).DocContent;
                    employedetail.UploadaddressProof = (appdoclist.Find(x => x.DocType == ((int)EnumDocType.AddressProof).ToString()) == null) ? "" : appdoclist.Find(x => x.DocType == ((int)EnumDocType.AddressProof).ToString()).DocContent;
                    employedetail.UploadIdDocument = (appdoclist.Find(x => x.DocType == ((int)EnumDocType.IdProof).ToString()) == null) ? "" : appdoclist.Find(x => x.DocType == ((int)EnumDocType.IdProof).ToString()).DocContent;

                }
                return employedetail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<string> InsertAsync(AbsModels.EmployeeDetails employeedetails, CancellationToken cancellationToken)
        {
            var result = 0;
            if (employeedetails == null)
            {
                throw new ArgumentNullException(nameof(employeedetails));
            }

            var chkefemployeedetails = await this.unitOfWork.EmployeeDetailsRepository.FindByAsync(r => r.EmpId == employeedetails.EmpId, default);
            if (chkefemployeedetails != null)
            {
                throw new EntityFoundException($"This Records {chkefemployeedetails} already exists");
            }

            employeedetails.SubmitTime = DateTime.Now;
            employeedetails.Ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var efemployeedetails = this.mapper.Map<Data.EF.Models.EmployeeDetails>(employeedetails);
            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    await this.unitOfWork.EmployeeDetailsRepository.InsertAsync(efemployeedetails, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                    string Str = "10000" + efemployeedetails.EmpId.ToString();
                    string StrF = DateTime.Now.Year.ToString() + Str.Substring(Str.Length - 6, 6);
                    efemployeedetails.EmpCode = StrF;
                    await this.unitOfWork.EmployeeDetailsRepository.UpdateAsync(efemployeedetails, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                    AppDocumentUploadedDetail tempAppDocumentUploadedDetailPhoto = new AppDocumentUploadedDetail();
                    AppDocumentUploadedDetail tempAppDocumentUploadedDetailAddressProof = new AppDocumentUploadedDetail();
                    AppDocumentUploadedDetail tempAppDocumentUploadedDetailIdProof = new AppDocumentUploadedDetail();
                    tempAppDocumentUploadedDetailPhoto.ModuleRefId = efemployeedetails.EmpCode;
                    tempAppDocumentUploadedDetailPhoto.Activityid = ((int)Enumactivity.EmployeeDetails).ToString();
                    tempAppDocumentUploadedDetailPhoto.DocType = ((int)EnumDocType.Photo).ToString();
                    tempAppDocumentUploadedDetailPhoto.DocContent = employeedetails.UploadFile;
                    tempAppDocumentUploadedDetailPhoto.SubTime = DateTime.Now;
                    tempAppDocumentUploadedDetailPhoto.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    var appDocumentUploadedDetailPhoto = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(tempAppDocumentUploadedDetailPhoto);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(appDocumentUploadedDetailPhoto, cancellationToken).ConfigureAwait(false);

                    tempAppDocumentUploadedDetailAddressProof.ModuleRefId = efemployeedetails.EmpCode;
                    tempAppDocumentUploadedDetailAddressProof.Activityid = ((int)Enumactivity.EmployeeDetails).ToString();
                    tempAppDocumentUploadedDetailAddressProof.DocType = ((int)EnumDocType.AddressProof).ToString();
                    tempAppDocumentUploadedDetailAddressProof.DocContent = employeedetails.UploadaddressProof;
                    tempAppDocumentUploadedDetailAddressProof.SubTime = DateTime.Now;
                    tempAppDocumentUploadedDetailAddressProof.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    var appDocumentUploadedDetailAddressProof = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(tempAppDocumentUploadedDetailAddressProof);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(appDocumentUploadedDetailAddressProof, cancellationToken).ConfigureAwait(false);

                    tempAppDocumentUploadedDetailIdProof.ModuleRefId = efemployeedetails.EmpCode;
                    tempAppDocumentUploadedDetailIdProof.Activityid = ((int)Enumactivity.EmployeeDetails).ToString();
                    tempAppDocumentUploadedDetailIdProof.DocType = ((int)EnumDocType.IdProof).ToString();
                    tempAppDocumentUploadedDetailIdProof.DocContent = employeedetails.UploadIdDocument;
                    tempAppDocumentUploadedDetailIdProof.SubTime = DateTime.Now;
                    tempAppDocumentUploadedDetailIdProof.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    var appDocumentUploadedDetailIdProof = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(tempAppDocumentUploadedDetailIdProof);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(appDocumentUploadedDetailIdProof, cancellationToken).ConfigureAwait(false);
                    result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                return efemployeedetails.EmpCode;
            }
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.EmployeeDetails employeedetails, CancellationToken cancellationToken)
        {
            int employeeDetailsresults = 0;
            int appDocumentUploadedDetailresults = 0;
            if (employeedetails.EmpCode == "")
            {
                throw new ArgumentException(nameof(employeedetails.EmpCode));
            }

            Data.EF.Models.EmployeeDetails entityUpd = await unitOfWork.EmployeeDetailsRepository.FindByAsync(e => e.EmpCode == employeedetails.EmpCode, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.EmpName = employeedetails.EmpName;
                entityUpd.Designation = employeedetails.Designation;
                entityUpd.FName = employeedetails.FName;
                entityUpd.MName = employeedetails.MName;
                entityUpd.Dob = employeedetails.Dob;
                entityUpd.Address = employeedetails.Address;
                entityUpd.PhoneNumber = employeedetails.PhoneNumber;
                entityUpd.MobileNumber = employeedetails.MobileNumber;
                entityUpd.EmailId = employeedetails.EmailId;
                entityUpd.Id = employeedetails.Id;
                entityUpd.IdNumber = employeedetails.IdNumber;
                entityUpd.JoinDate = employeedetails.JoinDate;
                entityUpd.ReportingOfficer = employeedetails.ReportingOfficer;
                entityUpd.Remarks = employeedetails.Remarks;
                entityUpd.EmpStatus = employeedetails.EmpStatus;
                entityUpd.ResignDate = employeedetails.ResignDate;
                entityUpd.Division = employeedetails.Division;
                entityUpd.SubmitTime = DateTime.Now;
                entityUpd.Ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }

            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {

                try
                {
                    await unitOfWork.EmployeeDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
                    employeeDetailsresults = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                    if (employeedetails.UploadaddressProof != null && employeedetails.UploadaddressProof != "")
                    {
                        Data.EF.Models.AppDocumentUploadedDetail uploadAddressProof = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.DocType == ((int)EnumDocType.AddressProof).ToString() && e.ModuleRefId == employeedetails.EmpCode, cancellationToken);
                        if (uploadAddressProof == null)
                        {
                            var insertUploadAddressProof = new Data.EF.Models.AppDocumentUploadedDetail();
                            insertUploadAddressProof.DocContent = employeedetails.UploadaddressProof;
                            insertUploadAddressProof.DocType = ((int)EnumDocType.AddressProof).ToString();
                            insertUploadAddressProof.ModuleRefId = employeedetails.EmpCode;
                            insertUploadAddressProof.Activityid = ((int)Enumactivity.EmployeeDetails).ToString();    
                            await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(insertUploadAddressProof, cancellationToken).ConfigureAwait(false);

                        }
                        else
                        {
                            uploadAddressProof.DocContent = employeedetails.UploadaddressProof;
                            uploadAddressProof.SubTime = DateTime.Now;
                            uploadAddressProof.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                            await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(uploadAddressProof, cancellationToken).ConfigureAwait(false);
                        }

                    }

                    if (employeedetails.UploadFile != null && employeedetails.UploadFile != "")
                    {
                        Data.EF.Models.AppDocumentUploadedDetail uploadPhoto = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.DocType == EnumDocType.Photo.ToString() && e.ModuleRefId == employeedetails.EmpCode, cancellationToken);
                        if (uploadPhoto == null)
                        {
                            var insertuploadPhoto = new Data.EF.Models.AppDocumentUploadedDetail();
                            insertuploadPhoto.DocContent = employeedetails.UploadFile;
                            insertuploadPhoto.DocType = ((int)EnumDocType.Photo).ToString();
                            insertuploadPhoto.ModuleRefId = employeedetails.EmpCode;
                            insertuploadPhoto.Activityid = ((int)Enumactivity.EmployeeDetails).ToString(); 
                            await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(insertuploadPhoto, cancellationToken).ConfigureAwait(false);
                        }
                        else
                        {
                            uploadPhoto.DocContent = employeedetails.UploadFile;
                            uploadPhoto.SubTime = DateTime.Now;
                            uploadPhoto.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                            await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(uploadPhoto, cancellationToken).ConfigureAwait(false);

                        }

                    }

                    if (employeedetails.UploadIdDocument != null && employeedetails.UploadIdDocument != "")
                    {
                        Data.EF.Models.AppDocumentUploadedDetail uploadIdDocument = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.DocType == ((int)EnumDocType.IdProof).ToString() && e.ModuleRefId == employeedetails.EmpCode, cancellationToken);
                        if (uploadIdDocument == null)
                        {
                            var insertUploadIdDocument = new Data.EF.Models.AppDocumentUploadedDetail();
                            insertUploadIdDocument.DocContent = employeedetails.UploadIdDocument;
                            insertUploadIdDocument.DocType = ((int)EnumDocType.IdProof).ToString();
                            insertUploadIdDocument.ModuleRefId = employeedetails.EmpCode;
                            insertUploadIdDocument.Activityid = ((int)Enumactivity.EmployeeDetails).ToString();
                            await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(insertUploadIdDocument, cancellationToken).ConfigureAwait(false);
                        }
                        else
                        {
                            uploadIdDocument.DocContent = employeedetails.UploadIdDocument;
                            uploadIdDocument.SubTime = DateTime.Now;
                            uploadIdDocument.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                            await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(uploadIdDocument, cancellationToken).ConfigureAwait(false);

                        }
                    }
                    appDocumentUploadedDetailresults = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                if (employeeDetailsresults > 0 || appDocumentUploadedDetailresults > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int EmpId, CancellationToken cancellationToken)
        {
            if (EmpId == 0)
            {
                throw new ArgumentNullException(nameof(EmpId));
            }

            var entity = await this.unitOfWork.EmployeeDetailsRepository.FindByAsync(x => x.EmpId == EmpId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an EmpId {EmpId} was not found.");
            }

            await this.unitOfWork.EmployeeDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.EmployeeDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
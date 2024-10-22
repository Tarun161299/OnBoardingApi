//-----------------------------------------------------------------------
// <copyright file="MdMinistryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using DocumentFormat.OpenXml.Drawing.Charts;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using DocumentFormat.OpenXml.Office2010.ExcelAc;
    using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using EFModel = OnBoardingSystem.Data.EF.Models;

    public class ApplicationSummaryDirector : IApplicationSummaryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ApplicationSummaryDirector(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>

        /// <inheritdoc />
        public virtual async Task<List<ApplicationSummary>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var projectlist = await this.unitOfWork.ApplicationSummaryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return this.mapper.Map<List<ApplicationSummary>>(projectlist.OrderByDescending(rs => rs.AppYear).ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteAsync(ApplicationSummary zmstApplicationSummary, CancellationToken cancellationToken)
        {

            var entity = await this.unitOfWork.ApplicationSummaryRepository.FindByAsync(
                x => x.AppType == zmstApplicationSummary.AppType && x.AppId == zmstApplicationSummary.AppId && x.AppYear == zmstApplicationSummary.AppYear, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                // throw new EntityNotFoundException($"The Ministries with an MinistryId {ministryId} was not found.");
            }

            await this.unitOfWork.ApplicationSummaryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ApplicationSummaryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(ApplicationSummary applicationSummary, CancellationToken cancellationToken)
        {

            try
            {
                string URL = "";
                string mode = "Insert";
                if (applicationSummary == null)
                {
                    throw new ArgumentNullException(nameof(applicationSummary));
                }
                if (applicationSummary.AppUrl.Contains("admission.nic.in"))
                {
                    URL = "/API/";
                    applicationSummary.ApiLink = applicationSummary.AppUrl + URL;
                    string Result1 = getApplicationInfo(applicationSummary.ApiLink, applicationSummary.AppId, applicationSummary.AppType, mode);
                    applicationSummary.Summary = Result1;
                }
                else if (applicationSummary.AppType == "REGST")
                {
                    URL = "/AppSummary/API/";
                    applicationSummary.ApiLink = applicationSummary.AppUrl + URL;
                    string Result1 = getRegistrationApplicationInfo(applicationSummary.ApiLink, applicationSummary.AppId, applicationSummary.AppType, mode);
                    applicationSummary.Summary = Result1;
                }
                else if (applicationSummary.AppType == "COUNS")
                {
                    URL = "/API/";
                    applicationSummary.ApiLink = applicationSummary.AppUrl + URL;
                    string Result1 = getApplicationInfo(applicationSummary.ApiLink, applicationSummary.AppId, applicationSummary.AppType, mode);
                    applicationSummary.Summary = Result1;
                }
                var chkefApplicationSummary = await this.unitOfWork.ApplicationSummaryRepository.FindByAsync(r => r.AppType == applicationSummary.AppType && r.AppId == applicationSummary.AppId && r.AppYear == applicationSummary.AppYear, default);

                /*
                     if @ApplicationURL like '%admissions.nic.in%'  
                     begin  
                      Set @Url='/appsummary/API/'   
                     end  
                     else if(@ApplicationType='REGST')   
                     Begin   
                      Set @Url='/examsummary21/API/'   
                     End   
                     else if(@ApplicationType='COUNS')   
                     Begin   
                      Set @Url='/appsummary/API/'   
                     End   
                */
                if (chkefApplicationSummary != null)
                {
                    throw new EntityFoundException($"This Records {chkefApplicationSummary} already exists");
                }

                var efAppSummary = this.mapper.Map<EFModel.ApplicationSummary>(applicationSummary);
                await this.unitOfWork.ApplicationSummaryRepository.InsertAsync(efAppSummary, cancellationToken).ConfigureAwait(false);
                return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string getApplicationInfo(string url, string appId, string Type, string mode)
        {
            using (var client = new HttpClient())
            {
                string result = "";

                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);//(SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;//(SecurityProtocolType)3072;
                                                                                                              //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (appId.Substring(0, 3).Equals("116") || appId.Substring(0, 3).Equals("106"))
                {

                }
                else
                {
                    client.DefaultRequestHeaders.Add("ClientId", "5432");
                    client.DefaultRequestHeaders.Add("Authtoken", "9876");
                }

                try
                {
                    if (Type == "COUNS")
                    {
                        var responseTask = client.GetStringAsync("Couns?AppFormId=" + appId);
                        responseTask.Wait();
                        var jo = JObject.Parse(responseTask.Result);
                        CounsParameters obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CounsParameters>(jo["data"].ToString());
                        if (mode == "Insert")
                        {
                            result = JsonConvert.SerializeObject(obj);

                        }
                        else if (mode == "Update")
                        {
                            if (obj.ELG != 0 && obj.REG != 0 && obj.TCH != 0 && obj.TST != 0 && obj.LCK != 0 && obj.PIS != 0)
                            {
                                result = JsonConvert.SerializeObject(obj);
                            }
                            else
                            {
                                result = "Not Update";
                            }
                        }
                    }
                    else if (Type == "REGST")
                    {
                        var responseTask = client.GetStringAsync("Values?AppFormId=" + appId);
                        responseTask.Wait();
                        var jo = JObject.Parse(responseTask.Result);
                        RegistParameters obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegistParameters>(jo["data"].ToString());
                        if (mode == "Insert")
                        {
                            result = JsonConvert.SerializeObject(obj);

                        }
                        else if (mode == "Update")
                        {
                            if (obj.RFS != 0 && obj.AFS != 0 && obj.IMU != 0 && obj.AFP != 0 && obj.GND != 0 && obj.SCD != 0 && obj.STD != 0 && obj.OBC != 0 && obj.MLC != 0 && obj.FMC != 0 && obj.TGC != 0)
                            {
                                result = JsonConvert.SerializeObject(obj);
                            }
                            else
                            {
                                result = "Not Update";
                            }
                        }
                    }
                    else if (Type == "daywisereg")
                    {
                        var responseTask = client.GetStringAsync("daywisereg?AppFormId=" + appId);
                        responseTask.Wait();
                        result = responseTask.Result;
                    }
                    else
                    {
                        var responseTask = client.GetStringAsync("Schedule?appformid=" + appId);
                        responseTask.Wait();
                        result = responseTask.Result;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }
        }
        public string getRegistrationApplicationInfo(string url, string appId, string Type, string mode)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    client.BaseAddress = new Uri(url);
                    //client.BaseAddress = new Uri("https://admissions.nic.in/wbjeeb/appmgmt/AppSummary/API/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (appId.Substring(0, 3).Equals("116") || appId.Substring(0, 3).Equals("106"))
                    {

                    }
                    else
                    {
                        client.DefaultRequestHeaders.Add("ClientId", "5432");
                        client.DefaultRequestHeaders.Add("Authtoken", "9876");
                    }
                    var responseTask = client.GetStringAsync("values?AppFormId=" + appId.ToString());

                    RegistParameters obj;
                    responseTask.Wait();
                    var jo = JObject.Parse(responseTask.Result);
                    obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegistParameters>(jo["data"].ToString());
                    var result = JsonConvert.SerializeObject(obj);
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(ApplicationSummary applicationSummary, CancellationToken cancellationToken)
        {
            string URL = "";
            string mode = "Update";
            if (applicationSummary.AppId == "")
            {
                throw new ArgumentException(nameof(applicationSummary.Id));
            }
            URL = "/API/";
            //applicationSummary.ApiLink = applicationSummary.AppUrl + URL;
            /*
            if (zmstApplicationSummary.AppUrl.Contains("admission.nic.in"))
            {
                URL = "/appmgmt/API/";
            }
            else if (zmstApplicationSummary.AppType == "REGST")
            {
                URL = "/appmgmt/AppSummary/API/";
            }
            else if (zmstApplicationSummary.AppType == "COUNS")
            {
                URL = "/appmgmt/API/";
            }
            zmstApplicationSummary.ApiLink = zmstApplicationSummary.AppUrl + URL;
            */
            string Result1 = getApplicationInfo(applicationSummary.ApiLink, applicationSummary.AppId, applicationSummary.AppType, mode);

            if (Result1 != "Not Update")
            {
                applicationSummary.Summary = Result1;
            }

            EFModel.ApplicationSummary entity = await unitOfWork.ApplicationSummaryRepository.FindByAsync(r => r.AppType == applicationSummary.AppType && r.AppId == applicationSummary.AppId && r.AppYear == applicationSummary.AppYear, cancellationToken);
            if (entity != null)
            {
                entity.Summary = applicationSummary.Summary;
                entity.AppYear = applicationSummary.AppYear;
                entity.AppType = applicationSummary.AppType;
                entity.AppTitle = applicationSummary.AppTitle;
                entity.AppId = applicationSummary.AppId;
                entity.AppUrl = applicationSummary.AppUrl;
                entity.Status = applicationSummary.Status;
                entity.Priority = applicationSummary.Priority;
                entity.TotalRound = applicationSummary.TotalRound;
                entity.AdminUrl = applicationSummary.AdminUrl;
                entity.UpdatedTime = DateTime.Now;
                entity.UpdatedBy = applicationSummary.UpdatedBy;
                entity.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                await unitOfWork.ApplicationSummaryRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        public virtual async Task<List<ApplicationSummary>> GetAllRegistrationAsync(CancellationToken cancellationToken)
        {
            try
            {
                var projectlist = await this.unitOfWork.ApplicationSummaryRepository.FindAllByAsync(x => x.AppType == "REGST", cancellationToken).ConfigureAwait(false);
                var temp = this.mapper.Map<List<ApplicationSummary>>(projectlist.OrderByDescending(rs => rs.AppYear).ToList());
                return this.mapper.Map<List<ApplicationSummary>>(projectlist.OrderByDescending(rs => rs.AppYear).ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateExamManagementServiceAsync(ApplicationSummary applicationSummary, CancellationToken cancellationToken)
        {
            string URL = "";
            string mode = "Update";
            if (applicationSummary.AppId == "")
            {
                throw new ArgumentException(nameof(applicationSummary.Id));
            }
            string Result1 = getRegistrationApplicationInfo(applicationSummary.AdminUrl, applicationSummary.AppId, applicationSummary.AppType, mode);

            if (Result1 != "Not Update")
            {
                applicationSummary.Summary = Result1;
            }

            try
            {
                EFModel.ApplicationSummary entity = await unitOfWork.ApplicationSummaryRepository.FindByAsync(r => r.AppType == applicationSummary.AppType && r.AppId == applicationSummary.AppId && r.AppYear == applicationSummary.AppYear, cancellationToken);
                if (entity != null)
                {
                    entity.Summary = applicationSummary.Summary;
                    entity.AppYear = applicationSummary.AppYear;
                    entity.AppType = applicationSummary.AppType;
                    entity.AppTitle = applicationSummary.AppTitle;
                    entity.AppId = applicationSummary.AppId;
                    entity.AppUrl = applicationSummary.AppUrl;
                    entity.Status = applicationSummary.Status;
                    entity.Priority = applicationSummary.Priority;
                    entity.TotalRound = applicationSummary.TotalRound;
                    entity.AdminUrl = applicationSummary.AdminUrl;
                    entity.UpdatedTime = DateTime.Now;
                    entity.UpdatedBy = applicationSummary.UpdatedBy;
                    entity.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                    await unitOfWork.ApplicationSummaryRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
                }

                return await unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<List<GetApplicationSummary>> GetAppSummaryData(CancellationToken cancellationToken)
        {
            using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + "sp_GetApiLink";
                List<Abstractions.Models.GetApplicationSummary> getApplicationSummary = new List<Abstractions.Models.GetApplicationSummary>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getApplicationSummary.Add(new Abstractions.Models.GetApplicationSummary
                        {
                            AgencyId = reader.GetString(reader.GetOrdinal("AgencyId")),
                            AgencyName = reader.GetString(reader.GetOrdinal("AgencyName")),
                            apiLink = reader.GetString(reader.GetOrdinal("apiLink")),
                            appYear = reader.GetString(reader.GetOrdinal("appYear")),
                        });
                    }
                }
                connection.Close();
                return getApplicationSummary.ToList();
            }

        }

        /// <inheritdoc/>
        public virtual async Task<List<CounsellingAndAdmissionSystemSummary>> GetAppSummaryByCouns(CancellationToken cancellationToken)
        {
            var applicationSummary = await this.unitOfWork.ApplicationSummaryRepository.FindAllByAsync(x => x.AppType == "COUNS", cancellationToken).ConfigureAwait(false);
            var applicationSchedule = await this.unitOfWork.ApplicationScheduleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); /*Need to change here. Map with Application Schedule summary*/
            List<CounsellingAndAdmissionSystemSummary> summary = new List<CounsellingAndAdmissionSystemSummary>();
            CounsParameters zmstApplication = new CounsParameters();
            var resultcouns = from appsum in applicationSummary
                              join appSchedule in applicationSchedule on appsum.AppId equals appSchedule.AppId into temp
                              from appSchedule in temp.DefaultIfEmpty()
                              select new CounsellingAndAdmissionSystemSummary
                              {
                                  CurrentRound = (appSchedule == null) ? "0" : getSummaryData(appSchedule.Summary),
                                  Couns = (appsum.Summary != null) ? Newtonsoft.Json.JsonConvert.DeserializeObject<CounsParameters>(appsum.Summary) : null,//(appType == "COUNS") ? Newtonsoft.Json.JsonConvert.DeserializeObject<CounsParameters>(appsum.Summary) : null,
                                  Regis = null,//(appType == "REGST") ? Newtonsoft.Json.JsonConvert.DeserializeObject<RegistParameters>(appsum.Summary) : null,
                                  AgencyId = appsum.AgencyId,
                                  AppYear = appsum.AppYear,
                                  AppType = appsum.AppType,
                                  AppId = appsum.AppId,
                                  AppTitle = appsum.AppTitle,
                                  AppUrl = appsum.AppUrl,
                                  Status = appsum.Status,
                                  ApiLink = appsum.ApiLink,
                                  UpdatedTime = appsum.UpdatedTime,
                                  UpdatedBy = appsum.UpdatedBy,
                                  IpAddress = appsum.IpAddress,
                                  Priority = appsum.Priority,
                                  ScheduleDoc = appsum.ScheduleDoc,
                                  TotalRound = appsum.TotalRound,
                              };
            return resultcouns.ToList();
        }

        public string getSummaryData(string Summary)
        {

            List<AppScheduleData> applicationschedule = new List<AppScheduleData>();

            applicationschedule = new List<AppScheduleData>();
            JArray summaryArray = JArray.Parse(Summary);
            List<AppScheduleData> appScheduleDataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AppScheduleData>>(summaryArray.ToString());
            var result = from appSchedule in appScheduleDataList
                         select new AppScheduleData
                         {
                             boardid = appSchedule.boardid,
                             roundno = appSchedule.roundno,
                             activityid = appSchedule.activityid,
                             ActivityNm = appSchedule.ActivityNm,
                             description = appSchedule.description,
                             sdate = appSchedule.sdate,
                             cDate = appSchedule.cDate,
                             ScheduleStatus = appSchedule.ScheduleStatus,

                         };
            string curretRound = result.OrderByDescending(x => x.roundno).ToArray()[0].roundno;
            return curretRound;
        }

        /// <inheritdoc/>
        public virtual async Task<List<CounsellingAndAdmissionSystemSummary>> GetAppSummaryByRegst(CancellationToken cancellationToken)
        {
            var applicationSummary = await this.unitOfWork.ApplicationSummaryRepository.FindAllByAsync(x => x.AppType == "REGST", cancellationToken).ConfigureAwait(false);
            var applicationSchedule = await this.unitOfWork.ApplicationScheduleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); /*Need to change here. Map with Application Schedule summary*/
            List<CounsellingAndAdmissionSystemSummary> summary = new List<CounsellingAndAdmissionSystemSummary>();
            CounsParameters zmstApplication = new CounsParameters();
            var resultcouns = from appsum in applicationSummary
                              join appSchedule in applicationSchedule on appsum.AppId equals appSchedule.AppId into temp
                              from appSchedule in temp.DefaultIfEmpty()
                              select new CounsellingAndAdmissionSystemSummary
                              {
                                  CurrentRound = (appSchedule == null) ? "0" : getSummaryData(appSchedule.Summary),
                                  Couns = null,//(appType == "COUNS") ? Newtonsoft.Json.JsonConvert.DeserializeObject<CounsParameters>(appsum.Summary) : null,
                                  Regis = (appsum.Summary != null) ? Newtonsoft.Json.JsonConvert.DeserializeObject<RegistParameters>(appsum.Summary) : null,//(appType == "REGST") ? Newtonsoft.Json.JsonConvert.DeserializeObject<RegistParameters>(appsum.Summary) : null,
                                  AgencyId = appsum.AgencyId,
                                  AppYear = appsum.AppYear,
                                  AppType = appsum.AppType,
                                  AppId = appsum.AppId,
                                  AppTitle = appsum.AppTitle,
                                  AppUrl = appsum.AppUrl,
                                  Status = appsum.Status,
                                  ApiLink = appsum.ApiLink,
                                  UpdatedTime = appsum.UpdatedTime,
                                  UpdatedBy = appsum.UpdatedBy,
                                  IpAddress = appsum.IpAddress,
                                  Priority = appsum.Priority,
                                  ScheduleDoc = appsum.ScheduleDoc,
                                  TotalRound = appsum.TotalRound,
                              };
            return resultcouns.ToList();
        }
    }

}

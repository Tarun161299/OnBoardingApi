//-----------------------------------------------------------------------
// <copyright file="ApplicationScheduleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure;
    using OnBoardingSystem.Data.Business.Services;
    using System.Globalization;
    using System.Threading;
    using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System.Data;

    //using OnBoardingSystem.Data.EF.Models;

    //using OnBoardingSystem.Data.EF.Models;

    public class ApplicationScheduleDirector : IApplicationScheduleDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationScheduleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ApplicationScheduleDirector(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            httpContextAccessor = httpContext;
        }

        /// <inheritdoc />
        public virtual async Task<List<ApplicationSchedule>> GetAllAsync(calendarDate startEndDate, CancellationToken cancellationToken)
        {
            try
            {
                var applicationSchedule = await this.unitOfWork.ApplicationScheduleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                // List<ApplicationSchedule> applicationschedule = new List<ApplicationSchedule>();
                var applicationschedule = this.mapper.Map<List<OnBoardingSystem.Data.Abstractions.Models.ApplicationSchedule>>(applicationSchedule).ToList();

                var result = getSummaryData(startEndDate, applicationschedule);
                var finalresult = getData(result);
                return finalresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc />
        public virtual async Task<List<AppScheduleData>> GetAllActivityAsync(CancellationToken cancellationToken)
        {
            try
            {
                var applicationSchedule = await this.unitOfWork.ApplicationScheduleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                // List<ApplicationSchedule> applicationschedule = new List<ApplicationSchedule>();
                var applicationschedule = this.mapper.Map<List<OnBoardingSystem.Data.Abstractions.Models.ApplicationSchedule>>(applicationSchedule).ToList();

                // var result = (startEndDate, applicationschedule);
                var finalresult = getDistinctAppdata(applicationschedule);
                return finalresult;
                //return Task.FromResult(finalresult);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ApplicationSchedule> getSummaryData(calendarDate startEndDate, List<ApplicationSchedule> applicationSchedules)
        {
            try
            {
                List<AppScheduleData> applicationschedule;
                foreach (var appSchedule in applicationSchedules)
                {
                    applicationschedule = new List<AppScheduleData>();
                    JArray joe = JArray.Parse(appSchedule.Summary);
                    foreach (var data in joe)
                    {
                        AppScheduleData obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AppScheduleData>(data.ToString());
                        applicationschedule.Add(obj);
                    }

                    appSchedule.Summary = null;
                    if (startEndDate.mode == "Calendar")
                    {
                        appSchedule.JSummary = applicationschedule.Where(x =>
                                            (Convert.ToDateTime(ChangeFormat(x.sdate)) >= Convert.ToDateTime(ChangeFormat(startEndDate.startdate)) &&
                                            Convert.ToDateTime(ChangeFormat(x.sdate)) <= Convert.ToDateTime(ChangeFormat(startEndDate.enddate)))
                                            &&
                                            (Convert.ToDateTime(ChangeFormat(x.cDate)) <= Convert.ToDateTime(ChangeFormat(startEndDate.enddate)) &&
                                            Convert.ToDateTime(ChangeFormat(x.cDate)) >= Convert.ToDateTime(ChangeFormat(startEndDate.startdate)))).ToList();
                    }
                    else if (startEndDate.mode == "Not Calendar")
                    {
                        appSchedule.JSummary = applicationschedule.Where(x =>
                                           (Convert.ToDateTime(ChangeFormat(x.sdate)) >= Convert.ToDateTime(ChangeFormat(startEndDate.startdate)) &&
                                           Convert.ToDateTime(ChangeFormat(x.sdate)) <= Convert.ToDateTime(ChangeFormat(startEndDate.enddate)))
                                           &&
                                           (Convert.ToDateTime(ChangeFormat(x.cDate)) <= Convert.ToDateTime(ChangeFormat(startEndDate.enddate)) &&
                                           Convert.ToDateTime(ChangeFormat(x.cDate)) >= Convert.ToDateTime(ChangeFormat(startEndDate.startdate))) &&
                                           (x.activityid == startEndDate.activityId)).ToList();
                    }
                }

                return applicationSchedules.Where(X => X.JSummary.Count != 0).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ChangeFormat(string Input)
        {
            //'2017-04-25'-SQL DATE FORMAT
            DateTime myDt;
            if (!string.IsNullOrEmpty(Input))
            {
                Input = Input.Substring(0, 10);

                try
                {

                    myDt = DateTime.ParseExact(Input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    myDt = default(DateTime);
                }
            }
            else
            {
                myDt = default(DateTime);
            }
            return myDt;
        }

        public List<ApplicationSchedule> getData(List<ApplicationSchedule> appScheduleData)
        {
            try
            {
                var projectList = this.unitOfWork.ZmstProjectRepository.GetAll();
                var appSchedulerList = appScheduleData;
                var result = from appSchedule in appSchedulerList
                             join project in projectList on appSchedule.AppId equals ((project.ProjectId).ToString()) into list
                             from project in list.DefaultIfEmpty()
                             select new Abstractions.Models.ApplicationSchedule
                             {
                                 Id = appSchedule.Id,
                                 AppId = appSchedule.AppId,
                                 Summary = appSchedule.Summary,
                                 JSummary = appSchedule.JSummary,
                                 UpdatedTime = appSchedule.UpdatedTime,
                                 UpdatedBy = appSchedule.UpdatedBy,
                                 IpAddress = appSchedule.IpAddress,
                                 Priority = appSchedule.Priority,
                                 Status = appSchedule.Status,
                                 projectName = project.ProjectName,
                             };
                var test = result.ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AppScheduleData> getDistinctAppdata(List<ApplicationSchedule> applicationSchedules)
        {
            try
            {
                List<AppScheduleData> applicationschedule;
                applicationschedule = new List<AppScheduleData>();
                foreach (var appSchedule in applicationSchedules)
                {

                    JArray joe = JArray.Parse(appSchedule.Summary);
                    foreach (var data in joe)
                    {
                        AppScheduleData obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AppScheduleData>(data.ToString());
                        applicationschedule.Add(obj);
                    }
                }
                return applicationschedule.DistinctBy(X => X.activityid).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<string> GetByProjectId(List<ZmstProjectSchedule> ZmstProjectSchedule, CancellationToken cancellationToken)
        {
            try
            { string ProjectIds= "";
                string JsonString = "";
                int i = 0;
                if (ZmstProjectSchedule.Count == 1 && ZmstProjectSchedule[0].ProjectId == 0)
                {
                    ProjectIds = "";
                }
                else
                {
                    foreach (var project in ZmstProjectSchedule)
                    {
                        i = i + 1;
                        if (ZmstProjectSchedule.Count() == i)
                        {
                            ProjectIds = ProjectIds + project.ProjectId.ToString();
                        }
                        else
                        {
                            ProjectIds = ProjectIds + project.ProjectId.ToString() + ",";
                        }
                    };
                }
                var param = new SqlParameter[]
             {
                   new SqlParameter()
                   {
                       ParameterName = "@BoardIds",
                       SqlDbType = System.Data.SqlDbType.VarChar,
                       Value = ProjectIds,
                   },
                };
                using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                  
                    command.CommandText = "EXEC " + "USP_GetScheduleReport  @BoardIds";
                    foreach (var parameterDefinition in param)
                    {
                        command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JsonString = Convert.ToString(reader["JsonString"]);
                        }
                    }
                }
               return JsonString;
            }   
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;
using OnBoardingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    public class DaywiseRegistrationDirector : IDaywiseRegistrationDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationScheduleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public DaywiseRegistrationDirector(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public virtual async Task<List<ApplicationDayWise>> GetAllAsync(DaywiseRegistration startEndDate, CancellationToken cancellationToken)
        {
            try
            {
                var applicationDayWise = await this.unitOfWork.ApplicationDayWiseRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                // List<ApplicationSchedule> applicationschedule = new List<ApplicationSchedule>();
                var applicationDayWiseData = this.mapper.Map<List<OnBoardingSystem.Data.Abstractions.Models.ApplicationDayWise>>(applicationDayWise).ToList();

                var result = getSummaryData(startEndDate, applicationDayWiseData);
                var finalresult = getData(result);
                return finalresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ApplicationDayWise> getSummaryData(DaywiseRegistration startEndDate, List<ApplicationDayWise> applicationSchedules)
        {
            try
            {
                List<ApplicationDaywiseData> applicationschedule;
                foreach (var appSchedule in applicationSchedules)
                {
                    applicationschedule = new List<ApplicationDaywiseData>();
                    JArray joe = JArray.Parse(appSchedule.Summary);
                    foreach (var data in joe)
                    {
                        ApplicationDaywiseData obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationDaywiseData>(data.ToString());
                        applicationschedule.Add(obj);
                    }

                    appSchedule.Summary = null;
                    appSchedule.JSummary = applicationschedule.Where(x =>
                                            (Convert.ToDateTime(x.regdate) >= Convert.ToDateTime(ChangeFormat(startEndDate.startdate)) &&
                                            (Convert.ToDateTime(x.regdate) <= Convert.ToDateTime(ChangeFormat(startEndDate.enddate))))
                                            ).ToList();
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
                try
                {
                   // myDt = DateTime.Parse(Input, null, System.Globalization.DateTimeStyles.RoundtripKind);
                   // string dd1= myDt.ToString();
                   // DateTime date = DateTime.Parse(dd1);
                   // DateTime date3 = DateTime.ParseExact(dd1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                   // myDt = DateTime.ParseExact(dd1, "dd/M/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
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

        //public DateTime ChangeISOFormat(string Input)
        //{
        //    DateTime d2 = DateTime.Parse(Input, null, System.Globalization.DateTimeStyles.RoundtripKind);
        //}

        public List<ApplicationDayWise> getData(List<ApplicationDayWise> applicationDayWiseData)
        {
            try
            {
                var projectList = this.unitOfWork.ZmstProjectRepository.GetAll();
                var applicationDayWiseList = applicationDayWiseData;
                var result = from appSchedule in applicationDayWiseList
                             join project in projectList on appSchedule.AppId equals ((project.ProjectId).ToString()) into list
                             from project in list.DefaultIfEmpty()
                             select new Abstractions.Models.ApplicationDayWise
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
    }

}

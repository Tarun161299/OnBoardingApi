

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Castle.Core.Resource;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    public class RequestListInfoDirector : IRequestListInfoDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RequestListInfoDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<List<RequestList>> GetRequestListAsync(CancellationToken cancellationToken)
        {
            var requestlist = (from mi in this.unitOfWork.RequestListInfoRepository.GetAll()
                               select new RequestList
                               {
                                   Id = mi.Id,
                                   RequestId = mi.RequestId,
                                   AgencyType = mi.AgencyType,
                                   OranizationName = mi.OranizationName,
                                   RequestDate = mi.RequestDate,
                                   Status = mi.Status
                               }).Distinct<RequestList>();

            return await Task.FromResult(requestlist.OrderByDescending(rs => rs.RequestId).ToList()).ConfigureAwait(false);
        }

        //public Task<OnBoardingRequestDetailUpsert> GetRequestListByIdAsync(int requestId, CancellationToken cancellationToken)
        //{
        //    var param = new ParameterDefinition[]
        //    {
        //        new ParameterDefinition()
        //        {
        //            ParameterName = "@requestno",
        //            ParameterDataType = System.Data.SqlDbType.Int,
        //            ParameterValue = requestId,
        //        },
        //    };

        //    var storedProcedureName = $"{"Usp_getrequestdetails"} @requestno";
        //    var mdMinistry = this.unitOfWork.RequestListInfoRepository.FromSqlRawAsync(storedProcedureName, param, cancellationToken).ConfigureAwait(false);
        //    var result = this.mapper.Map<List<OnBoardingRequestDetailUpsert>>(mdMinistry)[0];
        //    return result;
        //}

    }
}

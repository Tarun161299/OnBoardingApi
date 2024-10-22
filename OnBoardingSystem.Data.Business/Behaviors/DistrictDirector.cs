
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    public class DistrictDirector : IDistrictDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>

        public DistrictDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.MdDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Districtlist = await this.unitOfWork.MdDistrictRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.MdDistrict>>(Districtlist);
        }

        /// <inheritdoc/>
        public virtual async Task<List<Abstractions.Models.MdDistrict>> GetDistrictListByStateIdAsync(string stateID, CancellationToken cancellationToken)
        {
            var DistrictListById = await this.unitOfWork.MdDistrictRepository.FindAllByAsync(x => x.StateId == stateID, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.MdDistrict>>(DistrictListById);
            return result.OrderBy(x => x.StateId).ToList();
        }
    }
}

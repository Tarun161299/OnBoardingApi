//-----------------------------------------------------------------------
// <copyright file="MdMinistryDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class ZmstServiceTypeDirector : IZmstServiceTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstServiceTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstServiceType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var servicelist = await this.unitOfWork.ZmstServiceTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.ZmstServiceType>>(servicelist);
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstServiceType>> GetByRequestNo(string requestNo, CancellationToken cancellationToken)
        {
            var request = await this.unitOfWork.AppOnboardingRequestRepository.FindByAsync(x => x.RequestNo == requestNo, cancellationToken).ConfigureAwait(false);
            var servicelist = await this.unitOfWork.ZmstServiceTypeRepository.FindAllByAsync(x => request.Services.Contains(x.ServiceTypeId), cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.ZmstServiceType>>(servicelist);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(Abstractions.Models.ZmstServiceType zmstservicetype, CancellationToken cancellationToken)
        {
            if (zmstservicetype == null)
            {
                throw new ArgumentNullException(nameof(zmstservicetype));
            }

            var chkefzmstservicetype = await this.unitOfWork.ZmstServiceTypeRepository.FindByAsync(r => r.ServiceTypeId == zmstservicetype.ServiceTypeId, default);
            if (chkefzmstservicetype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstservicetype} already exists");
            }

            var efzmstservicetype = this.mapper.Map<Data.EF.Models.ZmstServiceType>(zmstservicetype);

            await this.unitOfWork.ZmstServiceTypeRepository.InsertAsync(efzmstservicetype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(Abstractions.Models.ZmstServiceType zmstservicetype, CancellationToken cancellationToken)

        {
            if (zmstservicetype.ServiceTypeId == "0")
            {
                throw new ArgumentException(nameof(zmstservicetype.ServiceTypeId));
            }

            Data.EF.Models.ZmstServiceType entityUpd = await unitOfWork.ZmstServiceTypeRepository.FindByAsync(e => e.ServiceTypeId == zmstservicetype.ServiceTypeId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.ServiceTypeId = zmstservicetype.ServiceTypeId;
                entityUpd.ServiceTypeName = zmstservicetype.ServiceTypeName;

                await unitOfWork.ZmstServiceTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string ServiceTypeId, CancellationToken cancellationToken)
        {
            if (ServiceTypeId == "0")
            {
                throw new ArgumentNullException(nameof(ServiceTypeId));
            }

            var entity = await this.unitOfWork.ZmstServiceTypeRepository.FindByAsync(x => x.ServiceTypeId == ServiceTypeId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ServiceTypeId {ServiceTypeId} was not found.");
            }

            await this.unitOfWork.ZmstServiceTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstServiceTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using System.Text;
    using OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class ZmstAgencyDirector : IZmstAgencyDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstAgencyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstAgency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var agencylist = this.unitOfWork.ZmstAgencyRepository.GetAll();
            var stateList = this.unitOfWork.MdStateRepository.GetAll();
            var result = from agency in agencylist
                         join state in stateList on agency.StateId equals state.Id
                         select new ZmstAgency
                         {
                             AgencyId = agency.AgencyId,
                             AgencyName = agency.AgencyName,
                             AgencyAbbr = agency.AgencyAbbr,
                             AgencyType = agency.AgencyType,
                             Address = agency.Address,
                             StateName = state.Description,
                             StateId = state.Id,
                             ServiceTypeId = agency.ServiceTypeId,
                             IsActive = agency.IsActive,
                             Priority = agency.Priority,
                             BoardRequestLetter = "",
                         };
            return result.ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstAgency> GetByIdAsync(string AgencyId, CancellationToken cancellationToken)
        {
            var zmstagencylist = await this.unitOfWork.ZmstAgencyRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstAgency>(zmstagencylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstAgency zmstagency, CancellationToken cancellationToken)
        {
            if (zmstagency == null)
            {
                throw new ArgumentNullException(nameof(zmstagency));
            }

            var chkefzmstagency = await this.unitOfWork.ZmstAgencyRepository.FindByAsync(r => r.AgencyId == zmstagency.AgencyId, default);
            if (chkefzmstagency != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstagency} already exists");
            }
            Data.EF.Models.ZmstAgency agency = new Data.EF.Models.ZmstAgency();
            agency.Priority = zmstagency.Priority;
            agency.AgencyName = zmstagency.AgencyName;
            agency.Address = zmstagency.Address;
            agency.StateId = zmstagency.StateId;
            agency.ServiceTypeId = zmstagency.ServiceTypeId;
            agency.AgencyId = zmstagency.AgencyId;
            agency.AgencyAbbr = zmstagency.AgencyAbbr;
            agency.AgencyType = zmstagency.AgencyType;
            agency.IsActive = zmstagency.IsActive;
            agency.BoardRequestLetter = Encoding.UTF8.GetBytes(zmstagency.BoardRequestLetter);
            await this.unitOfWork.ZmstAgencyRepository.InsertAsync(agency, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstAgency zmstagency, CancellationToken cancellationToken)

        {
            if (zmstagency.AgencyId == "0")
            {
                throw new ArgumentException(nameof(zmstagency.AgencyId));
            }

            Data.EF.Models.ZmstAgency entityUpd = await unitOfWork.ZmstAgencyRepository.FindByAsync(e => e.AgencyId == zmstagency.AgencyId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.AgencyId = zmstagency.AgencyId;
                entityUpd.AgencyName = zmstagency.AgencyName;
                entityUpd.AgencyAbbr = zmstagency.AgencyAbbr;
                entityUpd.AgencyType = zmstagency.AgencyType;
                entityUpd.StateId = zmstagency.StateId;
                entityUpd.ServiceTypeId = zmstagency.ServiceTypeId;
                entityUpd.Address = zmstagency.Address;
                entityUpd.IsActive = zmstagency.IsActive;
                entityUpd.Priority = zmstagency.Priority;
                /////entityUpd.BoardRequestLetter= zmstagency.BoardRequestLetter;

                await unitOfWork.ZmstAgencyRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string AgencyId, CancellationToken cancellationToken)
        {
            if (AgencyId == "0")
            {
                throw new ArgumentNullException(nameof(AgencyId));
            }

            var entity = await this.unitOfWork.ZmstAgencyRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an AgencyId {AgencyId} was not found.");
            }

            await this.unitOfWork.ZmstAgencyRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstAgencyRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
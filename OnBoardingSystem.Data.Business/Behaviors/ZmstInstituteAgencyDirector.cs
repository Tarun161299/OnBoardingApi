
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteAgencyDirector.cs" company="NIC">
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

    /// <inheritdoc />
    public class ZmstInstituteAgencyDirector : IZmstInstituteAgencyDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteAgencyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstInstituteAgencyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstInstituteAgency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstinstituteagencylist = await this.unitOfWork.ZmstInstituteAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var agencyList = await this.unitOfWork.MDAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var instituteList = await this.unitOfWork.ZmstInstituteRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = from instAgencyList in zmstinstituteagencylist
                         join agency in agencyList on instAgencyList.AgencyId equals agency.AgencyId.ToString()
                         join institutes in instituteList on instAgencyList.InstCd equals institutes.InstCd.ToString()
                         select new AbsModels.ZmstInstituteAgency
                         {
                             InstCd = instAgencyList.InstCd,
                             AgencyId = instAgencyList.AgencyId,
                             InstName = institutes.InstNm,
                             AgencyName = agency.AgencyName

                         };
            return this.mapper.Map<List<AbsModels.ZmstInstituteAgency>>(result);
        }
        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstInstituteAgency> GetByIdAsync(string InstCd, CancellationToken cancellationToken)
        {
            var zmstinstituteagencylist = await this.unitOfWork.ZmstInstituteAgencyRepository.FindByAsync(x => x.AgencyId == InstCd, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstInstituteAgency>(zmstinstituteagencylist);
            return result;
        }
        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstInstituteAgency zmstinstituteagency, CancellationToken cancellationToken)
        {
            try
            {
                if (zmstinstituteagency == null)
                {
                    throw new ArgumentNullException(nameof(zmstinstituteagency));
                }

                var chkefzmstinstituteagency = await this.unitOfWork.ZmstInstituteAgencyRepository.FindByAsync(r => r.InstCd == zmstinstituteagency.InstCd && r.AgencyId == zmstinstituteagency.AgencyId, default);
                if (chkefzmstinstituteagency != null)
                {
                    throw new EntityFoundException($"This Records {chkefzmstinstituteagency} already exists");
                }

                var efzmstinstituteagency = this.mapper.Map<Data.EF.Models.ZmstInstituteAgency>(zmstinstituteagency);

                await this.unitOfWork.ZmstInstituteAgencyRepository.InsertAsync(efzmstinstituteagency, cancellationToken).ConfigureAwait(false);
                return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstInstituteAgency zmstinstituteagency, CancellationToken cancellationToken)

        {
            if (zmstinstituteagency.InstCd == "0")
            {
                throw new ArgumentException(nameof(zmstinstituteagency.InstCd));
            }

            Data.EF.Models.ZmstInstituteAgency entityUpd = await unitOfWork.ZmstInstituteAgencyRepository.FindByAsync(e => e.InstCd == zmstinstituteagency.InstCd, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.InstCd = zmstinstituteagency.InstCd;
                entityUpd.AgencyId = zmstinstituteagency.AgencyId;

                await unitOfWork.ZmstInstituteAgencyRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken)
        {
            if (InstCd == "0")
            {
                throw new ArgumentNullException(nameof(InstCd));
            }

            var entity = await this.unitOfWork.ZmstInstituteAgencyRepository.FindByAsync(x => x.InstCd == InstCd, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an InstCd {InstCd} was not found.");
            }

            await this.unitOfWork.ZmstInstituteAgencyRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstInstituteAgencyRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
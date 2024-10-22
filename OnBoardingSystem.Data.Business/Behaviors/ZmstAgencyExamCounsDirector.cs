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
    using OnBoardingSystem.Data.Interfaces;
    public class ZmstAgencyExamCounsDirector:IZmstAgencyExamCounsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstAgencyExamCounsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<ZmstAgencyExamCouns>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstagencyexamcounslist = await this.unitOfWork.ZmstAgencyExamCounRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstagencylist = await this.unitOfWork.ZmstAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var agencyexamcouns = from agencyexam in zmstagencyexamcounslist
                                  join agencylist in zmstagencylist on agencyexam.AgencyId.ToString() equals agencylist.AgencyId
                                  select new Abstractions.Models.ZmstAgencyExamCouns
                                  {
                                    AgencyId= agencyexam.AgencyId,
                                    ExamCounsId=agencyexam.ExamCounsId,
                                    Description=agencyexam.Description,
                                    AgencyName= agencylist.AgencyName,
                                  };

            return this.mapper.Map<List<ZmstAgencyExamCouns>>(agencyexamcouns);
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstAgencyExamCouns>> GetByIdAsync(int agencyid,CancellationToken cancellationToken)
        {
            var servicelist = await this.unitOfWork.ZmstAgencyExamCounRepository.FindAllByAsync(x=>x.AgencyId== agencyid,cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.ZmstAgencyExamCouns>>(servicelist);
        }

        public async Task<int> InsertAsync(ZmstAgencyExamCouns zmstagencyexamcouns, CancellationToken cancellationToken)
        {
            if (zmstagencyexamcouns == null)
            {
                throw new ArgumentNullException(nameof(zmstagencyexamcouns));
            }

            var chkefzmstagencyexamcouns = await this.unitOfWork.ZmstAgencyExamCounRepository.FindByAsync(r => r.AgencyId == zmstagencyexamcouns.AgencyId && r.ExamCounsId== zmstagencyexamcouns.ExamCounsId, default);
            if (chkefzmstagencyexamcouns != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstagencyexamcouns} already exists");
            }

            var efzmstagencyexamcouns = this.mapper.Map<Data.EF.Models.ZmstAgencyExamCouns>(zmstagencyexamcouns);

            await this.unitOfWork.ZmstAgencyExamCounRepository.InsertAsync(efzmstagencyexamcouns, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(ZmstAgencyExamCouns zmstagencyexamcouns, CancellationToken cancellationToken)

        {
            if (zmstagencyexamcouns.AgencyId == 0)
            {
                throw new ArgumentException(nameof(zmstagencyexamcouns.AgencyId));
            }

            Data.EF.Models.ZmstAgencyExamCouns entityUpd = await unitOfWork.ZmstAgencyExamCounRepository.FindByAsync(e => e.AgencyId == zmstagencyexamcouns.AgencyId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.AgencyId = zmstagencyexamcouns.AgencyId;
                entityUpd.ExamCounsId = zmstagencyexamcouns.ExamCounsId;
                entityUpd.Description = zmstagencyexamcouns.Description;

                await unitOfWork.ZmstAgencyExamCounRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int AgencyId, CancellationToken cancellationToken)
        {
            if (AgencyId == 0)
            {
                throw new ArgumentNullException(nameof(AgencyId));
            }

            var entity = await this.unitOfWork.ZmstAgencyExamCounRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an AgencyId {AgencyId} was not found.");
            }

            await this.unitOfWork.ZmstAgencyExamCounRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstAgencyExamCounRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

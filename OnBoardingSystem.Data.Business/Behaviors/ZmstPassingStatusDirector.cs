
//-----------------------------------------------------------------------
// <copyright file="ZmstPassingStatusDirector.cs" company="NIC">
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

    /// <inheritdoc />
    public class ZmstPassingStatusDirector : IZmstPassingStatusDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstPassingStatusDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstPassingStatusDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstPassingStatus>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstpassingstatuslist = await this.unitOfWork.ZmstPassingStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstPassingStatus>>(zmstpassingstatuslist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstPassingStatus> GetByIdAsync(string PassingStatusId, CancellationToken cancellationToken)
        {
            var zmstpassingstatuslist = await this.unitOfWork.ZmstPassingStatusRepository.FindByAsync(x => x.PassingStatusId == PassingStatusId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstPassingStatus>(zmstpassingstatuslist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstPassingStatus zmstpassingstatus, CancellationToken cancellationToken)
        {
            if (zmstpassingstatus == null)
            {
                throw new ArgumentNullException(nameof(zmstpassingstatus));
            }

            var chkefzmstpassingstatus = await this.unitOfWork.ZmstPassingStatusRepository.FindByAsync(r => r.PassingStatusId == zmstpassingstatus.PassingStatusId, default);
            if (chkefzmstpassingstatus != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstpassingstatus} already exists");
            }

            var efzmstpassingstatus = this.mapper.Map<Data.EF.Models.ZmstPassingStatus>(zmstpassingstatus);

            await this.unitOfWork.ZmstPassingStatusRepository.InsertAsync(efzmstpassingstatus, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstPassingStatus zmstpassingstatus, CancellationToken cancellationToken)

        {
            if (zmstpassingstatus.PassingStatusId == "0")
            {
                throw new ArgumentException(nameof(zmstpassingstatus.PassingStatusId));
            }

            Data.EF.Models.ZmstPassingStatus entityUpd = await unitOfWork.ZmstPassingStatusRepository.FindByAsync(e => e.PassingStatusId == zmstpassingstatus.PassingStatusId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.PassingStatusId = zmstpassingstatus.PassingStatusId;
                entityUpd.Description = zmstpassingstatus.Description;
                entityUpd.AlternateNames = zmstpassingstatus.AlternateNames;

                await unitOfWork.ZmstPassingStatusRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string PassingStatusId, CancellationToken cancellationToken)
        {
            if (PassingStatusId == "0")
            {
                throw new ArgumentNullException(nameof(PassingStatusId));
            }

            var entity = await this.unitOfWork.ZmstPassingStatusRepository.FindByAsync(x => x.PassingStatusId == PassingStatusId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an PassingStatusId {PassingStatusId} was not found.");
            }

            await this.unitOfWork.ZmstPassingStatusRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstPassingStatusRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

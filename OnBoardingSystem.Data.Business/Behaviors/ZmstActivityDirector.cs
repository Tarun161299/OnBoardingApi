
//-----------------------------------------------------------------------
// <copyright file="ZmstActivityDirector.cs" company="NIC">
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
    public class ZmstActivityDirector : IZmstActivityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstActivityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstActivityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstActivity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstactivitylist = await this.unitOfWork.ZmstActivityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstActivity>>(zmstactivitylist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstActivity> GetByIdAsync(string ActivityId, CancellationToken cancellationToken)
        {
            var zmstactivitylist = await this.unitOfWork.ZmstActivityRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstActivity>(zmstactivitylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstActivity zmstactivity, CancellationToken cancellationToken)
        {
            if (zmstactivity == null)
            {
                throw new ArgumentNullException(nameof(zmstactivity));
            }

            var chkefzmstactivity = await this.unitOfWork.ZmstActivityRepository.FindByAsync(r => r.ActivityId == zmstactivity.ActivityId, default);
            if (chkefzmstactivity != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstactivity} already exists");
            }

            var efzmstactivity = this.mapper.Map<Data.EF.Models.ZmstActivity>(zmstactivity);

            await this.unitOfWork.ZmstActivityRepository.InsertAsync(efzmstactivity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstActivity zmstactivity, CancellationToken cancellationToken)

        {
            if (zmstactivity.ActivityId == "0")
            {
                throw new ArgumentException(nameof(zmstactivity.ActivityId));
            }

            Data.EF.Models.ZmstActivity entityUpd = await unitOfWork.ZmstActivityRepository.FindByAsync(e => e.ActivityId == zmstactivity.ActivityId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.ActivityId = zmstactivity.ActivityId;
                entityUpd.ActivityName = zmstactivity.ActivityName;
                entityUpd.DisplayPriority = zmstactivity.DisplayPriority;

                await unitOfWork.ZmstActivityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string ActivityId, CancellationToken cancellationToken)
        {
            if (ActivityId == "0")
            {
                throw new ArgumentNullException(nameof(ActivityId));
            }

            var entity = await this.unitOfWork.ZmstActivityRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ActivityId {ActivityId} was not found.");
            }

            await this.unitOfWork.ZmstActivityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstActivityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

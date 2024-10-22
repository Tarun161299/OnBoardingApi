
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatGroupDirector.cs" company="NIC">
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
    public class ZmstSeatGroupDirector : IZmstSeatGroupDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatGroupDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSeatGroupDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSeatGroup>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstseatgrouplist = await this.unitOfWork.ZmstSeatGroupRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSeatGroup>>(zmstseatgrouplist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSeatGroup> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstseatgrouplist = await this.unitOfWork.ZmstSeatGroupRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSeatGroup>(zmstseatgrouplist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSeatGroup zmstseatgroup, CancellationToken cancellationToken)
        {
            if (zmstseatgroup == null)
            {
                throw new ArgumentNullException(nameof(zmstseatgroup));
            }

            var chkefzmstseatgroup = await this.unitOfWork.ZmstSeatGroupRepository.FindByAsync(r => r.Id == zmstseatgroup.Id, default);
            if (chkefzmstseatgroup != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstseatgroup} already exists");
            }

            var efzmstseatgroup = this.mapper.Map<Data.EF.Models.ZmstSeatGroup>(zmstseatgroup);

            await this.unitOfWork.ZmstSeatGroupRepository.InsertAsync(efzmstseatgroup, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSeatGroup zmstseatgroup, CancellationToken cancellationToken)

        {
            if (zmstseatgroup.Id == "0")
            {
                throw new ArgumentException(nameof(zmstseatgroup.Id));
            }

            Data.EF.Models.ZmstSeatGroup entityUpd = await unitOfWork.ZmstSeatGroupRepository.FindByAsync(e => e.Id == zmstseatgroup.Id, cancellationToken);
            entityUpd.Description= zmstseatgroup.Description;
            entityUpd.AlternateNames= zmstseatgroup.AlternateNames;
            if (entityUpd != null)
            {

                await unitOfWork.ZmstSeatGroupRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstSeatGroupRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstSeatGroupRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSeatGroupRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

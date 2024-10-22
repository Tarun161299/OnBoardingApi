
//-----------------------------------------------------------------------
// <copyright file="ZmstTradeDirector.cs" company="NIC">
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
    public class ZmstTradeDirector : IZmstTradeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstTradeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstTradeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstTrade>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmsttradelist = await this.unitOfWork.ZmstTradeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstTrade>>(zmsttradelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstTrade> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmsttradelist = await this.unitOfWork.ZmstTradeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstTrade>(zmsttradelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstTrade zmsttrade, CancellationToken cancellationToken)
        {
            if (zmsttrade == null)
            {
                throw new ArgumentNullException(nameof(zmsttrade));
            }

            var chkefzmsttrade = await this.unitOfWork.ZmstTradeRepository.FindByAsync(r => r.Id == zmsttrade.Id, default);
            if (chkefzmsttrade != null)
            {
                throw new EntityFoundException($"This Records {chkefzmsttrade} already exists");
            }

            var efzmsttrade = this.mapper.Map<Data.EF.Models.ZmstTrade>(zmsttrade);

            await this.unitOfWork.ZmstTradeRepository.InsertAsync(efzmsttrade, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstTrade zmsttrade, CancellationToken cancellationToken)

        {
            if (zmsttrade.Id == "0")
            {
                throw new ArgumentException(nameof(zmsttrade.Id));
            }

            Data.EF.Models.ZmstTrade entityUpd = await unitOfWork.ZmstTradeRepository.FindByAsync(e => e.Id == zmsttrade.Id, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.Description = zmsttrade.Description;
                entityUpd.AlternateNames = zmsttrade.AlternateNames;

                await unitOfWork.ZmstTradeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

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

            var entity = await this.unitOfWork.ZmstTradeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstTradeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstTradeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}


//-----------------------------------------------------------------------
// <copyright file="ZmstSeatTypeDirector.cs" company="NIC">
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
    public class ZmstSeatTypeDirector : IZmstSeatTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSeatTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSeatType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstseattypelist = await this.unitOfWork.ZmstSeatTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSeatType>>(zmstseattypelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSeatType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstseattypelist = await this.unitOfWork.ZmstSeatTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSeatType>(zmstseattypelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSeatType zmstseattype, CancellationToken cancellationToken)
        {
            if (zmstseattype == null)
            {
                throw new ArgumentNullException(nameof(zmstseattype));
            }

            var chkefzmstseattype = await this.unitOfWork.ZmstSeatTypeRepository.FindByAsync(r => r.Id == zmstseattype.Id, default);
            if (chkefzmstseattype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstseattype} already exists");
            }

            var efzmstseattype = this.mapper.Map<Data.EF.Models.ZmstSeatType>(zmstseattype);

            await this.unitOfWork.ZmstSeatTypeRepository.InsertAsync(efzmstseattype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSeatType zmstseattype, CancellationToken cancellationToken)

        {
            if (zmstseattype.Id == "0")
            {
                throw new ArgumentException(nameof(zmstseattype.Id));
            }

            Data.EF.Models.ZmstSeatType entityUpd = await unitOfWork.ZmstSeatTypeRepository.FindByAsync(e => e.Id == zmstseattype.Id, cancellationToken);
            entityUpd.Description = zmstseattype.Description;
            entityUpd.AlternateNames = zmstseattype.AlternateNames;
            if (entityUpd != null)
            {

                await unitOfWork.ZmstSeatTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

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

            var entity = await this.unitOfWork.ZmstSeatTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstSeatTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSeatTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

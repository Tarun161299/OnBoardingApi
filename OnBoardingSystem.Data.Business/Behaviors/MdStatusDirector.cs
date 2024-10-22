
//-----------------------------------------------------------------------
// <copyright file="MdStatusDirector.cs" company="NIC">
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
    public class MdStatusDirector : IMdStatusDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdStatusDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdStatusDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdStatus>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdstatuslist = await this.unitOfWork.mdstatusDirectorRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdStatus>>(mdstatuslist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdStatus> GetByIdAsync(string StatusId, CancellationToken cancellationToken)
        {
            var mdstatuslist = await this.unitOfWork.mdstatusDirectorRepository.FindByAsync(x => x.StatusId == StatusId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdStatus>(mdstatuslist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdStatus mdstatus, CancellationToken cancellationToken)
        {
            if (mdstatus == null)
            {
                throw new ArgumentNullException(nameof(mdstatus));
            }

            var chkefmdstatus = await this.unitOfWork.mdstatusDirectorRepository.FindByAsync(r => r.StatusId == mdstatus.StatusId, default);
            if (chkefmdstatus != null)
            {
                throw new EntityFoundException($"This Records {chkefmdstatus} already exists");
            }

            var efmdstatus = this.mapper.Map<Data.EF.Models.MdStatus>(mdstatus);

            await this.unitOfWork.mdstatusDirectorRepository.InsertAsync(efmdstatus, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.MdStatus mdstatus, CancellationToken cancellationToken)

        {
            if (mdstatus.StatusId == "0")
            {
                throw new ArgumentException(nameof(mdstatus.StatusId));
            }

            Data.EF.Models.MdStatus entityUpd = await unitOfWork.mdstatusDirectorRepository.FindByAsync(e => e.StatusId == mdstatus.StatusId, cancellationToken);
            if (entityUpd != null)
            {

                await unitOfWork.mdstatusDirectorRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string StatusId, CancellationToken cancellationToken)
        {
            if (StatusId == "0")
            {
                throw new ArgumentNullException(nameof(StatusId));
            }

            var entity = await this.unitOfWork.mdstatusDirectorRepository.FindByAsync(x => x.StatusId == StatusId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an StatusId {StatusId} was not found.");
            }

            await this.unitOfWork.mdstatusDirectorRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.mdstatusDirectorRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

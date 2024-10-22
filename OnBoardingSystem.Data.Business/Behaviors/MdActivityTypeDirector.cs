
//-----------------------------------------------------------------------
// <copyright file="MdActivityTypeDirector.cs" company="NIC">
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
    public class MdActivityTypeDirector : IMdActivityTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActivityTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdActivityTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdActivityType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdactivitytypelist = await this.unitOfWork.MdActivityTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdActivityType>>(mdactivitytypelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdActivityType> GetByIdAsync(int ActivityId, CancellationToken cancellationToken)
        {
            var mdactivitytypelist = await this.unitOfWork.MdActivityTypeRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdActivityType>(mdactivitytypelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdActivityType mdactivitytype, CancellationToken cancellationToken)
        {
            if (mdactivitytype == null)
            {
                throw new ArgumentNullException(nameof(mdactivitytype));
            }

            var chkefmdactivitytype = await this.unitOfWork.MdActivityTypeRepository.FindByAsync(r => r.ActivityId == mdactivitytype.ActivityId, default);
            if (chkefmdactivitytype != null)
            {
                throw new EntityFoundException($"This Records {chkefmdactivitytype} already exists");
            }

            var efmdactivitytype = this.mapper.Map<Data.EF.Models.MdActivityType>(mdactivitytype);

            await this.unitOfWork.MdActivityTypeRepository.InsertAsync(efmdactivitytype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.MdActivityType mdactivitytype, CancellationToken cancellationToken)

        {
            if (mdactivitytype.ActivityId == 0)
            {
                throw new ArgumentException(nameof(mdactivitytype.ActivityId));
            }

            Data.EF.Models.MdActivityType entityUpd = await unitOfWork.MdActivityTypeRepository.FindByAsync(e => e.ActivityId == mdactivitytype.ActivityId, cancellationToken);
            if (entityUpd != null)
            {

                await unitOfWork.MdActivityTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int ActivityId, CancellationToken cancellationToken)
        {
            if (ActivityId == 0)
            {
                throw new ArgumentNullException(nameof(ActivityId));
            }

            var entity = await this.unitOfWork.MdActivityTypeRepository.FindByAsync(x => x.ActivityId == ActivityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ActivityId {ActivityId} was not found.");
            }

            await this.unitOfWork.MdActivityTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdActivityTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

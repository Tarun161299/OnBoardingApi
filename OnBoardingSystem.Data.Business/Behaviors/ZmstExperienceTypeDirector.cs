
//-----------------------------------------------------------------------
// <copyright file="ZmstExperienceTypeDirector.cs" company="NIC">
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
    public class ZmstExperienceTypeDirector : IZmstExperienceTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstExperienceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstExperienceTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstExperienceType>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var zmstexperiencetypelist = await this.unitOfWork.ZmstExperienceTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return this.mapper.Map<List<AbsModels.ZmstExperienceType>>(zmstexperiencetypelist);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstExperienceType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstexperiencetypelist = await this.unitOfWork.ZmstExperienceTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstExperienceType>(zmstexperiencetypelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstExperienceType zmstexperiencetype, CancellationToken cancellationToken)
        {
            if (zmstexperiencetype == null)
            {
                throw new ArgumentNullException(nameof(zmstexperiencetype));
            }

            var chkefzmstexperiencetype = await this.unitOfWork.ZmstExperienceTypeRepository.FindByAsync(r => r.Id == zmstexperiencetype.Id, default);
            if (chkefzmstexperiencetype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstexperiencetype} already exists");
            }

            var efzmstexperiencetype = this.mapper.Map<Data.EF.Models.ZmstExperienceType>(zmstexperiencetype);

            await this.unitOfWork.ZmstExperienceTypeRepository.InsertAsync(efzmstexperiencetype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstExperienceType zmstexperiencetype, CancellationToken cancellationToken)

        {
            if (zmstexperiencetype.Id == "0")
            {
                throw new ArgumentException(nameof(zmstexperiencetype.Id));
            }

            Data.EF.Models.ZmstExperienceType entityUpd = await unitOfWork.ZmstExperienceTypeRepository.FindByAsync(e => e.Id == zmstexperiencetype.Id, cancellationToken);
            entityUpd.ExperienceType = zmstexperiencetype.ExperienceType;
            if (entityUpd != null)
            {

                await unitOfWork.ZmstExperienceTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

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

            var entity = await this.unitOfWork.ZmstExperienceTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstExperienceTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstExperienceTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

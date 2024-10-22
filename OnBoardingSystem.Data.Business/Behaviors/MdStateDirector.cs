
//-----------------------------------------------------------------------
// <copyright file="MdStateDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Business.Services;
    using Microsoft.AspNetCore.Http;

    /// <inheritdoc />
    public class MdStateDirector : IMdStateDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly MDApiKeyValidateService _mDApiKeyValidateService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdStateDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdStateDirector(IMapper mapper, IUnitOfWork unitOfWork, MDApiKeyValidateService mDApiKeyValidateService, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this._mDApiKeyValidateService = mDApiKeyValidateService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdState>> GetAllAsync(CancellationToken cancellationToken)
        {
            
                var mdstatelist = await this.unitOfWork.MdStateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return this.mapper.Map<List<AbsModels.MdState>>(mdstatelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdState> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mdstatelist = await this.unitOfWork.MdStateRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdState>(mdstatelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdState mdstate, CancellationToken cancellationToken)
        {
            if (mdstate == null)
            {
                throw new ArgumentNullException(nameof(mdstate));
            }

            var chkefmdstate = await this.unitOfWork.MdStateRepository.FindByAsync(r => r.Id == mdstate.Id, default);
            if (chkefmdstate != null)
            {
                throw new EntityFoundException($"This Records {chkefmdstate} already exists");
            }

            var efmdstate = this.mapper.Map<Data.EF.Models.MdState>(mdstate);

            await this.unitOfWork.MdStateRepository.InsertAsync(efmdstate, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.MdState mdstate, string Id, CancellationToken cancellationToken)

        {
            //try {
            if (mdstate.Id == "0")
            {
                throw new ArgumentException(nameof(mdstate.Id));
            }
            if (mdstate == null)
            {
                throw new ArgumentNullException(nameof(mdstate));
            }

            var chkefmdstate = await this.unitOfWork.MdStateRepository.FindByAsync(r => r.Id == mdstate.Id, default);
            if (chkefmdstate == null)
            {
                throw new EntityFoundException($"This Records {chkefmdstate} already exists");
            }
            Data.EF.Models.MdState entityUpd = await unitOfWork.MdStateRepository.FindByAsync(e => e.Id == Id, cancellationToken);
            Data.EF.Models.MdState entityDel = await unitOfWork.MdStateRepository.FindByAsync(e => e.Id == Id, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.Id = mdstate.Id;
                entityUpd.Description = mdstate.Description;
                entityUpd.CreatedDate = mdstate.CreatedDate;
                entityUpd.CreatedBy = mdstate.CreatedBy;
                entityUpd.ModifiedDate = mdstate.ModifiedDate;
                entityUpd.ModifiedBy = mdstate.ModifiedBy;

                await this.unitOfWork.MdStateRepository.DeleteAsync(entityDel, cancellationToken).ConfigureAwait(false);
                await this.unitOfWork.MdStateRepository.InsertAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            try
            {
                if (Id == "0")
                {
                    throw new ArgumentNullException(nameof(Id));
                }

                var entity = await this.unitOfWork.MdStateRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
                var entityDistrict= await this.unitOfWork.MdDistrictRepository.FindByAsync(x => x.StateId == Id, cancellationToken).ConfigureAwait(false);
                if (entityDistrict != null)
                {
                    throw new Exception("Data Can't Be Deleted.");
                }

                if (entity == null)
                {
                    throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
                }

                await this.unitOfWork.MdStateRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
                return await this.unitOfWork.MdStateRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

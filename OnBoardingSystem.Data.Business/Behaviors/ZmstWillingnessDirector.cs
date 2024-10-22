
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
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using EFModel = OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class ZmstWillingnessDirector : IZmstWillingnessDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstWillingnessDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstWillingness>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var servicelist = await this.unitOfWork.ZmstWillingnessRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return this.mapper.Map<List<Abstractions.Models.ZmstWillingness>>(servicelist);
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }
        /// <inheritdoc />
        public async Task<int> InsertAsync(Abstractions.Models.ZmstWillingness zmstWillingnessData, CancellationToken cancellationToken)
        {
            if (zmstWillingnessData == null)
            {
                throw new ArgumentNullException(nameof(zmstWillingnessData));
            }

            var chkefmddistrict = await this.unitOfWork.ZmstWillingnessRepository.FindByAsync(r => r.WillingnessId == zmstWillingnessData.WillingnessId && r.Description == zmstWillingnessData.Description, default);
            if (chkefmddistrict != null)
            {
                throw new EntityFoundException($"This Records {chkefmddistrict} already exists");
            }
            var efmddistrict = this.mapper.Map<Data.EF.Models.ZmstWillingness>(zmstWillingnessData);
            await this.unitOfWork.ZmstWillingnessRepository.InsertAsync(efmddistrict, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(Abstractions.Models.ZmstWillingness zmstWillingnessId, CancellationToken cancellationToken)
        {
            if (zmstWillingnessId.WillingnessId == "0")
            {
                throw new ArgumentException(nameof(zmstWillingnessId.WillingnessId));
            }
            EFModel.ZmstWillingness data = await unitOfWork.ZmstWillingnessRepository.FindByAsync(e => e.WillingnessId == zmstWillingnessId.WillingnessId, cancellationToken);
            data.WillingnessId = zmstWillingnessId.WillingnessId;
            data.Description = zmstWillingnessId.Description;
            data.AlternateNames = zmstWillingnessId.AlternateNames;
            await unitOfWork.ZmstWillingnessRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);
            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstWillingnessRepository.FindByAsync(x => x.WillingnessId == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstWillingnessRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstWillingnessRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

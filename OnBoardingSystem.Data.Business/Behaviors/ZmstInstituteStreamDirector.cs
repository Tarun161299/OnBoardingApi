
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteStreamDirector.cs" company="NIC">
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
    using Abp.Domain.Entities;

    /// <inheritdoc />
    public class ZmstInstituteStreamDirector : IZmstInstituteStreamDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteStreamDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstInstituteStreamDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstInstituteStream>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstinstitutestreamlist = await this.unitOfWork.ZmstInstituteStreamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstInstituteStream>>(zmstinstitutestreamlist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstInstituteStream> GetByIdAsync(string InstCd, CancellationToken cancellationToken)
        {
            var zmstinstitutestreamlist = await this.unitOfWork.ZmstInstituteStreamRepository.FindByAsync(x => x.InstCd == InstCd, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstInstituteStream>(zmstinstitutestreamlist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(string instcode,List<AbsModels.ZmstInstituteStream> zmstinstitutestream, CancellationToken cancellationToken)
        {
            try
            {
                //string instCd = zmstinstitutestream[0].InstCd;
                string instCd = instcode;
                await this.unitOfWork.ZmstInstituteStreamRepository.DeleteAsync(x => x.InstCd == instCd, cancellationToken).ConfigureAwait(false);
                await unitOfWork.CommitAsync(cancellationToken);
                var efzmstinstitutestream = this.mapper.Map<List<Data.EF.Models.ZmstInstituteStream>>(zmstinstitutestream);
                await this.unitOfWork.ZmstInstituteStreamRepository.InsertAsync(efzmstinstitutestream, cancellationToken).ConfigureAwait(false);
                return await unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstInstituteStream zmstinstitutestream, CancellationToken cancellationToken)

        {
            if (zmstinstitutestream.InstCd == "0")
            {
                throw new ArgumentException(nameof(zmstinstitutestream.InstCd));
            }

            Data.EF.Models.ZmstInstituteStream entityUpd = await unitOfWork.ZmstInstituteStreamRepository.FindByAsync(e => e.InstCd == zmstinstitutestream.InstCd, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.InstCd = zmstinstitutestream.InstCd;
                entityUpd.StreamId = zmstinstitutestream.StreamId;

                await unitOfWork.ZmstInstituteStreamRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken)
        {
            if (InstCd == "0")
            {
                throw new ArgumentNullException(nameof(InstCd));
            }

            var entity = await this.unitOfWork.ZmstInstituteStreamRepository.FindByAsync(x => x.InstCd == InstCd, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
              //  throw new EntityNotFoundException($"The Data with an InstCd {InstCd} was not found.");
            }

            await this.unitOfWork.ZmstInstituteStreamRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstInstituteStreamRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

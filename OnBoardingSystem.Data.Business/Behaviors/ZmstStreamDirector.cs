
//-----------------------------------------------------------------------
// <copyright file="ZmstStreamDirector.cs" company="NIC">
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
    using System.IO;

    /// <inheritdoc />
    public class ZmstStreamDirector : IZmstStreamDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstStreamDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstStreamDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstStream>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmststreamlist = await this.unitOfWork.ZmstStreamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstStream>>(zmststreamlist);
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstStream>> GetListByInstcdAsync(string Instcd,CancellationToken cancellationToken)
        {
            var zmststreamlist = await this.unitOfWork.ZmstStreamRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstInstituteStream = await this.unitOfWork.ZmstInstituteStreamRepository.FindAllByAsync(x=>x.InstCd== Instcd, cancellationToken).ConfigureAwait(false);
            var result = from streamList in zmststreamlist
                         join instituteStream in zmstInstituteStream on streamList.StreamId equals instituteStream.StreamId
                         into templist
                         from instituteStream in templist.DefaultIfEmpty() 
                         select new AbsModels.ZmstStream
                         {
                             StreamId = streamList.StreamId,
                             StreamName = streamList.StreamName,
                             AlternateNames = streamList.AlternateNames,
                             instCd = (instituteStream==null)?"": instituteStream.InstCd,
                         };
            return this.mapper.Map<List<AbsModels.ZmstStream>>(result);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstStream> GetByIdAsync(string StreamId, CancellationToken cancellationToken)
        {
            var zmststreamlist = await this.unitOfWork.ZmstStreamRepository.FindByAsync(x => x.StreamId == StreamId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstStream>(zmststreamlist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstStream zmststream, CancellationToken cancellationToken)
        {
            if (zmststream == null)
            {
                throw new ArgumentNullException(nameof(zmststream));
            }

            var chkefzmststream = await this.unitOfWork.ZmstStreamRepository.FindByAsync(r => r.StreamId == zmststream.StreamId, default);
            if (chkefzmststream != null)
            {
                throw new EntityFoundException($"This Records {chkefzmststream} already exists");
            }

            var efzmststream = this.mapper.Map<Data.EF.Models.ZmstStream>(zmststream);

            await this.unitOfWork.ZmstStreamRepository.InsertAsync(efzmststream, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstStream zmststream, CancellationToken cancellationToken)

        {
            if (zmststream.StreamId == "0")
            {
                throw new ArgumentException(nameof(zmststream.StreamId));
            }

            Data.EF.Models.ZmstStream entityUpd = await unitOfWork.ZmstStreamRepository.FindByAsync(e => e.StreamId == zmststream.StreamId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.StreamId = zmststream.StreamId;
                entityUpd.StreamName = zmststream.StreamName;
                entityUpd.AlternateNames = zmststream.AlternateNames;

                await unitOfWork.ZmstStreamRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string StreamId, CancellationToken cancellationToken)
        {
            if (StreamId == "0")
            {
                throw new ArgumentNullException(nameof(StreamId));
            }

            var entity = await this.unitOfWork.ZmstStreamRepository.FindByAsync(x => x.StreamId == StreamId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an StreamId {StreamId} was not found.");
            }

            await this.unitOfWork.ZmstStreamRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstStreamRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}


//-----------------------------------------------------------------------
// <copyright file="ZmstAgencyVirtualDirectoryMappingDirector.cs" company="NIC">
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
    public class ZmstAgencyVirtualDirectoryMappingDirector : IZmstAgencyVirtualDirectoryMappingDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAgencyVirtualDirectoryMappingDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstAgencyVirtualDirectoryMappingDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstAgencyVirtualDirectoryMapping>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstagencyvirtualdirectorymappinglist = await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstAgency = await this.unitOfWork.ZmstAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = from zmstVirtual in zmstagencyvirtualdirectorymappinglist
                         join agency in zmstAgency on zmstVirtual.AgencyId equals agency.AgencyId
                         select new AbsModels.ZmstAgencyVirtualDirectoryMapping
                         {
                             AgencyId = zmstVirtual.AgencyId,
                             AgencyName = agency.AgencyName,
                             BaseDirectory = zmstVirtual.BaseDirectory,
                             VirtualDirectory = zmstVirtual.VirtualDirectory,
                             VirtualDirectoryType = zmstVirtual.VirtualDirectoryType,
                             IsActive = zmstVirtual.IsActive,

                         };
            return this.mapper.Map<List<AbsModels.ZmstAgencyVirtualDirectoryMapping>>(result);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstAgencyVirtualDirectoryMapping> GetByIdAsync(string AgencyId, CancellationToken cancellationToken)
        {
            var zmstagencyvirtualdirectorymappinglist = await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstAgencyVirtualDirectoryMapping>(zmstagencyvirtualdirectorymappinglist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping, CancellationToken cancellationToken)
        {
            if (zmstagencyvirtualdirectorymapping == null)
            {
                throw new ArgumentNullException(nameof(zmstagencyvirtualdirectorymapping));
            }

            var chkefzmstagencyvirtualdirectorymapping = await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.FindByAsync(r => r.AgencyId == zmstagencyvirtualdirectorymapping.AgencyId && r.VirtualDirectoryType== zmstagencyvirtualdirectorymapping.VirtualDirectoryType, default);
            if (chkefzmstagencyvirtualdirectorymapping != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstagencyvirtualdirectorymapping} already exists");
            }

            var efzmstagencyvirtualdirectorymapping = this.mapper.Map<Data.EF.Models.ZmstAgencyVirtualDirectoryMapping>(zmstagencyvirtualdirectorymapping);

            await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.InsertAsync(efzmstagencyvirtualdirectorymapping, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstAgencyVirtualDirectoryMapping zmstagencyvirtualdirectorymapping, CancellationToken cancellationToken)

        {
            if (zmstagencyvirtualdirectorymapping.AgencyId == "0")
            {
                throw new ArgumentException(nameof(zmstagencyvirtualdirectorymapping.AgencyId));
            }

            Data.EF.Models.ZmstAgencyVirtualDirectoryMapping entityUpd = await unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.FindByAsync(e => e.AgencyId == zmstagencyvirtualdirectorymapping.AgencyId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.AgencyId = zmstagencyvirtualdirectorymapping.AgencyId;
                entityUpd.BaseDirectory = zmstagencyvirtualdirectorymapping.BaseDirectory;
                entityUpd.VirtualDirectory = zmstagencyvirtualdirectorymapping.VirtualDirectory;
                entityUpd.VirtualDirectoryType = zmstagencyvirtualdirectorymapping.VirtualDirectoryType;
                entityUpd.IsActive = zmstagencyvirtualdirectorymapping.IsActive;

                await unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string AgencyId, CancellationToken cancellationToken)
        {
            if (AgencyId == "0")
            {
                throw new ArgumentNullException(nameof(AgencyId));
            }

            var entity = await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an AgencyId {AgencyId} was not found.");
            }

            await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstAgencyVirtualDirectoryMappingRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

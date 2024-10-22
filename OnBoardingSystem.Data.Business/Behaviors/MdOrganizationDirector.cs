//-----------------------------------------------------------------------
// <copyright file="MdOrganizationDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    //using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <inheritdoc />
    public class MdOrganizationDirector : IMdOrganizationDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdOrganizationDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdOrganizationDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdOrganization>> GetAllAsync(CancellationToken cancellationToken)
        {
            var organizationlist = await this.unitOfWork.MdOrganizationRepository.GetAllAsync(cancellationToken);
            var mdstatelist = await this.unitOfWork.StateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = from orglist in organizationlist
                         join statelist in mdstatelist on orglist.StateId.ToString() equals statelist.Id
                            select new Abstractions.Models.MdOrganization
                            {
                                OrganizationId = orglist.OrganizationId,
                                StateName = statelist.Description,
                                OrganizationName = orglist.OrganizationName,
                                StateId=orglist.StateId
                            };

            return this.mapper.Map<List<Abstractions.Models.MdOrganization>>(result);
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdOrganization>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var organizationlist = await this.unitOfWork.MdOrganizationRepository.FindByAsync(x => x.OrganizationId == id.ToString(), cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.MdOrganization>>(organizationlist);
            return result.OrderBy(x => x.OrganizationName).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<List<MdOrganization>> GetByStateIdAsync(string? stateid, CancellationToken cancellationToken)
        {
            var organizationlist = await this.unitOfWork.MdOrganizationRepository.FindAllByAsync(x => new[] { stateid, "-1" }.Contains(x.StateId), cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.MdOrganization>>(organizationlist);
            return result.OrderBy(x => x.OrganizationName).ToList();
        }
        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdOrganization mdorganization, CancellationToken cancellationToken)
        {
            if (mdorganization == null)
            {
                throw new ArgumentNullException(nameof(mdorganization));
            }

            var chkefmdorganization = await this.unitOfWork.MdOrganizationRepository.FindByAsync(r => r.OrganizationId == mdorganization.OrganizationId, default);
            if (chkefmdorganization != null)
            {
                throw new EntityFoundException($"This Records {chkefmdorganization} already exists");
            }

            var efmdorganization = this.mapper.Map<Data.EF.Models.MdOrganization>(mdorganization);

            await this.unitOfWork.MdOrganizationRepository.InsertAsync(efmdorganization, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(AbsModels.MdOrganization mdorganization, CancellationToken cancellationToken)
        {
            if (mdorganization.OrganizationId == "0")
            {
                throw new ArgumentException(nameof(mdorganization.OrganizationId));
            }

            Data.EF.Models.MdOrganization entity = await unitOfWork.MdOrganizationRepository.FindByAsync(e => e.OrganizationId == mdorganization.OrganizationId, cancellationToken);
            if (entity != null)
            {
                entity.OrganizationId = mdorganization.OrganizationId;
                entity.OrganizationName = mdorganization.OrganizationName;
                entity.StateId = mdorganization.StateId;

                await unitOfWork.MdOrganizationRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string OrganizationId, CancellationToken cancellationToken)
        {
            if (OrganizationId == "0")
            {
                throw new ArgumentNullException(nameof(OrganizationId));
            }

            var entity = await this.unitOfWork.MdOrganizationRepository.FindByAsync(x => x.OrganizationId == OrganizationId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an OrganizationId {OrganizationId} was not found.");
            }

            await this.unitOfWork.MdOrganizationRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdOrganizationRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
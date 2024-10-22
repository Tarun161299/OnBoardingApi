
//-----------------------------------------------------------------------
// <copyright file="ZmstDocumentTypeDirector.cs" company="NIC">
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
    public class ZmstDocumentTypeDirector : IZmstDocumentTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstDocumentTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstDocumentTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstDocumentType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstdocumenttypelist = await this.unitOfWork.ZmstDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstDocumentType>>(zmstdocumenttypelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstDocumentType> GetByIdAsync(string DocumentTypeId, CancellationToken cancellationToken)
        {
            var zmstdocumenttypelist = await this.unitOfWork.ZmstDocumentTypeRepository.FindByAsync(x => x.DocumentTypeId == DocumentTypeId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstDocumentType>(zmstdocumenttypelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstDocumentType zmstdocumenttype, CancellationToken cancellationToken)
        {
            if (zmstdocumenttype == null)
            {
                throw new ArgumentNullException(nameof(zmstdocumenttype));
            }

            var chkefzmstdocumenttype = await this.unitOfWork.ZmstDocumentTypeRepository.FindByAsync(r => r.DocumentTypeId == zmstdocumenttype.DocumentTypeId, default);
            if (chkefzmstdocumenttype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstdocumenttype} already exists");
            }

            var efzmstdocumenttype = this.mapper.Map<Data.EF.Models.ZmstDocumentType>(zmstdocumenttype);

            await this.unitOfWork.ZmstDocumentTypeRepository.InsertAsync(efzmstdocumenttype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstDocumentType zmstdocumenttype, CancellationToken cancellationToken)

        {
            if (zmstdocumenttype.DocumentTypeId == "0")
            {
                throw new ArgumentException(nameof(zmstdocumenttype.DocumentTypeId));
            }

            Data.EF.Models.ZmstDocumentType entityUpd = await unitOfWork.ZmstDocumentTypeRepository.FindByAsync(e => e.DocumentTypeId == zmstdocumenttype.DocumentTypeId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.DocumentTypeId = zmstdocumenttype.DocumentTypeId;
                entityUpd.Title = zmstdocumenttype.Title;
                entityUpd.AlternateNames = zmstdocumenttype.AlternateNames;

                await unitOfWork.ZmstDocumentTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string DocumentTypeId, CancellationToken cancellationToken)
        {
            if (DocumentTypeId == "0")
            {
                throw new ArgumentNullException(nameof(DocumentTypeId));
            }

            var entity = await this.unitOfWork.ZmstDocumentTypeRepository.FindByAsync(x => x.DocumentTypeId == DocumentTypeId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an DocumentTypeId {DocumentTypeId} was not found.");
            }

            await this.unitOfWork.ZmstDocumentTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstDocumentTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

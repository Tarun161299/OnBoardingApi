
//-----------------------------------------------------------------------
// <copyright file="MdSmsEmailTemplateDirector.cs" company="NIC">
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
    public class MdSmsEmailTemplateDirector : IMdSmsEmailTemplateDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdSmsEmailTemplateDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdSmsEmailTemplateDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdSmsEmailTemplate>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdsmsemailtemplatelist = await this.unitOfWork.MdSmsEmailTemplateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdSmsEmailTemplate>>(mdsmsemailtemplatelist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdSmsEmailTemplate> GetByIdAsync(string TemplateId, CancellationToken cancellationToken)
        {
            var mdsmsemailtemplatelist = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == TemplateId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdSmsEmailTemplate>(mdsmsemailtemplatelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdSmsEmailTemplate mdsmsemailtemplate, CancellationToken cancellationToken)
        {
            if (mdsmsemailtemplate == null)
            {
                throw new ArgumentNullException(nameof(mdsmsemailtemplate));
            }

            var chkefmdsmsemailtemplate = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(r => r.TemplateId == mdsmsemailtemplate.TemplateId, default);
            if (chkefmdsmsemailtemplate != null)
            {
                throw new EntityFoundException($"This Records {chkefmdsmsemailtemplate} already exists");
            }

            var efmdsmsemailtemplate = this.mapper.Map<Data.EF.Models.MdSmsEmailTemplate>(mdsmsemailtemplate);

            await this.unitOfWork.MdSmsEmailTemplateRepository.InsertAsync(efmdsmsemailtemplate, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(AbsModels.MdSmsEmailTemplate mdsmsemailtemplate, CancellationToken cancellationToken)
        {
            if (mdsmsemailtemplate.TemplateId == "0")
            {
                throw new ArgumentException(nameof(mdsmsemailtemplate.TemplateId));
            }

            Data.EF.Models.MdSmsEmailTemplate entity = await unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(e => e.TemplateId == mdsmsemailtemplate.TemplateId, cancellationToken);
            if (entity != null)
            {
                entity.TemplateId = mdsmsemailtemplate.TemplateId;
                entity.Description = mdsmsemailtemplate.Description;
                entity.MessageTypeId = mdsmsemailtemplate.MessageTypeId;
                entity.MessageSubject = mdsmsemailtemplate.MessageSubject;
                entity.MessageTemplate = mdsmsemailtemplate.MessageTemplate;
                entity.RegisteredTemplateId = mdsmsemailtemplate.RegisteredTemplateId;

                await unitOfWork.MdSmsEmailTemplateRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string TemplateId, CancellationToken cancellationToken)
        {
            if (TemplateId == "0")
            {
                throw new ArgumentNullException(nameof(TemplateId));
            }

            var entity = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == TemplateId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an TemplateId {TemplateId} was not found.");
            }

            await this.unitOfWork.MdSmsEmailTemplateRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdSmsEmailTemplateRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
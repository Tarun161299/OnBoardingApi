
//-----------------------------------------------------------------------
// <copyright file="ZmstSpecialExamPaperDirector.cs" company="NIC">
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
    public class ZmstSpecialExamPaperDirector : IZmstSpecialExamPaperDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSpecialExamPaperDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSpecialExamPaperDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSpecialExamPaper>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstspecialexampaperlist = await this.unitOfWork.ZmstSpecialExamPaperRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstexamtypelist = await this.unitOfWork.ZmstExamTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var specialexampaperList = from specialexampaper in zmstspecialexampaperlist
                                       join examtype in zmstexamtypelist on specialexampaper.SpecialExamId equals examtype.Id
                                       select new Abstractions.Models.ZmstSpecialExamPaper
                                       {
                                           Id = specialexampaper.Id,
                                           Description = specialexampaper.Description,
                                           AlternateNames = specialexampaper.AlternateNames,
                                           SpecialExamId = specialexampaper.SpecialExamId,
                                           SpecialExamName = examtype.Description,
                                       };

            return this.mapper.Map<List<AbsModels.ZmstSpecialExamPaper>>(specialexampaperList);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSpecialExamPaper> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var zmstspecialexampaperlist = await this.unitOfWork.ZmstSpecialExamPaperRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSpecialExamPaper>(zmstspecialexampaperlist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSpecialExamPaper zmstspecialexampaper, CancellationToken cancellationToken)
        {
            if (zmstspecialexampaper == null)
            {
                throw new ArgumentNullException(nameof(zmstspecialexampaper));
            }

            var chkefzmstspecialexampaper = await this.unitOfWork.ZmstSpecialExamPaperRepository.FindByAsync(r => r.Id == zmstspecialexampaper.Id, default);
            if (chkefzmstspecialexampaper != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstspecialexampaper} already exists");
            }

            var efzmstspecialexampaper = this.mapper.Map<Data.EF.Models.ZmstSpecialExamPaper>(zmstspecialexampaper);

            await this.unitOfWork.ZmstSpecialExamPaperRepository.InsertAsync(efzmstspecialexampaper, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSpecialExamPaper zmstspecialexampaper, CancellationToken cancellationToken)

        {
            try
            {
                if (zmstspecialexampaper.Id == "0")
                {
                    throw new ArgumentException(nameof(zmstspecialexampaper.Id));
                }

                Data.EF.Models.ZmstSpecialExamPaper entityUpd = await unitOfWork.ZmstSpecialExamPaperRepository.FindByAsync(e => e.Id == zmstspecialexampaper.Id, cancellationToken);
                if (entityUpd != null)
                {
                    entityUpd.Id = zmstspecialexampaper.Id;
                    entityUpd.Description = zmstspecialexampaper.Description;
                    entityUpd.AlternateNames = zmstspecialexampaper.AlternateNames;
                    entityUpd.SpecialExamId = zmstspecialexampaper.SpecialExamId;

                    await this.unitOfWork.ZmstSpecialExamPaperRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

                }

                return await this.unitOfWork.CommitAsync(cancellationToken);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstSpecialExamPaperRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstSpecialExamPaperRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSpecialExamPaperRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

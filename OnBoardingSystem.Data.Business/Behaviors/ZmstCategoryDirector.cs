
//-----------------------------------------------------------------------
// <copyright file="ZmstCategoryDirector.cs" company="NIC">
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
    public class ZmstCategoryDirector : IZmstCategoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCategoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstCategoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstcategorylist = await this.unitOfWork.ZmstCategoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstCategory>>(zmstcategorylist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstCategory> GetByIdAsync(string CategoryId, CancellationToken cancellationToken)
        {
            var zmstcategorylist = await this.unitOfWork.ZmstCategoryRepository.FindByAsync(x => x.CategoryId == CategoryId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstCategory>(zmstcategorylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstCategory zmstcategory, CancellationToken cancellationToken)
        {
            if (zmstcategory == null)
            {
                throw new ArgumentNullException(nameof(zmstcategory));
            }

            var chkefzmstcategory = await this.unitOfWork.ZmstCategoryRepository.FindByAsync(r => r.CategoryId == zmstcategory.CategoryId, default);
            if (chkefzmstcategory != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstcategory} already exists");
            }

            var efzmstcategory = this.mapper.Map<Data.EF.Models.ZmstCategory>(zmstcategory);

            await this.unitOfWork.ZmstCategoryRepository.InsertAsync(efzmstcategory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstCategory zmstcategory, CancellationToken cancellationToken)

        {
            if (zmstcategory.CategoryId == "0")
            {
                throw new ArgumentException(nameof(zmstcategory.CategoryId));
            }

            Data.EF.Models.ZmstCategory entityUpd = await unitOfWork.ZmstCategoryRepository.FindByAsync(e => e.CategoryId == zmstcategory.CategoryId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.CategoryId = zmstcategory.CategoryId;
                entityUpd.CategoryName = zmstcategory.CategoryName;
                entityUpd.AlternateNames = zmstcategory.AlternateNames;

                await unitOfWork.ZmstCategoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string CategoryId, CancellationToken cancellationToken)
        {
            if (CategoryId == "0")
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }

            var entity = await this.unitOfWork.ZmstCategoryRepository.FindByAsync(x => x.CategoryId == CategoryId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an CategoryId {CategoryId} was not found.");
            }

            await this.unitOfWork.ZmstCategoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstCategoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

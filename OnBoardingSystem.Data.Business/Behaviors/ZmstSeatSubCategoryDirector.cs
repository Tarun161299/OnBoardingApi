
//-----------------------------------------------------------------------
// <copyright file="ZmstSeatSubCategoryDirector.cs" company="NIC">
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
    public class ZmstSeatSubCategoryDirector : IZmstSeatSubCategoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSeatSubCategoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSeatSubCategoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSeatSubCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstseatsubcategorylist = await this.unitOfWork.ZmstSeatSubCategoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSeatSubCategory>>(zmstseatsubcategorylist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSeatSubCategory> GetByIdAsync(string SeatSubCategoryId, CancellationToken cancellationToken)
        {
            var zmstseatsubcategorylist = await this.unitOfWork.ZmstSeatSubCategoryRepository.FindByAsync(x => x.SeatSubCategoryId == SeatSubCategoryId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSeatSubCategory>(zmstseatsubcategorylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSeatSubCategory zmstseatsubcategory, CancellationToken cancellationToken)
        {
            if (zmstseatsubcategory == null)
            {
                throw new ArgumentNullException(nameof(zmstseatsubcategory));
            }

            var chkefzmstseatsubcategory = await this.unitOfWork.ZmstSeatSubCategoryRepository.FindByAsync(r => r.SeatSubCategoryId == zmstseatsubcategory.SeatSubCategoryId, default);
            if (chkefzmstseatsubcategory != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstseatsubcategory} already exists");
            }

            var efzmstseatsubcategory = this.mapper.Map<Data.EF.Models.ZmstSeatSubCategory>(zmstseatsubcategory);

            await this.unitOfWork.ZmstSeatSubCategoryRepository.InsertAsync(efzmstseatsubcategory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSeatSubCategory zmstseatsubcategory, CancellationToken cancellationToken)

        {
            if (zmstseatsubcategory.SeatSubCategoryId == "0")
            {
                throw new ArgumentException(nameof(zmstseatsubcategory.SeatSubCategoryId));
            }

            Data.EF.Models.ZmstSeatSubCategory entityUpd = await unitOfWork.ZmstSeatSubCategoryRepository.FindByAsync(e => e.SeatSubCategoryId == zmstseatsubcategory.SeatSubCategoryId, cancellationToken);
            entityUpd.Description = zmstseatsubcategory.Description;
            entityUpd.Alternatenames = zmstseatsubcategory.Alternatenames;
            if (entityUpd != null)
            {

                await unitOfWork.ZmstSeatSubCategoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string SeatSubCategoryId, CancellationToken cancellationToken)
        {
            if (SeatSubCategoryId == "0")
            {
                throw new ArgumentNullException(nameof(SeatSubCategoryId));
            }

            var entity = await this.unitOfWork.ZmstSeatSubCategoryRepository.FindByAsync(x => x.SeatSubCategoryId == SeatSubCategoryId, cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SeatSubCategoryId {SeatSubCategoryId} was not found.");
            }

            await this.unitOfWork.ZmstSeatSubCategoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSeatSubCategoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

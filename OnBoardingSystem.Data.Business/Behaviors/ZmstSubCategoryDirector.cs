
//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategoryDirector.cs" company="NIC">
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
    public class ZmstSubCategoryDirector : IZmstSubCategoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSubCategoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSubCategoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSubCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstsubcategorylist = await this.unitOfWork.ZmstSubCategoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSubCategory>>(zmstsubcategorylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSubCategory> GetByIdAsync(string SubCategoryId, CancellationToken cancellationToken)
        {
            var zmstsubcategorylist = await this.unitOfWork.ZmstSubCategoryRepository.FindByAsync(x => x.SubCategoryId == SubCategoryId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSubCategory>(zmstsubcategorylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSubCategory zmstsubcategory, CancellationToken cancellationToken)
        {
            if (zmstsubcategory == null)
            {
                throw new ArgumentNullException(nameof(zmstsubcategory));
            }

            var chkefzmstsubcategory = await this.unitOfWork.ZmstSubCategoryRepository.FindByAsync(r => r.SubCategoryId == zmstsubcategory.SubCategoryId, default);
            if (chkefzmstsubcategory != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstsubcategory} already exists");
            }

            var efzmstsubcategory = this.mapper.Map<Data.EF.Models.ZmstSubCategory>(zmstsubcategory);

            await this.unitOfWork.ZmstSubCategoryRepository.InsertAsync(efzmstsubcategory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSubCategory zmstsubcategory, CancellationToken cancellationToken)
        
		{
            if (zmstsubcategory.SubCategoryId == "0")
            {
                throw new ArgumentException(nameof(zmstsubcategory.SubCategoryId));
            }
			
			Data.EF.Models.ZmstSubCategory entityUpd = await unitOfWork.ZmstSubCategoryRepository.FindByAsync(e => e.SubCategoryId == zmstsubcategory.SubCategoryId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SubCategoryId= zmstsubcategory.SubCategoryId;
					entityUpd.SubCategoryName= zmstsubcategory.SubCategoryName;
					entityUpd.AlternateNames= zmstsubcategory.AlternateNames;
					
				await unitOfWork.ZmstSubCategoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SubCategoryId, CancellationToken cancellationToken)
        {
            if (SubCategoryId == "0")
            {
                throw new ArgumentNullException(nameof(SubCategoryId));
            }

            var entity = await this.unitOfWork.ZmstSubCategoryRepository.FindByAsync(x => x.SubCategoryId == SubCategoryId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SubCategoryId {SubCategoryId} was not found.");
            }

            await this.unitOfWork.ZmstSubCategoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSubCategoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
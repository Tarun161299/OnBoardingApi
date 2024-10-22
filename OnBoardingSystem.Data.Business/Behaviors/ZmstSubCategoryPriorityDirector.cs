
//-----------------------------------------------------------------------
// <copyright file="ZmstSubCategoryPriorityDirector.cs" company="NIC">
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
    public class ZmstSubCategoryPriorityDirector : IZmstSubCategoryPriorityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSubCategoryPriorityDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSubCategoryPriorityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSubCategoryPriority>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstsubcategoryprioritylist = await this.unitOfWork.ZmstSubCategoryPriorityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstsubcategorylist = await this.unitOfWork.ZmstSubCategoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            var subcategoryprioritylist = from ubcategoryprioritylist in zmstsubcategoryprioritylist
                                          join subcategorylist in zmstsubcategorylist on ubcategoryprioritylist.SubCategoryId equals subcategorylist.SubCategoryId
                            select new Abstractions.Models.ZmstSubCategoryPriority
                            {
                                SubCategoryPriorityId = ubcategoryprioritylist.SubCategoryPriorityId,
                                Description = ubcategoryprioritylist.Description,
                                SubCategoryId = ubcategoryprioritylist.SubCategoryId,
                                AlternateNames = ubcategoryprioritylist.AlternateNames,
                                SubCategoryName = subcategorylist.SubCategoryName,
                            };
            return this.mapper.Map<List<AbsModels.ZmstSubCategoryPriority>>(subcategoryprioritylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSubCategoryPriority> GetByIdAsync(string SubCategoryPriorityId, CancellationToken cancellationToken)
        {
            var zmstsubcategoryprioritylist = await this.unitOfWork.ZmstSubCategoryPriorityRepository.FindByAsync(x => x.SubCategoryPriorityId == SubCategoryPriorityId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSubCategoryPriority>(zmstsubcategoryprioritylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSubCategoryPriority zmstsubcategorypriority, CancellationToken cancellationToken)
        {
            if (zmstsubcategorypriority == null)
            {
                throw new ArgumentNullException(nameof(zmstsubcategorypriority));
            }

            var chkefzmstsubcategorypriority = await this.unitOfWork.ZmstSubCategoryPriorityRepository.FindByAsync(r => r.SubCategoryPriorityId == zmstsubcategorypriority.SubCategoryPriorityId, default);
            if (chkefzmstsubcategorypriority != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstsubcategorypriority} already exists");
            }

            var efzmstsubcategorypriority = this.mapper.Map<Data.EF.Models.ZmstSubCategoryPriority>(zmstsubcategorypriority);

            await this.unitOfWork.ZmstSubCategoryPriorityRepository.InsertAsync(efzmstsubcategorypriority, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSubCategoryPriority zmstsubcategorypriority, CancellationToken cancellationToken)
        
		{
            if (zmstsubcategorypriority.SubCategoryPriorityId == "0")
            {
                throw new ArgumentException(nameof(zmstsubcategorypriority.SubCategoryPriorityId));
            }
			
			Data.EF.Models.ZmstSubCategoryPriority entityUpd = await unitOfWork.ZmstSubCategoryPriorityRepository.FindByAsync(e => e.SubCategoryPriorityId == zmstsubcategorypriority.SubCategoryPriorityId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SubCategoryPriorityId= zmstsubcategorypriority.SubCategoryPriorityId;
					entityUpd.Description= zmstsubcategorypriority.Description;
					entityUpd.SubCategoryId= zmstsubcategorypriority.SubCategoryId;
					entityUpd.AlternateNames= zmstsubcategorypriority.AlternateNames;
					
				await unitOfWork.ZmstSubCategoryPriorityRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SubCategoryPriorityId, CancellationToken cancellationToken)
        {
            if (SubCategoryPriorityId == "0")
            {
                throw new ArgumentNullException(nameof(SubCategoryPriorityId));
            }

            var entity = await this.unitOfWork.ZmstSubCategoryPriorityRepository.FindByAsync(x => x.SubCategoryPriorityId == SubCategoryPriorityId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SubCategoryPriorityId {SubCategoryPriorityId} was not found.");
            }

            await this.unitOfWork.ZmstSubCategoryPriorityRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSubCategoryPriorityRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	

//-----------------------------------------------------------------------
// <copyright file="ZmstCourseAppliedLevelDirector.cs" company="NIC">
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
    public class ZmstCourseAppliedLevelDirector : IZmstCourseAppliedLevelDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCourseAppliedLevelDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstCourseAppliedLevelDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstCourseAppliedLevel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstcourseappliedlevellist = await this.unitOfWork.ZmstCourseAppliedLevelRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstCourseAppliedLevel>>(zmstcourseappliedlevellist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstCourseAppliedLevel> GetByIdAsync(string CourseLevelId, CancellationToken cancellationToken)
        {
            var zmstcourseappliedlevellist = await this.unitOfWork.ZmstCourseAppliedLevelRepository.FindByAsync(x => x.CourseLevelId == CourseLevelId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstCourseAppliedLevel>(zmstcourseappliedlevellist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstCourseAppliedLevel zmstcourseappliedlevel, CancellationToken cancellationToken)
        {
            if (zmstcourseappliedlevel == null)
            {
                throw new ArgumentNullException(nameof(zmstcourseappliedlevel));
            }

            var chkefzmstcourseappliedlevel = await this.unitOfWork.ZmstCourseAppliedLevelRepository.FindByAsync(r => r.CourseLevelId == zmstcourseappliedlevel.CourseLevelId, default);
            if (chkefzmstcourseappliedlevel != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstcourseappliedlevel} already exists");
            }

            var efzmstcourseappliedlevel = this.mapper.Map<Data.EF.Models.ZmstCourseAppliedLevel>(zmstcourseappliedlevel);

            await this.unitOfWork.ZmstCourseAppliedLevelRepository.InsertAsync(efzmstcourseappliedlevel, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstCourseAppliedLevel zmstcourseappliedlevel, CancellationToken cancellationToken)
        
		{
            if (zmstcourseappliedlevel.CourseLevelId == "0")
            {
                throw new ArgumentException(nameof(zmstcourseappliedlevel.CourseLevelId));
            }
			
			Data.EF.Models.ZmstCourseAppliedLevel entityUpd = await unitOfWork.ZmstCourseAppliedLevelRepository.FindByAsync(e => e.CourseLevelId == zmstcourseappliedlevel.CourseLevelId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.CourseLevelId= zmstcourseappliedlevel.CourseLevelId;
					entityUpd.CourseLevelName= zmstcourseappliedlevel.CourseLevelName;
					
				await unitOfWork.ZmstCourseAppliedLevelRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string CourseLevelId, CancellationToken cancellationToken)
        {
            if (CourseLevelId == "0")
            {
                throw new ArgumentNullException(nameof(CourseLevelId));
            }

            var entity = await this.unitOfWork.ZmstCourseAppliedLevelRepository.FindByAsync(x => x.CourseLevelId == CourseLevelId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an CourseLevelId {CourseLevelId} was not found.");
            }

            await this.unitOfWork.ZmstCourseAppliedLevelRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstCourseAppliedLevelRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
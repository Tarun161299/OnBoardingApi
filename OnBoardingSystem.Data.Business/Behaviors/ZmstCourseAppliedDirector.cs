
//-----------------------------------------------------------------------
// <copyright file="ZmstCourseAppliedDirector.cs" company="NIC">
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
    public class ZmstCourseAppliedDirector : IZmstCourseAppliedDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCourseAppliedDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstCourseAppliedDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstCourseApplied>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstcourseappliedlist = await this.unitOfWork.ZmstCourseAppliedRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstCourseApplied>>(zmstcourseappliedlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstCourseApplied> GetByIdAsync(string CourseId, CancellationToken cancellationToken)
        {
            var zmstcourseappliedlist = await this.unitOfWork.ZmstCourseAppliedRepository.FindByAsync(x => x.CourseId == CourseId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstCourseApplied>(zmstcourseappliedlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstCourseApplied zmstcourseapplied, CancellationToken cancellationToken)
        {
            if (zmstcourseapplied == null)
            {
                throw new ArgumentNullException(nameof(zmstcourseapplied));
            }

            var chkefzmstcourseapplied = await this.unitOfWork.ZmstCourseAppliedRepository.FindByAsync(r => r.CourseId == zmstcourseapplied.CourseId, default);
            if (chkefzmstcourseapplied != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstcourseapplied} already exists");
            }

            var efzmstcourseapplied = this.mapper.Map<Data.EF.Models.ZmstCourseApplied>(zmstcourseapplied);

            await this.unitOfWork.ZmstCourseAppliedRepository.InsertAsync(efzmstcourseapplied, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstCourseApplied zmstcourseapplied, CancellationToken cancellationToken)
        
		{
            if (zmstcourseapplied.CourseId == "0")
            {
                throw new ArgumentException(nameof(zmstcourseapplied.CourseId));
            }
			
			Data.EF.Models.ZmstCourseApplied entityUpd = await unitOfWork.ZmstCourseAppliedRepository.FindByAsync(e => e.CourseId == zmstcourseapplied.CourseId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.CourseId= zmstcourseapplied.CourseId;
					entityUpd.CourseName= zmstcourseapplied.CourseName;
					entityUpd.AlternameNames= zmstcourseapplied.AlternameNames;
					
				await unitOfWork.ZmstCourseAppliedRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string CourseId, CancellationToken cancellationToken)
        {
            if (CourseId == "0")
            {
                throw new ArgumentNullException(nameof(CourseId));
            }

            var entity = await this.unitOfWork.ZmstCourseAppliedRepository.FindByAsync(x => x.CourseId == CourseId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an CourseId {CourseId} was not found.");
            }

            await this.unitOfWork.ZmstCourseAppliedRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstCourseAppliedRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
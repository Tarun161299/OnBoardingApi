
//-----------------------------------------------------------------------
// <copyright file="ZmstQualifyingCourseDirector.cs" company="NIC">
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
    public class ZmstQualifyingCourseDirector : IZmstQualifyingCourseDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQualifyingCourseDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQualifyingCourseDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQualifyingCourse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstqualifyingcourselist = await this.unitOfWork.ZmstQualifyingCourseRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstqualificationNamelist = await this.unitOfWork.ZmstQualificationRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var zmstqualifyingcourselistWithName = from qulificationcourserList in zmstqualifyingcourselist
                                                   join qulificationnameList in zmstqualificationNamelist on qulificationcourserList.QualificationId equals qulificationnameList.QualificationId
                                                   select new Abstractions.Models.ZmstQualifyingCourse
                                                   {
                                                       QualificationCourseId = qulificationcourserList.QualificationCourseId,
                                                       QualificationCourseName = qulificationcourserList.QualificationCourseName,
                                                       QualificationId = qulificationcourserList.QualificationId,
                                                       QualificationName = qulificationnameList.Description,
                                                   };
            return this.mapper.Map<List<AbsModels.ZmstQualifyingCourse>>(zmstqualifyingcourselistWithName);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQualifyingCourse> GetByIdAsync(string QualificationCourseId, CancellationToken cancellationToken)
        {
            var zmstqualifyingcourselist = await this.unitOfWork.ZmstQualifyingCourseRepository.FindByAsync(x => x.QualificationCourseId == QualificationCourseId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQualifyingCourse>(zmstqualifyingcourselist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQualifyingCourse zmstqualifyingcourse, CancellationToken cancellationToken)
        {
            if (zmstqualifyingcourse == null)
            {
                throw new ArgumentNullException(nameof(zmstqualifyingcourse));
            }

            var chkefzmstqualifyingcourse = await this.unitOfWork.ZmstQualifyingCourseRepository.FindByAsync(r => r.QualificationCourseId == zmstqualifyingcourse.QualificationCourseId, default);
            if (chkefzmstqualifyingcourse != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstqualifyingcourse} already exists");
            }

            var efzmstqualifyingcourse = this.mapper.Map<Data.EF.Models.ZmstQualifyingCourse>(zmstqualifyingcourse);

            await this.unitOfWork.ZmstQualifyingCourseRepository.InsertAsync(efzmstqualifyingcourse, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQualifyingCourse zmstqualifyingcourse, CancellationToken cancellationToken)

        {
            if (zmstqualifyingcourse.QualificationCourseId == "0")
            {
                throw new ArgumentException(nameof(zmstqualifyingcourse.QualificationCourseId));
            }

            Data.EF.Models.ZmstQualifyingCourse entityUpd = await unitOfWork.ZmstQualifyingCourseRepository.FindByAsync(e => e.QualificationCourseId == zmstqualifyingcourse.QualificationCourseId, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.QualificationCourseId = zmstqualifyingcourse.QualificationCourseId;
                entityUpd.QualificationCourseName = zmstqualifyingcourse.QualificationCourseName;
                entityUpd.QualificationId = zmstqualifyingcourse.QualificationId;

                await unitOfWork.ZmstQualifyingCourseRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string QualificationCourseId, CancellationToken cancellationToken)
        {
            if (QualificationCourseId == "0")
            {
                throw new ArgumentNullException(nameof(QualificationCourseId));
            }

            var entity = await this.unitOfWork.ZmstQualifyingCourseRepository.FindByAsync(x => x.QualificationCourseId == QualificationCourseId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an QualificationCourseId {QualificationCourseId} was not found.");
            }

            await this.unitOfWork.ZmstQualifyingCourseRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQualifyingCourseRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

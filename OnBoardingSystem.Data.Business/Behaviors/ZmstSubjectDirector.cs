using AutoMapper;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Exceptions;
using OnBoardingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModel = OnBoardingSystem.Data.EF.Models;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    public class ZmstSubjectDirector : IZmstSubjectDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstServiceTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSubjectDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.ZmstSubject>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var Subjectlist = await this.unitOfWork.ZmstSubjectRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var zmstqualificationlist = await this.unitOfWork.ZmstQualificationRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var SubjectList = from Subj in Subjectlist
                                  join qualification in zmstqualificationlist on Subj.QualificationId equals qualification.QualificationId
                                  select new Abstractions.Models.ZmstSubject
                                  {
                                      qualificationId = Subj.QualificationId,
                                      subjectId = Subj.SubjectId,
                                      subjectName = Subj.SubjectName,
                                      alternateNames = Subj.AlternateNames,
                                      QuestionName = qualification.Description,
                                  };
                return this.mapper.Map<List<Abstractions.Models.ZmstSubject>>(SubjectList);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <inheritdoc />
        public async Task<int> InsertAsync(Abstractions.Models.ZmstSubject zmstSubjectData, CancellationToken cancellationToken)
        {
            if (zmstSubjectData == null)
            {
                throw new ArgumentNullException(nameof(zmstSubjectData));
            }

            var chkefmddistrict = await this.unitOfWork.ZmstSubjectRepository.FindByAsync(r => r.SubjectId == zmstSubjectData.subjectId && r.SubjectName == zmstSubjectData.subjectName, default);
            if (chkefmddistrict != null)
            {
                throw new EntityFoundException($"This Records {chkefmddistrict} already exists");
            }
            var efmddistrict = this.mapper.Map<Data.EF.Models.ZmstSubject>(zmstSubjectData);
            await this.unitOfWork.ZmstSubjectRepository.InsertAsync(efmddistrict, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(Abstractions.Models.ZmstSubject zmstSubjectId, CancellationToken cancellationToken)
        {
            if (zmstSubjectId.subjectId == "0")
            {
                throw new ArgumentException(nameof(zmstSubjectId.subjectId));
            }
            EFModel.ZmstSubject data = await unitOfWork.ZmstSubjectRepository.FindByAsync(e => e.SubjectId == zmstSubjectId.subjectId, cancellationToken);
            data.QualificationId = zmstSubjectId.qualificationId;
            data.SubjectId = zmstSubjectId.subjectId;
            data.SubjectName = zmstSubjectId.subjectName;
            data.AlternateNames = zmstSubjectId.alternateNames;
            await unitOfWork.ZmstSubjectRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);

            // var events = unitOfWork.MdDistrictRepository.FindAllBy(e => e.Id == mddistrict.Id);
            //if (events != null)
            //{
            //    foreach (var evt in events)
            //    {
            //        evt.StateId = mddistrict.StateId;
            //        evt.Id = mddistrict.Id;
            //        evt.CreatedDate = mddistrict.CreatedDate;
            //        evt.ModifiedDate = mddistrict.ModifiedDate;
            //        evt.CreatedBy = mddistrict.CreatedBy;
            //        evt.ModifiedBy = mddistrict.ModifiedBy;
            //        await unitOfWork.MdDistrictRepository.UpdateAsync(evt, cancellationToken).ConfigureAwait(false);
            //    }
            //}

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.ZmstSubjectRepository.FindByAsync(x => x.SubjectId == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.ZmstSubjectRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSubjectRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

    }
}

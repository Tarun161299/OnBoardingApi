//-----------------------------------------------------------------------
// <copyright file="MdDistrictDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using EFModel = OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class MdDistrictDirector : IMdDistrictDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDistrictDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdDistrictDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mddistrictlist = await this.unitOfWork.MdDistrictRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdstatelist = await this.unitOfWork.StateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            var stateList = from districtlist in mddistrictlist
                            join statelist in mdstatelist on districtlist.StateId equals statelist.Id
                            select new Abstractions.Models.MdDistrict
                            {
                                StateId = statelist.Id,
                                StateName = statelist.Description,
                                Id = districtlist.Id,
                                Description = districtlist.Description,
                                CreatedDate= districtlist.CreatedDate,
                                CreatedBy= districtlist.CreatedBy,
                            };

            return this.mapper.Map<List<AbsModels.MdDistrict>>(stateList);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdDistrict> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mddistrictlist = await this.unitOfWork.MdDistrictRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdDistrict>(mddistrictlist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdDistrict mddistrict, CancellationToken cancellationToken)
        {
            if (mddistrict == null)
            {
                throw new ArgumentNullException(nameof(mddistrict));
            }

            var chkefmddistrict = await this.unitOfWork.MdDistrictRepository.FindByAsync(r => r.Id == mddistrict.Id && r.StateId==mddistrict.StateId, default);
            if (chkefmddistrict != null)
            {
                throw new EntityFoundException($"This Records {chkefmddistrict} already exists");
            }
            var efmddistrict = this.mapper.Map<Data.EF.Models.MdDistrict>(mddistrict);
            await this.unitOfWork.MdDistrictRepository.InsertAsync(efmddistrict, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(AbsModels.MdDistrict mddistrict, CancellationToken cancellationToken)
        {
            if (mddistrict.Id == "0")
            {
                throw new ArgumentException(nameof(mddistrict.Id));
            }
            EFModel.MdDistrict data = await unitOfWork.MdDistrictRepository.FindByAsync(e => e.Id == mddistrict.Id, cancellationToken);
            data.StateId = mddistrict.StateId;
            data.Id = mddistrict.Id;
            data.Description= mddistrict.Description;
            data.CreatedDate = mddistrict.CreatedDate;
            data.ModifiedDate = mddistrict.ModifiedDate;
            data.CreatedBy = mddistrict.CreatedBy;
            data.ModifiedBy = mddistrict.ModifiedBy;
            await unitOfWork.MdDistrictRepository.UpdateAsync(data, cancellationToken).ConfigureAwait(false);

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

            var entity = await this.unitOfWork.MdDistrictRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdDistrictRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdDistrictRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdDistrict>> GetListByStateIdAsync(string StateId, CancellationToken cancellationToken)
        {
            var mddistrictlist = await this.unitOfWork.MdDistrictRepository.FindAllByAsync(x => x.StateId == StateId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<Abstractions.Models.MdDistrict>>(mddistrictlist);
            return result;
        }
    }
}

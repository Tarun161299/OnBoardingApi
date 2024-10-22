
//-----------------------------------------------------------------------
// <copyright file="ZmstApplicantTypeDirector.cs" company="NIC">
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
    public class ZmstApplicantTypeDirector : IZmstApplicantTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstApplicantTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstApplicantTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstApplicantType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstapplicanttypelist = await this.unitOfWork.ZmstApplicantTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstApplicantType>>(zmstapplicanttypelist);
            //  var result = new List<AbsModels.ZmstApplicantType>();

           // return result;
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstApplicantType> GetByIdAsync(int ID, CancellationToken cancellationToken)
        {
            var zmstapplicanttypelist = await this.unitOfWork.ZmstApplicantTypeRepository.FindByAsync(x => x.Id == ID, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstApplicantType>(zmstapplicanttypelist);
            //var result=new AbsModels.ZmstApplicantType();

            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstApplicantType zmstapplicanttype, CancellationToken cancellationToken)
        {
            if (zmstapplicanttype == null)
            {
                throw new ArgumentNullException(nameof(zmstapplicanttype));
            }

            var chkefzmstapplicanttype = await this.unitOfWork.ZmstApplicantTypeRepository.FindByAsync(r => r.Id == zmstapplicanttype.ID, default);
            if (chkefzmstapplicanttype != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstapplicanttype} already exists");
            }

            var efzmstapplicanttype = this.mapper.Map<Data.EF.Models.ZmstApplicantType>(zmstapplicanttype);

            await this.unitOfWork.ZmstApplicantTypeRepository.InsertAsync(efzmstapplicanttype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            //return 1;
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstApplicantType zmstapplicanttype, CancellationToken cancellationToken)

        {
            if (zmstapplicanttype.ID == 0)
            {
                throw new ArgumentException(nameof(zmstapplicanttype.ID));
            }

            Data.EF.Models.ZmstApplicantType entityUpd = await unitOfWork.ZmstApplicantTypeRepository.FindByAsync(e => e.Id == zmstapplicanttype.ID, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.Id = zmstapplicanttype.ID;
                entityUpd.TypeName = zmstapplicanttype.TypeName;

                await unitOfWork.ZmstApplicantTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
            //return 1;
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int ID, CancellationToken cancellationToken)
        {
            if (ID == 0)
            {
                throw new ArgumentNullException(nameof(ID));
            }

            var entity = await this.unitOfWork.ZmstApplicantTypeRepository.FindByAsync(x => x.Id == ID, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ID {ID} was not found.");
            }

            await this.unitOfWork.ZmstApplicantTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstApplicantTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
            //return 1;
        }
    }
}


//-----------------------------------------------------------------------
// <copyright file="ZmstCountryDirector.cs" company="NIC">
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

    /// <inheritdoc />
    public class ZmstCountryDirector : IZmstCountryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstCountryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstCountryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstCountry>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstcountrylist = await this.unitOfWork.ZmstCountryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstCountry>>(zmstcountrylist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstCountry> GetByIdAsync(string Code, CancellationToken cancellationToken)
        {
            var zmstcountrylist = await this.unitOfWork.ZmstCountryRepository.FindByAsync(x => x.Code == Code, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstCountry>(zmstcountrylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstCountry zmstcountry, CancellationToken cancellationToken)
        {
            if (zmstcountry == null)
            {
                throw new ArgumentNullException(nameof(zmstcountry));
            }

            var chkefzmstcountry = await this.unitOfWork.ZmstCountryRepository.FindByAsync(r => r.Code == zmstcountry.Code, default);
            if (chkefzmstcountry != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstcountry} already exists");
            }

            var efzmstcountry = this.mapper.Map<Data.EF.Models.ZmstCountry>(zmstcountry);

            await this.unitOfWork.ZmstCountryRepository.InsertAsync(efzmstcountry, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstCountry zmstcountry, CancellationToken cancellationToken)
        {
            if (zmstcountry.Code == "0")
            {
                throw new ArgumentException(nameof(zmstcountry.Code));
            }

            Data.EF.Models.ZmstCountry entity = await unitOfWork.ZmstCountryRepository.FindByAsync(e => e.Code == zmstcountry.Code, cancellationToken);
            if (entity != null)
            {
                entity.Code = zmstcountry.Code;
                entity.Name = zmstcountry.Name;
                entity.SAarccode = zmstcountry.SAarccode;
                entity.SAarcname = zmstcountry.SAarcname;
                entity.Isdcode = zmstcountry.Isdcode;
                entity.Priority = zmstcountry.Priority;

                await unitOfWork.ZmstCountryRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string Code, CancellationToken cancellationToken)
        {
            if (Code == "0")
            {
                throw new ArgumentNullException(nameof(Code));
            }

            var entity = await this.unitOfWork.ZmstCountryRepository.FindByAsync(x => x.Code == Code, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Code {Code} was not found.");
            }

            await this.unitOfWork.ZmstCountryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstCountryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

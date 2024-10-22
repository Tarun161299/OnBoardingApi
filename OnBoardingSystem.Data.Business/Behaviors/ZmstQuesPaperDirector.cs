
//-----------------------------------------------------------------------
// <copyright file="ZmstQuesPaperDirector.cs" company="NIC">
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
    public class ZmstQuesPaperDirector : IZmstQuesPaperDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstQuesPaperDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstQuesPaperDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstQuesPaper>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstquespaperlist = await this.unitOfWork.ZmstQuesPaperRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstQuesPaper>>(zmstquespaperlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstQuesPaper> GetByIdAsync(string PaperId, CancellationToken cancellationToken)
        {
            var zmstquespaperlist = await this.unitOfWork.ZmstQuesPaperRepository.FindByAsync(x => x.PaperId == PaperId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstQuesPaper>(zmstquespaperlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstQuesPaper zmstquespaper, CancellationToken cancellationToken)
        {
            if (zmstquespaper == null)
            {
                throw new ArgumentNullException(nameof(zmstquespaper));
            }

            var chkefzmstquespaper = await this.unitOfWork.ZmstQuesPaperRepository.FindByAsync(r => r.PaperId == zmstquespaper.PaperId, default);
            if (chkefzmstquespaper != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstquespaper} already exists");
            }

            var efzmstquespaper = this.mapper.Map<Data.EF.Models.ZmstQuesPaper>(zmstquespaper);

            await this.unitOfWork.ZmstQuesPaperRepository.InsertAsync(efzmstquespaper, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstQuesPaper zmstquespaper, CancellationToken cancellationToken)
        
		{
            if (zmstquespaper.PaperId == "0")
            {
                throw new ArgumentException(nameof(zmstquespaper.PaperId));
            }
			
			Data.EF.Models.ZmstQuesPaper entityUpd = await unitOfWork.ZmstQuesPaperRepository.FindByAsync(e => e.PaperId == zmstquespaper.PaperId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.PaperId= zmstquespaper.PaperId;
					entityUpd.PaperName= zmstquespaper.PaperName;
					
				await unitOfWork.ZmstQuesPaperRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string PaperId, CancellationToken cancellationToken)
        {
            if (PaperId == "0")
            {
                throw new ArgumentNullException(nameof(PaperId));
            }

            var entity = await this.unitOfWork.ZmstQuesPaperRepository.FindByAsync(x => x.PaperId == PaperId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an PaperId {PaperId} was not found.");
            }

            await this.unitOfWork.ZmstQuesPaperRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstQuesPaperRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
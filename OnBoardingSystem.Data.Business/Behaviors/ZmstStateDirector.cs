
//-----------------------------------------------------------------------
// <copyright file="ZmstStateDirector.cs" company="NIC">
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
    public class ZmstStateDirector : IZmstStateDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstStateDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstStateDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstState>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmststatelist = await this.unitOfWork.ZmstStateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstState>>(zmststatelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstState> GetByIdAsync(string StateId, CancellationToken cancellationToken)
        {
            var zmststatelist = await this.unitOfWork.ZmstStateRepository.FindByAsync(x => x.StateId == StateId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstState>(zmststatelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstState zmststate, CancellationToken cancellationToken)
        {
            if (zmststate == null)
            {
                throw new ArgumentNullException(nameof(zmststate));
            }

            var chkefzmststate = await this.unitOfWork.ZmstStateRepository.FindByAsync(r => r.StateId == zmststate.StateId, default);
            if (chkefzmststate != null)
            {
                throw new EntityFoundException($"This Records {chkefzmststate} already exists");
            }

            var efzmststate = this.mapper.Map<Data.EF.Models.ZmstState>(zmststate);

            await this.unitOfWork.ZmstStateRepository.InsertAsync(efzmststate, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstState zmststate, CancellationToken cancellationToken)
        
		{
            if (zmststate.StateId == "0")
            {
                throw new ArgumentException(nameof(zmststate.StateId));
            }
			
			Data.EF.Models.ZmstState entityUpd = await unitOfWork.ZmstStateRepository.FindByAsync(e => e.StateId == zmststate.StateId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.StateId= zmststate.StateId;
					entityUpd.StateName= zmststate.StateName;
					entityUpd.AlternateNames= zmststate.AlternateNames;
					
				await unitOfWork.ZmstStateRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string StateId, CancellationToken cancellationToken)
        {
            if (StateId == "0")
            {
                throw new ArgumentNullException(nameof(StateId));
            }

            var entity = await this.unitOfWork.ZmstStateRepository.FindByAsync(x => x.StateId == StateId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an StateId {StateId} was not found.");
            }

            await this.unitOfWork.ZmstStateRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstStateRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
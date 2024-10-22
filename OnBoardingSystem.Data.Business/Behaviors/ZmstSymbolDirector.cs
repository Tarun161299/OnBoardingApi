
//-----------------------------------------------------------------------
// <copyright file="ZmstSymbolDirector.cs" company="NIC">
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
    public class ZmstSymbolDirector : IZmstSymbolDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSymbolDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSymbolDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSymbol>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstsymbollist = await this.unitOfWork.ZmstSymbolRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSymbol>>(zmstsymbollist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSymbol> GetByIdAsync(string SymbolId, CancellationToken cancellationToken)
        {
            var zmstsymbollist = await this.unitOfWork.ZmstSymbolRepository.FindByAsync(x => x.SymbolId == SymbolId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSymbol>(zmstsymbollist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSymbol zmstsymbol, CancellationToken cancellationToken)
        {
            if (zmstsymbol == null)
            {
                throw new ArgumentNullException(nameof(zmstsymbol));
            }

            var chkefzmstsymbol = await this.unitOfWork.ZmstSymbolRepository.FindByAsync(r => r.SymbolId == zmstsymbol.SymbolId, default);
            if (chkefzmstsymbol != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstsymbol} already exists");
            }

            var efzmstsymbol = this.mapper.Map<Data.EF.Models.ZmstSymbol>(zmstsymbol);

            await this.unitOfWork.ZmstSymbolRepository.InsertAsync(efzmstsymbol, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSymbol zmstsymbol, CancellationToken cancellationToken)
        
		{
            if (zmstsymbol.SymbolId == "0")
            {
                throw new ArgumentException(nameof(zmstsymbol.SymbolId));
            }
			
			Data.EF.Models.ZmstSymbol entityUpd = await unitOfWork.ZmstSymbolRepository.FindByAsync(e => e.SymbolId == zmstsymbol.SymbolId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SymbolId= zmstsymbol.SymbolId;
					entityUpd.Description= zmstsymbol.Description;
					entityUpd.AlternateNames= zmstsymbol.AlternateNames;
					
				await unitOfWork.ZmstSymbolRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SymbolId, CancellationToken cancellationToken)
        {
            if (SymbolId == "0")
            {
                throw new ArgumentNullException(nameof(SymbolId));
            }

            var entity = await this.unitOfWork.ZmstSymbolRepository.FindByAsync(x => x.SymbolId == SymbolId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SymbolId {SymbolId} was not found.");
            }

            await this.unitOfWork.ZmstSymbolRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSymbolRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
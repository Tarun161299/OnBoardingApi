
//-----------------------------------------------------------------------
// <copyright file="ZmstAuthenticationModeDirector.cs" company="NIC">
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
    public class ZmstAuthenticationModeDirector : IZmstAuthenticationModeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstAuthenticationModeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstAuthenticationModeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstAuthenticationMode>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstauthenticationmodelist = await this.unitOfWork.ZmstAuthenticationModeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstAuthenticationMode>>(zmstauthenticationmodelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstAuthenticationMode> GetByIdAsync(string AuthCode, CancellationToken cancellationToken)
        {
            var zmstauthenticationmodelist = await this.unitOfWork.ZmstAuthenticationModeRepository.FindByAsync(x => x.AuthCode == AuthCode, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstAuthenticationMode>(zmstauthenticationmodelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstAuthenticationMode zmstauthenticationmode, CancellationToken cancellationToken)
        {
            if (zmstauthenticationmode == null)
            {
                throw new ArgumentNullException(nameof(zmstauthenticationmode));
            }

            var chkefzmstauthenticationmode = await this.unitOfWork.ZmstAuthenticationModeRepository.FindByAsync(r => r.AuthCode == zmstauthenticationmode.AuthCode, default);
            if (chkefzmstauthenticationmode != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstauthenticationmode} already exists");
            }

            var efzmstauthenticationmode = this.mapper.Map<Data.EF.Models.ZmstAuthenticationMode>(zmstauthenticationmode);

            await this.unitOfWork.ZmstAuthenticationModeRepository.InsertAsync(efzmstauthenticationmode, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstAuthenticationMode zmstauthenticationmode, CancellationToken cancellationToken)
        
		{
            if (zmstauthenticationmode.AuthCode == "0")
            {
                throw new ArgumentException(nameof(zmstauthenticationmode.AuthCode));
            }
			
			Data.EF.Models.ZmstAuthenticationMode entityUpd = await unitOfWork.ZmstAuthenticationModeRepository.FindByAsync(e => e.AuthCode == zmstauthenticationmode.AuthCode, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.ZmstAuthenticationModeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string AuthCode, CancellationToken cancellationToken)
        {
            if (AuthCode == "0")
            {
                throw new ArgumentNullException(nameof(AuthCode));
            }

            var entity = await this.unitOfWork.ZmstAuthenticationModeRepository.FindByAsync(x => x.AuthCode == AuthCode, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an AuthCode {AuthCode} was not found.");
            }

            await this.unitOfWork.ZmstAuthenticationModeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstAuthenticationModeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
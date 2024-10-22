
//-----------------------------------------------------------------------
// <copyright file="ConfigurationEnvironmentDirector.cs" company="NIC">
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
    public class ConfigurationEnvironmentDirector : IConfigurationEnvironmentDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationEnvironmentDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ConfigurationEnvironmentDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ConfigurationEnvironment>> GetAllAsync(CancellationToken cancellationToken)
        {
            var configurationenvironmentlist = await this.unitOfWork.ConfigurationEnvironmentRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ConfigurationEnvironment>>(configurationenvironmentlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ConfigurationEnvironment> GetByIdAsync(int ApplicationId, CancellationToken cancellationToken)
        {
            var configurationenvironmentlist = await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(x => x.ApplicationId == ApplicationId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ConfigurationEnvironment>(configurationenvironmentlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ConfigurationEnvironment configurationenvironment, CancellationToken cancellationToken)
        {
            if (configurationenvironment == null)
            {
                throw new ArgumentNullException(nameof(configurationenvironment));
            }

            var chkefconfigurationenvironment = await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(r => r.ApplicationId == configurationenvironment.ApplicationId, default);
            if (chkefconfigurationenvironment != null)
            {
                throw new EntityFoundException($"This Records {chkefconfigurationenvironment} already exists");
            }

            var efconfigurationenvironment = this.mapper.Map<Data.EF.Models.ConfigurationEnvironment>(configurationenvironment);

            await this.unitOfWork.ConfigurationEnvironmentRepository.InsertAsync(efconfigurationenvironment, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ConfigurationEnvironment configurationenvironment, CancellationToken cancellationToken)
        
		{
            if (configurationenvironment.ApplicationId == 0)
            {
                throw new ArgumentException(nameof(configurationenvironment.ApplicationId));
            }
			
			Data.EF.Models.ConfigurationEnvironment entityUpd = await unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(e => e.ApplicationId == configurationenvironment.ApplicationId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.ConfigurationEnvironmentRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int ApplicationId, CancellationToken cancellationToken)
        {
            if (ApplicationId == 0)
            {
                throw new ArgumentNullException(nameof(ApplicationId));
            }

            var entity = await this.unitOfWork.ConfigurationEnvironmentRepository.FindByAsync(x => x.ApplicationId == ApplicationId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ApplicationId {ApplicationId} was not found.");
            }

            await this.unitOfWork.ConfigurationEnvironmentRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ConfigurationEnvironmentRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	

//-----------------------------------------------------------------------
// <copyright file="ZmstSecurityQuestionDirector.cs" company="NIC">
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
    public class ZmstSecurityQuestionDirector : IZmstSecurityQuestionDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstSecurityQuestionDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstSecurityQuestionDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstSecurityQuestion>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstsecurityquestionlist = await this.unitOfWork.ZmstSecurityQuestionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.ZmstSecurityQuestion>>(zmstsecurityquestionlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstSecurityQuestion> GetByIdAsync(string SecurityQuesId, CancellationToken cancellationToken)
        {
            var zmstsecurityquestionlist = await this.unitOfWork.ZmstSecurityQuestionRepository.FindByAsync(x => x.SecurityQuesId == SecurityQuesId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstSecurityQuestion>(zmstsecurityquestionlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstSecurityQuestion zmstsecurityquestion, CancellationToken cancellationToken)
        {
            if (zmstsecurityquestion == null)
            {
                throw new ArgumentNullException(nameof(zmstsecurityquestion));
            }

            var chkefzmstsecurityquestion = await this.unitOfWork.ZmstSecurityQuestionRepository.FindByAsync(r => r.SecurityQuesId == zmstsecurityquestion.SecurityQuesId, default);
            if (chkefzmstsecurityquestion != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstsecurityquestion} already exists");
            }

            var efzmstsecurityquestion = this.mapper.Map<Data.EF.Models.ZmstSecurityQuestion>(zmstsecurityquestion);

            await this.unitOfWork.ZmstSecurityQuestionRepository.InsertAsync(efzmstsecurityquestion, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstSecurityQuestion zmstsecurityquestion, CancellationToken cancellationToken)
        
		{
            if (zmstsecurityquestion.SecurityQuesId == "0")
            {
                throw new ArgumentException(nameof(zmstsecurityquestion.SecurityQuesId));
            }
			
			Data.EF.Models.ZmstSecurityQuestion entityUpd = await unitOfWork.ZmstSecurityQuestionRepository.FindByAsync(e => e.SecurityQuesId == zmstsecurityquestion.SecurityQuesId, cancellationToken);
			if (entityUpd != null)
            {
			
				await unitOfWork.ZmstSecurityQuestionRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string SecurityQuesId, CancellationToken cancellationToken)
        {
            if (SecurityQuesId == "0")
            {
                throw new ArgumentNullException(nameof(SecurityQuesId));
            }

            var entity = await this.unitOfWork.ZmstSecurityQuestionRepository.FindByAsync(x => x.SecurityQuesId == SecurityQuesId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SecurityQuesId {SecurityQuesId} was not found.");
            }

            await this.unitOfWork.ZmstSecurityQuestionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstSecurityQuestionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	
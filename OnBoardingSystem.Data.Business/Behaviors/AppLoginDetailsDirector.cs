
//-----------------------------------------------------------------------
// <copyright file="AppLoginDetailsDirector.cs" company="NIC">
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
    using DocumentFormat.OpenXml.Spreadsheet;
    using System.Linq.Dynamic.Core;
    using OnBoardingSystem.Data.Abstractions.Models;
    using DocumentFormat.OpenXml.InkML;
    using OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class AppLoginDetailsDirector : IAppLoginDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLoginDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppLoginDetailsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppLoginDetails>> GetAllPmuUsersAsync(CancellationToken cancellationToken)
        {
            try
            {

                var applogindetailslist = await this.unitOfWork.AppLoginDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var userrolmapping = await this.unitOfWork.AppUserRoleMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);//FindAllByAsync(x => x.RoleId != "USER" && string.IsNullOrEmpty(x.RoleId)  ,cancellationToken).ConfigureAwait(false);
                var absapplogindetailslist = this.mapper.Map<List<AbsModels.AppLoginDetails>>(applogindetailslist);
                var absuserrolmapping= this.mapper.Map<List<AbsModels.AppUserRoleMapping>>(userrolmapping);
                var distinctUserIds = (from l in absapplogindetailslist
                                       join u in absuserrolmapping
                                       on l.UserId equals u.UserId into userRoleMappingGroup
                                       from userRoleMapping in userRoleMappingGroup.DefaultIfEmpty()
                                       where (userRoleMapping == null || (userRoleMapping.RoleId.ToUpper() != "USER" && userRoleMapping.RoleId.ToUpper() != "SUPERADMIN")) && l.UserName!=null
                                       select new AbsModels.AppLoginDetails
                                       {
                                           UserId = l.UserId,
                                           UserName = l.UserName
                                       });
                   return distinctUserIds.GroupBy(m => new { m.UserId, m.UserName }).Select(group => group.First()).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

		/// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppLoginDetails>> GetByIdAsync(string UserID, CancellationToken cancellationToken)
        {

            var mdmodulelist = this.unitOfWork.MdRoleRepository.GetAll();
            var approlelist = this.unitOfWork.AppUserRoleMappingRepository.FindAllBy(x => x.UserId == UserID);

            var query = from mrole in mdmodulelist
                        join approle in approlelist on mrole.RoleName equals approle.RoleId into temp
                        from fl in temp.DefaultIfEmpty()
                        where mrole.RoleName != "USER"

                        select new AbsModels.AppLoginDetails
                        {
                            Role = Convert.ToString(mrole.RoleName).Trim(),
                            RoleId = Convert.ToString(mrole.RoleId).Trim(),
                            IsReadOnly = Convert.ToString(fl.IsReadOnly).Trim(),
                            IsActive = Convert.ToString(fl.IsActive).Trim(),
                            Assign = Convert.ToString(fl.RoleId).Trim(),
                           //RoleId = RoleId,
                        };

            return query.ToList();

        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppLoginDetails applogindetails, CancellationToken cancellationToken)
        {
            if (applogindetails == null)
            {
                throw new ArgumentNullException(nameof(applogindetails));
            }

            var chkefapplogindetails = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(r => r.RequestNo == applogindetails.RequestNo, default);
            if (chkefapplogindetails != null)
            {
                throw new EntityFoundException($"This Records {chkefapplogindetails} already exists");
            }

            var efapplogindetails = this.mapper.Map<Data.EF.Models.AppLoginDetails>(applogindetails);

            await this.unitOfWork.AppLoginDetailsRepository.InsertAsync(efapplogindetails, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppLoginDetails applogindetails, CancellationToken cancellationToken)
        
		{
            if (applogindetails.RequestNo == "0")
            {
                throw new ArgumentException(nameof(applogindetails.RequestNo));
            }
			
			Data.EF.Models.AppLoginDetails entityUpd = await unitOfWork.AppLoginDetailsRepository.FindByAsync(e => e.RequestNo == applogindetails.RequestNo, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.FailedLoginAttemptCount= applogindetails.FailedLoginAttemptCount;
					entityUpd.LastSuccessfulLoginTime= applogindetails.LastSuccessfulLoginTime;
					//entityUpd.LastSuccessfulLoginIP= applogindetails.LastSuccessfulLoginIP;
					entityUpd.LastPasswordChangeTime= applogindetails.LastPasswordChangeTime;
					//entityUpd.LastPasswordChangeIP= applogindetails.LastPasswordChangeIP;
					entityUpd.LastPasswordResetTime= applogindetails.LastPasswordResetTime;
					//entityUpd.LastPasswordResetIP= applogindetails.LastPasswordResetIP;
					entityUpd.UserToken= applogindetails.UserToken;
					entityUpd.AccessToken= applogindetails.AccessToken;
					
				await unitOfWork.AppLoginDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string RequestNo, CancellationToken cancellationToken)
        {
            if (RequestNo == "0")
            {
                throw new ArgumentNullException(nameof(RequestNo));
            }

            var entity = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(x => x.RequestNo == RequestNo, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an RequestNo {RequestNo} was not found.");
            }

            await this.unitOfWork.AppLoginDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppLoginDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	  }
	}
	
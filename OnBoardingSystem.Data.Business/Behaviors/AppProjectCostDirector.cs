//-----------------------------------------------------------------------
// <copyright file="MdMinistryDirector.cs" company="NIC">
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
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    public class AppProjectCostDirector:IAppProjectCostDiector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMinistryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppProjectCostDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        /// <inheritdoc/>
        public async Task<int> SaveAsync(Abstractions.Models.AppProjectCost appProjectCost, CancellationToken cancellationToken)
        {
            int result = 0;
            if (appProjectCost == null)
            {
                throw new ArgumentNullException(nameof(appProjectCost));
            }

            if (appProjectCost != null)
            {
                return await this.CommitMdMinistryChangesAsync(appProjectCost, cancellationToken);
            }

            return result;
        }
        private async Task<int> CommitMdMinistryChangesAsync(Abstractions.Models.AppProjectCost appProjectCost, CancellationToken cancellationToken)
        {
            if (appProjectCost == null)
            {
                throw new ArgumentNullException(nameof(appProjectCost));
            }

            //var ministry = await this.unitOfWork.MdMinistryRepository.FindByAsync(r => r.MinistryId == mdMinistry.MinistryId, default);
            //if (ministry != null)
            //{
                //throw new EntityFoundException($"This Ministry {mdMinistry} already exists");
            //}
            var checkProject=  await this.unitOfWork.AppProjectCostRepository.FindByAsync(r => r.ProjectId == appProjectCost.ProjectId && r.FinancialComponentId== appProjectCost.FinancialComponentId, default);
            if (checkProject != null) {
                return -2;
            }
            var _appProjectCost = this.mapper.Map<Data.EF.Models.AppProjectCost>(appProjectCost);

            await this.unitOfWork.AppProjectCostRepository.InsertAsync(_appProjectCost, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<List<AppProjectCostList>> GetByIdAsync(int ProjectId, CancellationToken cancellationToken)
        {
            var projectList=new List<AppProjectCostList>();
            var mdFinancialcomponent = this.unitOfWork.MdProjectFinancialComponentRepository.GetAll();
            var appProjectCost = this.unitOfWork.AppProjectCostRepository.FindAllBy(re => re.ProjectId == ProjectId);

            var projectcostListdata = from appprojectcost in appProjectCost
                                      join mdfinancialcomponent in mdFinancialcomponent on appprojectcost.FinancialComponentId equals mdfinancialcomponent.FinancialComponentId

                                      select new AppProjectCostList
                                      { FinancialComponent = mdfinancialcomponent.FinancialComponent,
                                        ProjectId = appprojectcost.ProjectId,
                                        FinancialComponentId = appprojectcost.FinancialComponentId,
                                        Amount = appprojectcost.Amount,
                                        //CreatedBy = appprojectcost.CreatedBy,
                                        //CreatedOn= appprojectcost.CreatedOn,
                                        //ModifiedBy = appprojectcost.ModifiedBy,
                                        ModifiedOn = appprojectcost.ModifiedOn,
                                        IsActive = appprojectcost.IsActive,
                                      };
            return await Task.FromResult(projectcostListdata.ToList()).ConfigureAwait(false);
        }
        
        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int financialComponentId, int ProjectId, CancellationToken cancellationToken)
        {
            
            var entity = await this.unitOfWork.AppProjectCostRepository.FindByAsync(
                x => x.FinancialComponentId == financialComponentId && x.ProjectId== ProjectId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                // throw new EntityNotFoundException($"The Ministries with an MinistryId {ministryId} was not found.");
            }

            await this.unitOfWork.AppProjectCostRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppProjectCostRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(Abstractions.Models.AppProjectCost appProjectCost, CancellationToken cancellationToken)
        {
            var events = unitOfWork.AppProjectCostRepository.FindAllBy(e => e.FinancialComponentId == appProjectCost.FinancialComponentId && e.ProjectId == appProjectCost.ProjectId);
            foreach (var evt in events)
            {
                evt.Amount = appProjectCost.Amount;
                evt.ModifiedBy = appProjectCost.ModifiedBy;
                evt.ModifiedOn = appProjectCost.ModifiedOn;
                await unitOfWork.AppProjectCostRepository.UpdateAsync(evt, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}

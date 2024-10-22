
//-----------------------------------------------------------------------
// <copyright file="AppProjectExpenditureDirector.cs" company="NIC">
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
    public class AppProjectExpenditureDirector : IAppProjectExpenditureDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectExpenditureDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppProjectExpenditureDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppProjectExpenditure>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appprojectexpenditurelist = await this.unitOfWork.AppProjectExpenditureRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppProjectExpenditure>>(appprojectexpenditurelist);
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppProjectExpenditure>> GetByIdAsync(int ProjectId, CancellationToken cancellationToken)
        {
            /*
            var appprojectexpenditurelist = await this.unitOfWork.AppProjectExpenditureRepository.FindByAsync(x => x.ProjectId == ProjectId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppProjectExpenditure>(appprojectexpenditurelist);
            return result;
            */

            try
            {
                var projectList = new List<AppProjectExpenditure>();
                var appProjectCost = await this.unitOfWork.AppProjectExpenditureRepository.FindAllByAsync(re => re.ProjectId == ProjectId,cancellationToken).ConfigureAwait(false);
                var mdFinancialcomponent = await this.unitOfWork.MdProjectFinancialComponentRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

                var projectcostListdata = from appprojectcost in appProjectCost
                                          join mdFinancial in mdFinancialcomponent on appprojectcost.FinancialComponentId equals mdFinancial.FinancialComponentId
                                          select new AppProjectExpenditure
                                          {
                                              FinancialComponent = mdFinancial.FinancialComponent,
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
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <inheritdoc/>
        //public async Task<int> InsertAsync(AbsModels.AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        if (appprojectexpenditure == null)
        //        {
        //            throw new ArgumentNullException(nameof(appprojectexpenditure));
        //        }

        //        var chkefappprojectexpenditure = await this.unitOfWork.AppProjectExpenditureRepository.FindByAsync(r => r.ProjectId == appprojectexpenditure.ProjectId, default);
        //        if (chkefappprojectexpenditure != null)
        //        {
        //            throw new EntityFoundException($"This Records {chkefappprojectexpenditure} already exists");
        //        }

        //        var efappprojectexpenditure = this.mapper.Map<Data.EF.Models.AppProjectExpenditure>(appprojectexpenditure);
        //        await this.unitOfWork.AppProjectExpenditureRepository.InsertAsync(efappprojectexpenditure, cancellationToken).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        //}

        /// <inheritdoc/>
        /// 
        public async Task<int> InsertAsync(Abstractions.Models.AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken)
        {
            int result = 0;
            if (appprojectexpenditure == null)
            {
                throw new ArgumentNullException(nameof(appprojectexpenditure));
            }

            if (appprojectexpenditure != null)
            {
                return await this.CommitMdMinistryChangesAsync(appprojectexpenditure, cancellationToken);
            }

            return result;
        }
        private async Task<int> CommitMdMinistryChangesAsync(Abstractions.Models.AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken)
        {
            if (appprojectexpenditure == null)
            {
                throw new ArgumentNullException(nameof(appprojectexpenditure));
            }
            var checkProject = await this.unitOfWork.AppProjectExpenditureRepository.FindByAsync(r => r.ProjectId == appprojectexpenditure.ProjectId && r.FinancialComponentId == appprojectexpenditure.FinancialComponentId, default);
            if (checkProject != null)
            {
                return -2;
            }
            var _appprojectexpenditure = this.mapper.Map<Data.EF.Models.AppProjectExpenditure>(appprojectexpenditure);

            await this.unitOfWork.AppProjectExpenditureRepository.InsertAsync(_appprojectexpenditure, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<int> UpdateAsync(AbsModels.AppProjectExpenditure appprojectexpenditure, CancellationToken cancellationToken)

        {
            if (appprojectexpenditure.ProjectId == 0)
            {
                throw new ArgumentException(nameof(appprojectexpenditure.ProjectId));
            }

            Data.EF.Models.AppProjectExpenditure entityUpd = await unitOfWork.AppProjectExpenditureRepository.FindByAsync(e => e.ProjectId == appprojectexpenditure.ProjectId && e.FinancialComponentId == appprojectexpenditure.FinancialComponentId, cancellationToken);
            entityUpd.ProjectId = appprojectexpenditure.ProjectId;
            entityUpd.FinancialComponentId = appprojectexpenditure.FinancialComponentId;
            entityUpd.Amount = appprojectexpenditure.Amount;
            entityUpd.CreatedOn = appprojectexpenditure.CreatedOn;
            entityUpd.CreatedBy = appprojectexpenditure.CreatedBy;
            entityUpd.ModifiedOn = appprojectexpenditure.ModifiedOn;
            entityUpd.ModifiedBy = appprojectexpenditure.ModifiedBy;
            entityUpd.IsActive= appprojectexpenditure.IsActive;

            if (entityUpd != null)
            {

                await unitOfWork.AppProjectExpenditureRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int ProjectId, CancellationToken cancellationToken)
        {
            if (ProjectId == 0)
            {
                throw new ArgumentNullException(nameof(ProjectId));
            }

            var entity = await this.unitOfWork.AppProjectExpenditureRepository.FindByAsync(x => x.ProjectId == ProjectId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ProjectId {ProjectId} was not found.");
            }

            await this.unitOfWork.AppProjectExpenditureRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppProjectExpenditureRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

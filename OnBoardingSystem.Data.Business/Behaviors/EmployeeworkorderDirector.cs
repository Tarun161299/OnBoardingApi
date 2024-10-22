
//-----------------------------------------------------------------------
// <copyright file="employeeWorkOrderDirector.cs" company="NIC">
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
    public class EmployeeWorkOrderDirector : IEmployeeWorkOrderDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeWorkOrderDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public EmployeeWorkOrderDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.EmployeeWorkOrder>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employeeworkorderlist = await this.unitOfWork.EmployeeWorkOrderRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var employeedetailslist = await this.unitOfWork.EmployeeDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var employeeworkorderlistObj = from employeeworkorder in employeeworkorderlist
                                           join employeedetails in employeedetailslist on employeeworkorder.EmpCode equals employeedetails.EmpCode
                                           select new Abstractions.Models.EmployeeWorkOrder
                                           {
                                               WorkorderNo = employeeworkorder.WorkorderNo,
                                               EmpCode = employeeworkorder.EmpCode,
                                               EmpName = employeedetails.EmpName
                                           };
            return this.mapper.Map<List<AbsModels.EmployeeWorkOrder>>(employeeworkorderlistObj);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.EmployeeWorkOrder> GetByIdAsync(string EmpCode, CancellationToken cancellationToken)
        {
            var employeeworkorderlist = await this.unitOfWork.EmployeeWorkOrderRepository.FindByAsync(x => x.EmpCode == EmpCode, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.EmployeeWorkOrder>(employeeworkorderlist);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.EmployeeWorkOrder>> GetByEmpCodeAsync(string EmpCode, CancellationToken cancellationToken)
        {
            var employeeworkorderlist = await this.unitOfWork.EmployeeWorkOrderRepository.FindAllByAsync(x => x.EmpCode == EmpCode, cancellationToken).ConfigureAwait(false);
            var employeedetailslist = await this.unitOfWork.EmployeeDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
           var workorderdetailslist = await this.unitOfWork.WorkOrderDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var employeeworkorderlistObj = from employeeworkorder in employeeworkorderlist
                                           join employeedetails in employeedetailslist on employeeworkorder.EmpCode equals employeedetails.EmpCode 
                                           join workorderdetails in workorderdetailslist on employeeworkorder.WorkorderNo equals workorderdetails.WorkorderNo
                                           select new Abstractions.Models.EmployeeWorkOrder
                                           {
                                               WorkorderNo = employeeworkorder.WorkorderNo,
                                               EmpCode = employeeworkorder.EmpCode,
                                               EmpName = employeedetails.EmpName,
                                               agencyName= workorderdetails.AgencyName,
                                               workorderFrom=workorderdetails.WorkorderFrom,
                                               workorderTo=workorderdetails.WorkorderTo
                                           };
            return this.mapper.Map<List<AbsModels.EmployeeWorkOrder>>(employeeworkorderlistObj);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken)
        {
            if (employeeworkorder == null)
            {
                throw new ArgumentNullException(nameof(employeeworkorder));
            }

            var chkefemployeeworkorder = await this.unitOfWork.EmployeeWorkOrderRepository.FindByAsync(r => r.EmpCode == employeeworkorder.EmpCode && r.WorkorderNo == employeeworkorder.WorkorderNo, default);
            if (chkefemployeeworkorder != null)
            {
                throw new EntityFoundException($"This Records {chkefemployeeworkorder} already exists");
            }

            var efemployeeworkorder = this.mapper.Map<Data.EF.Models.EmployeeWorkOrder>(employeeworkorder);

            await this.unitOfWork.EmployeeWorkOrderRepository.InsertAsync(efemployeeworkorder, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken)

        {
            if (employeeworkorder.EmpCode == "0")
            {
                throw new ArgumentException(nameof(employeeworkorder.EmpCode));
            }

            Data.EF.Models.EmployeeWorkOrder entityUpd = await this.unitOfWork.EmployeeWorkOrderRepository.FindByAsync(e => e.EmpCode == employeeworkorder.EmpCode, cancellationToken);
            Data.EF.Models.EmployeeWorkOrder entityInsert = await this.unitOfWork.EmployeeWorkOrderRepository.FindByAsync(e => e.EmpCode == employeeworkorder.EmpCode, cancellationToken);
            await this.unitOfWork.EmployeeWorkOrderRepository.DeleteAsync(entityUpd, cancellationToken).ConfigureAwait(false);
            if (entityInsert != null)
            {
                entityInsert.EmpCode = employeeworkorder.EmpCode;
                entityInsert.WorkorderNo = employeeworkorder.WorkorderNo;

                await this.unitOfWork.EmployeeWorkOrderRepository.InsertAsync(entityInsert, cancellationToken).ConfigureAwait(false);

            }

            return await this.unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(EmployeeWorkOrder employeeworkorder, CancellationToken cancellationToken)
        {
            if (employeeworkorder.EmpCode == "0")
            {
                throw new ArgumentNullException(nameof(employeeworkorder.EmpCode));
            }

            var entity = await this.unitOfWork.EmployeeWorkOrderRepository.FindByAsync(x => x.EmpCode == employeeworkorder.EmpCode && x.WorkorderNo == employeeworkorder.WorkorderNo, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an EmpCode {employeeworkorder.EmpCode} was not found.");
            }
            await this.unitOfWork.EmployeeWorkOrderRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.EmployeeWorkOrderRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

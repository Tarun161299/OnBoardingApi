
//-----------------------------------------------------------------------
// <copyright file="AppProjectPaymentDetailsDirector.cs" company="NIC">
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
    using OnBoardingSystem.Common.enums;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using DocumentFormat.OpenXml.Wordprocessing;
    using DocumentFormat.OpenXml.Drawing.Charts;
    using OnBoardingSystem.Data.EF.Models;

    /// <inheritdoc />
    public class AppProjectPaymentDetailsDirector : IAppProjectPaymentDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppProjectPaymentDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppProjectPaymentDetailsDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppProjectPaymentDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appprojectpaymentdetailslist = await this.unitOfWork.AppProjectPaymentDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppProjectPaymentDetails>>(appprojectpaymentdetailslist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.AppProjectPaymentDetails> GetByIdAsync(int PaymentId, CancellationToken cancellationToken)
        {
            var appprojectpaymentdetailslist = await this.unitOfWork.AppProjectPaymentDetailsRepository.FindByAsync(x => x.PaymentId == PaymentId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppProjectPaymentDetails>(appprojectpaymentdetailslist);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.AppProjectPaymentDetails> GetByPaymentParentRefIdAsync(string PaymentParentRefId, CancellationToken cancellationToken)
        {
            var appprojectpaymentdetailslist = await this.unitOfWork.AppProjectPaymentDetailsRepository.
             FindByAsync(x => x.PaymentParentRefId == PaymentParentRefId, cancellationToken).ConfigureAwait(false);
            //var result= this.mapper.Map<List<Abstractions.Models.AppProjectPaymentDetails>>(appprojectpaymentdetailslist);
            //var orderByDescendingResult = from s in result
            //                              orderby s.SubmitTime descending
            //                              select s;
            return this.mapper.Map<Abstractions.Models.AppProjectPaymentDetails>(appprojectpaymentdetailslist);
        }
        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken)
        {
            if (appprojectpaymentdetails == null)
            {
                throw new ArgumentNullException(nameof(appprojectpaymentdetails));
            }

            var chkefappprojectpaymentdetails = await this.unitOfWork.AppProjectPaymentDetailsRepository.FindByAsync(r => r.PaymentParentRefId == appprojectpaymentdetails.PaymentParentRefId, default);
            if (chkefappprojectpaymentdetails != null)
            {
                chkefappprojectpaymentdetails.Amount = appprojectpaymentdetails.Amount;
                chkefappprojectpaymentdetails.Utrno = appprojectpaymentdetails.UTRNo;
                chkefappprojectpaymentdetails.PaymentDate = appprojectpaymentdetails.PaymentDate;
                chkefappprojectpaymentdetails.IncomeTax = appprojectpaymentdetails.IncomeTax;
                chkefappprojectpaymentdetails.Gst = appprojectpaymentdetails.GST;
                chkefappprojectpaymentdetails.Tds = appprojectpaymentdetails.TDS;
                chkefappprojectpaymentdetails.Status = appprojectpaymentdetails.Status;
                chkefappprojectpaymentdetails.Ipaddress = appprojectpaymentdetails.IPAddress;
                chkefappprojectpaymentdetails.SubmitTime = appprojectpaymentdetails.SubmitTime;
                await unitOfWork.AppProjectPaymentDetailsRepository.UpdateAsync(chkefappprojectpaymentdetails, cancellationToken).ConfigureAwait(false);
                //throw new EntityFoundException($"This Records {chkefappprojectpaymentdetails} already exists");
            }
            else
            {
                var efappprojectpaymentdetails = this.mapper.Map<AppProjectPaymentDetails>(appprojectpaymentdetails);

                await this.unitOfWork.AppProjectPaymentDetailsRepository.InsertAsync(efappprojectpaymentdetails, cancellationToken).ConfigureAwait(false);

            }

            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken)

        {
            if (appprojectpaymentdetails.PaymentId == 0)
            {
                throw new ArgumentException(nameof(appprojectpaymentdetails.PaymentId));
            }

            AppProjectPaymentDetails entityUpd = await unitOfWork.AppProjectPaymentDetailsRepository.FindByAsync(e => e.PaymentId == appprojectpaymentdetails.PaymentId, cancellationToken);

            if (entityUpd != null)
            {
                entityUpd.PaymentId = appprojectpaymentdetails.PaymentId;
                entityUpd.PaymentParentRefId = appprojectpaymentdetails.PaymentParentRefId;

                entityUpd.Amount = appprojectpaymentdetails.Amount;
                entityUpd.Utrno = appprojectpaymentdetails.UTRNo;
                entityUpd.PaymentDate = appprojectpaymentdetails.PaymentDate;
                entityUpd.IncomeTax = appprojectpaymentdetails.IncomeTax;
                entityUpd.Gst = appprojectpaymentdetails.GST;
                entityUpd.Tds = appprojectpaymentdetails.TDS;
                entityUpd.Status = appprojectpaymentdetails.Status;
                entityUpd.Ipaddress = appprojectpaymentdetails.IPAddress;
                entityUpd.SubmitTime = appprojectpaymentdetails.SubmitTime;
                await unitOfWork.AppProjectPaymentDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int PaymentId, CancellationToken cancellationToken)
        {
            if (PaymentId == 0)
            {
                throw new ArgumentNullException(nameof(PaymentId));
            }

            var entity = await this.unitOfWork.AppProjectPaymentDetailsRepository.FindByAsync(x => x.PaymentId == PaymentId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an PaymentId {PaymentId} was not found.");
            }

            await this.unitOfWork.AppProjectPaymentDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppProjectPaymentDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<int> UpdateStatusAsync(AbsModels.AppProjectPaymentDetails appprojectpaymentdetails, CancellationToken cancellationToken)
        {
            if (appprojectpaymentdetails.PaymentId == 0)
            {
                throw new ArgumentException(nameof(appprojectpaymentdetails.PaymentId));
            }

            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    int result = 0;
                    AppProjectActivity projectActivityEntityPdp = await this.unitOfWork.AppProjectActivityRepository.FindByAsync(x => x.ActivityParentRefId == appprojectpaymentdetails.PaymentParentRefId && x.ActivityId == (int)Enumactivity.Payment, cancellationToken);
                    AppProjectPaymentDetails entityUpd = await unitOfWork.AppProjectPaymentDetailsRepository.FindByAsync(e => e.PaymentParentRefId == appprojectpaymentdetails.PaymentParentRefId, cancellationToken);
                    if (projectActivityEntityPdp != null)
                    {

                        projectActivityEntityPdp.Status = appprojectpaymentdetails.Status;

                        await unitOfWork.AppProjectActivityRepository.UpdateAsync(projectActivityEntityPdp, cancellationToken).ConfigureAwait(false);
                    }
                    entityUpd.Status = appprojectpaymentdetails.Status;
                    await unitOfWork.AppProjectPaymentDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

                    result = await unitOfWork.CommitAsync(cancellationToken);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}

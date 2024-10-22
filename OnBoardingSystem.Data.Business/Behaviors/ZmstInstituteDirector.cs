
//-----------------------------------------------------------------------
// <copyright file="ZmstInstituteDirector.cs" company="NIC">
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
    using Azure.Core;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using OnBoardingSystem.Data.EF.Models;
    using Castle.Core.Resource;
    using DocumentFormat.OpenXml.Wordprocessing;
    using DocumentFormat.OpenXml.InkML;
    using DocumentFormat.OpenXml.Spreadsheet;

    /// <inheritdoc />
    public class ZmstInstituteDirector : IZmstInstituteDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstInstituteDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstInstituteDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstInstitute>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var zmstinstitutelist = this.unitOfWork.ZmstInstituteRepository.GetAll();
                var zmstInstituteType = this.unitOfWork.ZmstInstituteTypeRepository.GetAll();
                var zmstInstituteAgency = this.unitOfWork.ZmstInstituteAgencyRepository.GetAll();
                var mdAgency = this.unitOfWork.MDAgencyRepository.GetAll();
                var result = from zI in zmstinstitutelist
                             join zIA in zmstInstituteAgency on zI.InstCd equals zIA.InstCd into zIAT
                             from zIA in zIAT.DefaultIfEmpty()
                             select new AbsModels.ZmstInstitute
                             {
                                 InstCd = zI.InstCd,
                                 InstNm = zI.InstNm,
                                 InstTypeId = zI.InstTypeId,
                                 SeatType = zI.SeatType,
                                 AgencyId = zIA == null ? "0" : zIA.AgencyId,
                                 // AgencyName = p.AgencyName,
                                 InstAdd = zI.InstAdd,
                                 State = zI.State,
                                 District = zI.District,
                                 Pincode = zI.Pincode,
                                 InstPhone = zI.InstPhone,
                                 InstFax = zI.InstFax,
                                 InstWebSite = zI.InstWebSite,
                                 EmailId = zI.EmailId,
                                 AltEmailId = zI.AltEmailId,
                                 ContactPerson = zI.ContactPerson,
                                 Designation = zI.Designation,
                                 MobileNo = zI.MobileNo,
                                 //AISHE = zI.Aishe,
                                 OldInstituteCode = zI.OldInstituteCode,

                             };

                return this.mapper.Map<List<AbsModels.ZmstInstitute>>(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstInstitute>> GetAllByIdsAsync(FilterInstitute filterInstitute, CancellationToken cancellationToken)
        {
            try
            {
                var zmstinstitutelist = this.unitOfWork.ZmstInstituteRepository.GetAll();
                var zmstInstituteType = this.unitOfWork.ZmstInstituteTypeRepository.GetAll();
                var zmstInstituteAgency = this.unitOfWork.ZmstInstituteAgencyRepository.GetAll();
                var mdAgency = this.unitOfWork.MDAgencyRepository.GetAll();
                var result = from zI in zmstinstitutelist
                             join zIA in zmstInstituteAgency on zI.InstCd equals zIA.InstCd into zIAT
                             from zIA in zIAT.DefaultIfEmpty()
                             select new AbsModels.ZmstInstitute
                             {
                                 InstCd = zI.InstCd,
                                 InstNm = zI.InstNm,
                                 InstTypeId = zI.InstTypeId,
                                 SeatType = zI.SeatType,
                                 AgencyId = zIA == null ? "0" : zIA.AgencyId,
                                 // AgencyName = p.AgencyName,
                                 InstAdd = zI.InstAdd,
                                 State = zI.State,
                                 District = zI.District,
                                 Pincode = zI.Pincode,
                                 InstPhone = zI.InstPhone,
                                 InstFax = zI.InstFax,
                                 InstWebSite = zI.InstWebSite,
                                 EmailId = zI.EmailId,
                                 AltEmailId = zI.AltEmailId,
                                 ContactPerson = zI.ContactPerson,
                                 Designation = zI.Designation,
                                 MobileNo = zI.MobileNo,
                                 //AISHE = zI.Aishe,
                                 OldInstituteCode = zI.OldInstituteCode,

                             };

                var Institutes = this.mapper.Map<List<AbsModels.ZmstInstitute>>(result);

                return Institutes.FindAll(x => x.AgencyId == (filterInstitute.AgencyId == "" ? x.AgencyId : filterInstitute.AgencyId)
                && x.InstTypeId == (filterInstitute.InstituteTypeId == "" ? x.InstTypeId : filterInstitute.InstituteTypeId)
               && x.State == (filterInstitute.StateId == "" ? x.State : filterInstitute.StateId));

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstInstitute> GetByIdAsync(string InstCd, CancellationToken cancellationToken)
        {
            var zmstinstitutelist = await this.unitOfWork.ZmstInstituteRepository.FindByAsync(x => x.InstCd == InstCd, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstInstitute>(zmstinstitutelist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstInstitute zmstinstitute, CancellationToken cancellationToken)
        {
            if (zmstinstitute == null)
            {
                throw new ArgumentNullException(nameof(zmstinstitute));
            }

            var chkefzmstinstitute = await this.unitOfWork.ZmstInstituteRepository.FindByAsync(r => r.InstCd == zmstinstitute.InstCd, default);
            if (chkefzmstinstitute != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstinstitute} already exists");
            }

            var efzmstinstitute = this.mapper.Map<Data.EF.Models.ZmstInstitute>(zmstinstitute);

            await this.unitOfWork.ZmstInstituteRepository.InsertAsync(efzmstinstitute, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.ZmstInstitute zmstinstitute, CancellationToken cancellationToken)

        {
            if (zmstinstitute.InstCd == "0")
            {
                throw new ArgumentException(nameof(zmstinstitute.InstCd));
            }

            Data.EF.Models.ZmstInstitute entityUpd = await unitOfWork.ZmstInstituteRepository.FindByAsync(e => e.InstCd == zmstinstitute.InstCd, cancellationToken);
            if (entityUpd != null)
            {
                entityUpd.InstCd = zmstinstitute.InstCd;
                entityUpd.InstNm = zmstinstitute.InstNm;
                entityUpd.AbbrNm = zmstinstitute.AbbrNm;
                entityUpd.InstTypeId = zmstinstitute.InstTypeId;
                //entityUpd.SeatType= zmstinstitute.SeatType;
                entityUpd.InstAdd = zmstinstitute.InstAdd;
                entityUpd.State = zmstinstitute.State;
                entityUpd.District = zmstinstitute.District;
                entityUpd.Pincode = zmstinstitute.Pincode;
                entityUpd.InstPhone = zmstinstitute.InstPhone;
                entityUpd.InstFax = zmstinstitute.InstFax;
                entityUpd.InstWebSite = zmstinstitute.InstWebSite;
                entityUpd.EmailId = zmstinstitute.EmailId;
                entityUpd.AltEmailId = zmstinstitute.AltEmailId;
                entityUpd.ContactPerson = zmstinstitute.ContactPerson;
                entityUpd.Designation = zmstinstitute.Designation;
                entityUpd.MobileNo = zmstinstitute.MobileNo;
                entityUpd.Aishe = zmstinstitute.AISHE;
                //entityUpd.OldInstituteCode= zmstinstitute.OldInstituteCode;

                await unitOfWork.ZmstInstituteRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);

            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string InstCd, CancellationToken cancellationToken)
        {
            if (InstCd == "0")
            {
                throw new ArgumentNullException(nameof(InstCd));
            }

            var entity = await this.unitOfWork.ZmstInstituteRepository.FindByAsync(x => x.InstCd == InstCd, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an InstCd {InstCd} was not found.");
            }

            await this.unitOfWork.ZmstInstituteRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstInstituteRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<string> GetMaxIntitueCode(CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.ZmstInstituteRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var maxId = entity.MaxBy(x => x.InstCd);
            return (Convert.ToDouble(maxId.InstCd) + 1).ToString();
        }

        public virtual async Task<List<AbsModels.InstituteStatistics>> GetInstituteStatistics(string Type, CancellationToken cancellationToken)
        {
            if (Type == "A")
            {
                var InstituteAgency = this.unitOfWork.ZmstInstituteAgencyRepository.GetAll();
                var agencylist = this.unitOfWork.ZmstAgencyRepository.GetAll();
                var instagencyWiseCount = from instagency in InstituteAgency
                                          join agenlist in agencylist on instagency.AgencyId equals agenlist.AgencyId
                                          group agencylist by new { agenlist.AgencyId, agenlist.AgencyName } into g
                                          select new InstituteStatistics
                                          {
                                              id = g.Key.AgencyId,
                                              Name = g.Key.AgencyName,
                                              Total = g.Count(),
                                          };

                return instagencyWiseCount.ToList();
            }
            else if (Type == "T")
            {
                var InstituteTypelist = this.unitOfWork.ZmstInstituteTypeRepository.GetAll();
                var institutelist = this.unitOfWork.ZmstInstituteRepository.GetAll();
                var insttypeWiseCount = from insttype in InstituteTypelist
                                        join state in institutelist on insttype.InstituteTypeId equals state.InstTypeId
                                        group insttype by new { state.InstTypeId, insttype.InstituteTypeId, insttype.InstituteType } into g
                                        select new InstituteStatistics
                                        {
                                            id = g.Key.InstTypeId,
                                            Name = g.Key.InstituteType,
                                            Total = g.Count(),
                                        };

                return insttypeWiseCount.ToList();
            }
            else if (Type == "SM")
            {
                var InstituteStream = this.unitOfWork.ZmstInstituteStreamRepository.GetAll();
                var streamlist = this.unitOfWork.ZmstStreamRepository.GetAll();
                var streamWiseCount = from instStram in InstituteStream
                                      join stream in streamlist on instStram.StreamId equals stream.StreamId
                                      group instStram by new { instStram.StreamId, stream.StreamName } into g
                                      select new InstituteStatistics
                                      {
                                          id = g.Key.StreamId,
                                          Name = g.Key.StreamName,
                                          Total = g.Count(),
                                      };

                return streamWiseCount.ToList();
            }
            else if (Type == "S")
            {
                var institutelist = this.unitOfWork.ZmstInstituteRepository.GetAll();
                var statelist = this.unitOfWork.MdStateRepository.GetAll();
                var instWiseCount = from inst in institutelist
                                    join state in statelist on inst.State equals state.Id
                                    group inst by new { state.Id, inst.State, state.Description } into g
                                    select new InstituteStatistics
                                    {
                                        id = g.Key.Id,
                                        Name = g.Key.Description,
                                        Total = g.Count(),
                                    };

                return instWiseCount.ToList();
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<List<AbsModels.ZmstInstitute>> GetAllCountData(FiterInstituteCount fiterInstituteCount, CancellationToken cancellationToken)
        {
            try
            {
                if (fiterInstituteCount.mode == "A")
                {
                    string agencyId = fiterInstituteCount.id;
                    var instc = await this.unitOfWork.ZmstInstituteAgencyRepository.FindAllByAsync(x => x.AgencyId == agencyId, cancellationToken).ConfigureAwait(false);
                    var institute = await this.unitOfWork.ZmstInstituteRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                    var instagencylist = from instagency in instc
                                         join institutelist in institute on instagency.InstCd equals institutelist.InstCd
                                         select new AbsModels.ZmstInstitute
                                         {
                                             InstCd = institutelist.InstCd,
                                             InstNm = institutelist.InstNm,
                                             InstTypeId = institutelist.InstTypeId,
                                             SeatType = institutelist.SeatType,
                                             AgencyId = instagency.AgencyId,
                                             InstAdd = institutelist.InstAdd,
                                             State = institutelist.State,
                                             District = institutelist.District,
                                             Pincode = institutelist.Pincode,
                                             InstPhone = institutelist.InstPhone,
                                             InstFax = institutelist.InstFax,
                                             InstWebSite = institutelist.InstWebSite,
                                             EmailId = institutelist.EmailId,
                                             AltEmailId = institutelist.AltEmailId,
                                             ContactPerson = institutelist.ContactPerson,
                                             Designation = institutelist.Designation,
                                             MobileNo = institutelist.MobileNo,
                                             OldInstituteCode = institutelist.OldInstituteCode,
                                         };

                    return instagencylist.ToList();
                }
                else if (fiterInstituteCount.mode == "T")
                {
                    string instTypeId = fiterInstituteCount.id;
                    var instc = await this.unitOfWork.ZmstInstituteRepository.FindAllByAsync(x => x.InstTypeId == instTypeId, cancellationToken).ConfigureAwait(false);
                    var institute = await this.unitOfWork.ZmstInstituteTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                    var instituteTypelist = from instagency in instc
                                            join instlist in institute on instagency.InstTypeId equals instlist.InstituteTypeId
                                            select new AbsModels.ZmstInstitute
                                            {
                                                InstCd = instagency.InstCd,
                                                InstNm = instagency.InstNm,
                                                InstTypeId = instagency.InstTypeId,
                                                SeatType = instagency.SeatType,
                                                InstAdd = instagency.InstAdd,
                                                State = instagency.State,
                                                District = instagency.District,
                                                Pincode = instagency.Pincode,
                                                InstPhone = instagency.InstPhone,
                                                InstFax = instagency.InstFax,
                                                InstWebSite = instagency.InstWebSite,
                                                EmailId = instagency.EmailId,
                                                AltEmailId = instagency.AltEmailId,
                                                ContactPerson = instagency.ContactPerson,
                                                Designation = instagency.Designation,
                                                MobileNo = instagency.MobileNo,
                                                OldInstituteCode = instagency.OldInstituteCode,
                                            };

                    return instituteTypelist.ToList();
                }
                else if (fiterInstituteCount.mode == "S")
                {
                    string stateId = fiterInstituteCount.id;
                    var statec = await this.unitOfWork.MdStateRepository.FindAllByAsync(x => x.Id == stateId, cancellationToken).ConfigureAwait(false);
                    var institute = await this.unitOfWork.ZmstInstituteRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                    var inststatelist = from instagency in statec
                                        join institutelist in institute on instagency.Id equals institutelist.State
                                        select new AbsModels.ZmstInstitute
                                        {
                                            InstCd = institutelist.InstCd,
                                            InstNm = institutelist.InstNm,
                                            InstTypeId = institutelist.InstTypeId,
                                            SeatType = institutelist.SeatType,
                                            InstAdd = institutelist.InstAdd,
                                            State = institutelist.State,
                                            District = institutelist.District,
                                            Pincode = institutelist.Pincode,
                                            InstPhone = institutelist.InstPhone,
                                            InstFax = institutelist.InstFax,
                                            InstWebSite = institutelist.InstWebSite,
                                            EmailId = institutelist.EmailId,
                                            AltEmailId = institutelist.AltEmailId,
                                            ContactPerson = institutelist.ContactPerson,
                                            Designation = institutelist.Designation,
                                            MobileNo = institutelist.MobileNo,
                                            OldInstituteCode = institutelist.OldInstituteCode,
                                        };

                    return inststatelist.ToList();
                }
                else if (fiterInstituteCount.mode == "SM")
                {

                    string stateId = fiterInstituteCount.id;
                    var instc = await this.unitOfWork.ZmstInstituteStreamRepository.FindAllByAsync(x => x.StreamId == stateId, cancellationToken).ConfigureAwait(false);
                    var institute = await this.unitOfWork.ZmstInstituteRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                    var inststreamlist = from instcode in instc
                                         join institutelist in institute on instcode.InstCd equals institutelist.InstCd
                                         select new AbsModels.ZmstInstitute
                                         {
                                             InstCd = institutelist.InstCd,
                                             InstNm = institutelist.InstNm,
                                             InstTypeId = institutelist.InstTypeId,
                                             SeatType = institutelist.SeatType,
                                             InstAdd = institutelist.InstAdd,
                                             State = institutelist.State,
                                             District = institutelist.District,
                                             Pincode = institutelist.Pincode,
                                             InstPhone = institutelist.InstPhone,
                                             InstFax = institutelist.InstFax,
                                             InstWebSite = institutelist.InstWebSite,
                                             EmailId = institutelist.EmailId,
                                             AltEmailId = institutelist.AltEmailId,
                                             ContactPerson = institutelist.ContactPerson,
                                             Designation = institutelist.Designation,
                                             MobileNo = institutelist.MobileNo,
                                             OldInstituteCode = institutelist.OldInstituteCode,
                                         };

                    return inststreamlist.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

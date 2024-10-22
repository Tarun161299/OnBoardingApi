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
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class MdMinistryDirector : IMdMinistryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdMinistryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdMinistryDirector(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            httpContextAccessor = httpContext;
        }

        /// <inheritdoc />
        public virtual async Task<List<Abstractions.Models.MdMinistry>> GetAllAsync(CancellationToken cancellationToken)
        {
            var ministrylist = await this.unitOfWork.MdMinistryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<Abstractions.Models.MdMinistry>>(ministrylist);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int ministryId, CancellationToken cancellationToken)
        {
            if (ministryId == 0)
            {
                throw new ArgumentNullException(nameof(ministryId));
            }

            var entity = await this.unitOfWork.MdMinistryRepository.FindByAsync(
                x => x.MinistryId == ministryId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Ministries with an MinistryId {ministryId} was not found.");
            }

            await this.unitOfWork.MdMinistryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdMinistryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<Abstractions.Models.MdMinistry> GetByIdAsync(int ministryId, CancellationToken cancellationToken)
        {
            var ministrylist = await this.unitOfWork.MdMinistryRepository.FindByAsync(x => x.MinistryId == ministryId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdMinistry>(ministrylist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync(Abstractions.Models.MdMinistry mdMinistry, CancellationToken cancellationToken)
        {
            //context.HttpContext.Request.Headers["Session"];
            httpContextAccessor.HttpContext.Session.SetString(Abstractions.Models.SessionVariable.Salt, "asfdasfds");
            int result = 0;
            if (mdMinistry == null)
            {
                throw new ArgumentNullException(nameof(mdMinistry));
            }

            if (mdMinistry != null)
            {
                return await this.CommitMdMinistryChangesAsync(mdMinistry, cancellationToken);
            }

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<int> SaveAsync(int ministryId, string ministryName, CancellationToken cancellationToken)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@MinistryId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = ministryId,
                },
                new SqlParameter()
                {
                    ParameterName = "@MinisrtyName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = ministryName,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
            };

            var storedProcedureName = $"{"SP_InsertMinistry"}  @MinistryId,@MinisrtyName,@IsError output";
            int result = await this.unitOfWork.MdMinistryRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);

            bool s = (bool)param[2].Value;
            return result;

            /*
            if (ministryId == null || ministryId == 0)
            {
                throw new ArgumentNullException(nameof(ministryId));
            }
            var checkMinistry = await this.unitOfWork.MdMinistryRepository.FindByAsync(r => r.MinistryId == ministryId,cancellationToken).ConfigureAwait(false);
            if (checkMinistry != null)
            {
                throw new EntityFoundException($"This Records {checkMinistry} already exists");
            }
            Data.EF.Models.MdMinistry mdMinistry = new Data.EF.Models.MdMinistry();
            mdMinistry.MinistryId = ministryId;
            mdMinistry.MinistryName = ministryName;
            await this.unitOfWork.MdMinistryRepository.InsertAsync(mdMinistry, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            */
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(int ministryId, string ministryName, CancellationToken cancellationToken)
        {
            if (ministryId == 0)
            {
                throw new ArgumentException(nameof(ministryId));
            }

            var events = unitOfWork.MdMinistryRepository.FindAllBy(e => e.MinistryId == ministryId);
            foreach (var evt in events)
            {
                evt.MinistryId = Convert.ToInt32(ministryId);
                evt.MinistryName = ministryName;
                await unitOfWork.MdMinistryRepository.UpdateAsync(evt, cancellationToken).ConfigureAwait(false);
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task<Abstractions.Models.MdMinistryRequestInfoList> GetMultipleByIdAsync(int ministryId, string requestid, CancellationToken cancellationToken)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@MinistryId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = ministryId,
                },
                new SqlParameter()
                {
                    ParameterName = "@RequestId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = requestid,
                },
            };
            var ministryRequestInfoList = new MdMinistryRequestInfoList();
            using (var connection = unitOfWork.OBSDBContext.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + "SP_MultipleValueReturn @MinistryId,@RequestId";
                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }

                List<Abstractions.Models.MdMinistry> lastMdMinistry = new List<Abstractions.Models.MdMinistry>();
                List<Abstractions.Models.RequestList> lastRequestList = new List<Abstractions.Models.RequestList>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lastMdMinistry.Add(new Abstractions.Models.MdMinistry
                        {
                            MinistryId = reader.GetInt32(reader.GetOrdinal("MinistryId")),
                            MinistryName = reader.GetString(reader.GetOrdinal("MinistryName")),
                        });
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        lastRequestList.Add(new Abstractions.Models.RequestList
                        {
                            RequestId = Convert.ToString(reader.GetOrdinal("RequestId")),
                            OranizationName = Convert.ToString(reader.GetOrdinal("OranizationName")),
                        });
                    }
                }

                ministryRequestInfoList.MdMinistries = lastMdMinistry;
                ministryRequestInfoList.RequestLists = lastRequestList;
            }

            return Task.FromResult(ministryRequestInfoList);
        }

        private async Task<int> CommitMdMinistryChangesAsync(Abstractions.Models.MdMinistry mdMinistry, CancellationToken cancellationToken)
        {
            if (mdMinistry == null)
            {
                throw new ArgumentNullException(nameof(mdMinistry));
            }

            var ministry = await this.unitOfWork.MdMinistryRepository.FindByAsync(r => r.MinistryId == mdMinistry.MinistryId, default);
            if (ministry != null)
            {
                //throw new EntityFoundException($"This Ministry {mdMinistry} already exists");
            }

            var emdministry = this.mapper.Map<Data.EF.Models.MdMinistry>(mdMinistry);

            await this.unitOfWork.MdMinistryRepository.InsertAsync(emdministry, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

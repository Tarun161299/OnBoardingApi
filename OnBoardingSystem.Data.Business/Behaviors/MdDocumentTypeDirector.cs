//-----------------------------------------------------------------------
// <copyright file="MdDocumentTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class MdDocumentTypeDirector : IMdDocumentTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDocumentTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdDocumentTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<MdDocumentType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var documentTypelist = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken);
            var result = this.mapper.Map<List<Abstractions.Models.MdDocumentType>>(documentTypelist);
            return result.OrderBy(x => x.Id).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<List<MdDocumentType>> GetByRoleAsync(string Role,CancellationToken cancellationToken)
        {
            try
            {
                var Md_DocumentType = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken);
                var AppDocumentroleMapping = await this.unitOfWork.AppDocumentTypeRoleMapping.GetAllAsync(cancellationToken);

                var result = from mddocumenttype in Md_DocumentType
                             join documenttyperolemapping in AppDocumentroleMapping on mddocumenttype.Id
                             equals documenttyperolemapping.DocumentTypeId
                             where documenttyperolemapping.RoleId == Role
                             select new MdDocumentType
                             {
                                 Id = mddocumenttype.Id,
                                 Title = mddocumenttype.Title,
                                 Format = mddocumenttype.Format,
                                 MinSize = mddocumenttype.MinSize,
                                 MaxSize = mddocumenttype.MaxSize,
                                 DisplayPriority = mddocumenttype.DisplayPriority,
                                 DocumentNatureType = mddocumenttype.DocumentNatureType,
                                 DocumentNatureTypeDesc = mddocumenttype.DocumentNatureTypeDesc,
                                 IsPasswordProtected = mddocumenttype.IsPasswordProtected,
                                 CreatedDate = mddocumenttype.CreatedDate,
                                 CreatedBy = mddocumenttype.CreatedBy,
                                 ModifiedDate = mddocumenttype.ModifiedDate,
                                 ModifiedBy = mddocumenttype.ModifiedBy,
                             };
                return result.OrderBy(x => x.Id).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

using AutoMapper;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Models;
using OnBoardingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    public class ConfigurationAPISecureKeyDirector: IConfigurationAPISecureKeyDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationAPISecureKeyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ConfigurationAPISecureKeyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        
        /// <inheritdoc />
        public virtual async Task<List<ConfigurationApisecureKey>> GetAllAsync(CancellationToken cancellationToken)
        {
            var configurationAPISecureKeyList = await this.unitOfWork.ConfigurationAPISecureKeyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<ConfigurationApisecureKey>>(configurationAPISecureKeyList);
        }
    }
}


namespace OnBoardingSystem.Data.Abstractions.Configuration.Http
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic Api Service to handle calls to Api resources.
    /// </summary>
    public interface IGenericApiService
    {
        /// <summary>
        /// Get IEnumerable of TDtoModel.
        /// </summary>
        /// <typeparam name="TDtoModel">Model of resource.</typeparam>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns><see cref="IEnumerable{TDtoModel}"/>.</returns>
        Task<IEnumerable<TDtoModel>> GetAllAsync<TDtoModel>(string endpoint, CancellationToken cancellationToken);

        /// <summary>
        /// Get singular TDtoModel.
        /// </summary>
        /// <typeparam name="TDtoModel">Model of resource.</typeparam>
        /// <param name="endpoint">Endpoint of the resource.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Singular instance of requested resource.</returns>
        Task<TDtoModel> GetAsync<TDtoModel>(string endpoint, CancellationToken cancellationToken);

        /// <summary>
        ///  Sets token for passing to another service.
        /// </summary>
        /// <param name="token">access token.</param>
        public void AccessToken(string token);
    }
}

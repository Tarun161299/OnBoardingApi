

namespace OnBoardingSystem.Data.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using OnBoardingSystem.Data.Abstractions.Configuration.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Generic Api Service to handle calls to Api resources.
    /// </summary>
    public class GenericApiService : IGenericApiService
    {
        /// <summary>
        /// Content type for Json Media Type.
        /// </summary>
        protected const string JsonMediaContentType = "application/json";

        private readonly HttpClient httpClient;

        private string token;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApiService"/> class.
        /// </summary>
        /// <param name="httpClient">Http Client.</param>
        public GenericApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <inheritdoc/>
        public void AccessToken(string token)
        {
            this.token = token;

            if (this.token != null)
            {
                if (httpClient != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDtoModel>> GetAllAsync<TDtoModel>(string endpoint, CancellationToken cancellationToken)
        {
            var response = await httpClient
                .GetAsync(endpoint, cancellationToken)
                .ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }

                throw;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<TDtoModel>>(content);
        }

        /// <inheritdoc/>
        public virtual async Task<TDtoModel> GetAsync<TDtoModel>(string endpoint, CancellationToken cancellationToken)
        {
            var response = await httpClient
                .GetAsync(endpoint, cancellationToken)
                .ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }

                throw;
            }

            var content = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<TDtoModel>(content);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.Sterling;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter
{
    public class AGTIntegrationAdapter : IAGTIntegrationAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientConnectionModel _httpClientConnection;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IWebHostEnvironment _env;
        public AGTIntegrationAdapter(
        IHttpClientFactory httpClientFactory,
        IOptions<HttpClientConfigModel> connectionOptions,
        IWebHostEnvironment env,
        IStringLocalizer<SharedResources> stringLocalizer)
        {
            _httpClientFactory = httpClientFactory;
            _stringLocalizer = stringLocalizer;
            _httpClientConnection = connectionOptions.Value.AGTConnection;
            _env = env;
        }
        public async Task<ServiceResultModel<FOMOrderResponseModel>> SendFOMOrderToAGTAsync(FOMOrderRequestModel input)
        {
            var client = _httpClientConnection.GetHttpClient(_httpClientFactory);

            var response = await client.PostAsJsonAsync("samedaydelivery/create-fom-order", input);

            var responseContent = await response?.Content?.ReadAsStringAsync();

            client.Dispose();

            var result = new ServiceResultModel<FOMOrderResponseModel>
            {
                Code = (int)response.StatusCode,
                Data = string.IsNullOrWhiteSpace(responseContent) || !response.IsSuccessStatusCode ?
          default :
              JsonConvert.DeserializeObject<FOMOrderResponseModel>(responseContent),
                Message = !response.IsSuccessStatusCode ? response.ReasonPhrase : string.Empty
            };

            if (!response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
            {
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var errorMessage = responseJson?.error?.message;

                result.Message = $"{_stringLocalizer["Messages.Integration.Error"].Value}: {errorMessage ?? result.Message}";
            }

            return result;
        }
    }
}

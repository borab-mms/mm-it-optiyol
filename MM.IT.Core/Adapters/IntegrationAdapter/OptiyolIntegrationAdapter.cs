using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using MM.Optiyol.Api.Models.Optiyol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter
{
    public class OptiyolIntegrationAdapter : IOptiyolIntegrationAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientConnectionModel _httpClientConnection;
        public OptiyolIntegrationAdapter(IHttpClientFactory httpClientFactory, IOptions<HttpClientConfigModel> connectionOptions)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientConnection = connectionOptions.Value.OptiyolConnection;
        }
        public async Task<ServiceResultModel<OptiyolBarcodeCancelResponseModel>> CancelBarcodeAsync(OptiyolBarcodeCancelRequestModel model)
        {
            try
            {
                var client = _httpClientConnection.GetHttpClient(_httpClientFactory);

                client.DefaultRequestHeaders.Add("Authorization", _httpClientConnection.Parameters["Authorization"]);

                var response = await client.PostAsJsonAsync($"/api/unplanneds/v1/cancel-order/?company_id={_httpClientConnection.Parameters["CompanyId"]}", model);

                var responseContent = await response?.Content?.ReadAsStringAsync();

                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                client.Dispose();

                var result = new ServiceResultModel<OptiyolBarcodeCancelResponseModel>
                {
                    Code = (int)response.StatusCode,
                    Data = string.IsNullOrWhiteSpace(responseContent) ?
                default :
                    JsonConvert.DeserializeObject<OptiyolBarcodeCancelResponseModel>(responseContent),
                    Message = !response.IsSuccessStatusCode ? response.ReasonPhrase : string.Empty
                };

                if (!response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
                {
                    var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var errorMessage = responseJson?.error?.message;

                    result.Message = errorMessage ?? string.Empty;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<OptiyolBarcodeCancelResponseModel> { Data = new OptiyolBarcodeCancelResponseModel { } };
            }
        }
    }
}

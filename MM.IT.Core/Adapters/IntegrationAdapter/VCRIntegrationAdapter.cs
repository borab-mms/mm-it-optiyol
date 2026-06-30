using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.Integration.VCR;
using MM.IT.Common.Models.Sms;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using Nancy;
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
    public class VCRIntegrationAdapter : IVCRIntegrationAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientConnectionModel _httpClientConnection;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IWebHostEnvironment _env;
        public VCRIntegrationAdapter(
            IHttpClientFactory httpClientFactory,
            IOptions<HttpClientConfigModel> connectionOptions,
            IWebHostEnvironment env,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _httpClientFactory = httpClientFactory;
            _stringLocalizer = stringLocalizer;
            _httpClientConnection = connectionOptions.Value.VCRConnection;
            _env = env;
        }
        public async Task<ServiceResultModel<VCRInvoiceResponseModel>> CreateInvoiceAsync(VCRInvoiceRequestModel input)
        {
            try
            {
                var client = _httpClientConnection.GetHttpClient(_httpClientFactory);

                var authInput = new
                {
                    UserName = _httpClientConnection.Username,
                    Password = _httpClientConnection.Password
                };

                var authResponse = await client.PostAsJsonAsync("/vcr-api/auth/login", authInput);

                if (!authResponse.IsSuccessStatusCode)
                {
                    return new ServiceResultModel<VCRInvoiceResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = _stringLocalizer["Messages.Integration.Error.Token"].Value,
                        Data = null
                    };
                }

                var authResponseContent = await authResponse.Content.ReadFromJsonAsync<VCRAuthResponseModel>();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponseContent.Data.Token);

                var response = await client.PostAsJsonAsync("/vcr-api/createInvoice", input);

                //var responseContent = await response?.Content?.ReadAsStringAsync();

                var jsonContent = await response?.Content?.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<VCRInvoiceResponseModel>(jsonContent);

                client.Dispose();

                var result = new ServiceResultModel<VCRInvoiceResponseModel>
                {
                    Code = (int)response.StatusCode,
                    Data = responseContent,
                    Message = !response.IsSuccessStatusCode ? response.ReasonPhrase : string.Empty
                };

                //if (!response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(jsonContent))
                //{
                //    var responseJson = JsonConvert.DeserializeObject<dynamic>(jsonContent);
                //    var errorMessage = responseJson?.error?.message;

                //    result.Message = $"{_stringLocalizer["Messages.Integration.Error"].Value}: {errorMessage ?? result.Message}";
                //}

                return result;

            }
            catch (Exception ex)
            {
                var result = new ServiceResultModel<VCRInvoiceResponseModel>
                {
                    Code = 404,
                    Message = ex.Message
                };

                return result;
            }
        }

    }
}

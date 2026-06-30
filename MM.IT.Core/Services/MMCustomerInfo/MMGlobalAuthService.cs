using Microsoft.Extensions.Logging;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Common.Models.Common;
using IdentityModel.OidcClient;
using MM.IT.Core.Services.Base;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using Org.BouncyCastle.Asn1.Ocsp;
using MM.IT.Common.Models.EKOLStock;
using System.Xml.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using MM.IT.Common.Enums;
using MM.IT.Common.Extensions;
using Microsoft.Extensions.Localization;
using MM.IT.Core.Adapters.IntegrationAdapter;
using System.Net.Http.Json;

namespace MM.IT.Core.Services.MMCustomerInfo
{

    public class MMGlobalAuthService : BaseService, IMMGlobalAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientConnectionModel _httpClientConnection;
        private readonly ILogger<MMGlobalAuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<HttpClientConfigModel> _httpClientConfigModel;
        public MMGlobalAuthService(IServiceProvider serviceProvider
            , ILogger<MMGlobalAuthService> logger
            , IConfiguration configuration
            , IOptions<HttpClientConfigModel> httpClientConfigModel
            , IHttpClientFactory httpClientFactory
            , IOptions<HttpClientConfigModel> connectionOptions) : base(serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientConfigModel = httpClientConfigModel;
            _httpClientFactory = httpClientFactory;
            _httpClientConnection = connectionOptions.Value.MMGlobalV3AuthConnection;
        }
        //public async Task<ServiceResultModel<AuthResponseModel>> GetTokenOld(AuthRequestModel model)
        //{
        //    try
        //    {
        //        #region checkModel

        //        if (string.IsNullOrEmpty(model.username))
        //        {
        //            return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.NotNullUsername.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullUsername);
        //        }
        //        if (string.IsNullOrEmpty(model.password))
        //        {
        //            return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.NotNullPassword.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullPassword);
        //        }

        //        #endregion

        //        #region proxy
        //        AuthResponseModel response = new AuthResponseModel();
        //        var proxy = new WebProxy
        //        {
        //            Address = new Uri("http://ciscowsa.media-saturn.com:80/"),
        //            BypassProxyOnLocal = false,
        //            UseDefaultCredentials = false
        //        };
        //        HttpClientHandler clientHandler = new HttpClientHandler()
        //        {
        //            //Proxy = proxy
        //        };
        //        //clientHandler.PreAuthenticate = true;
        //        //clientHandler.UseDefaultCredentials = false;

        //        //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        //        using (var httpClient = new HttpClient(clientHandler))
        //        {
        //            httpClient.BaseAddress = new Uri(_httpClientConfigModel.Value.MMGlobalV3AuthConnection.BaseUrl);
        //            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(model.username + ":" + model.password));
        //            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
        //            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
        //                {
        //                    new KeyValuePair<string, string>( "grant_type", "client_credentials" )
        //                };
        //            HttpContent content = new FormUrlEncodedContent(pairs);

        //            HttpResponseMessage tokenResponse = await httpClient.PostAsync(httpClient.BaseAddress, content);
        //            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
        //            response = JsonConvert.DeserializeObject<AuthResponseModel>(jsonContent);
        //            return Result<AuthResponseModel>(response);
        //        }

        //        #endregion

        //        #region Old
        //        //AuthResponseModel response = new AuthResponseModel();

        //        //var client = _httpClientConnection.GetHttpClient(_httpClientFactory);

        //        //client.BaseAddress = new Uri(_httpClientConfigModel.Value.MMGlobalAuthConnection.BaseUrl);
        //        //string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(model.username + ":" + model.password));
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
        //        //List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
        //        //        {
        //        //            new KeyValuePair<string, string>( "grant_type", "client_credentials" )
        //        //        };
        //        //HttpContent content = new FormUrlEncodedContent(pairs);

        //        //var result = await client.PostAsync("", content);

        //        //var responseContent = await result?.Content?.ReadAsStringAsync();

        //        //client.Dispose();
        //        //if (responseContent != null)
        //        //{
        //        //    response = JsonConvert.DeserializeObject<AuthResponseModel>(responseContent);
        //        //}

        //        //return Result<AuthResponseModel>(response);

        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"<AuthService>GetToken:{ex.Message}</AuthService>");
        //        return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.WrongUsernameOrPassword.GetDisplayName(), (int)MMCustomerInfoMessageCodes.WrongUsernameOrPassword);

        //    }
        //}
        public async Task<ServiceResultModel<AuthResponseModel>> GetToken(AuthRequestModel model)
        {
            try
            {
                #region checkModel

                if (string.IsNullOrEmpty(model.username))
                {
                    return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.NotNullUsername.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullUsername);
                }
                if (string.IsNullOrEmpty(model.password))
                {
                    return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.NotNullPassword.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullPassword);
                }

                #endregion

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_httpClientConnection.BaseUrl);

                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(model.username + ":" + model.password));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
                List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "client_credentials" )
                        };
                HttpContent content = new FormUrlEncodedContent(pairs);
                var authResponse = await client.PostAsync("/login/oauth/token", content);

                var jsonContent = await authResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthResponseModel>(jsonContent);
                client.Dispose();

                return Result(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"<AuthService>GetToken:{ex.Message}</AuthService>");
                return Result<AuthResponseModel>(null, MMCustomerInfoMessageCodes.WrongUsernameOrPassword.GetDisplayName(), (int)MMCustomerInfoMessageCodes.WrongUsernameOrPassword);

            }
        }
        private T GetDeserializedData<T>(string data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null

            });

        }
    }
}

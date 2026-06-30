using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MarketPlace
{
    public class ESBClientService : IESBClientService
    {
        private readonly ILogger<ESBClientService> _logger;
        private readonly IOptions<HttpClientConfigModel> _httpClientConfigModel;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        public ESBClientService(ILogger<ESBClientService> logger
         , IConfiguration configuration
         , HttpClient httpClient
         , IOptions<HttpClientConfigModel> httpClientConfigModel)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientConfigModel = httpClientConfigModel;

            httpClient.BaseAddress = new Uri(_httpClientConfigModel.Value.ESBConnection.BaseUrl);
            _client = httpClient;
        }
        public async Task<T> PostAsync<T>(string url, HttpContent contentPost)
        {
            #region withProxy
            //T data;

            //try
            //{
            //    var proxy = new WebProxy
            //    {
            //        Address = new Uri("http://ciscowsa.media-saturn.com:80/"),
            //        BypassProxyOnLocal = false,
            //        UseDefaultCredentials = false
            //    };
            //    HttpClientHandler clientHandler = new HttpClientHandler()
            //    {
            //        Proxy = proxy
            //    };

            //    clientHandler.PreAuthenticate = true;
            //    clientHandler.UseDefaultCredentials = false;

            //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //    HttpResponseMessage response;

            //    using (var httpClient = new HttpClient(clientHandler))
            //    {
            //        httpClient.BaseAddress = new Uri(_httpClientConfigModel.Value.ESBConnection.BaseUrl);

            //        response = await httpClient.PostAsync(url, contentPost);
            //        using (HttpContent content = response.Content)
            //        {
            //            string stringResult = await content.ReadAsStringAsync();
            //            if (!string.IsNullOrEmpty(stringResult))
            //            {
            //                data = GetDeserializedData<T>(stringResult);
            //                return (T)data;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"<OfflineProductClientService>GetProxyAsync:{ex.Message}</OfflineProductClientService>");
            //}

            //Object o = new Object();
            //return (T)o;

            #endregion

            T data;

            using (HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + url, contentPost))
            using (HttpContent content = response.Content)
            {
                string stringResult = await content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(stringResult))
                {
                    data = GetDeserializedData<T>(stringResult);
                    return (T)data;
                }
            }
            Object o = new Object();
            return (T)o;
        }
        private T GetDeserializedData<T>(string data)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = null

                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"<OfflineProductClientService>GetDeserializedData:{ex.Message}</OfflineProductClientService>");

            }
            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null

            });

        }
    }
}

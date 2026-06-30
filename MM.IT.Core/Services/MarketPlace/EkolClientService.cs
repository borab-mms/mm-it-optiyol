using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MM.IT.Core.Services.MarketPlace.Interfaces;

namespace MM.IT.Core.Services.MarketPlace
{
    public class EkolClientService: IEkolClientService
    {
        private readonly ILogger<EkolClientService> _logger;
        HttpClient _client;
        private readonly IConfiguration _configuration;

        public EkolClientService(HttpClient httpClient, IConfiguration configuration, ILogger<EkolClientService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            httpClient.BaseAddress = new Uri("https://int.ekol.com:4092/ws/simple/");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YXllbnNvZnRAZWtvbGxvamlzdGlrLVpNM0UxVzpiNGMwZWI1OS00MTY1LTRhYTEtOTc2OS1iNzU4ZGJlOGYwN2Y=");
            _client = httpClient;
        }
        public async Task<T> PostAsync<T>(string url, HttpContent contentPost)
        {
            #region MyRegion
            //T data;
            //try
            //{

            //    using (HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + url, contentPost))
            //    using (HttpContent content = response.Content)
            //    {
            //        string stringResult = await content.ReadAsStringAsync();
            //        if (!string.IsNullOrEmpty(stringResult))
            //        {
            //            data = GetDeserializedData<T>(stringResult);
            //            return (T)data;
            //        }
            //    }
            //    Object o = new Object();
            //    return (T)o;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Hata:" + ex.Message);
            //    Object o = new Object();
            //    return (T)o;
            //}
            #endregion
            T data;
            var proxy = new WebProxy
            {
                //Address = new Uri("http://ciscowsa.media-saturn.com:80/"),
                //BypassProxyOnLocal = false,
                //UseDefaultCredentials = false
            };
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                //Proxy = proxy
            };

            //clientHandler.PreAuthenticate = true;
            //clientHandler.UseDefaultCredentials = false;

            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpResponseMessage response;

            using (var httpClient = new HttpClient(clientHandler))
            {
                httpClient.BaseAddress = new Uri("https://int.ekol.com:4092/ws/simple/");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YXllbnNvZnRAZWtvbGxvamlzdGlrLVpNM0UxVzpiNGMwZWI1OS00MTY1LTRhYTEtOTc2OS1iNzU4ZGJlOGYwN2Y=");

                response = await httpClient.PostAsync(url, contentPost);
                using (HttpContent content = response.Content)
                {
                    string stringResult = await content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        data = GetDeserializedData<T>(stringResult);
                        return (T)data;
                    }
                }
            }
            Object o = new Object();
            return (T)o;
        }
        private T GetDeserializedData<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null

            });

        }
    }
}

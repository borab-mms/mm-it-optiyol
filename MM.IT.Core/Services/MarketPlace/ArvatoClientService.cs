using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.MarketPlace;
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
    public class ArvatoClientService : IArvatoClientService
    {
        private readonly ILogger<ArvatoClientService> _logger;
        HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly HttpClientConnectionModel _httpClientConnection;

        public ArvatoClientService(HttpClient httpClient, IConfiguration configuration, ILogger<ArvatoClientService> logger
            , IOptions<HttpClientConfigModel> connectionOptions)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientConnection = connectionOptions.Value.ArvatoAuthConnection;
            httpClient.BaseAddress = new Uri(_httpClientConnection.BaseUrl);
            _client = httpClient;
        }
        public async Task<T> PostAsync<T>(string url, HttpContent contentPost)
        {
            try
            {
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
                    httpClient.BaseAddress = new Uri("https://prodslotservices.arvatoscm.com.tr/extapi/");
                    var myAuth = GetToken("common/authenticate");
                    httpClient.DefaultRequestHeaders.Add("token", myAuth.Result.data.token);

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
            catch (Exception ex)
            {
                Object o = new Object();
                return (T)o;
            }
        }
        public async Task<ArvatoAuthResponseModel> GetToken(string url)
        {
            #region Proxy

            //ArvatoAuthResponseModel data = new ArvatoAuthResponseModel();
            //try
            //{
            //    ArvatoAuthRequestModel authRequest = new ArvatoAuthRequestModel()
            //    {
            //        userName = "omer.orkmez",
            //        password = "OO_123_QAWSX"
            //    };
            //    var contentPost = new StringContent(JsonSerializer.Serialize(authRequest), Encoding.UTF8, "application/json");

            //    //var proxy = new WebProxy
            //    //{
            //    //    Address = new Uri("http://ciscowsa.media-saturn.com:80/"),
            //    //    BypassProxyOnLocal = false,
            //    //    UseDefaultCredentials = false
            //    //};
            //    HttpClientHandler clientHandler = new HttpClientHandler()
            //    {
            //        //Proxy = proxy
            //    };

            //    //clientHandler.PreAuthenticate = true;
            //    //clientHandler.UseDefaultCredentials = false;

            //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //    HttpResponseMessage response;

            //    using (var httpClient = new HttpClient(clientHandler))
            //    {
            //        httpClient.BaseAddress = new Uri("https://prodslotservices.arvatoscm.com.tr/extapi/");
            //        response = await httpClient.PostAsync(url, contentPost);
            //        using (HttpContent content = response.Content)
            //        {
            //            string stringResult = await content.ReadAsStringAsync();
            //            if (!string.IsNullOrEmpty(stringResult))
            //            {
            //                data = GetDeserializedData<ArvatoAuthResponseModel>(stringResult);
            //                return data;
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("arvato PostAsync:" + ex.Message); ;
            //}
            //return data;

            #endregion

            ArvatoAuthResponseModel data = new ArvatoAuthResponseModel();
            try
            {
                ArvatoAuthRequestModel authRequest = new ArvatoAuthRequestModel()
                {
                    userName = _httpClientConnection.Username,
                    password = _httpClientConnection.BaseUrl
                };
                var contentPost = new StringContent(JsonSerializer.Serialize(authRequest), Encoding.UTF8, "application/json");

                var proxy = new WebProxy
                {
                    Address = new Uri("http://ciscowsa.media-saturn.com:80/"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false
                };
                HttpClientHandler clientHandler = new HttpClientHandler()
                {
                    Proxy = proxy
                };

                clientHandler.PreAuthenticate = true;
                clientHandler.UseDefaultCredentials = false;

                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpResponseMessage response;

                using (var httpClient = new HttpClient(clientHandler))
                {
                    httpClient.BaseAddress = new Uri(_httpClientConnection.BaseUrl);
                    response = await httpClient.PostAsync(url, contentPost);
                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            data = GetDeserializedData<ArvatoAuthResponseModel>(stringResult);
                            return data;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("arvato PostAsync:" + ex.Message); ;
            }
            return data;
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

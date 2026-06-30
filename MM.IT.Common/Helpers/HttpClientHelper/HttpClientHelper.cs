using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Helpers.HttpClientHelper.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MM.IT.Common.Helpers.HttpClientHelper;
public class HttpClientHelper : IHttpClientHelper
{
    private readonly ILogger<HttpClientHelper> _logger;
    System.Net.Http.HttpClient client;
    public HttpClientHelper(ILogger<HttpClientHelper> logger, System.Net.Http.HttpClient client)
    {
        _logger = logger;
        this.client = client;
    }
    public async Task<T> GetDataWithBasicAuthAsync<T>(string baseUrl, string username, string password, string url)
    {
        T data;
        try
        {

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

            using (client = new System.Net.Http.HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseUrl);
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);

                response = await client.GetAsync(url);

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
        }
        catch (Exception ex)
        {
            _logger.LogError($"<HttpClientHelper>GetDataWithBasicAuthAsync:{ex}</HttpClientHelper>");

            return (T)new Object();
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


using MM.IT.Common.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter;

/// <summary>
/// HttpClientConnectionModelExtensions Extension Methodları
/// </summary>
public static class HttpClientConnectionModelExtensions
{
    /// <summary>
    /// HttpClient Nesnesini Oluşturup Döndürür.
    /// </summary>
    /// <param name="httpClientConnection"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="extraHeaders"></param>
    /// <returns></returns>
    public static HttpClient GetHttpClient(this HttpClientConnectionModel httpClientConnection, IHttpClientFactory httpClientFactory, Dictionary<string, string> extraHeaders = null)
    {
        var client = httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(httpClientConnection.BaseUrl);

        if (extraHeaders != null && extraHeaders.Count > 0)
        {
            foreach (var header in extraHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        return client;
    }
}
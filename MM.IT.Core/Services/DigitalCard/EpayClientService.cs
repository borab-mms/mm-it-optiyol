using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.DigitalCard;
using MM.IT.Core.Services.DigitalCard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MM.IT.Core.Services.DigitalCard
{
    public class EpayClientService : IEpayClientService
    {
        private readonly ILogger<EpayClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClientConnectionModel _epayConnection;
        public EpayClientService(ILogger<EpayClientService> logger
            , IConfiguration configuration
            , IOptions<HttpClientConfigModel> options)
        {
            _configuration = configuration;
            _logger = logger;
            _epayConnection = options.Value.EpayConnection;
        }
        public async Task<SerialCodeInEpayResponseModel> PostAsync(string url, HttpContent contentPost)
        {
            SerialCodeInEpayResponseModel result = new SerialCodeInEpayResponseModel();


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
                httpClient.BaseAddress = new Uri(_epayConnection.BaseUrl);

                response = await httpClient.PostAsync(url, contentPost);

                using (HttpContent content = response.Content)
                {
                    string stringResult = await content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        var xmlSer = new XmlSerializer(typeof(SerialCodeInEpayResponseModel), new XmlRootAttribute("RESPONSE"));
                        var stringReader = new StringReader(stringResult);
                        var reader = xmlSer.Deserialize(stringReader);


                        result = (SerialCodeInEpayResponseModel)reader;
                        result.ResponseData = stringResult;

                        return result;
                    }
                }
            }
            Object o = new Object();
            return (SerialCodeInEpayResponseModel)o;
        }
    }
}

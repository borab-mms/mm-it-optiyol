using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Core.Services.EKOLStock.Interfaces;
using NetTopologySuite.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MM.IT.Core.Services.EKOLStock
{

    public class EKOLDesiClientService : IEKOLDesiClientService
    {
        private readonly ILogger<EKOLDesiClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClientConnectionModel _eKOLDesiConnection;
        HttpClient _client;
        public EKOLDesiClientService(ILogger<EKOLDesiClientService> logger
            , IConfiguration configuration
            , HttpClient httpClient
            , IOptions<HttpClientConfigModel> options)
        {
            _configuration = configuration;
            _logger = logger;
            _client = httpClient;
            _eKOLDesiConnection = options.Value.EKOLDesiConnection;
            httpClient.BaseAddress = new Uri(_eKOLDesiConnection.BaseUrl);
        }
        public async Task<List<EKOLDesiModel>> PostXmlAsync(string url, HttpContent contentPost)
        {

            var eKOLDesiParams=_eKOLDesiConnection.Parameters.FirstOrDefault();
            _client.DefaultRequestHeaders.Add("Authorization", "Basic " + System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_eKOLDesiConnection.Username + ":" + _eKOLDesiConnection.Password)));
            //_client.DefaultRequestHeaders.Add(eKOLDesiParams.Key, eKOLDesiParams.Value);
            _client.DefaultRequestHeaders.Add("SOAPAction", "getArticleMeasurementsOut");
            _client.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

            using (HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + url, contentPost))
            using (HttpContent content = response.Content)
            {
                string stringResult = await content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(stringResult))
                {

                    XDocument xml = XDocument.Parse(stringResult);

                    var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "WSC_ARTICLE_MEASUREMENTS_TYPE").ToList();

                    List<EKOLDesiModel> lists = new List<EKOLDesiModel>();
                    try
                    {
                        foreach (var x in soapResponse)
                        {
                            EKOLDesiModel eKOLDesiModel = new EKOLDesiModel();

                            eKOLDesiModel.ORG_CODE = (string)x.Element(x.Name.Namespace + "ORG_CODE");
                            eKOLDesiModel.ARTICLE_CODE = (string)x.Element(x.Name.Namespace + "ARTICLE_CODE");
                            eKOLDesiModel.UOM = (string)x.Element(x.Name.Namespace + "UOM");
                            try
                            {
                                eKOLDesiModel.QUANTITY = (int)x.Element(x.Name.Namespace + "QUANTITY");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                eKOLDesiModel.WIDTH = (decimal)x.Element(x.Name.Namespace + "WIDTH");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                eKOLDesiModel.HEIGHT = (decimal)x.Element(x.Name.Namespace + "HEIGHT");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                eKOLDesiModel.LENGTH = (decimal)x.Element(x.Name.Namespace + "LENGTH");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                eKOLDesiModel.WEIGHT = (decimal)x.Element(x.Name.Namespace + "WEIGHT");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                eKOLDesiModel.NET_WEIGHT = (decimal)x.Element(x.Name.Namespace + "NET_WEIGHT");
                            }
                            catch (Exception)
                            {
                            }
                            eKOLDesiModel.BARCODE = (string)x.Element(x.Name.Namespace + "BARCODE");
                            eKOLDesiModel.WEIGHT_UNIT = (string)x.Element(x.Name.Namespace + "WEIGHT_UNIT");
                            try
                            {
                                eKOLDesiModel.QUANTITY_PER_UNIT = (decimal)x.Element(x.Name.Namespace + "QUANTITY_PER_UNIT");
                            }
                            catch (Exception)
                            {
                            }
                            eKOLDesiModel.LENGTH_UNIT = (string)x.Element(x.Name.Namespace + "LENGTH_UNIT");
                            try
                            {
                                eKOLDesiModel.DESI = (decimal)x.Element(x.Name.Namespace + "VOLUME");
                            }
                            catch (Exception)
                            {
                            }
                            eKOLDesiModel.PACKING_TYPE = (string)x.Element(x.Name.Namespace + "PACKING_TYPE");
                            eKOLDesiModel.VOLUME_UNIT = (string)x.Element(x.Name.Namespace + "VOLUME_UNIT");

                            lists.Add(eKOLDesiModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"<EKOLStockJob>AddOrUpdateArtikelDesiAsync:{ex}</EKOLStockJob>");
                    }

                    return lists;


                }
                return new List<EKOLDesiModel>();
            }
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

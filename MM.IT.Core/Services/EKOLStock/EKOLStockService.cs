using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.EKOLStock.Interfaces;
using OperationDataServiceReference;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MM.IT.Core.Services.EKOLStock
{
    public class EKOLStockService : BaseService, IEKOLStockService
    {
        private readonly ILogger<EKOLStockService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClientConnectionModel _eKOLStockConnection;
        HttpClient _client;
        public EKOLStockService(IServiceProvider serviceProvider
            , ILogger<EKOLStockService> logger
            , IConfiguration configuration
            , HttpClient httpClient
            , IOptions<HttpClientConfigModel> options) : base(serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _client = httpClient;
            _eKOLStockConnection = options.Value.EKOLStockConnection;
            httpClient.BaseAddress = new Uri(_eKOLStockConnection.BaseUrl);
        }
        public async Task<ServiceResultModel<List<EKOLStockModel>>> PostXmlAsync(string url, HttpContent contentPost)
        {
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

                using (var httpClient = new HttpClient(clientHandler))
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(450);
                    httpClient.BaseAddress = new Uri(_eKOLStockConnection.BaseUrl);
                    httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://schemas.ekol.com/clientdataexch/2011/IOperationDataService/GetStock");
                    httpClient.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

                    response = await httpClient.PostAsync(url, contentPost);

                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            XDocument xml = XDocument.Parse(stringResult);
                            var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "Stock").Select(x => new EKOLStockModel()
                            {
                                EKOLStockId = (decimal)x.Element(x.Name.Namespace + "Id"),
                                ArticleCode = (string)x.Element(x.Name.Namespace + "ArticleCode"),
                                AsnNo = (string)x.Element(x.Name.Namespace + "AsnNo"),
                                BatchNr = (string)x.Element(x.Name.Namespace + "BatchNr"),
                                ExpireDate = (DateTime)x.Element(x.Name.Namespace + "ExpireDate"),
                                OrgCode = (string)x.Element(x.Name.Namespace + "OrgCode"),
                                PackagingGroup = (string)x.Element(x.Name.Namespace + "PackagingGroup"),
                                QtyStock = (decimal)x.Element(x.Name.Namespace + "QtyStock"),
                                QtyStockInPlanned = (decimal)x.Element(x.Name.Namespace + "QtyStockInPlanned"),
                                QtyStockOutPlanned = (decimal)x.Element(x.Name.Namespace + "QtyStockOutPlanned"),
                                SerialNr = (string)x.Element(x.Name.Namespace + "SerialNr"),
                                StockLocation = (string)x.Element(x.Name.Namespace + "StockLocation"),
                                StockType = (string)x.Element(x.Name.Namespace + "StockType"),
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            }).ToList();

                            return Result(soapResponse);
                        }

                    }
                }
                return Result<List<EKOLStockModel>>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                return Result<List<EKOLStockModel>>(null, "Bilinmeyen hata:" + ex.Message, StatusCodes.Status400BadRequest);
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

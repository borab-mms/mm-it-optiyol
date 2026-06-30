using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MarketPlace.AyenSoft;
using MM.IT.Common.Models.OnlineOrder;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.EKOLStock;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MM.IT.Core.Services.MarketPlace
{
    public class AyenSoftClientService : BaseService, IAyenSoftClientService
    {
        private readonly ILogger<AyenSoftClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClientConnectionModel _ayenSoftConnection;
        HttpClient _client;
        public AyenSoftClientService(IServiceProvider serviceProvider
            , ILogger<AyenSoftClientService> logger
            , IConfiguration configuration
            , IOptions<HttpClientConfigModel> options
            , HttpClient httpClient) :base(serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _ayenSoftConnection = options.Value.AyenSoftConnection;
            _client = httpClient;

        }
        public async Task<ServiceResultModel<IEnumerable<AyenSoftResponseModel>>> AnlikStokFiyatGuncelleAsync(string url, HttpContent contentPost)
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
                    httpClient.Timeout = TimeSpan.FromSeconds(150);
                    httpClient.BaseAddress = new Uri(_ayenSoftConnection.BaseUrl);
                    httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://tempuri.org/AnlikStokFiyatGuncelle");
                    //httpClient.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

                    response = await httpClient.PostAsync(url, contentPost);

                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            XDocument xml = XDocument.Parse(stringResult);
                            var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "PlatformBazliMusteriUrunModel").Select(x => new AyenSoftResponseModel()
                            {
                                StokKodu = (int)x.Element(x.Name.Namespace + "StokKodu"),
                                //Fiyat = (decimal)x.Element(x.Name.Namespace + "Fiyat "),
                                //PsfFiyat = (decimal)x.Element(x.Name.Namespace + "PsfFiyat"),
                                //Stok = (int)x.Element(x.Name.Namespace + "Stok"),
                                //MusteriTedarikciId = (int)x.Element(x.Name.Namespace + "MusteriTedarikciId"),
                                Success = (bool)x.Element(x.Name.Namespace + "Success")
                            }).AsEnumerable();

                            return Result(soapResponse);
                        }

                    }
                }
                return Result<IEnumerable<AyenSoftResponseModel>>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AyenSoftResponseModel>>(null, "Bilinmeyen hata:" + ex.Message, StatusCodes.Status400BadRequest);

            }
        }     
        public async Task<ServiceResultModel<SiparisTicariSistemCariGuncelleResponseModel>> SiparisTicariSistemCariGuncelle(string url, HttpContent contentPost)
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
                    httpClient.Timeout = TimeSpan.FromSeconds(150);
                    httpClient.BaseAddress = new Uri(_ayenSoftConnection.BaseUrl);
                    //httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://tempuri.org/AnlikStokFiyatGuncelle");
                    //httpClient.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

                    response = await httpClient.PostAsync(url, contentPost);

                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            XDocument xml = XDocument.Parse(stringResult);
                            var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "SiparisTicariSistemCariGuncelleResult").Select(x => new SiparisTicariSistemCariGuncelleResponseModel()
                            {
                                Success = (bool)x.Element(x.Name.Namespace + "Success"),
                                Message = (string)x.Element(x.Name.Namespace + "Message"),
                            }).FirstOrDefault();

                            if (soapResponse==null)
                            {
                                var soapResponseSuccess = xml.Descendants().Where(x => x.Name.LocalName == "SiparisTicariSistemCariGuncelleResult").Select(x => new SiparisTicariSistemCariGuncelleResponseModel()
                                {
                                    Success = (bool)x.Element(x.Name.Namespace + "Success"),
                                }).FirstOrDefault();

                                return Result(soapResponseSuccess);
                            }
                            return Result(soapResponse);
                        }

                    }
                }
                return Result<SiparisTicariSistemCariGuncelleResponseModel>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                return Result<SiparisTicariSistemCariGuncelleResponseModel>(null, "Bilinmeyen hata:" + ex.Message, StatusCodes.Status400BadRequest);

            }
        }
        public async Task<ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>> PlatformKargoBilgisiniGirAsync(string url, HttpContent contentPost)
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
                    httpClient.Timeout = TimeSpan.FromSeconds(150);
                    httpClient.BaseAddress = new Uri(_ayenSoftConnection.BaseUrl);
                    //httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://tempuri.org/AnlikStokFiyatGuncelle");
                    //httpClient.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

                    response = await httpClient.PostAsync(url, contentPost);

                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            XDocument xml = XDocument.Parse(stringResult);
                            var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoBilgisiniGirResult").Select(x => new PlatformKargoBilgisiniGirResponseModel()
                            {
                                Success = (bool)x.Element(x.Name.Namespace + "Success"),
                            }).FirstOrDefault();
                            if (soapResponse == null)
                            {
                                var soapResponseSuccess = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoBilgisiniGirResult").Select(x => new PlatformKargoBilgisiniGirResponseModel()
                                {
                                    Success = (bool)x.Element(x.Name.Namespace + "Success"),
                                }).FirstOrDefault();

                                if (soapResponseSuccess==null)
                                {
                                    var soapResponseError = new PlatformKargoBilgisiniGirResponseModel()
                                    {
                                        Success = false,
                                        Data = stringResult
                                    };
                                    soapResponseError.Data = stringResult;
                                    return Result(soapResponseError);
                                }
                                soapResponseSuccess.Data = stringResult;
                                return Result(soapResponseSuccess);
                            }
                            soapResponse.Data = stringResult;
                            return Result(soapResponse);
                        }

                    }
                }
                return Result<PlatformKargoBilgisiniGirResponseModel>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                return Result<PlatformKargoBilgisiniGirResponseModel>(null, "Bilinmeyen hata:" + ex.Message, StatusCodes.Status400BadRequest);

            }
        }
        public async Task<ServiceResultModel<PlatformKargoStatuGuncelleResponseModel>> PlatformKargoStatuGuncelleAsync(string url, HttpContent contentPost)
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
                    httpClient.Timeout = TimeSpan.FromSeconds(150);
                    httpClient.BaseAddress = new Uri(_ayenSoftConnection.BaseUrl);
                    //httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://tempuri.org/AnlikStokFiyatGuncelle");
                    //httpClient.DefaultRequestHeaders.Add("Proxy-Connection", "Keep-Alive");

                    response = await httpClient.PostAsync(url, contentPost);

                    using (HttpContent content = response.Content)
                    {
                        string stringResult = await content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(stringResult))
                        {
                            XDocument xml = XDocument.Parse(stringResult);
                            var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoStatuGuncelleResult").Select(x => new PlatformKargoStatuGuncelleResponseModel()
                            {
                                Success = (bool)x.Element(x.Name.Namespace + "Success"),
                            }).FirstOrDefault();
                            if (soapResponse == null)
                            {
                                var soapResponseSuccess = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoStatuGuncelleResult").Select(x => new PlatformKargoStatuGuncelleResponseModel()
                                {
                                    Success = (bool)x.Element(x.Name.Namespace + "Success"),
                                }).FirstOrDefault();

                                if (soapResponseSuccess == null)
                                {
                                    var soapResponseError = new PlatformKargoStatuGuncelleResponseModel()
                                    {
                                        Success = false,
                                        Data = stringResult
                                    };
                                    return Result(soapResponseError);
                                }
                                soapResponseSuccess.Data = stringResult;
                                return Result(soapResponseSuccess);
                            }
                            soapResponse.Data= stringResult;
                            return Result(soapResponse);
                        }

                    }
                }
                return Result<PlatformKargoStatuGuncelleResponseModel>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                return Result<PlatformKargoStatuGuncelleResponseModel>(null, "Bilinmeyen hata:" + ex.Message, StatusCodes.Status400BadRequest);

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

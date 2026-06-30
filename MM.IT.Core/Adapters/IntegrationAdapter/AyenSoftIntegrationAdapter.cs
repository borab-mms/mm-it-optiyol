using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.OnlineOrder;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MM.IT.Core.Adapters.IntegrationAdapter
{
    public class AyenSoftIntegrationAdapter : IAyenSoftIntegrationAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientConnectionModel _httpClientConnection;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IWebHostEnvironment _env;
        public AyenSoftIntegrationAdapter(
        IHttpClientFactory httpClientFactory,
        IOptions<HttpClientConfigModel> connectionOptions,
        IWebHostEnvironment env,
        IStringLocalizer<SharedResources> stringLocalizer)
        {
            _httpClientFactory = httpClientFactory;
            _stringLocalizer = stringLocalizer;
            _httpClientConnection = connectionOptions.Value.AyenSoftConnection;
            _env = env;
        }
        public async Task<ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>> PlatformKargoBilgisiniGirAsync(PlatformKargoBilgisiniGirRequestModel input)
        {
            var client = _httpClientConnection.GetHttpClient(_httpClientFactory);

            #region createContent

            var ayenSoftSb = new StringBuilder();
            var ayenSoftXml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"> <soapenv:Header/> " +
                "<soapenv:Body> " +
                "<tem:PlatformKargoBilgisiniGir>" +
                "<tem:request> " +
                "<tem:Platform>" + input.Platform + "</tem:Platform> " +
                //"<tem:KargoSirketi>" + input.KargoSirketi + "</tem:KargoSirketi>" +
                "<tem:KargoSirketi>Aras</tem:KargoSirketi>" +
                "<tem:SiparisKodu>" + input.SiparisKodu + "</tem:SiparisKodu>" +
                //"<tem:TakipNumarasi>" + input.TakipNumarasi + "</tem:TakipNumarasi>" +
                "<tem:TakipNumarasi>3045532464617</tem:TakipNumarasi>" +
                "<tem:KargoTakipUrl>" + input.KargoTakipUrl + "</tem:KargoTakipUrl>" +
                "<tem:TicariSistemKayitNumarasi>" + input.TicariSistemKayitNumarasi + "</tem:TicariSistemKayitNumarasi>" +
                "<tem:KullaniciAdi>" + _httpClientConnection.Username + "</tem:KullaniciAdi> " +
                "<tem:Sifre>" + _httpClientConnection.Password + "</tem:Sifre> " +
                "</tem:request> " +
                "</tem:PlatformKargoBilgisiniGir> " +
                "</soapenv:Body> " +
                "</soapenv:Envelope>";

            ayenSoftSb.Append(ayenSoftXml);

            var stringContent = new StringContent(ayenSoftSb.ToString(), Encoding.UTF8, "text/xml");
            #endregion

            var response = await client.PostAsJsonAsync("?op=PlatformKargoBilgisiniGir", stringContent);

            var responseContent = await response?.Content?.ReadAsStringAsync();

            client.Dispose();

            if (!string.IsNullOrEmpty(responseContent))
            {
                XDocument xml = XDocument.Parse(responseContent);
                var soapResponse = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoBilgisiniGirResult").Select(x => new PlatformKargoBilgisiniGirResponseModel()
                {
                    Success = (bool)x.Element(x.Name.Namespace + "Success")
                }).FirstOrDefault();

                var soapResponseSuccess = xml.Descendants().Where(x => x.Name.LocalName == "PlatformKargoBilgisiniGirResult").Select(x => new PlatformKargoBilgisiniGirResponseModel()
                {
                    Success = (bool)x.Element(x.Name.Namespace + "Success"),
                }).FirstOrDefault();

                var result = new ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>
                {
                    Code = (int)response.StatusCode,
                    Data = string.IsNullOrWhiteSpace(responseContent) || !response.IsSuccessStatusCode ?
     default : soapResponseSuccess,
                };

                if (!response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
                {
                    var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var errorMessage = responseJson?.error?.message;

                    result.Message = $"{_stringLocalizer["Messages.Integration.Error"].Value}: {errorMessage ?? result.Message}";
                }
                return result;
            }
            return new ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>();
        }
    }
}

using Microsoft.Extensions.Logging;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Enums;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Sterling;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Castle.Core.Internal;
using System.Net.Http;

namespace MM.IT.Core.Services.MMCustomerInfo
{
    public class SterlingOrderClientService : BaseService, ISterlingOrderClientService
    {
        private readonly ILogger<SterlingOrderClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<HttpClientConfigModel> _httpClientConfigModel;
        private readonly IMMGlobalAuthService _mMGlobalAuthService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpClientFactory _httpClientFactory;

        public SterlingOrderClientService(IServiceProvider serviceProvider
            , ILogger<SterlingOrderClientService> logger
            , IConfiguration configuration
            , IOptions<HttpClientConfigModel> httpClientConfigModel
            , IMMGlobalAuthService mMGlobalAuthService
            , IWebHostEnvironment environment
            , IHttpClientFactory httpClientFactory) : base(serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientConfigModel = httpClientConfigModel;
            _mMGlobalAuthService = mMGlobalAuthService;
            _environment = environment;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResultModel<List<CustomerInfoResponseModel>>> GetCustomerInfoAsync(string url)
        {
            #region MyRegion
            var response = new List<CustomerInfoResponseModel>();

            var error = "";
            try
            {
                #region getToken

                var model = new AuthRequestModel()
                {
                    username = _httpClientConfigModel.Value.MMGlobalAuthConnection.Username,
                    password = _httpClientConfigModel.Value.MMGlobalAuthConnection.Password
                };
                var myToken = await _mMGlobalAuthService.GetToken(model);

                #endregion
                var _client = _httpClientFactory.CreateClient();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", myToken.Data.AccessToken);
                _client.DefaultRequestHeaders.Add("x-api-key", "oOLeZwkcDQv6k14vrVA26uqUyJ1gJyFd");
                _client.DefaultRequestHeaders.Add("country", "TR");
                _client.DefaultRequestHeaders.Add("brand", "MM");
                _client.DefaultRequestHeaders.Add("sourceSystemId", "TRLocalCCCRAPI");

                using (HttpResponseMessage tokenResponse = await _client.GetAsync(_client.BaseAddress + url))
                using (HttpContent content = tokenResponse.Content)
                {
                    string stringResult = await content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        if (stringResult.Equals("[]"))
                        {
                            return Result<List<CustomerInfoResponseModel>>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);
                        }
                        var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
                        error = jsonContent.ToString();
                        response = JsonConvert.DeserializeObject<List<CustomerInfoResponseModel>>(jsonContent);

                        return Result<List<CustomerInfoResponseModel>>(response);
                    }
                    return Result<List<CustomerInfoResponseModel>>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"<MMProductService>GetAsync:{ex.Message}hata:{error}</MMProductService>");
                return Result<List<CustomerInfoResponseModel>>(null, MMCustomerInfoMessageCodes.UnknownError.GetDisplayName(), (int)MMCustomerInfoMessageCodes.UnknownError);

            }
            #endregion

            #region proxy

            //var response = new List<CustomerInfoResponseModel>();
            //var error = "";
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
            //    //HttpResponseMessage response;

            //    using (var httpClient = new HttpClient(clientHandler))
            //    {
            //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //        httpClient.DefaultRequestHeaders.Add("x-api-key", "oOLeZwkcDQv6k14vrVA26uqUyJ1gJyFd");
            //        httpClient.DefaultRequestHeaders.Add("country", "TR");
            //        httpClient.DefaultRequestHeaders.Add("brand", "MM");
            //        httpClient.DefaultRequestHeaders.Add("sourceSystemId", "TRLocalCCCRAPI");

            //        using (HttpResponseMessage tokenResponse = await httpClient.GetAsync(_client.BaseAddress + url))
            //        using (HttpContent content = tokenResponse.Content)
            //        {
            //            string stringResult = await content.ReadAsStringAsync();
            //            if (!string.IsNullOrEmpty(stringResult))
            //            {
            //                if (stringResult.Equals("[]"))
            //                {
            //                    return Result<List<CustomerInfoResponseModel>>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);
            //                }
            //                error = stringResult;
            //                var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            //                response = JsonConvert.DeserializeObject<List<CustomerInfoResponseModel>>(jsonContent);

            //                return Result<List<CustomerInfoResponseModel>>(response);
            //            }

            //            return Result<List<CustomerInfoResponseModel>>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"<MMProductService>GetAsync:{ex.Message}</MMProductService>");
            //    return Result<List<CustomerInfoResponseModel>>(null, MMCustomerInfoMessageCodes.UnknownError.GetDisplayName(), (int)MMCustomerInfoMessageCodes.UnknownError);
            //}
            #endregion
        }
        public async Task<ServiceResultModel<CustomerOrderModel>> GetCustomerOrdersAsyncOld(string url)
        {
            var response = new CustomerOrderModel();
            try
            {
                #region getToken

                var model = new AuthRequestModel()
                {
                    username = _httpClientConfigModel.Value.MMGlobalV3AuthConnection.Username,
                    password = _httpClientConfigModel.Value.MMGlobalV3AuthConnection.Password
                };
                var myToken = await _mMGlobalAuthService.GetToken(model);

                #endregion
                var _client = _httpClientFactory.CreateClient();
                _client.BaseAddress = new Uri(_httpClientConfigModel.Value.MMGlobalV3Connection.BaseUrl);

                var xapikey = _httpClientConfigModel.Value.MMGlobalV3Connection.Parameters["xapikey"];
                var XFlowId = _httpClientConfigModel.Value.MMGlobalV3Connection.Parameters["XFlowId"];
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", myToken.Data.AccessToken);
                _client.DefaultRequestHeaders.Add("x-api-key", xapikey);

                using (HttpResponseMessage tokenResponse = await _client.GetAsync(_client.BaseAddress + url))
                using (HttpContent content = tokenResponse.Content)
                {
                    string stringResult = await content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        if (stringResult.Equals("[]"))
                        {
                            return Result<CustomerOrderModel>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);
                        }
                        var jsonContent = await tokenResponse.Content.ReadAsStringAsync();

                        _client.Dispose();

                        try
                        {
                            response = JsonConvert.DeserializeObject<CustomerOrderModel>(jsonContent);

                            #region price_adjustmentsAndaddress_components

                            var priceAdjustments = new List<PriceAdjustments>();
                            var addressComponents = new List<AddressComponent>();
                            var addressComponentsForShippingAddresss = new List<AddressComponent>();
                            var contractualPartnerAddresss = new List<ContractualPartnerAddress>();
                            var holdsTemp = new List<HoldsTemp>();

                            JObject jsonObject = JObject.Parse(jsonContent);

                            var customer_orders = jsonObject["customer_orders"];

                            foreach (var customerOrderItem in customer_orders)
                            {
                                #region PriceAdjustments

                                var items = customerOrderItem["mms_order"]["items"];
                                if (items != null)
                                {
                                    foreach (var itemItem in items)
                                    {
                                        var price_adjustments = itemItem["price_adjustments"];

                                        if (price_adjustments != null && price_adjustments.Count() > 0)
                                        {
                                            foreach (var priceAdjustmentItem in price_adjustments)
                                            {
                                                var priceAdjustment = new PriceAdjustments();
                                                priceAdjustment.CustomerOrderNumber = customerOrderItem["number"].ToString();
                                                priceAdjustment.OrderItemId = itemItem["order_item_id"].ToString();

                                                priceAdjustment.Id = priceAdjustmentItem["id"].ToString();
                                                priceAdjustment.PromotionType = priceAdjustmentItem["promotionType"] != null ? priceAdjustmentItem["promotionType"].ToString() : null;
                                                priceAdjustment.Type = priceAdjustmentItem["type"] != null ? priceAdjustmentItem["type"].ToString() : null;
                                                priceAdjustment.Name = priceAdjustmentItem["name"] != null ? priceAdjustmentItem["name"].ToString() : null;
                                                priceAdjustment.CostCenter = priceAdjustmentItem["cost_center"] != null ? priceAdjustmentItem["cost_center"].ToString() : null;
                                                priceAdjustment.DiscountClass = priceAdjustmentItem["discount_class"] != null ? priceAdjustmentItem["discount_class"].ToString() : null;

                                                var gross_amount = priceAdjustmentItem["gross_amount"];
                                                if (gross_amount != null && gross_amount.Count() > 0)
                                                {
                                                    var amount = gross_amount["amount"] != null ? gross_amount["amount"].ToString() : null;
                                                    var currency = gross_amount["currency"] != null ? gross_amount["currency"].ToString() : null;
                                                    priceAdjustment.GrossAmount = new GrossAmount()
                                                    {
                                                        amount = amount,
                                                        currency = currency
                                                    };
                                                }


                                                priceAdjustments.Add(priceAdjustment);
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region AddressComponents

                                var address_components = customerOrderItem["billing_address"]["address_components"];
                                if (address_components != null && address_components.Count() > 0)
                                {
                                    var addressComponent = new AddressComponent();

                                    foreach (var addressComponentItem in address_components)
                                    {
                                        addressComponent.CustomerOrderNumber = customerOrderItem["number"].ToString();
                                        var type = addressComponentItem["type"].ToString();

                                        if (type.Equals("addressee_component"))
                                        {
                                            addressComponent.AddresseeComponent = addressComponentItem.ToObject<AddresseeComponent>();
                                        }
                                        if (type.Equals("street_component"))
                                        {
                                            addressComponent.StreetComponent = addressComponentItem.ToObject<StreetComponent>();
                                        }
                                        if (type.Equals("city_component"))
                                        {
                                            addressComponent.CityComponent = addressComponentItem.ToObject<CityComponent>();
                                        }
                                        if (type.Equals("company_component"))
                                        {
                                            addressComponent.CompanyComponent = addressComponentItem.ToObject<CompanyComponent>();
                                        }
                                        if (type.Equals("email_component"))
                                        {
                                            addressComponent.EmailComponent = addressComponentItem.ToObject<EmailComponent>();
                                        }
                                        if (type.Equals("address_selector_component"))
                                        {
                                            addressComponent.AddressSelectorComponent = addressComponentItem.ToObject<AddressSelectorComponent>();
                                        }
                                        if (type.Equals("customer_component"))
                                        {
                                            addressComponent.CustomerComponent = addressComponentItem.ToObject<CustomerComponent>();
                                        }
                                        if (type.Equals("phone_component"))
                                        {
                                            addressComponent.PhoneComponent = addressComponentItem.ToObject<PhoneComponent>();
                                        }
                                    }

                                    addressComponents.Add(addressComponent);
                                }
                                #endregion

                                #region AddressComponents

                                try
                                {
                                    var address_componentsFormms_order = customerOrderItem["mms_order"];
                                    if (address_componentsFormms_order != null && address_componentsFormms_order.Count() > 0)
                                    {
                                        var address_componentsFormms_orderForshipping_address = customerOrderItem["mms_order"]["shipping_address"];

                                        if (address_componentsFormms_orderForshipping_address != null && address_componentsFormms_orderForshipping_address.Count() > 0)
                                        {
                                            var address_componentsFormms_orderForshipping_addressForaddress_components = customerOrderItem["mms_order"]["shipping_address"]["address_components"];
                                            if (address_componentsFormms_orderForshipping_addressForaddress_components != null && address_componentsFormms_orderForshipping_addressForaddress_components.Count() > 0)
                                            {
                                                var address_componentsForshipping_address = customerOrderItem["mms_order"]["shipping_address"]["address_components"];
                                                if (address_componentsForshipping_address != null && address_componentsForshipping_address.Count() > 0)
                                                {
                                                    var addressComponentForshipping_addressInstance = new AddressComponent();

                                                    foreach (var addressComponentItem in address_componentsForshipping_address)
                                                    {
                                                        addressComponentForshipping_addressInstance.CustomerOrderNumber = customerOrderItem["number"].ToString();
                                                        var type = addressComponentItem["type"].ToString();

                                                        if (type.Equals("addressee_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.AddresseeComponent = addressComponentItem.ToObject<AddresseeComponent>();
                                                        }
                                                        if (type.Equals("street_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.StreetComponent = addressComponentItem.ToObject<StreetComponent>();
                                                        }
                                                        if (type.Equals("city_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.CityComponent = addressComponentItem.ToObject<CityComponent>();
                                                        }
                                                        if (type.Equals("company_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.CompanyComponent = addressComponentItem.ToObject<CompanyComponent>();
                                                        }
                                                        if (type.Equals("email_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.EmailComponent = addressComponentItem.ToObject<EmailComponent>();
                                                        }
                                                        if (type.Equals("address_selector_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.AddressSelectorComponent = addressComponentItem.ToObject<AddressSelectorComponent>();
                                                        }
                                                        if (type.Equals("customer_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.CustomerComponent = addressComponentItem.ToObject<CustomerComponent>();
                                                        }
                                                        if (type.Equals("phone_component"))
                                                        {
                                                            addressComponentForshipping_addressInstance.PhoneComponent = addressComponentItem.ToObject<PhoneComponent>();
                                                        }
                                                    }

                                                    addressComponentsForShippingAddresss.Add(addressComponentForshipping_addressInstance);
                                                }
                                            }
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                }
                                #endregion

                                #region ContractualPartner

                                var contractual_partner = customerOrderItem["contractual_partner"]["address"];
                                if (contractual_partner != null && contractual_partner.Count() > 0)
                                {
                                    var address_componentsForContractual_partner = contractual_partner["address_components"];
                                    if (address_componentsForContractual_partner != null && address_componentsForContractual_partner.Count() > 0)
                                    {
                                        var contractualPartnerAddress = new ContractualPartnerAddress();
                                        foreach (var address_componentsForContractual_partnerItem in address_componentsForContractual_partner)
                                        {
                                            contractualPartnerAddress.CustomerOrderNumber = customerOrderItem["number"].ToString();
                                            //priceAdjustment.OrderItemId = itemItem["order_item_id"].ToString();

                                            if (address_componentsForContractual_partnerItem["type"].ToString() == "street_component")
                                            {
                                                contractualPartnerAddress.StreetComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<StreetComponentForContractualPartner>();
                                            }
                                            else if (address_componentsForContractual_partnerItem["type"].ToString() == "city_component")
                                            {
                                                contractualPartnerAddress.CityComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<CityComponentForContractualPartner>();
                                            }
                                            else if (address_componentsForContractual_partnerItem["type"].ToString() == "company_component")
                                            {
                                                contractualPartnerAddress.CompanyComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<CompanyComponentForContractualPartner>();
                                            }
                                        }
                                        contractualPartnerAddresss.Add(contractualPartnerAddress);
                                    }

                                }

                                #endregion

                                #region Hold

                                if (items != null)
                                {
                                    foreach (var itemItem in items)
                                    {
                                        var hold_items = itemItem["holds"];

                                        if (hold_items != null && hold_items.Count() > 0)
                                        {
                                            foreach (var holdItem in hold_items)
                                            {
                                                var type = holdItem["type"].ToString();

                                                var hold_item = new HoldsTemp();

                                                hold_item.CustomerOrderNumber = customerOrderItem["number"].ToString();
                                                hold_item.OrderItemId = itemItem["order_item_id"].ToString();

                                                if (type.Equals("AWAITING_SALESDOC"))
                                                {
                                                    hold_item.AWAITINGSALESDOCComponent = holdItem.ToObject<AWAITINGSALESDOCComponent>();
                                                }
                                                if (type.Equals("AWAITING_SHIPMENT_ID"))
                                                {
                                                    hold_item.AWAITINGSHIPMENTIDComponent = holdItem.ToObject<AWAITINGSHIPMENTIDComponent>();
                                                }

                                                holdsTemp.Add(hold_item);
                                            }
                                        }
                                    }
                                }


                                #endregion
                            }


                            #endregion

                            response.price_adjustments = priceAdjustments;
                            response.address_components = addressComponents;
                            response.address_componentsForshipping_address = addressComponentsForShippingAddresss;
                            response.contractualPartnerAddresss = contractualPartnerAddresss;
                            response.holdsTemp = holdsTemp;

                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"<SterlingOrderClientService>json:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");
                        }

                        //response.jsonData = jsonContent;

                        return Result<CustomerOrderModel>(response);
                    }
                    return Result<CustomerOrderModel>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"<SterlingOrderClientService>GetCustomerOrdersAsync:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");
                return Result<CustomerOrderModel>(null, MMCustomerInfoMessageCodes.UnknownError.GetDisplayName(), (int)MMCustomerInfoMessageCodes.UnknownError);

            }
        }
        public async Task<ServiceResultModel<CustomerOrderModel>> GetCustomerOrdersAsync(string url)
        {
            var response = new CustomerOrderModel();
            try
            {
                #region getToken

                var model = new AuthRequestModel()
                {
                    username = _httpClientConfigModel.Value.MMGlobalV3AuthConnection.Username,
                    password = _httpClientConfigModel.Value.MMGlobalV3AuthConnection.Password
                };
                var myToken = await _mMGlobalAuthService.GetToken(model);

                #endregion
                var _client = _httpClientFactory.CreateClient();
                _client.BaseAddress = new Uri(_httpClientConfigModel.Value.MMGlobalV3Connection.BaseUrl);

                var xapikey = _httpClientConfigModel.Value.MMGlobalV3Connection.Parameters["xapikey"];
                var XFlowId = _httpClientConfigModel.Value.MMGlobalV3Connection.Parameters["XFlowId"];
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", myToken.Data.AccessToken);
                _client.DefaultRequestHeaders.Add("x-api-key", xapikey);

                using (HttpResponseMessage tokenResponse = await _client.GetAsync(_client.BaseAddress + url))
                using (HttpContent content = tokenResponse.Content)
                {
                    string stringResult = await content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        if (stringResult.Equals("[]"))
                        {
                            return Result<CustomerOrderModel>(null, "Sonuç bulunamadı!", StatusCodes.Status400BadRequest);
                        }
                        var jsonContent = await tokenResponse.Content.ReadAsStringAsync();

                        _client.Dispose();

                        try
                        {
                            response = JsonConvert.DeserializeObject<CustomerOrderModel>(jsonContent);

                            #region price_adjustmentsAndaddress_components

                            var priceAdjustments = new List<PriceAdjustments>();
                            var addressComponents = new List<AddressComponent>();
                            var addressComponents2 = new List<AddressComponent2>();
                            var addressComponentsForShippingAddresss = new List<AddressComponent>();
                            var contractualPartnerAddresss = new List<ContractualPartnerAddress>();
                            var holdsTemp = new List<HoldsTemp>();

                            JObject jsonObject = JObject.Parse(jsonContent);

                            var customer_orders = jsonObject["customer_orders"];

                            if (customer_orders != null && customer_orders.Any() && customer_orders.Count() > 0)
                            {
                                try
                                {

                                    foreach (var customerOrderItem in customer_orders)
                                    {
                                        if (customerOrderItem.Any() && customerOrderItem.Count() > 0)
                                        {
                                            try
                                            {
                                                var items = customerOrderItem["mms_order"]?["items"];

                                                if (items != null && items.Any() && items.Count() > 0)
                                                {

                                                    #region PriceAdjustments
                                                    try
                                                    {
                                                        if (items != null)
                                                        {
                                                            foreach (var itemItem in items)
                                                            {
                                                                if (itemItem.Any() && itemItem.Count() > 0)
                                                                {
                                                                    var price_adjustments = itemItem["price_adjustments"];

                                                                    if (price_adjustments != null && price_adjustments.Count() > 0)
                                                                    {
                                                                        foreach (var priceAdjustmentItem in price_adjustments)
                                                                        {
                                                                            var priceAdjustment = new PriceAdjustments();
                                                                            priceAdjustment.CustomerOrderNumber = customerOrderItem?["number"]?.ToString();
                                                                            priceAdjustment.OrderItemId = itemItem["order_item_id"].ToString();

                                                                            priceAdjustment.Id = priceAdjustmentItem?["id"]?.ToString();
                                                                            priceAdjustment.PromotionType = priceAdjustmentItem["promotionType"] != null ? priceAdjustmentItem["promotionType"].ToString() : null;
                                                                            priceAdjustment.Type = priceAdjustmentItem["type"] != null ? priceAdjustmentItem["type"].ToString() : null;
                                                                            priceAdjustment.Name = priceAdjustmentItem["name"] != null ? priceAdjustmentItem["name"].ToString() : null;
                                                                            priceAdjustment.CostCenter = priceAdjustmentItem["cost_center"] != null ? priceAdjustmentItem["cost_center"].ToString() : null;
                                                                            priceAdjustment.DiscountClass = priceAdjustmentItem["discount_class"] != null ? priceAdjustmentItem["discount_class"].ToString() : null;

                                                                            var gross_amount = priceAdjustmentItem["gross_amount"];
                                                                            if (gross_amount != null && gross_amount.Count() > 0)
                                                                            {
                                                                                var amount = gross_amount["amount"] != null ? gross_amount?["amount"].ToString() : null;
                                                                                var currency = gross_amount?["currency"] != null ? gross_amount?["currency"].ToString() : null;
                                                                                priceAdjustment.GrossAmount = new GrossAmount()
                                                                                {
                                                                                    amount = amount,
                                                                                    currency = currency
                                                                                };
                                                                            }


                                                                            priceAdjustments.Add(priceAdjustment);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        _logger.LogError($"<SterlingOrderClientService>json4:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                                    }

                                                    #endregion

                                                    #region ContractualPartner

                                                    try
                                                    {
                                                        var contractual = customerOrderItem?["contractual_partner"];

                                                        if (contractual != null && contractual.Count() > 0)
                                                        {
                                                            var contractual_partner = customerOrderItem?["contractual_partner"]?["address"];
                                                            if (contractual_partner != null && contractual_partner.Count() > 0)
                                                            {
                                                                var address_componentsForContractual_partner = contractual_partner["address_components"];
                                                                if (address_componentsForContractual_partner != null && address_componentsForContractual_partner.Count() > 0)
                                                                {
                                                                    var contractualPartnerAddress = new ContractualPartnerAddress();
                                                                    foreach (var address_componentsForContractual_partnerItem in address_componentsForContractual_partner)
                                                                    {
                                                                        contractualPartnerAddress.CustomerOrderNumber = customerOrderItem?["number"]?.ToString();
                                                                        //priceAdjustment.OrderItemId = itemItem["order_item_id"].ToString();

                                                                        if (address_componentsForContractual_partnerItem?["type"]?.ToString() == "street_component")
                                                                        {
                                                                            contractualPartnerAddress.StreetComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<StreetComponentForContractualPartner>();
                                                                        }
                                                                        else if (address_componentsForContractual_partnerItem?["type"]?.ToString() == "city_component")
                                                                        {
                                                                            contractualPartnerAddress.CityComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<CityComponentForContractualPartner>();
                                                                        }
                                                                        else if (address_componentsForContractual_partnerItem?["type"]?.ToString() == "company_component")
                                                                        {
                                                                            contractualPartnerAddress.CompanyComponentForContractualPartner = address_componentsForContractual_partnerItem.ToObject<CompanyComponentForContractualPartner>();
                                                                        }
                                                                    }
                                                                    contractualPartnerAddresss.Add(contractualPartnerAddress);
                                                                }

                                                            }
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        _logger.LogError($"<SterlingOrderClientService>contractualPartnerAddress:{customerOrderItem?["number"]?.ToString()}{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                                    }

                                                    #endregion

                                                    #region Hold

                                                    if (items != null)
                                                    {
                                                        try
                                                        {
                                                            foreach (var itemItem in items)
                                                            {
                                                                if (itemItem.Any() && itemItem.Count() > 0)
                                                                {
                                                                    var hold_items = itemItem["holds"];

                                                                    if (hold_items != null && hold_items.Count() > 0)
                                                                    {
                                                                        foreach (var holdItem in hold_items)
                                                                        {
                                                                            var type = holdItem?["type"]?.ToString();
                                                                            if (!string.IsNullOrEmpty(type))
                                                                            {
                                                                                var hold_item = new HoldsTemp();

                                                                                hold_item.CustomerOrderNumber = customerOrderItem?["number"]?.ToString();
                                                                                hold_item.OrderItemId = itemItem?["order_item_id"]?.ToString();

                                                                                if (type.Equals("AWAITING_SALESDOC"))
                                                                                {
                                                                                    hold_item.AWAITINGSALESDOCComponent = holdItem.ToObject<AWAITINGSALESDOCComponent>();
                                                                                }
                                                                                if (type.Equals("AWAITING_SHIPMENT_ID"))
                                                                                {
                                                                                    hold_item.AWAITINGSHIPMENTIDComponent = holdItem.ToObject<AWAITINGSHIPMENTIDComponent>();
                                                                                }

                                                                                holdsTemp.Add(hold_item);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            _logger.LogError($"<SterlingOrderClientService>json5:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");
                                                        }
                                                    }


                                                    #endregion

                                                    #region AddressComponents
                                                    try
                                                    {

                                                        var addressComponentsItem = customerOrderItem?["billing_address"]?["address_components"];
                                                        if (addressComponentsItem != null)
                                                        {
                                                            foreach (var component in addressComponentsItem)
                                                            {
                                                                if (component != null && component.Any() && component.Count() > 0)
                                                                {
                                                                    string componentType = component?["type"]?.ToString();
                                                                    if (componentType != null)
                                                                    {
                                                                        var addressComponent2 = new AddressComponent2();
                                                                        var addressComponent2Items = new List<AddressComponent2Item>();

                                                                        addressComponent2.AddressType = "BillingAddress";
                                                                        addressComponent2.Type = componentType;
                                                                        addressComponent2.CustomerOrderNumber = customerOrderItem?["number"]?.ToString();

                                                                        foreach (JProperty prop in component.Children<JProperty>())
                                                                        {
                                                                            if (prop.Name == "type")
                                                                                continue;

                                                                            string attributeName = prop.Name;
                                                                            string attributeValue = prop?.Value?.ToString();
                                                                            if (attributeValue != null)
                                                                            {
                                                                                addressComponent2Items.Add(new AddressComponent2Item()
                                                                                {
                                                                                    Name = attributeName,
                                                                                    Value = attributeValue,
                                                                                });
                                                                            }
                                                                            addressComponent2.AddressComponent2Items = addressComponent2Items;
                                                                        }
                                                                        addressComponents2.Add(addressComponent2);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        _logger.LogError($"<SterlingOrderClientService>jsonAdres1:{customerOrderItem?["number"]?.ToString()}{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                                    }

                                                    #region AddressComponentsShippingAddress

                                                    try
                                                    {
                                                        var address_componentsFormms_order = customerOrderItem?["mms_order"];
                                                        if (address_componentsFormms_order != null && address_componentsFormms_order.Count() > 0)
                                                        {
                                                            var address_componentsFormms_orderForshipping_address = customerOrderItem?["mms_order"]?["shipping_address"];

                                                            if (address_componentsFormms_orderForshipping_address != null && address_componentsFormms_orderForshipping_address.Count() > 0)
                                                            {
                                                                var address_componentsFormms_orderForshipping_addressForaddress_components = customerOrderItem?["mms_order"]?["shipping_address"]?["address_components"];
                                                                if (address_componentsFormms_orderForshipping_addressForaddress_components != null && address_componentsFormms_orderForshipping_addressForaddress_components.Count() > 0)
                                                                {
                                                                    var address_componentsForshipping_address = customerOrderItem?["mms_order"]?["shipping_address"]?["address_components"];
                                                                    if (address_componentsForshipping_address != null && address_componentsForshipping_address.Count() > 0)
                                                                    {
                                                                        foreach (var component in address_componentsForshipping_address)
                                                                        {
                                                                            if (component != null)
                                                                            {
                                                                                string componentType = component?["type"]?.ToString();
                                                                                if (componentType != null)
                                                                                {
                                                                                    var addressComponent2 = new AddressComponent2();
                                                                                    var addressComponent2Items = new List<AddressComponent2Item>();

                                                                                    addressComponent2.AddressType = "ShippingAddress";
                                                                                    addressComponent2.Type = componentType;
                                                                                    addressComponent2.CustomerOrderNumber = customerOrderItem?["number"]?.ToString();


                                                                                    foreach (JProperty prop in component.Children<JProperty>())
                                                                                    {
                                                                                        if (prop.Name == "type")
                                                                                            continue;

                                                                                        string attributeName = prop.Name;
                                                                                        string attributeValue = prop?.Value?.ToString();
                                                                                        if (attributeValue != null)
                                                                                        {
                                                                                            addressComponent2Items.Add(new AddressComponent2Item()
                                                                                            {
                                                                                                Name = attributeName,
                                                                                                Value = attributeValue,
                                                                                            });
                                                                                        }
                                                                                        addressComponent2.AddressComponent2Items = addressComponent2Items;
                                                                                    }
                                                                                    addressComponents2.Add(addressComponent2);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        _logger.LogError($"<SterlingOrderClientService>jsonAdres2:{customerOrderItem?["number"]?.ToString()}{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                                    }

                                                    #endregion

                                                    #endregion
                                                }


                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.LogError($"<SterlingOrderClientService>json1:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                            }
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"<SterlingOrderClientService>json2:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");

                                }
                            }

                            #endregion

                            response.price_adjustments = priceAdjustments;
                            //response.address_components = addressComponents;
                            if (addressComponents2.Any() && addressComponents2.Count() > 0)
                            {
                                response.address_components2 = addressComponents2.OrderBy(a => a.AddressType).OrderBy(a => a.Type).ToList();
                            }
                            response.address_componentsForshipping_address = addressComponentsForShippingAddresss;
                            response.contractualPartnerAddresss = contractualPartnerAddresss;
                            response.holdsTemp = holdsTemp;

                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"<SterlingOrderClientService>json:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");
                        }

                        //response.jsonData = jsonContent;

                        return Result<CustomerOrderModel>(response);
                    }
                    return Result<CustomerOrderModel>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"<SterlingOrderClientService>GetCustomerOrdersAsync:{ex.Message}{ex.InnerException}</SterlingOrderClientService>");
                return Result<CustomerOrderModel>(null, MMCustomerInfoMessageCodes.UnknownError.GetDisplayName(), (int)MMCustomerInfoMessageCodes.UnknownError);

            }
        }
        private List<T> GetDeserializedDataList<T>(string data)
        {
            List<T> myDeserializedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            return myDeserializedClass;

        }
    }
}

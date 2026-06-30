using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MM.IT.Common.Helpers.SmsHelper.Interfaces;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Sms;
using MM.IT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using MM.IT.Common.Models.Configs;
using Microsoft.Extensions.Localization;
using System.Net;
using MM.IT.Common.Extensions;
using System.Net.Http;
using System.Reflection;
using System.Runtime;
using System.Reflection.Metadata;
using System.Text.Json;

namespace MM.IT.Common.Helpers.SmsHelper;

/// <summary>
/// ISmsHelper şartlarını barındıran Email Helper Nesnesi
/// </summary>
public class SmsHelper : ISmsHelper
{
    private readonly HttpClientConnectionModel _smsConnection;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly HttpClientConnectionModel _mobileDevSmsConnection;
    private readonly IHttpClientFactory _httpClientFactory;


    public SmsHelper(IOptions<HttpClientConfigModel> options
        , IStringLocalizer<SharedResources> stringLocalizer
        , IHttpClientFactory httpClientFactory)
    {
        _mobileDevSmsConnection = options.Value.MobileDevSmsConnection;
        _smsConnection = options.Value.SmsSenderConnection;
        _stringLocalizer = stringLocalizer;
        _httpClientFactory = httpClientFactory;

    }
    public bool CheckDateTimeFormat(string inputString)
    {
        DateTime dDate;

        if (DateTime.TryParse(inputString, out dDate))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<ServiceResultModel<SMSResponseModel>> SendSingleAsync(SmsSingleRequestModel input)
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
        var client = new HttpClient(clientHandler);

        client.BaseAddress = new Uri(_smsConnection.BaseUrl);


        var authInput = new
        {
            UserName = _smsConnection.Username,
            Password = _smsConnection.Password
        };

        var response = await client.PostAsJsonAsync("/smsSender/api/Auth/login", authInput);

        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResultModel<SMSResponseModel>
            {
                Code = StatusCodes.Status401Unauthorized,
                Message = _stringLocalizer["Helpers.SmsHelper.Error.Token"].Value
            };
        }

        var authResponse = await response.Content.ReadFromJsonAsync<SMSAuthResponseModel>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.data.token);

        var smsResponse = await client.PostAsJsonAsync("/smsSender/api/SMS/SMSSender", input);
        var jsonString = smsResponse.Content.ReadAsStringAsync();

        if (!smsResponse.IsSuccessStatusCode)
        {
            return new ServiceResultModel<SMSResponseModel>
            {
                Code = StatusCodes.Status406NotAcceptable,
                Message = _stringLocalizer["Helpers.SmsHelper.Error.Request"].Value
            };
        }

       var smsResult= JsonSerializer.Deserialize<SMSResponseModel>(jsonString.Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null

        });


        return new ServiceResultModel<SMSResponseModel>
        {
            Data = smsResult,
            Code = StatusCodes.Status200OK,
            Message = _stringLocalizer["Message.ProcessCompletedSuccessfully"]
        };
    }
    public async Task<ServiceResultModel> SendMultipleAsync(SmsMultipleRequestModel input)
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
        var client = new HttpClient(clientHandler);

        client.BaseAddress = new Uri(_smsConnection.BaseUrl);

        var authInput = new
        {
            UserName = _smsConnection.Username,
            Password = _smsConnection.Password
        };

        var response = await client.PostAsJsonAsync("/smsSender/api/Auth/login", authInput);

        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResultModel
            {
                Code = StatusCodes.Status401Unauthorized,
                Message = _stringLocalizer["Helpers.SmsHelper.Error.Token"].Value
            };
        }

        var authResponse = await response.Content.ReadFromJsonAsync<SMSAuthResponseModel>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.data.token);

        var smsResponse = await client.PostAsJsonAsync("/smsSender/api/SMS/SMSMultiSender", input);

        if (!smsResponse.IsSuccessStatusCode)
        {
            return new ServiceResultModel
            {
                Code = StatusCodes.Status406NotAcceptable,
                Message = _stringLocalizer["Helpers.SmsHelper.Error.Request"].Value
            };
        }

        return new ServiceResultModel
        {
            Code = StatusCodes.Status200OK,
            Message = _stringLocalizer["Message.ProcessCompletedSuccessfully"]
        };
    }
    public async Task<List<MobilDevSmsResultResponse>> GetMobilDevSmsResult(string proccessId)
    {
        var request = new MobilDevSmsResultRequest()
        {
            Username = _mobileDevSmsConnection.Username,
            Password = _mobileDevSmsConnection.Password,
            Action = 3,
            ProccessId = proccessId,
        };

        string xmlContent = request.ToXMLString(
            new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            }, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));


        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_mobileDevSmsConnection.BaseUrl);

        var response = await client.PostAsync("", new StringContent(xmlContent));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Response Code Different 200 ");
        }
        else
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            if (stringResponse.StartsWith(proccessId))
            {
                var mappedResponse = new List<MobilDevSmsResultResponse>();
                var rows = stringResponse.Split("\r\n").ToList();
                foreach (var item in rows)
                {
                    var responseParts = item.Replace("  ", " ").Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    mappedResponse.Add(new MobilDevSmsResultResponse()
                    {
                        ProccessId = responseParts[0],
                        GSM = responseParts[1],
                        Status = int.Parse(responseParts[2]),
                        Reason = responseParts[3],
                        SmsCount = responseParts[4],
                        MessageType = responseParts[5],
                        RecipientType = responseParts[6],
                        IYSBrandCode = responseParts[7],
                        Encoding = responseParts[8],

                    });
                }
                return mappedResponse;
            }
            throw new Exception("Service Response Not StartWith ProccessId");
        }
    }
    public async Task<string> SendMobilDevSingleSMS(SMSSenderRequest model)
    {

        #region MyRegion

        StringBuilder sb = new StringBuilder();

        sb.Append("<MainmsgBody>");
        sb.Append("<UserName>2476968362</UserName>");
        sb.Append("<PassWord>87423873296943619926b0051dbf55f7</PassWord>");
        sb.Append("<Action>0</Action>");
        sb.Append("<Mesgbody>" + model.Mesgbody + "</Mesgbody>");
        sb.Append("<Numbers>" + model.Numbers + "</Numbers>");
        sb.Append("<AccountId></AccountId>");
        sb.Append("<Originator></Originator>");
        sb.Append("<Blacklist>1</Blacklist>");
        sb.Append("<SDate>" + Convert.ToDateTime(model.SDate).ToString("ddMMyyyyHHmm") + "</SDate>");
        if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
        {
            sb.Append("<EDate>" + Convert.ToDateTime(model.EDate).ToString("ddMMyyyyHHmm") + "</EDate>");
        }
        sb.Append("<Encoding>1</Encoding>");
        sb.Append("<MessageType>" + model.MessageType.ToUpper() + "</MessageType>");
        sb.Append("<RecipientType>" + model.RecipientType.ToUpper() + "</RecipientType>");
        sb.Append("</MainmsgBody>");

        #endregion

        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_mobileDevSmsConnection.BaseUrl);

        var stringContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
        var response = await client.PostAsync("", stringContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Response Code Different 200 ");
        }
        else
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            if (stringResponse.Length > 3 && stringResponse.StartsWith("ID"))
            {
                return stringResponse.ReplaceAll(new[] { "ID", ":", " " }, "").Trim();
            }
            throw new Exception("Service Response Not Contain ID");
        }
    }
    public async Task<string> SendMobilDevMultiSMS(SMSMultiSenderRequest model)
    {
        #region MyRegion

        StringBuilder sb = new StringBuilder();

        sb.Append("<MainmsgBody>");
        sb.Append("<UserName>2476968362</UserName>");
        sb.Append("<PassWord>87423873296943619926b0051dbf55f7</PassWord>");
        sb.Append("<Action>1</Action>");
        sb.Append("<Messages>");
        foreach (var item in model.Messages)
        {
            sb.Append("<Message>");
            sb.Append("<Mesgbody>" + item.Mesgbody + "</Mesgbody>");
            sb.Append("<Number>" + item.Number + "</Number>");
            sb.Append("</Message>");
        }
        sb.Append("</Messages>");
        sb.Append("<AccountId></AccountId>");
        sb.Append("<Originator></Originator>");
        sb.Append("<SDate>" + Convert.ToDateTime(model.SDate).ToString("ddMMyyyyHHmm") + "</SDate>");
        if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
        {
            sb.Append("<EDate>" + Convert.ToDateTime(model.SDate).ToString("ddMMyyyyHHmm") + "</EDate>");
        }
        sb.Append("<Encoding>1</Encoding>");
        sb.Append("<MessageType>" + model.MessageType.ToUpper() + "</MessageType>");
        sb.Append("<RecipientType>" + model.RecipientType.ToUpper() + "</RecipientType>");
        sb.Append("</MainmsgBody>");

        #endregion

        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_mobileDevSmsConnection.BaseUrl);

        var stringContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
        var response = await client.PostAsync("", stringContent);

        if (!response.IsSuccessStatusCode)
        {
            return ("Response Code Different 200 ");
        }
        else
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            if (stringResponse.Length > 3 && stringResponse.StartsWith("ID"))
            {
                return stringResponse.ReplaceAll(new[] { "ID", ":", " " }, "").Trim();
            }
            return ("Service Response Not Contain ID:" + stringResponse);
        }
    }

    public async Task<string> SendMobilDevOTPSMS(OTPSMSSenderRequest model)
    {

        #region MyRegion

        #region MyRegion

        StringBuilder sb = new StringBuilder();

        sb.Append("<MainmsgBody>");
        sb.Append("<UserName>1344644272</UserName>");
        sb.Append("<PassWord>f7d34abd4c47414484d27a9be9a97a3e </PassWord>");
        sb.Append("<Action>0</Action>");
        sb.Append("<Mesgbody>" + model.Mesgbody + "</Mesgbody>");
        sb.Append("<Numbers>" + model.Numbers + "</Numbers>");
        sb.Append("<AccountId></AccountId>");
        sb.Append("<Originator></Originator>");
        sb.Append("<Encoding>0</Encoding>");
        sb.Append("<MessageType>N</MessageType>");
        sb.Append("<RecipientType></RecipientType>");
        sb.Append("</MainmsgBody>");

        #endregion

        #endregion

        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_mobileDevSmsConnection.BaseUrl);

        var stringContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
        var response = await client.PostAsync("", stringContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Response Code Different 200 ");
        }
        else
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            if (stringResponse.Length > 3 && stringResponse.StartsWith("ID"))
            {
                return stringResponse.ReplaceAll(new[] { "ID", ":", " " }, "").Trim();
            }
            throw new Exception("Service Response Not Contain ID");
        }
    }

    public async Task<List<MobilDevSmsResultResponse>> GetMobilDevSmsResultByProccessIds(string proccessIds)
    {
        try
        {
            var request = new MobilDevSmsResultRequest()
            {
                Username = _mobileDevSmsConnection.Username,
                Password = _mobileDevSmsConnection.Password,
                Action = 3,
                ProccessId = proccessIds,
            };

            string xmlContent = request.ToXMLString(
                new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                }, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));


            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_mobileDevSmsConnection.BaseUrl);

            var response = await client.PostAsync("", new StringContent(xmlContent));
            if (!response.IsSuccessStatusCode)
            {
                return new List<MobilDevSmsResultResponse>();
            }
            else
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                if (stringResponse.Any())
                {
                    var mappedResponse = new List<MobilDevSmsResultResponse>();
                    var rows = stringResponse.Split("\r\n").ToList();
                    foreach (var item in rows)
                    {
                        var responseParts = item.Replace("  ", " ").Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        mappedResponse.Add(new MobilDevSmsResultResponse()
                        {
                            ProccessId = responseParts[0],
                            GSM = responseParts[1],
                            Status = int.Parse(responseParts[2]),
                            Reason = responseParts[3],
                            SmsCount = responseParts[4],
                            MessageType = responseParts[5],
                            RecipientType = responseParts[6],
                            IYSBrandCode = responseParts[7],
                            Encoding = responseParts[8],

                        });
                    }
                    return mappedResponse;
                }
                return new List<MobilDevSmsResultResponse>();
            }

        }
        catch
        {
            return new List<MobilDevSmsResultResponse>();
        }
    }
}

using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Helpers.SmsHelper.Interfaces;

/// <summary>
/// Sms Helper Interface Tanım Nesnesi
/// </summary>
public interface ISmsHelper
{
    /// <summary>
    /// Async: Default SMS API üzerinden Tekli Gönderim Sağlar.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<ServiceResultModel<SMSResponseModel>> SendSingleAsync(SmsSingleRequestModel emailMessage);

    /// <summary>
    /// Async: Default SMS API üzerinden Çoklu Gönderim Sağlar.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<ServiceResultModel> SendMultipleAsync(SmsMultipleRequestModel emailMessage);

    /// <summary>
    /// Async: Default Mobildevden sms sonucunu getirir.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<List<MobilDevSmsResultResponse>> GetMobilDevSmsResult(string proccessId);

    /// <summary>
    /// Async: Default Mobildevden sms sonuçlarını getirir.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<List<MobilDevSmsResultResponse>> GetMobilDevSmsResultByProccessIds(string proccessIds);
    /// <summary>
    /// Async: Default Mobildeve tekli sms gönderir.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<string> SendMobilDevSingleSMS(SMSSenderRequest model);

    /// <summary>
    /// Async: Default Mobildeve çoklu sms gönderir.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<string> SendMobilDevMultiSMS(SMSMultiSenderRequest model);

    /// <summary>
    /// Async: Default Mobildeve OTP sms gönderir.
    /// </summary>
    /// <param name="input">Sms Detayı</param>
    public Task<string> SendMobilDevOTPSMS(OTPSMSSenderRequest model);

}
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MarketPlace.Interfaces;

/// <summary>
/// MarketPlace Servis Interface Tanımı
/// </summary>
public interface IMarketPlaceService : IService
{
    /// <summary>
    /// MarketPlace servisindeki gelen talepleri kaydeder.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<ReturnDemandResponseModel>> AddReturnDemandAsync(ReturnDemandRequestModel model);
    
    /// <summary>
    /// MarketPlace servisindeki gelen yeni siparişleri kaydeder.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<OrderResponseModel>> NewOrderAsync(OrderRequestModel model);
    
    /// <summary>
    /// MarketPlace servisindeki OrderHead tablosunu günceller.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<UpdateOrderStatusResponseModel>> UpdateOrderStatusAsync(UpdateOrderStatusRequestModel model);
    
    /// <summary>
    /// MarketPlace servisindeki OrderHead tablosunu günceller.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<OrderCancellationResponseModel>> OrderCancellationAsync(OrderCancellationRequestModel model);

    /// <summary>
    /// VatKeys tablosundaki koşul setine göre key rates lerini getirir.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<IEnumerable<KeyValueModel<string, decimal>>>> GetVatRateBySearchAsync(string search);
    
    /// <summary>
    /// VatKeys tablosundaki koşul setine göre key rates lerini getirir.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<IEnumerable<KeyValueModel<string, decimal>>>> GetVatRateByVatKeyAsync(List<int> vatKeys);

}
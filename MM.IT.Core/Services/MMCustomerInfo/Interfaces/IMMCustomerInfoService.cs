using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MMCustomerInfo.Interfaces;


/// <summary>
/// IMMCustomerInfoService Servis Interface Tanımı
/// </summary>
public interface IMMCustomerInfoService : IService
{
    /// <summary>
    /// IMMCustomerInfoService servisindeki müşteri bilgileri getirir.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<CustomerInfoSummaryModel>> GetCustomerInfo(CustomerInfoRequestModel model);
}

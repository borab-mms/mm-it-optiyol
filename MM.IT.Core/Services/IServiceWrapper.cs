using MM.IT.Core.Services.Base.Interfaces;
using MM.IT.Core.Services.DigitalCard.Interfaces;
using MM.IT.Core.Services.EKOLStock.Interfaces;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using MM.IT.Core.Services.SMS.Interfaces;
using MM.Optiyol.Api.Services.Optiyol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services;

/// <summary>
/// Constructor injection kullanımını basitleştirmek için tüm business servis tanımlamalarını içerir. 
/// Kullanılmak istenen IService'den türeyecek servis bu kısımda tanımlanmalı.
/// </summary>
public interface IServiceWrapper
{
    IEKOLStockService EKOLStockService { get; }
    IMediaMarktService MediaMarktService { get; }
    IMarketPlaceService MarketPlaceService { get; }
    IMMCustomerInfoService MMCustomerInfoService { get; }
    IDigitalCardService DigitalCardService { get; }
    ISMSSenderService SMSSenderService { get; }
    IMMITService MMITService { get; }
    IOptiyolService OptiyolService { get; }
}
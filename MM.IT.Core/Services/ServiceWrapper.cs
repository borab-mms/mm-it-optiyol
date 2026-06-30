using Microsoft.Extensions.DependencyInjection;
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
/// IServiceWrapper şartlarını barındıran Business ServiceWrapper Nesnesi
/// </summary>
public class ServiceWrapper : IServiceWrapper
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceWrapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IEKOLStockService EKOLStockService => _serviceProvider.GetService<IEKOLStockService>();
    public IMediaMarktService MediaMarktService => _serviceProvider.GetService<IMediaMarktService>();
    public IMarketPlaceService MarketPlaceService => _serviceProvider.GetService<IMarketPlaceService>();
    public IMMCustomerInfoService MMCustomerInfoService => _serviceProvider.GetService<IMMCustomerInfoService>();
    public IDigitalCardService DigitalCardService => _serviceProvider.GetService<IDigitalCardService>();
    public ISMSSenderService SMSSenderService => _serviceProvider.GetService<ISMSSenderService>();
    public IMMITService MMITService => _serviceProvider.GetService<IMMITService>();
    public IOptiyolService OptiyolService => _serviceProvider.GetService<IOptiyolService>();
}
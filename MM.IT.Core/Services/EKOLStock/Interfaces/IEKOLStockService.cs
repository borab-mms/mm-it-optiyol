using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Core.Services.Base.Interfaces;
using OperationDataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.EKOLStock.Interfaces;

/// <summary>
/// EKOL Stock Servis Interface Tanımı
/// </summary>
public interface IEKOLStockService : IService
{
    Task<ServiceResultModel<List<EKOLStockModel>>> PostXmlAsync(string url, HttpContent contentPost);
}


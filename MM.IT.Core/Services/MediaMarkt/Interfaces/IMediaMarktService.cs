using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Core.Services.Base.Interfaces;
using OperationDataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MediaMarkt.Interfaces;

/// <summary>
/// MediaMarkt Servis Interface Tanımı
/// </summary>
public interface IMediaMarktService : IService
{
    /// <summary>
    /// MediaMarkt servisindeki tüm stok Verilerinin Detaylarını Döndürür.
    /// </summary>
    /// <returns></returns>
    Task<ServiceResultModel<StockModel>> GetMMITStocksAsync();
    Task<ServiceResultModel<StockModel>> GetStocksByArtikelIdsAsync(StockRequestModel model);
    Task<ServiceResultModel<IEnumerable<KeyValueModel<string, string>>>> GetEmailContentByIdAsync(int Id);
    Task<ServiceResultModel<StockModel>> GetStoreStocksAsync();
    Task<ServiceResultModel<PushStocksResponseModel>> PushStocks();

}
